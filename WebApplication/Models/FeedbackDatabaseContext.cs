using Microsoft.EntityFrameworkCore;

namespace WebApplication.Models
{
    public class FeedbackDatabaseContext : DbContext
    {
        public FeedbackDatabaseContext(DbContextOptions<EfDatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Feedback> Feedbacks { get; set; }
    }
}