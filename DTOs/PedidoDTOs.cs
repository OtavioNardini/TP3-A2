using System.ComponentModel.DataAnnotations;

namespace PlataformaB2B_A2_TP3.DTOs
{
    // Pedido DTOs
    public class PedidoCreateDto
    {
        [Required]
        public int CompradorId { get; set; }
        
        [Required]
        public int OrgId { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Total { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Status { get; set; }
        
        public int? EnderecoEntregaId { get; set; }
        public int? CentroCustoId { get; set; }
    }

    public class PedidoUpdateDto
    {
        [Range(0.01, double.MaxValue)]
        public decimal? Total { get; set; }
        
        [MaxLength(50)]
        public string Status { get; set; }
        
        public int? EnderecoEntregaId { get; set; }
        public int? CentroCustoId { get; set; }
    }

    public class PedidoReadDto
    {
        public int Id { get; set; }
        public int CompradorId { get; set; }
        public string CompradorNome { get; set; }
        public int OrgId { get; set; }
        public string OrganizacaoNome { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public int? EnderecoEntregaId { get; set; }
        public int? CentroCustoId { get; set; }
    }
}
