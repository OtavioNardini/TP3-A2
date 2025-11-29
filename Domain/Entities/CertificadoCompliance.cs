using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaB2B_A2_TP3.Domain.Entities
{
    public class CertificadoCompliance
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public DateTime Validade { get; set; }

        public int FornecedorId { get; set; }
        [ForeignKey("FornecedorId")]
        public Fornecedor Fornecedor { get; set; }
    }
}
