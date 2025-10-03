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
        private readonly decimal descontoSugestaoPercent = 0.20m;

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
    .Where(s => s.SugestaoDoChefe == true && s.Periodo == PeriodoCardapio.Almoco)
    .ToListAsync();

            foreach (var item in pedido.PedidoItens)
            {
                decimal unit = item.PrecoUnitario;
                // verificar se item é sugestão do chefe no dia e no mesmo periodo
                var ic = item.ItemCardapio;
                var sugestao = sugestoes.FirstOrDefault(s => s.Id == ic.Id && s.Periodo == ic.Periodo);
                if (sugestao != null)
                {
                    unit = unit * (1 - descontoSugestaoPercent);
                }
                subtotal += unit * item.Quantidade;
            }

            // carregar atendimento
            await _db.Entry(pedido).Reference(p => p.Atendimento).LoadAsync();
            decimal taxa = 0m;
            if (pedido.Atendimento is AtendimentoDeliveryProprio proprio)
            {
                taxa = proprio.TaxaFixa;
            }
            else if (pedido.Atendimento is AtendimentoDeliveryAplicativo app)
            {
                taxa = app.TaxaParceiro;
            }
            // presencial taxa normalmente 0
            var total = subtotal + taxa;
            pedido.ValorTotal = total;
            return total;
        }
    }

}
