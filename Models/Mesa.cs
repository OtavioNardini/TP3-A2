namespace RestauranteAPP_TP3.Models
{
    // Models/Mesa.cs
    public class Mesa
    {
        public int Id { get; set; }
        public string Numero { get; set; }    // ou int Numero
        public int Capacidade { get; set; }

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }

}
