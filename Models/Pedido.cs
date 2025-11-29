public class Pedido
{
    public int Id { get; set; }
    public int CompradorId { get; set; }
    public int OrgId { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
    public DateTime DataCriacao { get; set; }
}