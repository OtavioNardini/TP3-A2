using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaB2B_A2_TP3.Data;
using PlataformaB2B_A2_TP3.DTOs;
using PlataformaB2B_A2_TP3.Models;

namespace PlataformaB2B_A2_TP3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacoesController : ControllerBase
    {
        private readonly PlataformaB2B_A2_TP3Context _context;

        public NotificacoesController(PlataformaB2B_A2_TP3Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todas as notificações
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificacaoReadDto>>> GetNotificacoes()
        {
            var notificacoes = await _context.Notificacoes
                .Include(n => n.Destinatario)
                .Select(n => new NotificacaoReadDto
                {
                    Id = n.Id,
                    Tipo = n.Tipo,
                    DestinatarioId = n.DestinatarioId,
                    DestinatarioNome = n.Destinatario.Nome,
                    Lida = n.Lida,
                    Payload = n.Payload
                })
                .ToListAsync();

            return Ok(notificacoes);
        }

        /// <summary>
        /// Busca uma notificação por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificacaoReadDto>> GetNotificacao(int id)
        {
            var notificacao = await _context.Notificacoes
                .Include(n => n.Destinatario)
                .Where(n => n.Id == id)
                .Select(n => new NotificacaoReadDto
                {
                    Id = n.Id,
                    Tipo = n.Tipo,
                    DestinatarioId = n.DestinatarioId,
                    DestinatarioNome = n.Destinatario.Nome,
                    Lida = n.Lida,
                    Payload = n.Payload
                })
                .FirstOrDefaultAsync();

            if (notificacao == null)
                return NotFound();

            return Ok(notificacao);
        }

        /// <summary>
        /// Cria uma nova notificação
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<NotificacaoReadDto>> CreateNotificacao(NotificacaoCreateDto dto)
        {
            var notificacao = new Notificacao
            {
                Tipo = dto.Tipo,
                DestinatarioId = dto.DestinatarioId,
                Lida = false,
                Payload = dto.Payload
            };

            _context.Notificacoes.Add(notificacao);
            await _context.SaveChangesAsync();

            var result = await GetNotificacao(notificacao.Id);
            return CreatedAtAction(nameof(GetNotificacao), new { id = notificacao.Id }, result.Value);
        }

        /// <summary>
        /// Marca notificação como lida
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotificacao(int id, NotificacaoUpdateDto dto)
        {
            var notificacao = await _context.Notificacoes.FindAsync(id);
            if (notificacao == null)
                return NotFound();

            if (dto.Lida.HasValue)
                notificacao.Lida = dto.Lida.Value;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificacaoExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Remove uma notificação
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotificacao(int id)
        {
            var notificacao = await _context.Notificacoes.FindAsync(id);
            if (notificacao == null)
                return NotFound();

            _context.Notificacoes.Remove(notificacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotificacaoExists(int id)
        {
            return _context.Notificacoes.Any(e => e.Id == id);
        }
    }
}
