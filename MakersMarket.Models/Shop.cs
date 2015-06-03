namespace MakersMarket.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Shop
    {
        public Shop()
        {
            this.Products = new HashSet<Product>();
            this.Images = new HashSet<ImageShop>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        
        public virtual ICollection<ImageShop> Images { get; set; }
    }
}
