using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestauranteAPP_TP3.Data;
using RestauranteAPP_TP3.Models;

namespace RestauranteAPP_TP3.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly ApplicationDbContext _db;

        public ProfileController(UserManager<Usuario> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
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
    }
}