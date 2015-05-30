namespace MakersMarket.Web.Areas.Store.Models.Product
{
    
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MakersMarket.Models;
    public class ProductDetailsViewModel
    {
        [Required]
        public Product Product { get; set; }

        [Required]
        public IEnumerable<Product> RelatedProducts { get; set; }
    }
}