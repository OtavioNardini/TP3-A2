using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProdutoServicoController : ControllerBase
{
    private static List<ProdutoServico> produtos = new();

    [HttpGet]
    public ActionResult<IEnumerable<ProdutoServico>> Get() => produtos;

    [HttpGet("{id}")]
    public ActionResult<ProdutoServico> Get(int id)
    {
        var produto = produtos.FirstOrDefault(p => p.Id == id);
        return produto is null ? NotFound() : produto;
    }

    [HttpPost]
    public ActionResult<ProdutoServico> Post(ProdutoServico produto)
    {
        produto.Id = produtos.Count > 0 ? produtos.Max(p => p.Id) + 1 : 1;
        produtos.Add(produto);
        return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, ProdutoServico produto)
    {
        var existing = produtos.FirstOrDefault(p => p.Id == id);
        if (existing is null) return NotFound();
        existing.Sku = produto.Sku;
        existing.Descricao = produto.Descricao;
        existing.Unidade = produto.Unidade;
        existing.Categoria = produto.Categoria;
        existing.FornecedorId = produto.FornecedorId;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var produto = produtos.FirstOrDefault(p => p.Id == id);
        if (produto is null) return NotFound();
        produtos.Remove(produto);
        return NoContent();
    }
}