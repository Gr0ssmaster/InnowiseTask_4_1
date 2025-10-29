using AuthorsWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorsWebAPI.Data
{
    public class AuthorApiContext : DbContext
    {
        public AuthorApiContext(DbContextOptions<AuthorApiContext> options) : base(options) { }

        public DbSet<Authors> Authors { get; set; }
        public DbSet<Books> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authors>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Authors>().HasData(
                new Authors { Id = 1, Name = "Lewis Carrol", DateOfBirth = new DateTime(2000, 6, 25) }
            );

            modelBuilder.Entity<Books>().HasData(
                new Books { Id = 1, Title = "Alice in WonderLand", PublishedYear = 200, AuthorId = 1 }
            );
        }
    }
}
