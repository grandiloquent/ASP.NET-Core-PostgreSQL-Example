using Microsoft.EntityFrameworkCore;

namespace WebApplication.Models
{
    public class EfDatabaseContext : DbContext

    {
        public EfDatabaseContext(DbContextOptions<EfDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Video> Videos { get; set; }
        
    }
}