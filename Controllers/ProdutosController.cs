using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestauranteAPP_TP3.Data;
using RestauranteAPP_TP3.Models;

namespace RestauranteAPP_TP3.Controllers
{
    [AllowAnonymous]
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // MVC: GET /Produtos
        public async Task<IActionResult> Index()
        {
            var itens = await _context.ItensCardapio.ToListAsync();
            return View(itens);
        }

        // MVC: GET /Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var produto = await _context.ItensCardapio.FirstOrDefaultAsync(p => p.Id == id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        // MVC: GET /Produtos/Create
        public IActionResult Create() => View();

        // MVC: POST /Produtos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descricao,PrecoBase")] Produtos produtos)
        {
            if (ModelState.IsValid)
            {
                _context.ItensCardapio.Add(produtos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produtos);
        }

        // MVC: GET /Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var produto = await _context.ItensCardapio.FindAsync(id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        // MVC: POST /Produtos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,PrecoBase")] Produtos produtos)
        {
            if (id != produtos.Id) return NotFound();
            if (!ModelState.IsValid) return View(produtos);

            try
            {
                _context.Update(produtos);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ItensCardapio.Any(e => e.Id == produtos.Id)) return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // MVC: GET /Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var produto = await _context.ItensCardapio.FirstOrDefaultAsync(p => p.Id == id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        // MVC: POST /Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.ItensCardapio.FindAsync(id);
            if (produto != null)
            {
                _context.ItensCardapio.Remove(produto);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // --- Explicit API endpoints (keep these for Home fetch / JS) ---
        [HttpGet("/api/produtos")]
        public async Task<ActionResult<IEnumerable<Produtos>>> GetApiProdutos()
        {
            return await _context.ItensCardapio.ToListAsync();
        }

        [HttpGet("/api/produtos/{id}")]
        public async Task<ActionResult<Produtos>> GetApiProduto(int id)
        {
            var p = await _context.ItensCardapio.FindAsync(id);
            if (p == null) return NotFound();
            return p;
        }

        [HttpPost("/api/produtos")]
        public async Task<ActionResult<Produtos>> PostApiProduto(Produtos produtos)
        {
            _context.ItensCardapio.Add(produtos);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetApiProduto), new { id = produtos.Id }, produtos);
        }

        [HttpPut("/api/produtos/{id}")]
        public async Task<IActionResult> PutApiProduto(int id, Produtos produtos)
        {
            if (id != produtos.Id) return BadRequest();
            _context.Entry(produtos).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.ItensCardapio.Any(e => e.Id == id)) return NotFound();
                throw;
            }
            return NoContent();
        }

        [HttpDelete("/api/produtos/{id}")]
        public async Task<IActionResult> DeleteApiProduto(int id)
        {
            var prod = await _context.ItensCardapio.FindAsync(id);
            if (prod == null) return NotFound();
            _context.ItensCardapio.Remove(prod);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}