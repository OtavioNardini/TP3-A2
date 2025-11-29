using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaB2B_A2_TP3.Domain.Entities
{
    public class LogAuditoria
    {
        public int Id { get; set; }
        public string Entidade { get; set; }
        public string Acao { get; set; }
        public DateTime Timestamp { get; set; }
        public string Delta { get; set; } // JSON stored as string

        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
    }
}
