using System.ComponentModel.DataAnnotations;

namespace FilmsTestTask.Services.Models
{
    public class CategoryMvcModel : BaseMvcModel
    {
        [Required]

        public string Name { get; set; }

        public int? ParentCategoryId { get; set; }

        public int AmountOfFilms { get; set; }

        public int Layer { get; set; }
    }
}
