using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaB2B_A2_TP3.Models
{
    public class Aprovacao
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Comentario { get; set; }
        public DateTime Data { get; set; }

        public int PedidoId { get; set; }
        [ForeignKey("PedidoId")]
        public Pedido Pedido { get; set; }

        public int AprovadorId { get; set; }
        [ForeignKey("AprovadorId")]
        public Usuario Aprovador { get; set; }
    }
}
