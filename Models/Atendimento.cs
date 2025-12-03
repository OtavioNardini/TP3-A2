namespace RestauranteAPP_TP3.Models
{
    // Models/Atendimento (abstract) + subclasses
    public abstract class Atendimento
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
    }

}
