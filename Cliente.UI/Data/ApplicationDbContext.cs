using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Cliente.UI.Models;

namespace Cliente.UI.Data
{
    public class ApplicationDbContext : IdentityDbContext<Models.Cliente>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Cliente.UI.Models.Endereco> Endereco { get; set; }

        public DbSet<Cliente.UI.Models.Estado> Estado { get; set; }

        public DbSet<Cliente.UI.Models.Cidade> Cidade { get; set; }

        public DbSet<Cliente.UI.Models.FormaPagamento> FormaPagamento { get; set; }

        public DbSet<Cliente.UI.Models.Marca> Marca { get; set; }

        public DbSet<Cliente.UI.Models.Cliente> Cliente { get; set; }

        public DbSet<Cliente.UI.Models.Produto> Produto { get; set; }
<<<<<<< HEAD

        public DbSet<Cliente.UI.Models.TipoPagamento> TipoPagamento { get; set; }

        public DbSet<Cliente.UI.Models.NotaFiscal> NotaFiscal { get; set; }

        public DbSet<Cliente.UI.Models.ItemNota> ItemNota { get; set; }
=======
>>>>>>> 137e93d17f0a904fa47d83cc4226007cabbd58b3
    }
}
