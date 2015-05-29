using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MakersMarket.Models.Domain
{
    public class Shop
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public string Description { get; set; }

        
        public string UserId { get; set; }

        public virtual User User { get; set; }

    }
}