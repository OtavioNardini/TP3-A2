using System.ComponentModel.DataAnnotations;

namespace PlataformaB2B_A2_TP3.Models
{
    public class TerritorialIBGE
    {
        [Key]
        public string CodigoIbge { get; set; }
        public string Uf { get; set; }
        public string Municipio { get; set; }
        public string Regiao { get; set; }
    }
}
