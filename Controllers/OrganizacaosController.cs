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
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizacaosController : Controller
    {
        private readonly PlataformaB2B_A2_TP3Context _context;

        public OrganizacaosController(PlataformaB2B_A2_TP3Context context)
        {
            _context = context;
        }

        // GET: Organizacaos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Organizacoes.ToListAsync());
        }

        // GET: Organizacaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizacao = await _context.Organizacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizacao == null)
            {
                return NotFound();
            }

            return View(organizacao);
        }

        // GET: Organizacaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organizacaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cnpj")] Organizacao organizacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organizacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organizacao);
        }

        // GET: Organizacaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizacao = await _context.Organizacoes.FindAsync(id);
            if (organizacao == null)
            {
                return NotFound();
            }
            return View(organizacao);
        }

        // POST: Organizacaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cnpj")] Organizacao organizacao)
        {
            if (id != organizacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organizacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganizacaoExists(organizacao.Id))
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
            return View(organizacao);
        }

        // GET: Organizacaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizacao = await _context.Organizacoes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (organizacao == null)
            {
                return NotFound();
            }

            return View(organizacao);
        }

        // POST: Organizacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var organizacao = await _context.Organizacoes.FindAsync(id);
            if (organizacao != null)
            {
                _context.Organizacoes.Remove(organizacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganizacaoExists(int id)
        {
            return _context.Organizacoes.Any(e => e.Id == id);
        }
    }
}
