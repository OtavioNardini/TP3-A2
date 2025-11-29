using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaB2B_A2_TP3.Models
{
    public class DocumentoFornecedor
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public byte[] Arquivo { get; set; } // Blob mapped to byte[]
        
        public int FornecedorId { get; set; }
        [ForeignKey("FornecedorId")]
        public Fornecedor Fornecedor { get; set; }
    }
}
