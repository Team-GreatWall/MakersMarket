using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MakersMarket.Models.Domain
{
    public class Brand
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public string Description { get; set; } 
    }
}