using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlataformaB2B_A2_TP3.Data;
using PlataformaB2B_A2_TP3.Models;

namespace PlataformaB2B_A2_TP3.Controllers
{
    public class ItemCatalogoesController : Controller
    {
        private readonly PlataformaB2B_A2_TP3Context _context;

        public ItemCatalogoesController(PlataformaB2B_A2_TP3Context context)
        {
            _context = context;
        }

        // GET: ItemCatalogoes
        public async Task<IActionResult> Index()
        {
            var plataformaB2B_A2_TP3Context = _context.ItensCatalogo.Include(i => i.Catalogo).Include(i => i.Produto);
            return View(await plataformaB2B_A2_TP3Context.ToListAsync());
        }

        // GET: ItemCatalogoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCatalogo = await _context.ItensCatalogo
                .Include(i => i.Catalogo)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemCatalogo == null)
            {
                return NotFound();
            }

            return View(itemCatalogo);
        }

        // GET: ItemCatalogoes/Create
        public IActionResult Create()
        {
            ViewData["CatalogoId"] = new SelectList(_context.Catalogos, "Id", "Id");
            ViewData["ProdutoId"] = new SelectList(_context.ProdutosServicos, "Id", "Id");
            return View();
        }

        // POST: ItemCatalogoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Preco,Disponivel,CatalogoId,ProdutoId")] ItemCatalogo itemCatalogo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemCatalogo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatalogoId"] = new SelectList(_context.Catalogos, "Id", "Id", itemCatalogo.CatalogoId);
            ViewData["ProdutoId"] = new SelectList(_context.ProdutosServicos, "Id", "Id", itemCatalogo.ProdutoId);
            return View(itemCatalogo);
        }

        // GET: ItemCatalogoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCatalogo = await _context.ItensCatalogo.FindAsync(id);
            if (itemCatalogo == null)
            {
                return NotFound();
            }
            ViewData["CatalogoId"] = new SelectList(_context.Catalogos, "Id", "Id", itemCatalogo.CatalogoId);
            ViewData["ProdutoId"] = new SelectList(_context.ProdutosServicos, "Id", "Id", itemCatalogo.ProdutoId);
            return View(itemCatalogo);
        }

        // POST: ItemCatalogoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Preco,Disponivel,CatalogoId,ProdutoId")] ItemCatalogo itemCatalogo)
        {
            if (id != itemCatalogo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemCatalogo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemCatalogoExists(itemCatalogo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatalogoId"] = new SelectList(_context.Catalogos, "Id", "Id", itemCatalogo.CatalogoId);
            ViewData["ProdutoId"] = new SelectList(_context.ProdutosServicos, "Id", "Id", itemCatalogo.ProdutoId);
            return View(itemCatalogo);
        }

        // GET: ItemCatalogoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCatalogo = await _context.ItensCatalogo
                .Include(i => i.Catalogo)
                .Include(i => i.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemCatalogo == null)
            {
                return NotFound();
            }

            return View(itemCatalogo);
        }

        // POST: ItemCatalogoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemCatalogo = await _context.ItensCatalogo.FindAsync(id);
            if (itemCatalogo != null)
            {
                _context.ItensCatalogo.Remove(itemCatalogo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemCatalogoExists(int id)
        {
            return _context.ItensCatalogo.Any(e => e.Id == id);
        }
    }
}
