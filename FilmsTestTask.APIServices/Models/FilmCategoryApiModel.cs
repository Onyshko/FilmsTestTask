using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsTestTask.APIServices.Models
{
    public class FilmCategoryApiModel : BaseApiModel
    {
        public int? FilmId { get; set; }

        public int? CategoryId { get; set; }
    }
}
