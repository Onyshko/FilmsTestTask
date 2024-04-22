using FilmsTestTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmsTestTask.Repositories.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Film> Films { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<FilmCategory> FilmCategories { get; set; }
    }
}
