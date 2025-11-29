using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PlataformaB2B_A2_TP3.Data
{
    public class PlataformaB2B_A2_TP3Context : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public PlataformaB2B_A2_TP3Context (DbContextOptions<PlataformaB2B_A2_TP3Context> options)
            : base(options)
        {
        }

        public DbSet<ProdutoServico> ProdutoServico { get; set; } = default!;
        public DbSet<Fornecedor> Fornecedor { get; set; } = default!;
        // Identity gerencia usuários/roles; remova DbSet<Usuario> ou migre os dados manualmente.
    }
}
