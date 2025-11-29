using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaB2B_A2_TP3.Domain.Entities
{
    public class Notificacao
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public bool Lida { get; set; }
        public string Payload { get; set; } // JSON stored as string

        public int DestinatarioId { get; set; }
        [ForeignKey("DestinatarioId")]
        public Usuario Destinatario { get; set; }
    }
}
