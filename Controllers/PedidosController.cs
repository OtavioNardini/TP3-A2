using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaB2B_A2_TP3.Data;
using PlataformaB2B_A2_TP3.DTOs;
using PlataformaB2B_A2_TP3.Models;

namespace PlataformaB2B_A2_TP3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly PlataformaB2B_A2_TP3Context _context;

        public PedidosController(PlataformaB2B_A2_TP3Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todos os pedidos
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoReadDto>>> GetPedidos()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Comprador)
                .Include(p => p.Organizacao)
                .Select(p => new PedidoReadDto
                {
                    Id = p.Id,
                    CompradorId = p.CompradorId,
                    CompradorNome = p.Comprador.Nome,
                    OrgId = p.OrgId,
                    OrganizacaoNome = p.Organizacao.Nome,
                    Total = p.Total,
                    Status = p.Status,
                    DataCriacao = p.DataCriacao,
                    EnderecoEntregaId = p.EnderecoEntregaId,
                    CentroCustoId = p.CentroCustoId
                })
                .ToListAsync();

            return Ok(pedidos);
        }

        /// <summary>
        /// Busca um pedido por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoReadDto>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Comprador)
                .Include(p => p.Organizacao)
                .Where(p => p.Id == id)
                .Select(p => new PedidoReadDto
                {
                    Id = p.Id,
                    CompradorId = p.CompradorId,
                    CompradorNome = p.Comprador.Nome,
                    OrgId = p.OrgId,
                    OrganizacaoNome = p.Organizacao.Nome,
                    Total = p.Total,
                    Status = p.Status,
                    DataCriacao = p.DataCriacao,
                    EnderecoEntregaId = p.EnderecoEntregaId,
                    CentroCustoId = p.CentroCustoId
                })
                .FirstOrDefaultAsync();

            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }

        /// <summary>
        /// Cria um novo pedido
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<PedidoReadDto>> CreatePedido(PedidoCreateDto dto)
        {
            var pedido = new Pedido
            {
                CompradorId = dto.CompradorId,
                OrgId = dto.OrgId,
                Total = dto.Total,
                Status = dto.Status,
                DataCriacao = DateTime.UtcNow,
                EnderecoEntregaId = dto.EnderecoEntregaId,
                CentroCustoId = dto.CentroCustoId
            };

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            var result = await GetPedido(pedido.Id);
            return CreatedAtAction(nameof(GetPedido), new { id = pedido.Id }, result.Value);
        }

        /// <summary>
        /// Atualiza um pedido existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePedido(int id, PedidoUpdateDto dto)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
                return NotFound();

            if (dto.Total.HasValue)
                pedido.Total = dto.Total.Value;
            
            if (!string.IsNullOrEmpty(dto.Status))
                pedido.Status = dto.Status;
            
            if (dto.EnderecoEntregaId.HasValue)
                pedido.EnderecoEntregaId = dto.EnderecoEntregaId;
            
            if (dto.CentroCustoId.HasValue)
                pedido.CentroCustoId = dto.CentroCustoId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Remove um pedido
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
                return NotFound();

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
