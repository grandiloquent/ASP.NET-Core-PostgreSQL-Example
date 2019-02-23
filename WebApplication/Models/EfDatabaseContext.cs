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
            modelBuilder.Entity<Tag>()
                .HasIndex(a => a.Name)
                .IsUnique();
            modelBuilder.Entity<VideoTag>()
                .HasKey(t => new {t.VideoId, t.TagId});

            modelBuilder.Entity<Album>()
                .HasMany(c => c.Videos)
                .WithOne(e => e.Album)
                .IsRequired();
        }

        public DbSet<Video> Videos { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}