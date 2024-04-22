using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmsTestTask.Domain.Entities
{
    [Table("categories")]
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [ForeignKey("ParentCategory")]
        public int? ParentCategoryId { get; set; }

        public Category ParentCategory { get; set; }

        public ICollection<FilmCategory> FilmCategories { get; set; }
    }
}
