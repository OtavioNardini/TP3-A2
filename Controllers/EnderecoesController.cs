using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestauranteAPP_TP3.Data;
using RestauranteAPP_TP3.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace RestauranteAPP_TP3.Controllers
{
    [Authorize]
    public class EnderecoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnderecoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Enderecoes
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.Enderecos
                                             .Include(e => e.Usuario)
                                             .Where(e => e.UsuarioId == userId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Enderecoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var endereco = await _context.Enderecos
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (endereco == null) return NotFound();
            return View(endereco);
        }

        // GET: Enderecoes/Create
        public IActionResult Create() => View();

        // POST: Enderecoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rua,Cidade,CEP")] Endereco input)
        {
            // require authenticated user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Challenge();

            // build new Endereco and set FK server-side (pattern like Pedido)
            var endereco = new Endereco
            {
                Rua = input?.Rua?.Trim(),
                Cidade = input?.Cidade?.Trim(),
                CEP = input?.CEP?.Trim(),
                UsuarioId = userId
            };

            // re-run validation after setting UsuarioId
            ModelState.Clear();
            TryValidateModel(endereco);

            if (!ModelState.IsValid) return View(endereco);

            _context.Enderecos.Add(endereco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Enderecoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null) return NotFound();
            return View(endereco);
        }

        // POST: Enderecoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rua,Cidade,CEP")] Endereco input)
        {
            if (id != input.Id) return NotFound();

            var existing = await _context.Enderecos.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            if (existing == null) return NotFound();

            // preserve UsuarioId and rebuild object to validate cleanly
            var endereco = new Endereco
            {
                Id = id,
                Rua = input.Rua?.Trim(),
                Cidade = input.Cidade?.Trim(),
                CEP = input.CEP?.Trim(),
                UsuarioId = existing.UsuarioId
            };

            ModelState.Clear();
            TryValidateModel(endereco);

            if (!ModelState.IsValid) return View(endereco);

            try
            {
                _context.Update(endereco);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnderecoExists(endereco.Id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Enderecoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var endereco = await _context.Enderecos
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (endereco == null) return NotFound();
            return View(endereco);
        }

        // POST: Enderecoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco != null) _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnderecoExists(int id) => _context.Enderecos.Any(e => e.Id == id);
    }
}
