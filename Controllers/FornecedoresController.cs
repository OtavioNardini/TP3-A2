using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaB2B_A2_TP3.Data;
using PlataformaB2B_A2_TP3.DTOs;
using PlataformaB2B_A2_TP3.Models;

namespace PlataformaB2B_A2_TP3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedoresController : ControllerBase
    {
        private readonly PlataformaB2B_A2_TP3Context _context;

        public FornecedoresController(PlataformaB2B_A2_TP3Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os fornecedores
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorReadDto>>> GetFornecedores()
        {
            var fornecedores = await _context.Fornecedores
                .Select(f => new FornecedorReadDto
                {
                    Id = f.Id,
                    RazaoSocial = f.RazaoSocial,
                    Cnpj = f.Cnpj,
                    StatusKYC = f.StatusKYC,
                    Certificacoes = f.Certificacoes
                })
                .ToListAsync();

            return Ok(fornecedores);
        }

        /// <summary>
        /// Busca um fornecedor por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<FornecedorReadDto>> GetFornecedor(int id)
        {
            var fornecedor = await _context.Fornecedores
                .Where(f => f.Id == id)
                .Select(f => new FornecedorReadDto
                {
                    Id = f.Id,
                    RazaoSocial = f.RazaoSocial,
                    Cnpj = f.Cnpj,
                    StatusKYC = f.StatusKYC,
                    Certificacoes = f.Certificacoes
                })
                .FirstOrDefaultAsync();

            if (fornecedor == null)
                return NotFound();

            return Ok(fornecedor);
        }

        /// <summary>
        /// Cria um novo fornecedor
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<FornecedorReadDto>> CreateFornecedor(FornecedorCreateDto dto)
        {
            var fornecedor = new Fornecedor
            {
                RazaoSocial = dto.RazaoSocial,
                Cnpj = dto.Cnpj,
                StatusKYC = dto.StatusKYC ?? "Pendente",
                Certificacoes = dto.Certificacoes
            };

            _context.Fornecedores.Add(fornecedor);
            await _context.SaveChangesAsync();

            var result = await GetFornecedor(fornecedor.Id);
            return CreatedAtAction(nameof(GetFornecedor), new { id = fornecedor.Id }, result.Value);
        }

        /// <summary>
        /// Atualiza um fornecedor existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFornecedor(int id, FornecedorUpdateDto dto)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null)
                return NotFound();

            if (!string.IsNullOrEmpty(dto.RazaoSocial))
                fornecedor.RazaoSocial = dto.RazaoSocial;
            
            if (!string.IsNullOrEmpty(dto.StatusKYC))
                fornecedor.StatusKYC = dto.StatusKYC;
            
            if (dto.Certificacoes != null)
                fornecedor.Certificacoes = dto.Certificacoes;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FornecedorExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Remove um fornecedor
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFornecedor(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null)
                return NotFound();

            _context.Fornecedores.Remove(fornecedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FornecedorExists(int id)
        {
            return _context.Fornecedores.Any(e => e.Id == id);
        }
    }
}
