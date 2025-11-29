using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaB2B_A2_TP3.Models
{
    public class Catalogo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        
        public int OrganizacaoId { get; set; }
        [ForeignKey("OrganizacaoId")]
        public Organizacao Organizacao { get; set; }

        public ICollection<ItemCatalogo> Itens { get; set; }
    }
}
