using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaB2B_A2_TP3.Data;
using PlataformaB2B_A2_TP3.DTOs;
using PlataformaB2B_A2_TP3.Models;

namespace PlataformaB2B_A2_TP3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogosController : ControllerBase
    {
        private readonly PlataformaB2B_A2_TP3Context _context;

        public CatalogosController(PlataformaB2B_A2_TP3Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os catálogos
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogoReadDto>>> GetCatalogos()
        {
            var catalogos = await _context.Catalogos
                .Include(c => c.Organizacao)
                .Select(c => new CatalogoReadDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    OrganizacaoId = c.OrganizacaoId,
                    OrganizacaoNome = c.Organizacao.Nome
                })
                .ToListAsync();

            return Ok(catalogos);
        }

        /// <summary>
        /// Busca um catálogo por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogoReadDto>> GetCatalogo(int id)
        {
            var catalogo = await _context.Catalogos
                .Include(c => c.Organizacao)
                .Where(c => c.Id == id)
                .Select(c => new CatalogoReadDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    OrganizacaoId = c.OrganizacaoId,
                    OrganizacaoNome = c.Organizacao.Nome
                })
                .FirstOrDefaultAsync();

            if (catalogo == null)
                return NotFound();

            return Ok(catalogo);
        }

        /// <summary>
        /// Cria um novo catálogo
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CatalogoReadDto>> CreateCatalogo(CatalogoCreateDto dto)
        {
            var catalogo = new Catalogo
            {
                Nome = dto.Nome,
                OrganizacaoId = dto.OrganizacaoId
            };

            _context.Catalogos.Add(catalogo);
            await _context.SaveChangesAsync();

            var result = await GetCatalogo(catalogo.Id);
            return CreatedAtAction(nameof(GetCatalogo), new { id = catalogo.Id }, result.Value);
        }

        /// <summary>
        /// Atualiza um catálogo existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCatalogo(int id, CatalogoUpdateDto dto)
        {
            var catalogo = await _context.Catalogos.FindAsync(id);
            if (catalogo == null)
                return NotFound();

            if (!string.IsNullOrEmpty(dto.Nome))
                catalogo.Nome = dto.Nome;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatalogoExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Remove um catálogo
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCatalogo(int id)
        {
            var catalogo = await _context.Catalogos.FindAsync(id);
            if (catalogo == null)
                return NotFound();

            _context.Catalogos.Remove(catalogo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CatalogoExists(int id)
        {
            return _context.Catalogos.Any(e => e.Id == id);
        }
    }
}
