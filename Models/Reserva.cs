namespace RestauranteAPP_TP3.Models
{
    // Models/Reserva.cs
    using System;

    public class Reserva
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int MesaId { get; set; }
        public Mesa Mesa { get; set; }

        public DateTime DataHora { get; set; }
        public string CodigoConfirmacao { get; set; }
    }

}
