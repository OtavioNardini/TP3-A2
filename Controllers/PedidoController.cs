using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ControllerBase
{
    private static List<Pedido> pedidos = new();

    [HttpGet]
    public ActionResult<IEnumerable<Pedido>> Get() => pedidos;

    [HttpGet("{id}")]
    public ActionResult<Pedido> Get(int id)
    {
        var pedido = pedidos.FirstOrDefault(p => p.Id == id);
        return pedido is null ? NotFound() : pedido;
    }

    [HttpPost]
    public ActionResult<Pedido> Post(Pedido pedido)
    {
        pedido.Id = pedidos.Count > 0 ? pedidos.Max(p => p.Id) + 1 : 1;
        pedidos.Add(pedido);
        return CreatedAtAction(nameof(Get), new { id = pedido.Id }, pedido);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Pedido pedido)
    {
        var existing = pedidos.FirstOrDefault(p => p.Id == id);
        if (existing is null) return NotFound();
        existing.CompradorId = pedido.CompradorId;
        existing.OrgId = pedido.OrgId;
        existing.Total = pedido.Total;
        existing.Status = pedido.Status;
        existing.DataCriacao = pedido.DataCriacao;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pedido = pedidos.FirstOrDefault(p => p.Id == id);
        if (pedido is null) return NotFound();
        pedidos.Remove(pedido);
        return NoContent();
    }
}