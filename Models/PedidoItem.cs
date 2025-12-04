namespace RestauranteAPP_TP3.Models
{
    // Models/PedidoItem.cs (join Pedido <-> ItemCardapio)
    public class PedidoItem
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        public int ItemCardapioId { get; set; }
        public Produtos Produtos { get; set; }

        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; } // snapshot do preço no momento do pedido
    }

}
