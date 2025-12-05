using System.Collections.Generic;

namespace RestauranteAPP_TP3.Models
{
    public class ProfileViewModel
    {
        public Usuario User { get; set; } = null!;
        public List<Endereco> Enderecos { get; set; } = new();
    }
}