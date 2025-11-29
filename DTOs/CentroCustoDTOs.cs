using System.ComponentModel.DataAnnotations;

namespace PlataformaB2B_A2_TP3.DTOs
{
    public class CentroCustoCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Codigo { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Descricao { get; set; }
        
        [Required]
        public int OrgId { get; set; }
    }

    public class CentroCustoUpdateDto
    {
        [MaxLength(50)]
        public string Codigo { get; set; }
        
        [MaxLength(200)]
        public string Descricao { get; set; }
    }

    public class CentroCustoReadDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int OrgId { get; set; }
        public string OrganizacaoNome { get; set; }
    }
}
