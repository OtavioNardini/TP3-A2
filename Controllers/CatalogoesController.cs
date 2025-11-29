using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlataformaB2B_A2_TP3.Data;
using PlataformaB2B_A2_TP3.Models;


    [ApiController]
    [Route("api/[controller]")]
    public class CatalogoesController : Controller
    {
        private readonly PlataformaB2B_A2_TP3Context _context;

        public CatalogoesController(PlataformaB2B_A2_TP3Context context)
        {
            _context = context;
        }

        // GET: Catalogoes
        public async Task<IActionResult> Index()
        {
            var plataformaB2B_A2_TP3Context = _context.Catalogos.Include(c => c.Organizacao);
            return View(await plataformaB2B_A2_TP3Context.ToListAsync());
        }

        // GET: Catalogoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogo = await _context.Catalogos
                .Include(c => c.Organizacao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogo == null)
            {
                return NotFound();
            }

            return View(catalogo);
        }

        // GET: Catalogoes/Create
        public IActionResult Create()
        {
            ViewData["OrganizacaoId"] = new SelectList(_context.Organizacoes, "Id", "Id");
            return View();
        }

        // POST: Catalogoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,OrganizacaoId")] Catalogo catalogo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalogo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrganizacaoId"] = new SelectList(_context.Organizacoes, "Id", "Id", catalogo.OrganizacaoId);
            return View(catalogo);
        }

        // GET: Catalogoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogo = await _context.Catalogos.FindAsync(id);
            if (catalogo == null)
            {
                return NotFound();
            }
            ViewData["OrganizacaoId"] = new SelectList(_context.Organizacoes, "Id", "Id", catalogo.OrganizacaoId);
            return View(catalogo);
        }

        // POST: Catalogoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,OrganizacaoId")] Catalogo catalogo)
        {
            if (id != catalogo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalogo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogoExists(catalogo.Id))
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
            ViewData["OrganizacaoId"] = new SelectList(_context.Organizacoes, "Id", "Id", catalogo.OrganizacaoId);
            return View(catalogo);
        }

        // GET: Catalogoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalogo = await _context.Catalogos
                .Include(c => c.Organizacao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalogo == null)
            {
                return NotFound();
            }

            return View(catalogo);
        }

        // POST: Catalogoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalogo = await _context.Catalogos.FindAsync(id);
            if (catalogo != null)
            {
                _context.Catalogos.Remove(catalogo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogoExists(int id)
        {
            return _context.Catalogos.Any(e => e.Id == id);
        }
    }

