using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestauranteAPP_TP3.Data;
using RestauranteAPP_TP3.Models;

namespace RestauranteAPP_TP3.Controllers
{
    [Authorize]
    public class PedidosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // MVC: GET /Pedidos
        public async Task<IActionResult> Index()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Usuario)
                .Include(p => p.PedidoItens).ThenInclude(pi => pi.Produtos)
                .ToListAsync();

            return View(pedidos);
        }

        // MVC: GET /Pedidos/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Cardapio = await _context.ItensCardapio.ToListAsync();
            return View();
        }

        // MVC: POST /Pedidos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form)
        {
            // require authenticated user to create an order
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Challenge(); // redirect to login

            var selected = form["CardapioItemIds"];
            if (selected.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Escolha pelo menos um item do cardápio.");
                ViewBag.Cardapio = await _context.ItensCardapio.ToListAsync();
                return View();
            }

            var pedido = new Pedido
            {
                Data = DateTime.Now,
                UsuarioId = userId,
                PedidoItens = new List<PedidoItem>()
            };

            foreach (var sid in selected)
            {
                if (!int.TryParse(sid, out var prodId))
                    continue;

                var produto = await _context.ItensCardapio.FindAsync(prodId);
                if (produto == null)
                    continue;

                var qString = form[$"Quantidades[{prodId}]"].FirstOrDefault() ?? "1";
                if (!int.TryParse(qString, out var quantidade))
                    quantidade = 1;
                if (quantidade <= 0) quantidade = 1;

                var pi = new PedidoItem
                {
                    ItemCardapioId = prodId,
                    Quantidade = quantidade,
                    PrecoUnitario = produto.PrecoBase
                };
                pedido.PedidoItens.Add(pi);
            }

            pedido.ValorTotal = pedido.PedidoItens.Sum(pi => pi.PrecoUnitario * pi.Quantidade);

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // MVC: GET /Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return NotFound();

            ViewBag.UsuarioId = new SelectList(_context.Users, "Id", "Nome", pedido.UsuarioId);
            return View(pedido);
        }

        // MVC: POST /Pedidos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,UsuarioId,ValorTotal")] Pedido pedido)
        {
            if (id != pedido.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                ViewBag.UsuarioId = new SelectList(_context.Users, "Id", "Nome", pedido.UsuarioId);
                return View(pedido);
            }

            try
            {
                _context.Update(pedido);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(pedido.Id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // MVC: POST /Pedidos/Delete/5  (called from a form in Index)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // --- Explicit API endpoints (if any external JS/clients use them) ---
        [HttpGet("/api/pedidos")]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetApiPedidos()
        {
            return await _context.Pedidos
                .Include(p => p.PedidoItens).ThenInclude(pi => pi.Produtos)
                .ToListAsync();
        }

        [HttpGet("/api/pedidos/{id}")]
        public async Task<ActionResult<Pedido>> GetApiPedido(int id)
        {
            var p = await _context.Pedidos
                .Include(x => x.PedidoItens).ThenInclude(pi => pi.Produtos)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (p == null) return NotFound();
            return p;
        }

        private bool PedidoExists(int id) => _context.Pedidos.Any(e => e.Id == id);
    }
}