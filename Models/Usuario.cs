namespace RestauranteAPP_TP3.Models
{
    // Models/Usuario.cs
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class Usuario : IdentityUser
    {
        // Identity já fornece Id, Email, PasswordHash, etc.
        public required string Nome { get; set; }

        // Navegações
        public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>(); // 1-N
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }

}
