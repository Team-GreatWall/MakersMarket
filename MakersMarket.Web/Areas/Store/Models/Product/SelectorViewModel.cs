namespace MakersMarket.Web.Areas.Store.Models.Product
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Routing;
    using MakersMarket.Models;
    public class SelectorViewModel
    {
        [Required]
        public IEnumerable<Brand> Brands { get; set; }

        [Required]
        public IEnumerable<Category> Categories { get; set; }

        [Required]
        public RouteValueDictionary Filters { get; set; }
    }
}