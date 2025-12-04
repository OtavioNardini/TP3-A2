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

        public DbSet<Produtos> ItensCardapio { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItens { get; set; }

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
                .HasOne(pi => pi.Produtos)
                .WithMany(i => i.PedidoItens)
                .HasForeignKey(pi => pi.ItemCardapioId);

        }
    }

}
