using System.ComponentModel.DataAnnotations;

namespace FilmsTestTask.APIServices.Models
{
    public class FilmApiModel : BaseApiModel
    {
        public string Name { get; set; }

        public string Director { get; set; }

        public DateTime Release { get; set; }

        public IList<FilmCategoryApiModel> FilmCategories { get; set; }
    }
}
