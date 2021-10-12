using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETCOREFULL.Entity.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Category Name can not be empty!")]
        public string Name { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
