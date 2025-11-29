using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaB2B_A2_TP3.Data;
using PlataformaB2B_A2_TP3.DTOs;
using PlataformaB2B_A2_TP3.Models;

namespace PlataformaB2B_A2_TP3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizacoesController : ControllerBase
    {
        private readonly PlataformaB2B_A2_TP3Context _context;

        public OrganizacoesController(PlataformaB2B_A2_TP3Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todas as organizações
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizacaoReadDto>>> GetOrganizacoes()
        {
            var organizacoes = await _context.Organizacoes
                .Select(o => new OrganizacaoReadDto
                {
                    Id = o.Id,
                    Nome = o.Nome,
                    Cnpj = o.Cnpj
                })
                .ToListAsync();

            return Ok(organizacoes);
        }

        /// <summary>
        /// Busca uma organização por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizacaoReadDto>> GetOrganizacao(int id)
        {
            var organizacao = await _context.Organizacoes
                .Where(o => o.Id == id)
                .Select(o => new OrganizacaoReadDto
                {
                    Id = o.Id,
                    Nome = o.Nome,
                    Cnpj = o.Cnpj
                })
                .FirstOrDefaultAsync();

            if (organizacao == null)
                return NotFound();

            return Ok(organizacao);
        }

        /// <summary>
        /// Cria uma nova organização
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<OrganizacaoReadDto>> CreateOrganizacao(OrganizacaoCreateDto dto)
        {
            var organizacao = new Organizacao
            {
                Nome = dto.Nome,
                Cnpj = dto.Cnpj
            };

            _context.Organizacoes.Add(organizacao);
            await _context.SaveChangesAsync();

            var result = await GetOrganizacao(organizacao.Id);
            return CreatedAtAction(nameof(GetOrganizacao), new { id = organizacao.Id }, result.Value);
        }

        /// <summary>
        /// Atualiza uma organização existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganizacao(int id, OrganizacaoUpdateDto dto)
        {
            var organizacao = await _context.Organizacoes.FindAsync(id);
            if (organizacao == null)
                return NotFound();

            if (!string.IsNullOrEmpty(dto.Nome))
                organizacao.Nome = dto.Nome;
            
            if (!string.IsNullOrEmpty(dto.Cnpj))
                organizacao.Cnpj = dto.Cnpj;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizacaoExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Remove uma organização
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizacao(int id)
        {
            var organizacao = await _context.Organizacoes.FindAsync(id);
            if (organizacao == null)
                return NotFound();

            _context.Organizacoes.Remove(organizacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrganizacaoExists(int id)
        {
            return _context.Organizacoes.Any(e => e.Id == id);
        }
    }
}
