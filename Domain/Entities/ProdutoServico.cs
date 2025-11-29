using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaB2B_A2_TP3.Domain.Entities
{
    public class ProdutoServico
    {
        public int Id { get; set; } // Added ID to match ItemCatalogo FK requirement
        public string Sku { get; set; }
        public string Descricao { get; set; }
        public string Unidade { get; set; }
        public string Categoria { get; set; }
        
        public int FornecedorId { get; set; }
        [ForeignKey("FornecedorId")]
        public Fornecedor Fornecedor { get; set; }
    }
}
