using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaB2B_A2_TP3.Data;
using PlataformaB2B_A2_TP3.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class ProdutoServicoController : ControllerBase
{
    private readonly PlataformaB2B_A2_TP3Context _context;

    public ProdutoServicoController(PlataformaB2B_A2_TP3Context context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoServico>>> Get()
    {
        return await _context.ProdutosServicos.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoServico>> Get(int id)
    {
        var produto = await _context.ProdutosServicos.FindAsync(id);
        return produto is null ? NotFound() : produto;
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoServico>> Post(ProdutoServico produto)
    {
        _context.ProdutosServicos.Add(produto);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ProdutoServico produto)
    {
        if (id != produto.Id) return BadRequest();

        _context.Entry(produto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProdutoServicoExists(id)) return NotFound();
            else throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var produto = await _context.ProdutosServicos.FindAsync(id);
        if (produto is null) return NotFound();

        _context.ProdutosServicos.Remove(produto);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool ProdutoServicoExists(int id)
    {
        return _context.ProdutosServicos.Any(e => e.Id == id);
    }
}