using System.ComponentModel.DataAnnotations;

namespace PlataformaB2B_A2_TP3.DTOs
{
    // Aprovacao DTOs
    public class AprovacaoCreateDto
    {
        [Required]
        public int PedidoId { get; set; }
        
        [Required]
        public int AprovadorId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Status { get; set; }
        
        [MaxLength(500)]
        public string Comentario { get; set; }
    }

    public class AprovacaoUpdateDto
    {
        [MaxLength(50)]
        public string Status { get; set; }
        
        [MaxLength(500)]
        public string Comentario { get; set; }
    }

    public class AprovacaoReadDto
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public int AprovadorId { get; set; }
        public string AprovadorNome { get; set; }
        public string Status { get; set; }
        public string Comentario { get; set; }
        public DateTime Data { get; set; }
    }
}
