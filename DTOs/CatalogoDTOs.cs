using System.ComponentModel.DataAnnotations;

namespace PlataformaB2B_A2_TP3.DTOs
{
    // Catalogo DTOs
    public class CatalogoCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string Nome { get; set; }
        
        [Required]
        public int OrganizacaoId { get; set; }
    }

    public class CatalogoUpdateDto
    {
        [MaxLength(200)]
        public string Nome { get; set; }
    }

    public class CatalogoReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int OrganizacaoId { get; set; }
        public string OrganizacaoNome { get; set; }
    }

    // ItemCatalogo DTOs
    public class ItemCatalogoCreateDto
    {
        [Required]
        public int CatalogoId { get; set; }
        
        [Required]
        public int ProdutoId { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Preco { get; set; }
        
        public bool Disponivel { get; set; } = true;
    }

    public class ItemCatalogoUpdateDto
    {
        [Range(0.01, double.MaxValue)]
        public decimal? Preco { get; set; }
        
        public bool? Disponivel { get; set; }
    }

    public class ItemCatalogoReadDto
    {
        public int Id { get; set; }
        public int CatalogoId { get; set; }
        public int ProdutoId { get; set; }
        public string ProdutoDescricao { get; set; }
        public decimal Preco { get; set; }
        public bool Disponivel { get; set; }
    }
}
