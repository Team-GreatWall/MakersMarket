using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MakersMarket.Models.View.Product
{
    public class ProductDetailsViewModel
    {
        [Required]
        public Domain.Product Product { get; set; }

        [Required]
        public IEnumerable<Domain.Product> RelatedProducts { get; set; }
    }
}