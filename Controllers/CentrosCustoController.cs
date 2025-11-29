using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaB2B_A2_TP3.Data;
using PlataformaB2B_A2_TP3.DTOs;
using PlataformaB2B_A2_TP3.Models;

namespace PlataformaB2B_A2_TP3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CentrosCustoController : ControllerBase
    {
        private readonly PlataformaB2B_A2_TP3Context _context;

        public CentrosCustoController(PlataformaB2B_A2_TP3Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os centros de custo
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CentroCustoReadDto>>> GetCentrosCusto()
        {
            var centros = await _context.CentrosCusto
                .Include(c => c.Organizacao)
                .Select(c => new CentroCustoReadDto
                {
                    Id = c.Id,
                    Codigo = c.Codigo,
                    Descricao = c.Descricao,
                    OrgId = c.OrgId,
                    OrganizacaoNome = c.Organizacao.Nome
                })
                .ToListAsync();

            return Ok(centros);
        }

        /// <summary>
        /// Busca um centro de custo por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CentroCustoReadDto>> GetCentroCusto(int id)
        {
            var centro = await _context.CentrosCusto
                .Include(c => c.Organizacao)
                .Where(c => c.Id == id)
                .Select(c => new CentroCustoReadDto
                {
                    Id = c.Id,
                    Codigo = c.Codigo,
                    Descricao = c.Descricao,
                    OrgId = c.OrgId,
                    OrganizacaoNome = c.Organizacao.Nome
                })
                .FirstOrDefaultAsync();

            if (centro == null)
                return NotFound();

            return Ok(centro);
        }

        /// <summary>
        /// Cria um novo centro de custo
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CentroCustoReadDto>> CreateCentroCusto(CentroCustoCreateDto dto)
        {
            var centro = new CentroCusto
            {
                Codigo = dto.Codigo,
                Descricao = dto.Descricao,
                OrgId = dto.OrgId
            };

            _context.CentrosCusto.Add(centro);
            await _context.SaveChangesAsync();

            var result = await GetCentroCusto(centro.Id);
            return CreatedAtAction(nameof(GetCentroCusto), new { id = centro.Id }, result.Value);
        }

        /// <summary>
        /// Atualiza um centro de custo existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCentroCusto(int id, CentroCustoUpdateDto dto)
        {
            var centro = await _context.CentrosCusto.FindAsync(id);
            if (centro == null)
                return NotFound();

            if (!string.IsNullOrEmpty(dto.Codigo))
                centro.Codigo = dto.Codigo;
            
            if (!string.IsNullOrEmpty(dto.Descricao))
                centro.Descricao = dto.Descricao;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CentroCustoExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Remove um centro de custo
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCentroCusto(int id)
        {
            var centro = await _context.CentrosCusto.FindAsync(id);
            if (centro == null)
                return NotFound();

            _context.CentrosCusto.Remove(centro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CentroCustoExists(int id)
        {
            return _context.CentrosCusto.Any(e => e.Id == id);
        }
    }
}
