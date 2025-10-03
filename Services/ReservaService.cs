namespace RestauranteAPP_TP3.Services
{
    // Services/ReservaService.cs
    using Microsoft.EntityFrameworkCore;
    using RestauranteAPP_TP3.Data;
    using RestauranteAPP_TP3.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReservaService
    {
        private readonly ApplicationDbContext _db;
        // janela exemplo: 19:00 - 22:00
        private readonly TimeSpan inicio = new TimeSpan(19, 0, 0);
        private readonly TimeSpan fim = new TimeSpan(22, 0, 0);

        public ReservaService(ApplicationDbContext db) => _db = db;

        public async Task<bool> ValidarHorarioReservaAsync(DateTime dataHora)
        {
            var hora = dataHora.TimeOfDay;
            return hora >= inicio && hora <= fim;
        }

        public async Task<bool> MesaDisponivelAsync(int mesaId, DateTime dataHora)
        {
            // simplificação: evita reservas com mesma dataHora exata
            var conflito = await _db.Reservas.AnyAsync(r => r.MesaId == mesaId && r.DataHora == dataHora);
            return !conflito;
        }

        // método para criar reserva
        public async Task<Reserva> CriarReservaAsync(string usuarioId, int mesaId, DateTime dataHora)
        {
            if (!await ValidarHorarioReservaAsync(dataHora))
                throw new InvalidOperationException("Hora fora da janela permitida para reservas.");

            if (!await MesaDisponivelAsync(mesaId, dataHora))
                throw new InvalidOperationException("Mesa já reservada neste horário.");

            var reserva = new Reserva
            {
                UsuarioId = usuarioId,
                MesaId = mesaId,
                DataHora = dataHora,
                CodigoConfirmacao = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()
            };

            _db.Reservas.Add(reserva);
            await _db.SaveChangesAsync();
            return reserva;
        }
    }

}
