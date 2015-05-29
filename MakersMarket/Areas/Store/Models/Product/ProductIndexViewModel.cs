using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MakersMarket.Models.View.Product
{
    public class ProductIndexViewModel
    {
        [Required]
        public SelectorViewModel Selector { get; set; }

        [Required]
        public IPagedList<Domain.Product> Products { get; set; }
    }
}