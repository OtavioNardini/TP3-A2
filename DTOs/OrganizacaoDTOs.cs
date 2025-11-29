using System.ComponentModel.DataAnnotations;

namespace PlataformaB2B_A2_TP3.DTOs
{
    // Organizacao DTOs
    public class OrganizacaoCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string Nome { get; set; }
        
        [Required]
        [StringLength(14, MinimumLength = 14)]
        public string Cnpj { get; set; }
    }

    public class OrganizacaoUpdateDto
    {
        [MaxLength(200)]
        public string Nome { get; set; }
        
        [StringLength(14, MinimumLength = 14)]
        public string Cnpj { get; set; }
    }

    public class OrganizacaoReadDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
    }
}
