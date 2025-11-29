using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaB2B_A2_TP3.Data;
using PlataformaB2B_A2_TP3.DTOs;
using PlataformaB2B_A2_TP3.Models;

namespace PlataformaB2B_A2_TP3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosServicosController : ControllerBase
    {
        private readonly PlataformaB2B_A2_TP3Context _context;

        public ProdutosServicosController(PlataformaB2B_A2_TP3Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os produtos/serviços
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoServicoReadDto>>> GetProdutosServicos()
        {
            var produtos = await _context.ProdutosServicos
                .Include(p => p.Fornecedor)
                .Select(p => new ProdutoServicoReadDto
                {
                    Id = p.Id,
                    Sku = p.Sku,
                    Descricao = p.Descricao,
                    Unidade = p.Unidade,
                    Categoria = p.Categoria,
                    FornecedorId = p.FornecedorId,
                    FornecedorNome = p.Fornecedor.RazaoSocial
                })
                .ToListAsync();

            return Ok(produtos);
        }

        /// <summary>
        /// Busca um produto/serviço por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoServicoReadDto>> GetProdutoServico(int id)
        {
            var produto = await _context.ProdutosServicos
                .Include(p => p.Fornecedor)
                .Where(p => p.Id == id)
                .Select(p => new ProdutoServicoReadDto
                {
                    Id = p.Id,
                    Sku = p.Sku,
                    Descricao = p.Descricao,
                    Unidade = p.Unidade,
                    Categoria = p.Categoria,
                    FornecedorId = p.FornecedorId,
                    FornecedorNome = p.Fornecedor.RazaoSocial
                })
                .FirstOrDefaultAsync();

            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        /// <summary>
        /// Cria um novo produto/serviço
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ProdutoServicoReadDto>> CreateProdutoServico(ProdutoServicoCreateDto dto)
        {
            var produto = new ProdutoServico
            {
                Sku = dto.Sku,
                Descricao = dto.Descricao,
                Unidade = dto.Unidade,
                Categoria = dto.Categoria,
                FornecedorId = dto.FornecedorId
            };

            _context.ProdutosServicos.Add(produto);
            await _context.SaveChangesAsync();

            var result = await GetProdutoServico(produto.Id);
            return CreatedAtAction(nameof(GetProdutoServico), new { id = produto.Id }, result.Value);
        }

        /// <summary>
        /// Atualiza um produto/serviço existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProdutoServico(int id, ProdutoServicoUpdateDto dto)
        {
            var produto = await _context.ProdutosServicos.FindAsync(id);
            if (produto == null)
                return NotFound();

            if (!string.IsNullOrEmpty(dto.Sku))
                produto.Sku = dto.Sku;
            
            if (!string.IsNullOrEmpty(dto.Descricao))
                produto.Descricao = dto.Descricao;
            
            if (!string.IsNullOrEmpty(dto.Unidade))
                produto.Unidade = dto.Unidade;
            
            if (!string.IsNullOrEmpty(dto.Categoria))
                produto.Categoria = dto.Categoria;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoServicoExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Remove um produto/serviço
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdutoServico(int id)
        {
            var produto = await _context.ProdutosServicos.FindAsync(id);
            if (produto == null)
                return NotFound();

            _context.ProdutosServicos.Remove(produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoServicoExists(int id)
        {
            return _context.ProdutosServicos.Any(e => e.Id == id);
        }
    }
}
