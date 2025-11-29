using System.ComponentModel.DataAnnotations;

namespace PlataformaB2B_A2_TP3.DTOs
{
    // ProdutoServico DTOs
    public class ProdutoServicoCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Sku { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Descricao { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Unidade { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Categoria { get; set; }
        
        [Required]
        public int FornecedorId { get; set; }
    }

    public class ProdutoServicoUpdateDto
    {
        [MaxLength(100)]
        public string Sku { get; set; }
        
        [MaxLength(500)]
        public string Descricao { get; set; }
        
        [MaxLength(50)]
        public string Unidade { get; set; }
        
        [MaxLength(100)]
        public string Categoria { get; set; }
    }

    public class ProdutoServicoReadDto
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Descricao { get; set; }
        public string Unidade { get; set; }
        public string Categoria { get; set; }
        public int FornecedorId { get; set; }
        public string FornecedorNome { get; set; }
    }
}
