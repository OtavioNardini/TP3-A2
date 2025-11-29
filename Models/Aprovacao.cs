public class Aprovacao
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public int AprovadorId { get; set; }
    public string Status { get; set; }
    public string Comentario { get; set; }
    public DateTime Data { get; set; }
}