namespace RestauranteAPP_TP3.Models
{
    using System.Collections.Generic;

    public class ItemCardapio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal PrecoBase { get; set; }
        public PeriodoCardapio Periodo { get; set; }

        // Indica se o item é sugestão do chefe
        public bool SugestaoDoChefe { get; set; } = false;

        // relacionamento N-N com Pedido via PedidoItem
        public ICollection<PedidoItem> PedidoItens { get; set; } = new List<PedidoItem>();
    }
}
