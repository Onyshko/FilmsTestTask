using FilmsTestTask.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FilmsTestTask.API.Models
{
    public class FilmApiVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Director { get; set; }

        public DateTime Release { get; set; }

        public ICollection<int> FilmCategories { get; set; }
    }
}
