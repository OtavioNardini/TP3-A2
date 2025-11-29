using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaB2B_A2_TP3.Models
{
    public class RegraAprovacao
    {
        public int Id { get; set; }
        public decimal LimiteValor { get; set; }
        public string AprovadorNecessario { get; set; }

        public int OrgId { get; set; }
        [ForeignKey("OrgId")]
        public Organizacao Organizacao { get; set; }

        public int CentroCustoId { get; set; }
        [ForeignKey("CentroCustoId")]
        public CentroCusto CentroCusto { get; set; }
    }
}
