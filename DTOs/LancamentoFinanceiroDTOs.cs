using System.ComponentModel.DataAnnotations;

namespace PlataformaB2B_A2_TP3.DTOs
{
    public class LancamentoFinanceiroCreateDto
    {
        [Required]
        public int PedidoId { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Valor { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Tipo { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Status { get; set; }
    }

    public class LancamentoFinanceiroUpdateDto
    {
        [MaxLength(50)]
        public string Status { get; set; }
    }

    public class LancamentoFinanceiroReadDto
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public decimal Valor { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
    }
}
