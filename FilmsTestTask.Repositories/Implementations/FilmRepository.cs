using FilmsTestTask.Domain.Entities;
using FilmsTestTask.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace FilmsTestTask.Repositories.Implementations
{
    public class FilmRepository : BaseRepository<Film>
    {
        public FilmRepository(AppDbContext context) : base(context)
        {
        }

        protected override void UpdateRelatedEntities(Film existingEntity, Film newEntity)
        {
            UpdateFilmCategory(existingEntity, newEntity);
        }

        public override Task<Film> GetAsync(int id)
        {
            return GetAsync(id, x => x.FilmCategories);
        }

        public override Task<List<Film>> GetAllAsync()
        {
            return GetAllAsync(x => x.FilmCategories);
        }
        private void UpdateFilmCategory(Film existingEntity, Film newEntity)
        {
            var toDeleteFilmCategories = existingEntity.FilmCategories.Where(x => !newEntity.FilmCategories.Any(y => y.Id == x.Id)).ToList();
            foreach (var degree in toDeleteFilmCategories)
            {
                existingEntity.FilmCategories.Remove(degree);
                var a = _context.Entry(degree).State;
            }

            var toAddFilmCategories = newEntity.FilmCategories.Where(x => !existingEntity.FilmCategories.Any(y => y.Id == x.Id)).ToList();
            foreach (var degree in toAddFilmCategories)
            {
                existingEntity.FilmCategories.Add(degree);
            }
        }
    }
}
