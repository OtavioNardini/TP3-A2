using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaB2B_A2_TP3.Models
{
    public class LancamentoFinanceiro
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }

        public int PedidoId { get; set; }
        [ForeignKey("PedidoId")]
        public Pedido Pedido { get; set; }
    }
}
