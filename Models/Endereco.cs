namespace RestauranteAPP_TP3.Models
{
    // Models/Endereco.cs
    public class Endereco
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public string Rua { get; set; }
        public string Cidade { get; set; }
        public string CEP { get; set; }
        // complemento etc se desejar
    }

}
