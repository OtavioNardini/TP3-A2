namespace RestauranteAPP_TP3.Models
{
    public class Endereco
    {
        public int Id { get; set; }

        // FK should remain non-nullable (we set it server-side)
        public required string UsuarioId { get; set; }

        // navigation should be nullable so model validation won't require it
        public Usuario? Usuario { get; set; }

        public string Rua { get; set; }
        public string Cidade { get; set; }
        public string CEP { get; set; }
    }
}
