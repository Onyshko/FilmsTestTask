namespace FilmsTestTask.Services.Models
{
    public class FilmCategoryMvcModel : BaseMvcModel
    {
        public int FilmId { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
