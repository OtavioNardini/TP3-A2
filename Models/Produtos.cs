namespace RestauranteAPP_TP3.Models
{
    using System.Collections.Generic;

    public class Produtos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal PrecoBase { get; set; }


        // relacionamento N-N com Pedido via PedidoItem
        public ICollection<PedidoItem> PedidoItens { get; set; } = new List<PedidoItem>();
    }
}
