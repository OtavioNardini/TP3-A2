using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AprovacaoController : ControllerBase
{
    private static List<Aprovacao> aprovacoes = new();

    [HttpGet]
    public ActionResult<IEnumerable<Aprovacao>> Get() => aprovacoes;

    [HttpGet("{id}")]
    public ActionResult<Aprovacao> Get(int id)
    {
        var aprovacao = aprovacoes.FirstOrDefault(a => a.Id == id);
        return aprovacao is null ? NotFound() : aprovacao;
    }

    [HttpPost]
    public ActionResult<Aprovacao> Post(Aprovacao aprovacao)
    {
        aprovacao.Id = aprovacoes.Count > 0 ? aprovacoes.Max(a => a.Id) + 1 : 1;
        aprovacoes.Add(aprovacao);
        return CreatedAtAction(nameof(Get), new { id = aprovacao.Id }, aprovacao);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Aprovacao aprovacao)
    {
        var existing = aprovacoes.FirstOrDefault(a => a.Id == id);
        if (existing is null) return NotFound();
        existing.PedidoId = aprovacao.PedidoId;
        existing.AprovadorId = aprovacao.AprovadorId;
        existing.Status = aprovacao.Status;
        existing.Comentario = aprovacao.Comentario;
        existing.Data = aprovacao.Data;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var aprovacao = aprovacoes.FirstOrDefault(a => a.Id == id);
        if (aprovacao is null) return NotFound();
        aprovacoes.Remove(aprovacao);
        return NoContent();
    }
}