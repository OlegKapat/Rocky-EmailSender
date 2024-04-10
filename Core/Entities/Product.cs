using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [
            Range(
                1,
                int.MaxValue,
                ErrorMessage = "Price must be greater than ${1}")
        ]
        public double Price { get; set; }

        [Display(Name = "Category Type")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

         [Display(Name = "Type")]
        public int TypeId { get; set; }
      
        [ForeignKey("TypeId")]
        public virtual Type Type { get; set; }
    }
}
