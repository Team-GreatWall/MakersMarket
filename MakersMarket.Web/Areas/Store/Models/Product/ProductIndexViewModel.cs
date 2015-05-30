namespace MakersMarket.Web.Areas.Store.Models.Product
{
    using PagedList;
    using System.ComponentModel.DataAnnotations;
    using MakersMarket.Models;
    public class ProductIndexViewModel
    {
        [Required]
        public SelectorViewModel Selector { get; set; }

        [Required]
        public IPagedList<Product> Products { get; set; }
    }
}