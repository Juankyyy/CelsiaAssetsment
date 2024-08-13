using Microsoft.EntityFrameworkCore;

namespace CelsiaAssetsment.Data
{
    public class CelsiaAssetsmentContext : DbContext
    {
        public CelsiaAssetsmentContext(DbContextOptions<CelsiaAssetsmentContext> options) : base(options) {}
    }
}