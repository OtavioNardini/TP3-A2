using System.Collections.Generic;

namespace PlataformaB2B_A2_TP3.Domain.Entities
{
    public class Organizacao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }

        // Relationships
        public ICollection<Usuario> Usuarios { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }
        public ICollection<Catalogo> Catalogos { get; set; }
        public ICollection<Endereco> Enderecos { get; set; }
        public ICollection<RegraAprovacao> RegrasAprovacao { get; set; }
        public ICollection<CentroCusto> CentrosCusto { get; set; }
    }
}
