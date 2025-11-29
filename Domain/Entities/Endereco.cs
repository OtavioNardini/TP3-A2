using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaB2B_A2_TP3.Domain.Entities
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public int MunicipioId { get; set; }
        public string Uf { get; set; }

        // Relationships
        public int? FornecedorId { get; set; }
        [ForeignKey("FornecedorId")]
        public Fornecedor Fornecedor { get; set; }

        public int? OrganizacaoId { get; set; }
        [ForeignKey("OrganizacaoId")]
        public Organizacao Organizacao { get; set; }
    }
}
