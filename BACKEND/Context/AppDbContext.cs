using Microsoft.EntityFrameworkCore;
using Wallet_Crypto.Models;

namespace Wallet_Crypto.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Transaction> transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .Property(t => t.CryptoAmount)
                .HasPrecision(18, 8); 

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Money)
                .HasPrecision(18, 2); 
        }
    }
}
