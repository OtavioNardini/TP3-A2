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
    public class LancamentoFinanceiroesController : Controller
    {
        private readonly PlataformaB2B_A2_TP3Context _context;

        public LancamentoFinanceiroesController(PlataformaB2B_A2_TP3Context context)
        {
            _context = context;
        }

        // GET: LancamentoFinanceiroes
        public async Task<IActionResult> Index()
        {
            var plataformaB2B_A2_TP3Context = _context.LancamentosFinanceiros.Include(l => l.Pedido);
            return View(await plataformaB2B_A2_TP3Context.ToListAsync());
        }

        // GET: LancamentoFinanceiroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lancamentoFinanceiro = await _context.LancamentosFinanceiros
                .Include(l => l.Pedido)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lancamentoFinanceiro == null)
            {
                return NotFound();
            }

            return View(lancamentoFinanceiro);
        }

        // GET: LancamentoFinanceiroes/Create
        public IActionResult Create()
        {
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id");
            return View();
        }

        // POST: LancamentoFinanceiroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Valor,Tipo,Status,PedidoId")] LancamentoFinanceiro lancamentoFinanceiro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lancamentoFinanceiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id", lancamentoFinanceiro.PedidoId);
            return View(lancamentoFinanceiro);
        }

        // GET: LancamentoFinanceiroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lancamentoFinanceiro = await _context.LancamentosFinanceiros.FindAsync(id);
            if (lancamentoFinanceiro == null)
            {
                return NotFound();
            }
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id", lancamentoFinanceiro.PedidoId);
            return View(lancamentoFinanceiro);
        }

        // POST: LancamentoFinanceiroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Valor,Tipo,Status,PedidoId")] LancamentoFinanceiro lancamentoFinanceiro)
        {
            if (id != lancamentoFinanceiro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lancamentoFinanceiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LancamentoFinanceiroExists(lancamentoFinanceiro.Id))
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
            ViewData["PedidoId"] = new SelectList(_context.Pedidos, "Id", "Id", lancamentoFinanceiro.PedidoId);
            return View(lancamentoFinanceiro);
        }

        // GET: LancamentoFinanceiroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lancamentoFinanceiro = await _context.LancamentosFinanceiros
                .Include(l => l.Pedido)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lancamentoFinanceiro == null)
            {
                return NotFound();
            }

            return View(lancamentoFinanceiro);
        }

        // POST: LancamentoFinanceiroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lancamentoFinanceiro = await _context.LancamentosFinanceiros.FindAsync(id);
            if (lancamentoFinanceiro != null)
            {
                _context.LancamentosFinanceiros.Remove(lancamentoFinanceiro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LancamentoFinanceiroExists(int id)
        {
            return _context.LancamentosFinanceiros.Any(e => e.Id == id);
        }
    }
}
