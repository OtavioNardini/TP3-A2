using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaB2B_A2_TP3.Data;
using PlataformaB2B_A2_TP3.DTOs;
using PlataformaB2B_A2_TP3.Models;

namespace PlataformaB2B_A2_TP3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AprovacoesController : ControllerBase
    {
        private readonly PlataformaB2B_A2_TP3Context _context;

        public AprovacoesController(PlataformaB2B_A2_TP3Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todas as aprovações
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AprovacaoReadDto>>> GetAprovacoes()
        {
            var aprovacoes = await _context.Aprovacoes
                .Include(a => a.Aprovador)
                .Select(a => new AprovacaoReadDto
                {
                    Id = a.Id,
                    PedidoId = a.PedidoId,
                    AprovadorId = a.AprovadorId,
                    AprovadorNome = a.Aprovador.Nome,
                    Status = a.Status,
                    Comentario = a.Comentario,
                    Data = a.Data
                })
                .ToListAsync();

            return Ok(aprovacoes);
        }

        /// <summary>
        /// Busca uma aprovação por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<AprovacaoReadDto>> GetAprovacao(int id)
        {
            var aprovacao = await _context.Aprovacoes
                .Include(a => a.Aprovador)
                .Where(a => a.Id == id)
                .Select(a => new AprovacaoReadDto
                {
                    Id = a.Id,
                    PedidoId = a.PedidoId,
                    AprovadorId = a.AprovadorId,
                    AprovadorNome = a.Aprovador.Nome,
                    Status = a.Status,
                    Comentario = a.Comentario,
                    Data = a.Data
                })
                .FirstOrDefaultAsync();

            if (aprovacao == null)
                return NotFound();

            return Ok(aprovacao);
        }

        /// <summary>
        /// Cria uma nova aprovação
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<AprovacaoReadDto>> CreateAprovacao(AprovacaoCreateDto dto)
        {
            var aprovacao = new Aprovacao
            {
                PedidoId = dto.PedidoId,
                AprovadorId = dto.AprovadorId,
                Status = dto.Status,
                Comentario = dto.Comentario,
                Data = DateTime.UtcNow
            };

            _context.Aprovacoes.Add(aprovacao);
            await _context.SaveChangesAsync();

            var result = await GetAprovacao(aprovacao.Id);
            return CreatedAtAction(nameof(GetAprovacao), new { id = aprovacao.Id }, result.Value);
        }

        /// <summary>
        /// Atualiza uma aprovação existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAprovacao(int id, AprovacaoUpdateDto dto)
        {
            var aprovacao = await _context.Aprovacoes.FindAsync(id);
            if (aprovacao == null)
                return NotFound();

            if (!string.IsNullOrEmpty(dto.Status))
                aprovacao.Status = dto.Status;
            
            if (!string.IsNullOrEmpty(dto.Comentario))
                aprovacao.Comentario = dto.Comentario;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AprovacaoExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Remove uma aprovação
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAprovacao(int id)
        {
            var aprovacao = await _context.Aprovacoes.FindAsync(id);
            if (aprovacao == null)
                return NotFound();

            _context.Aprovacoes.Remove(aprovacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AprovacaoExists(int id)
        {
            return _context.Aprovacoes.Any(e => e.Id == id);
        }
    }
}
