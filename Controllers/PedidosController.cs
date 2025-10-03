using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestauranteAPP_TP3.Data;
using RestauranteAPP_TP3.Models;

namespace RestauranteAPP_TP3.Controllers
{
    [Authorize] // se estiver atrapalhando durante testes, comente temporariamente
    public class PedidosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public PedidosController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Pedidos
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var pedidos = await _context.Pedidos
                .Include(p => p.PedidoItens)
                    .ThenInclude(pi => pi.ItemCardapio)
                .Include(p => p.Usuario)
                .Where(p => p.UsuarioId == user.Id) // se quiser só do usuário atual
                .OrderByDescending(p => p.Data)
                .ToListAsync();

            return View(pedidos);
        }

        // GET: /Pedidos/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Cardapio = _context.ItensCardapio
                .OrderBy(i => i.Nome)
                .ToList();

            return View();
        }

        // POST: /Pedidos/Create
        [HttpPost("Pedidos/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int[] CardapioItemIds, Dictionary<int, int> Quantidades)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var pedido = new Pedido
            {
                Data = DateTime.Now,
                UsuarioId = user.Id,
                ValorTotal = 0m
            };

            foreach (var itemId in CardapioItemIds)
            {
                var item = await _context.ItensCardapio.FindAsync(itemId);
                if (item == null) continue;

                // Se o usuário não informar quantidade, assume 1
                var qtd = Quantidades.ContainsKey(itemId) ? Quantidades[itemId] : 1;

                pedido.PedidoItens.Add(new PedidoItem
                {
                    ItemCardapioId = item.Id,
                    Quantidade = qtd,
                    PrecoUnitario = item.PrecoBase
                });

                pedido.ValorTotal += item.PrecoBase * qtd;
            }

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
