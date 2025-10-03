namespace RestauranteAPP_TP3.Models
{
    // Models/Atendimento (abstract) + subclasses
    public abstract class Atendimento
    {
        public int Id { get; set; }
        public string Tipo { get; set; } // opcional: discriminator
                                         // FK 1-1 para Pedido (um atendimento está associado a 1 pedido)
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
    }

    // Presencial
    public class AtendimentoPresencial : Atendimento
    {
        // campos específicos se houver
    }

    // Delivery próprio (taxa fixa)
    public class AtendimentoDeliveryProprio : Atendimento
    {
        public decimal TaxaFixa { get; set; }
    }

    // Delivery por aplicativo (parceiro)
    public class AtendimentoDeliveryAplicativo : Atendimento
    {
        public string NomeParceiro { get; set; }
        public decimal TaxaParceiro { get; set; } // ou comissão
    }

}
