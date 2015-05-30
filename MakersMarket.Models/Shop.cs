namespace MakersMarket.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Shop
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public string Description { get; set; }


        public string UserId { get; set; }

        public virtual User User { get; set; }

    }
}
