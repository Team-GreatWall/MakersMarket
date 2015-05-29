using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MakersMarket.Models.Domain
{
    public class Product
    {
        public Product()
        {
            IsOnSale = false;
            this.Images = new HashSet<Image>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public int ShopId{ get; set; }

        public virtual Shop Shop { get; set; }
    
        public virtual Category Category { get; set; }

        public int CategoryID { get; set; }

  
        public virtual Brand Brand { get; set; }

        public int BrandID { get; set; }

        [Required]
        public Boolean IsOnSale { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public ICollection<Image> Images { get; set; }
       
    }
}