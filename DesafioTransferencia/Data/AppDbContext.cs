using DesafioTransferencia.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DesafioTransferencia.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<TransferModel> Transfers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Isso aplica todas as configurações de mapeamento do assembly atual

            base.OnModelCreating(modelBuilder);
        }

    }
}
