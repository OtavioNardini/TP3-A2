using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaB2B_A2_TP3.Domain.Entities
{
    public class ItemPedido
    {
        // Composite key usually, but let's see UML.
        // UML: pedido_id, item_catalogo_id, quantidade, preco_unitario.
        // Usually EF Core needs a key. We can use Composite Key in Context or add an Id.
        // UML doesn't show "Id" for ItemPedido. I will use composite key in Context or Shadow Property.
        // But for simplicity in class, I'll just properties.
        
        public int PedidoId { get; set; }
        [ForeignKey("PedidoId")]
        public Pedido Pedido { get; set; }

        public int ItemCatalogoId { get; set; }
        [ForeignKey("ItemCatalogoId")]
        public ItemCatalogo ItemCatalogo { get; set; }

        public decimal Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
