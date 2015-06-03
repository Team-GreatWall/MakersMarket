namespace MakersMarket.Web.Areas.Store.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using System.Web.Routing;
    using MakersMarket.Models;
    using Models.Product;
    using Data.Data;
    using Web.Controllers;
    using Search;
    using PagedList;
    public class ProductController : BaseController
    {
        public ProductController(IMakersMarketData data)
            : base(data)
        {
        }
        public ActionResult Index(string brand, string category, string sale, string search, int page = 1, int pageSize = 12)
        {
            // Filter the products
            var filters = new RouteValueDictionary();
            var prodcutsQuery = this.Data.Products.All().Include(p =>p.Images).AsQueryable();
            if (brand != null)
            {
                filters.Add("brand", brand);
                prodcutsQuery = prodcutsQuery.Where(p => p.Brand.Name == brand);
            }
            if (category != null)
            {
                filters.Add("category", category);
                prodcutsQuery = prodcutsQuery.Where(p => p.Category.Name == category);
            }
            
            if (sale != null)
            {
                filters.Add("sale", sale);
                prodcutsQuery = prodcutsQuery.Where(p => p.IsOnSale == true);
            }
            IEnumerable<Product> products;
            if (search != null)
            {
                filters.Add("search", search);
                var searchResults = LuceneSearch.Search(search, "Name").Select(p => p.Id);
                prodcutsQuery = prodcutsQuery.Where(p => searchResults.Contains(p.Id));
            }
            products = prodcutsQuery.OrderByDescending(product => product.Name).Include(p => p.Brand).Include(p => p.Category);

            ViewBag.FilterProductsWith = new System.Func<RouteValueDictionary, string, object, RouteValueDictionary>(FilterProductsWith);
            ViewBag.FilterProductsWithout = new System.Func<RouteValueDictionary, string, RouteValueDictionary>(FilterProductsWithout);

            SelectorViewModel selector = new SelectorViewModel
            {
                Brands = this.Data.Brands.All(),
                Categories = this.Data.Categories.All(),
                Filters = filters
            };
            ProductIndexViewModel productIndex = new ProductIndexViewModel
            {
                Selector = selector,
                Products = products.ToPagedList(page, pageSize)
            };
            return View(productIndex);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = this.Data.Products.Find(id);
            var images = this.Data.Images.All().Where(i => i.ProductId == id).Select(i => i).ToList();
            product.Images = images;
            if (product == null)
            {
                return HttpNotFound();
            }
            var relatedProducts = this.Data.Products.All().Include(p => p.Brand).Include(p => p.Category).Where(p => p.CategoryId == product.CategoryId && p.Id != product.Id).Take(4);
            this.ViewBag.relatedProducts = relatedProducts;
            return View(product);
        }

        private RouteValueDictionary FilterProductsWith(RouteValueDictionary filter, string key, object value)
        {
            var newFilter = new RouteValueDictionary(filter);
            newFilter.Add(key, value);
            return newFilter;
        }

        private RouteValueDictionary FilterProductsWithout(RouteValueDictionary filter, string key)
        {
            var newFilter = new RouteValueDictionary(filter);
            newFilter.Remove(key);
            return newFilter;
        }

    }
}