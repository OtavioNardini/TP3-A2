using System.ComponentModel.DataAnnotations;

namespace PlataformaB2B_A2_TP3.DTOs
{
    public class EnderecoCreateDto
    {
        [Required]
        [MaxLength(8)]
        public string Cep { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Logradouro { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Numero { get; set; }
        
        [MaxLength(100)]
        public string Complemento { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Bairro { get; set; }
        
        [Required]
        public int MunicipioId { get; set; }
        
        [Required]
        [StringLength(2)]
        public string Uf { get; set; }
        
        public int? FornecedorId { get; set; }
        public int? OrganizacaoId { get; set; }
    }

    public class EnderecoUpdateDto
    {
        [MaxLength(8)]
        public string Cep { get; set; }
        
        [MaxLength(200)]
        public string Logradouro { get; set; }
        
        [MaxLength(20)]
        public string Numero { get; set; }
        
        [MaxLength(100)]
        public string Complemento { get; set; }
        
        [MaxLength(100)]
        public string Bairro { get; set; }
        
        [StringLength(2)]
        public string Uf { get; set; }
    }

    public class EnderecoReadDto
    {
        public int Id { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public int MunicipioId { get; set; }
        public string Uf { get; set; }
        public int? FornecedorId { get; set; }
        public int? OrganizacaoId { get; set; }
    }
}
