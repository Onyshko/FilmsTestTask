using System.ComponentModel.DataAnnotations.Schema;

namespace FilmsTestTask.Domain.Entities
{

    [Table("film_categories")]
    public class FilmCategory : BaseEntity
    {

        [ForeignKey("Film")]
        public int? FilmId { get; set; }
        public Film Film { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
