namespace MakersMarket.Models
{
    using System.ComponentModel.DataAnnotations;
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public string CartId { get; set; }

        public int Quantity { get; set; }

        public System.DateTime DateCreated { get; set; }

        public string ProductImage { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
