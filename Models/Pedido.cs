using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaB2B_A2_TP3.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public DateTime DataCriacao { get; set; }

        public int CompradorId { get; set; }
        [ForeignKey("CompradorId")]
        public Usuario Comprador { get; set; }

        public int OrgId { get; set; }
        [ForeignKey("OrgId")]
        public Organizacao Organizacao { get; set; }

        public int? EnderecoEntregaId { get; set; }
        [ForeignKey("EnderecoEntregaId")]
        public Endereco EnderecoEntrega { get; set; }
        
        // Relationship to CentroCusto (1..* in UML: Pedido "*" -- "1" CentroCusto)
        public int? CentroCustoId { get; set; }
        [ForeignKey("CentroCustoId")]
        public CentroCusto CentroCusto { get; set; }

        public ICollection<ItemPedido> Itens { get; set; }
        public ICollection<Aprovacao> Aprovacoes { get; set; }
        public ICollection<LancamentoFinanceiro> Lancamentos { get; set; }
        public ICollection<PropostaFornecedor> Propostas { get; set; }
    }
}
