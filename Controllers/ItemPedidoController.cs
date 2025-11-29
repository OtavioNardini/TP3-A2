using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ItemPedidoController : ControllerBase
{
    private static List<ItemPedido> itens = new();

    [HttpGet]
    public ActionResult<IEnumerable<ItemPedido>> Get() => itens;

    [HttpGet("{id}")]
    public ActionResult<ItemPedido> Get(int id)
    {
        var item = itens.FirstOrDefault(i => i.Id == id);
        return item is null ? NotFound() : item;
    }

    [HttpPost]
    public ActionResult<ItemPedido> Post(ItemPedido item)
    {
        item.Id = itens.Count > 0 ? itens.Max(i => i.Id) + 1 : 1;
        itens.Add(item);
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, ItemPedido item)
    {
        var existing = itens.FirstOrDefault(i => i.Id == id);
        if (existing is null) return NotFound();
        existing.PedidoId = item.PedidoId;
        existing.ProdutoId = item.ProdutoId;
        existing.Quantidade = item.Quantidade;
        existing.PrecoUnitario = item.PrecoUnitario;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var item = itens.FirstOrDefault(i => i.Id == id);
        if (item is null) return NotFound();
        itens.Remove(item);
        return NoContent();
    }
}