using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestauranteAPP_TP3.Data;
using RestauranteAPP_TP3.Models;
using RestauranteAPP_TP3.Integração.Interfaces;

namespace RestauranteAPP_TP3.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IBrasilAPIIntegracao _brasilApi;

        public ProfileController(UserManager<Usuario> userManager, ApplicationDbContext db, IBrasilAPIIntegracao brasilApi)
        {
            _userManager = userManager;
            _db = db;
            _brasilApi = brasilApi;
        }

        // GET: /Profile
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Challenge();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var enderecos = await _db.Enderecos
                .Where(e => e.UsuarioId == userId)
                .ToListAsync();

            var model = new ProfileViewModel
            {
                User = user,
                Enderecos = enderecos
            };

            return View(model);
        }

        // POST: /Profile/UpdateCnpj
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCnpj(string cnpj)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Challenge();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            // normalize: remove non-digits
            var digits = string.IsNullOrWhiteSpace(cnpj) ? string.Empty :
                         new string(cnpj.Where(char.IsDigit).ToArray());

            var enderecos = await _db.Enderecos
                .Where(e => e.UsuarioId == userId)
                .ToListAsync();

            var vm = new ProfileViewModel
            {
                User = user,
                Enderecos = enderecos
            };

            if (string.IsNullOrEmpty(digits))
            {
                vm.CnpjIsValid = false;
                vm.CnpjValidationMessage = "Informe um CNPJ.";
                return View("Index", vm);
            }

            if (digits.Length != 14)
            {
                vm.CnpjIsValid = false;
                vm.CnpjValidationMessage = "CNPJ inválido. Informe 14 dígitos.";
                return View("Index", vm);
            }

            if (IsAllSameDigits(digits) || !ValidateCnpjChecksum(digits))
            {
                vm.CnpjIsValid = false;
                vm.CnpjValidationMessage = "CNPJ com formato/cheksum inválido.";
                return View("Index", vm);
            }

            // consulta BrasilAPI
            var data = await _brasilApi.ObterDadosViaCNPJ(digits);
            if (data == null || string.IsNullOrWhiteSpace(data.Cnpj))
            {
                vm.CnpjIsValid = false;
                vm.CnpjFound = false;
                vm.CnpjValidationMessage = "CNPJ bem formado, mas não encontrado na BrasilAPI.";
                return View("Index", vm);
            }

            // sucesso: mostrar mensagem e persistir
            vm.CnpjIsValid = true;
            vm.CnpjFound = true;
            var situacao = data.DescricaoSituacaoCadastral ?? data.SituacaoCadastral?.ToString() ?? "Unknown";
            var razao = data.RazaoSocial ?? data.NomeFantasia ?? "Unknown";
            var location = $"{data.Municipio ?? "Unknown"}, {data.Uf ?? "Unknown"}";
            vm.CnpjValidationMessage = $"Válido: {razao} — {situacao} — {location}";

            // persist
            user.CNPJ = digits;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                    ModelState.AddModelError(string.Empty, err.Description);

                return View("Index", vm);
            }

            // re-load saved value into vm.User (optional)
            vm.User = user;

            return View("Index", vm);
        }

        private static bool IsAllSameDigits(string digits) =>
            digits.Distinct().Count() == 1;

        // Standard CNPJ checksum validation
        private static bool ValidateCnpjChecksum(string cnpj)
        {
            if (cnpj.Length != 14) return false;

            int[] weight1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] weight2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string number = cnpj.Substring(0, 12);
            int sum = 0;
            for (int i = 0; i < 12; i++)
                sum += (number[i] - '0') * weight1[i];

            int remainder = sum % 11;
            int firstCheck = remainder < 2 ? 0 : 11 - remainder;
            number += firstCheck;

            sum = 0;
            for (int i = 0; i < 13; i++)
                sum += (number[i] - '0') * weight2[i];

            remainder = sum % 11;
            int secondCheck = remainder < 2 ? 0 : 11 - remainder;

            return cnpj[12] - '0' == firstCheck && cnpj[13] - '0' == secondCheck;
        }
    }
}