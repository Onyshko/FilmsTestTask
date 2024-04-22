using System.ComponentModel.DataAnnotations;

namespace FilmsTestTask.Services.Models
{
    public class FilmMvcModel : BaseMvcModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Director { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Release { get; set; }

        public string Categories { get; set; }

        public IList<FilmCategoryMvcModel> FilmCategories { get; set; } = new List<FilmCategoryMvcModel>();
    }
}
