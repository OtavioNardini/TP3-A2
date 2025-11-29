using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaB2B_A2_TP3.Models
{
    public class CentroCusto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }

        public int OrgId { get; set; }
        [ForeignKey("OrgId")]
        public Organizacao Organizacao { get; set; }
    }
}
