using DesafioTransferencia.Models;
using Microsoft.EntityFrameworkCore;

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
            base.OnModelCreating(modelBuilder);
        }

    }
}
