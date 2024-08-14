using Microsoft.EntityFrameworkCore;
using CelsiaAssetsment.Models;

namespace CelsiaAssetsment.Data
{
    public class CelsiaAssetsmentContext : DbContext
    {
        public CelsiaAssetsmentContext(DbContextOptions<CelsiaAssetsmentContext> options) : base(options) {}

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}