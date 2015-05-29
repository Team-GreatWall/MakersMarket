using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Routing;
using MakersMarket.Models.Domain;

namespace MakersMarket.Models.View.Product
{
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