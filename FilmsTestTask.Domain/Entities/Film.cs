using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmsTestTask.Domain.Entities
{

    [Table("films")]
    public class Film : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Director { get; set; }

        public DateTime Release { get; set; }

        public ICollection<FilmCategory> FilmCategories { get; set; }
    }
}
