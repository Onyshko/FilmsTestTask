using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsTestTask.APIServices.Models
{
    public class CategoryApiModel : BaseApiModel
    {
        public string Name { get; set; }

        public int? ParentCategoryId { get; set; }
    }
}
