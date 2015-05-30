namespace MakersMarket.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public string Description { get; set; }
    }
}
