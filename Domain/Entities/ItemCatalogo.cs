using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaB2B_A2_TP3.Domain.Entities
{
    public class ItemCatalogo
    {
        public int Id { get; set; }
        public decimal Preco { get; set; }
        public bool Disponivel { get; set; }

        public int CatalogoId { get; set; }
        [ForeignKey("CatalogoId")]
        public Catalogo Catalogo { get; set; }

        // UML says produto_id: Integer. But ProdutoServico has SKU (String) as key? 
        // UML says "ProdutoServico" has "sku: String". 
        // UML says "ItemCatalogo" has "produto_id: Integer". 
        // This is a mismatch. I will assume ProdutoServico SHOULD have an ID or ItemCatalogo refers to SKU.
        // Given "produto_id: Integer" in UML for ItemCatalogo, but "sku: String" in ProdutoServico.
        // I'll add an ID to ProdutoServico to be safe and consistent with "Integer" FKs, OR change FK to String.
        // Let's check ProdutoServico again. It has SKU.
        // I will modify ProdutoServico to have an ID as well, or change ItemCatalogo to use SKU.
        // To be safest and standard, I'll add Id to ProdutoServico.
        
        public int ProdutoId { get; set; }
        [ForeignKey("ProdutoId")]
        public ProdutoServico Produto { get; set; }
    }
}
