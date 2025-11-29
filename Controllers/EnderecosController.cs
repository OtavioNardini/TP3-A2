using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaB2B_A2_TP3.Data;
using PlataformaB2B_A2_TP3.DTOs;
using PlataformaB2B_A2_TP3.Models;

namespace PlataformaB2B_A2_TP3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecosController : ControllerBase
    {
        private readonly PlataformaB2B_A2_TP3Context _context;

        public EnderecosController(PlataformaB2B_A2_TP3Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os endereços
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnderecoReadDto>>> GetEnderecos()
        {
            var enderecos = await _context.Enderecos
                .Select(e => new EnderecoReadDto
                {
                    Id = e.Id,
                    Cep = e.Cep,
                    Logradouro = e.Logradouro,
                    Numero = e.Numero,
                    Complemento = e.Complemento,
                    Bairro = e.Bairro,
                    MunicipioId = e.MunicipioId,
                    Uf = e.Uf,
                    FornecedorId = e.FornecedorId,
                    OrganizacaoId = e.OrganizacaoId
                })
                .ToListAsync();

            return Ok(enderecos);
        }

        /// <summary>
        /// Busca um endereço por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<EnderecoReadDto>> GetEndereco(int id)
        {
            var endereco = await _context.Enderecos
                .Where(e => e.Id == id)
                .Select(e => new EnderecoReadDto
                {
                    Id = e.Id,
                    Cep = e.Cep,
                    Logradouro = e.Logradouro,
                    Numero = e.Numero,
                    Complemento = e.Complemento,
                    Bairro = e.Bairro,
                    MunicipioId = e.MunicipioId,
                    Uf = e.Uf,
                    FornecedorId = e.FornecedorId,
                    OrganizacaoId = e.OrganizacaoId
                })
                .FirstOrDefaultAsync();

            if (endereco == null)
                return NotFound();

            return Ok(endereco);
        }

        /// <summary>
        /// Cria um novo endereço
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<EnderecoReadDto>> CreateEndereco(EnderecoCreateDto dto)
        {
            var endereco = new Endereco
            {
                Cep = dto.Cep,
                Logradouro = dto.Logradouro,
                Numero = dto.Numero,
                Complemento = dto.Complemento,
                Bairro = dto.Bairro,
                MunicipioId = dto.MunicipioId,
                Uf = dto.Uf,
                FornecedorId = dto.FornecedorId,
                OrganizacaoId = dto.OrganizacaoId
            };

            _context.Enderecos.Add(endereco);
            await _context.SaveChangesAsync();

            var result = await GetEndereco(endereco.Id);
            return CreatedAtAction(nameof(GetEndereco), new { id = endereco.Id }, result.Value);
        }

        /// <summary>
        /// Atualiza um endereço existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEndereco(int id, EnderecoUpdateDto dto)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null)
                return NotFound();

            if (!string.IsNullOrEmpty(dto.Cep))
                endereco.Cep = dto.Cep;
            
            if (!string.IsNullOrEmpty(dto.Logradouro))
                endereco.Logradouro = dto.Logradouro;
            
            if (!string.IsNullOrEmpty(dto.Numero))
                endereco.Numero = dto.Numero;
            
            if (dto.Complemento != null)
                endereco.Complemento = dto.Complemento;
            
            if (!string.IsNullOrEmpty(dto.Bairro))
                endereco.Bairro = dto.Bairro;
            
            if (!string.IsNullOrEmpty(dto.Uf))
                endereco.Uf = dto.Uf;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnderecoExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Remove um endereço
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            var endereco = await _context.Enderecos.FindAsync(id);
            if (endereco == null)
                return NotFound();

            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnderecoExists(int id)
        {
            return _context.Enderecos.Any(e => e.Id == id);
        }
    }
}
