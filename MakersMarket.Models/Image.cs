namespace MakersMarket.Models
{
    public class Image
    {

        public int Id { get; set; }

        public string ImagePath { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
