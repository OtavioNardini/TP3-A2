// Services/PedidoService.cs
using Microsoft.EntityFrameworkCore;
using RestauranteAPP_TP3.Data;
using RestauranteAPP_TP3.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RestauranteAPP_TP3.Services
{
    public class PedidoService
    {
        private readonly ApplicationDbContext _db;

        public PedidoService(ApplicationDbContext db) => _db = db;

        public async Task<decimal> CalcularValorFinalAsync(Pedido pedido)
        {
            // carregar itens e atendimento
            await _db.Entry(pedido).Collection(p => p.PedidoItens).LoadAsync();
            foreach (var pi in pedido.PedidoItens)
                await _db.Entry(pi).Reference(p => p.ItemCardapio).LoadAsync();

            decimal subtotal = 0m;

            var hoje = DateTime.Today;

            var sugestoes = await _db.ItensCardapio
                .ToListAsync();

            foreach (var item in pedido.PedidoItens)
            {
                decimal unit = item.PrecoUnitario;
                var ic = item.ItemCardapio;
                subtotal += unit * item.Quantidade;
            }

            decimal total = subtotal; 
            pedido.ValorTotal = total;
            return total;
        }
    }

}
