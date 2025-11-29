using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaB2B_A2_TP3.Data;
using PlataformaB2B_A2_TP3.DTOs;
using PlataformaB2B_A2_TP3.Models;

namespace PlataformaB2B_A2_TP3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LancamentosFinanceirosController : ControllerBase
    {
        private readonly PlataformaB2B_A2_TP3Context _context;

        public LancamentosFinanceirosController(PlataformaB2B_A2_TP3Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os lançamentos financeiros
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LancamentoFinanceiroReadDto>>> GetLancamentos()
        {
            var lancamentos = await _context.LancamentosFinanceiros
                .Select(l => new LancamentoFinanceiroReadDto
                {
                    Id = l.Id,
                    PedidoId = l.PedidoId,
                    Valor = l.Valor,
                    Tipo = l.Tipo,
                    Status = l.Status
                })
                .ToListAsync();

            return Ok(lancamentos);
        }

        /// <summary>
        /// Busca um lançamento financeiro por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<LancamentoFinanceiroReadDto>> GetLancamento(int id)
        {
            var lancamento = await _context.LancamentosFinanceiros
                .Where(l => l.Id == id)
                .Select(l => new LancamentoFinanceiroReadDto
                {
                    Id = l.Id,
                    PedidoId = l.PedidoId,
                    Valor = l.Valor,
                    Tipo = l.Tipo,
                    Status = l.Status
                })
                .FirstOrDefaultAsync();

            if (lancamento == null)
                return NotFound();

            return Ok(lancamento);
        }

        /// <summary>
        /// Cria um novo lançamento financeiro
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<LancamentoFinanceiroReadDto>> CreateLancamento(LancamentoFinanceiroCreateDto dto)
        {
            var lancamento = new LancamentoFinanceiro
            {
                PedidoId = dto.PedidoId,
                Valor = dto.Valor,
                Tipo = dto.Tipo,
                Status = dto.Status
            };

            _context.LancamentosFinanceiros.Add(lancamento);
            await _context.SaveChangesAsync();

            var result = await GetLancamento(lancamento.Id);
            return CreatedAtAction(nameof(GetLancamento), new { id = lancamento.Id }, result.Value);
        }

        /// <summary>
        /// Atualiza um lançamento financeiro existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLancamento(int id, LancamentoFinanceiroUpdateDto dto)
        {
            var lancamento = await _context.LancamentosFinanceiros.FindAsync(id);
            if (lancamento == null)
                return NotFound();

            if (!string.IsNullOrEmpty(dto.Status))
                lancamento.Status = dto.Status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LancamentoExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Remove um lançamento financeiro
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLancamento(int id)
        {
            var lancamento = await _context.LancamentosFinanceiros.FindAsync(id);
            if (lancamento == null)
                return NotFound();

            _context.LancamentosFinanceiros.Remove(lancamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LancamentoExists(int id)
        {
            return _context.LancamentosFinanceiros.Any(e => e.Id == id);
        }
    }
}
