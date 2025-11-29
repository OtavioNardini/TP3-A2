using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaB2B_A2_TP3.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        [ForeignKey("PedidoId")]
        public Pedido Pedido { get; set; }

        ProdutoServico ProdutoServico { get; set; }

        public int ItemCatalogoId { get; set; }
        [ForeignKey("ItemCatalogoId")]
        public ItemCatalogo ItemCatalogo { get; set; }

        public decimal Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
