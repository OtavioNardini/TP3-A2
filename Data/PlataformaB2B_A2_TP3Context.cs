using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlataformaB2B_A2_TP3.Models;

namespace PlataformaB2B_A2_TP3.Data
{
    public class PlataformaB2B_A2_TP3Context : IdentityDbContext<Usuario, Role, int>
    {
        public PlataformaB2B_A2_TP3Context(DbContextOptions<PlataformaB2B_A2_TP3Context> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; } // IdentityDbContext already has Users, but we can expose it as Usuarios if we want, or just use Users. Let's keep it standard Users usually, but for UML match we can add alias or just use standard.
        // Actually, IdentityDbContext uses "Users" and "Roles". 
        // We can add additional DbSets for our specific entities.

        public DbSet<Organizacao> Organizacoes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<DocumentoFornecedor> DocumentosFornecedor { get; set; }
        public DbSet<ProdutoServico> ProdutosServicos { get; set; }
        public DbSet<Catalogo> Catalogos { get; set; }
        public DbSet<ItemCatalogo> ItensCatalogo { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Aprovacao> Aprovacoes { get; set; }
        public DbSet<CentroCusto> CentrosCusto { get; set; }
        public DbSet<LancamentoFinanceiro> LancamentosFinanceiros { get; set; }
        public DbSet<PropostaFornecedor> PropostasFornecedor { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }
        public DbSet<LogAuditoria> LogsAuditoria { get; set; }
        public DbSet<CertificadoCompliance> CertificadosCompliance { get; set; }
        public DbSet<RegraAprovacao> RegrasAprovacao { get; set; }
        public DbSet<TerritorialIBGE> TerritoriaisIBGE { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            // ItemPedido Composite Key (if we didn't add an Id)
            builder.Entity<ItemPedido>()
                .HasKey(ip => new { ip.PedidoId, ip.ItemCatalogoId });

            // Decimal precision configuration (important for financial values)
            builder.Entity<ItemCatalogo>().Property(p => p.Preco).HasColumnType("decimal(18,2)");
            builder.Entity<Pedido>().Property(p => p.Total).HasColumnType("decimal(18,2)");
            builder.Entity<ItemPedido>().Property(p => p.Quantidade).HasColumnType("decimal(18,2)"); // Quantity might be decimal
            builder.Entity<ItemPedido>().Property(p => p.PrecoUnitario).HasColumnType("decimal(18,2)");
            builder.Entity<LancamentoFinanceiro>().Property(p => p.Valor).HasColumnType("decimal(18,2)");
            builder.Entity<PropostaFornecedor>().Property(p => p.PrecoTotal).HasColumnType("decimal(18,2)");
            builder.Entity<RegraAprovacao>().Property(p => p.LimiteValor).HasColumnType("decimal(18,2)");

            // Relationships configuration if needed beyond conventions
        }
    }
}
