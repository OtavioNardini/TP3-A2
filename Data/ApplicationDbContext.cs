namespace RestauranteAPP_TP3.Data
{
    // Data/ApplicationDbContext.cs
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RestauranteAPP_TP3.Models;

    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Endereco> Enderecos { get; set; }

        public DbSet<ItemCardapio> ItensCardapio { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItens { get; set; }

        public DbSet<Atendimento> Atendimentos { get; set; }
        public DbSet<AtendimentoPresencial> AtendimentoPresencial { get; set; }
        public DbSet<AtendimentoDeliveryProprio> AtendimentoDeliveryProprio { get; set; }
        public DbSet<AtendimentoDeliveryAplicativo> AtendimentoDeliveryAplicativo { get; set; }

        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public object ItemCardapio { get; internal set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Endereco 1-N
            builder.Entity<Endereco>()
                .HasOne(e => e.Usuario)
                .WithMany(u => u.Enderecos)
                .HasForeignKey(e => e.UsuarioId);

            // PedidoItem
            builder.Entity<PedidoItem>()
                .HasOne(pi => pi.Pedido)
                .WithMany(p => p.PedidoItens)
                .HasForeignKey(pi => pi.PedidoId);

            builder.Entity<PedidoItem>()
                .HasOne(pi => pi.ItemCardapio)
                .WithMany(i => i.PedidoItens)
                .HasForeignKey(pi => pi.ItemCardapioId);

            // Atendimento: TPH (Table-per-hierarchy) com Discriminator
            builder.Entity<Atendimento>()
                .HasDiscriminator<string>("AtendimentoTipo")
                .HasValue<AtendimentoPresencial>("Presencial")
                .HasValue<AtendimentoDeliveryProprio>("DeliveryProprio")
                .HasValue<AtendimentoDeliveryAplicativo>("DeliveryAplicativo");

            // Atendimento 1-1 Pedido: garantir que Atendimento.PedidoId é único
            builder.Entity<Atendimento>()
                .HasOne(a => a.Pedido)
                .WithOne(p => p.Atendimento)
                .HasForeignKey<Atendimento>(a => a.PedidoId)
                .IsRequired();

            // Reserva: validações de FK
            builder.Entity<Reserva>()
                .HasOne(r => r.Usuario)
                .WithMany(u => u.Reservas)
                .HasForeignKey(r => r.UsuarioId);

            builder.Entity<Reserva>()
                .HasOne(r => r.Mesa)
                .WithMany(m => m.Reservas)
                .HasForeignKey(r => r.MesaId);

            // Mesa: número único opcional
            builder.Entity<Mesa>()
                .HasIndex(m => m.Numero)
                .IsUnique(false);
        }
    }

}
