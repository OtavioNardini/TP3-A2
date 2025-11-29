using System.ComponentModel.DataAnnotations;

namespace PlataformaB2B_A2_TP3.DTOs
{
    // Fornecedor DTOs
    public class FornecedorCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string RazaoSocial { get; set; }
        
        [Required]
        [StringLength(14, MinimumLength = 14)]
        public string Cnpj { get; set; }
        
        [MaxLength(50)]
        public string StatusKYC { get; set; }
        
        public string Certificacoes { get; set; }
    }

    public class FornecedorUpdateDto
    {
        [MaxLength(200)]
        public string RazaoSocial { get; set; }
        
        [MaxLength(50)]
        public string StatusKYC { get; set; }
        
        public string Certificacoes { get; set; }
    }

    public class FornecedorReadDto
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string StatusKYC { get; set; }
        public string Certificacoes { get; set; }
    }
}
