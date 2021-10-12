using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETCOREFULL.Entity.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Product Name can not be empty!")]
        public string Name { get; set; }
        [MaxLength(150,ErrorMessage ="150 harften fazla olamaz."), MinLength(10,ErrorMessage ="10 harften az olamaz.")]
        public string Description { get; set; }
        [Column("PhotoUrl")]
        public string Photo { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual Category Category { get; set; }
    }
}
