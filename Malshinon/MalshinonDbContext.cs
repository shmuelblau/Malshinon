using Microsoft.EntityFrameworkCore;

namespace Malshinon
{
    

    public class MalshinonDbContext : DbContext
    {
        public MalshinonDbContext(DbContextOptions<MalshinonDbContext> options) : base(options) { }

        
        public DbSet<People> People { get; set; }
        public DbSet<IntelReport> intelReport { get; set; }
    }
}