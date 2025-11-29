using System.ComponentModel.DataAnnotations;

namespace PlataformaB2B_A2_TP3.DTOs
{
    public class NotificacaoCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Tipo { get; set; }
        
        [Required]
        public int DestinatarioId { get; set; }
        
        [Required]
        public string Payload { get; set; }
    }

    public class NotificacaoUpdateDto
    {
        public bool? Lida { get; set; }
    }

    public class NotificacaoReadDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public int DestinatarioId { get; set; }
        public string DestinatarioNome { get; set; }
        public bool Lida { get; set; }
        public string Payload { get; set; }
    }
}
