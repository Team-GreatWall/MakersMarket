namespace MakersMarket.Web.Areas.LoggedUser.Models
{
    using System;
    public class CreateProductViewModel
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public int ShopId { get; set; }

        public Boolean IsOnSale { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}