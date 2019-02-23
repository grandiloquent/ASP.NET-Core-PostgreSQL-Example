using System;
using Microsoft.EntityFrameworkCore;

namespace WebApplication.Models
{
    public class EfDatabaseContext : DbContext

    {
        public EfDatabaseContext(DbContextOptions<EfDatabaseContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("OnModelCreating");
            modelBuilder.Entity<Album>()
                .HasIndex(a => a.Title)
                .IsUnique();
        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Album> Albums { get; set; }
    }
}