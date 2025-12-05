namespace RestauranteAPP_TP3.Models
{
    using System;
    using System.Collections.Generic;

    public enum PedidoStatus
    {
        EmAnalise = 0,
        Aprovado = 1,
        Reprovado = 2
    }

    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }

        public required string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        // Itens (N-N via PedidoItem)
        public ICollection<PedidoItem> PedidoItens { get; set; } = new List<PedidoItem>();

        public decimal ValorTotal { get; set; } // armazenado ou calculado - pode recalcular ao carregar

        // New status property. Default is EmAnalise.
        public PedidoStatus Status { get; set; } = PedidoStatus.EmAnalise;
    }

}
