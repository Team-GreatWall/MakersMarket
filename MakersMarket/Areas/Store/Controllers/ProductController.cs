using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using MakersMarket.Models;
using MakersMarket.Models.Domain;
using MakersMarket.Models.View.Product;
using MakersMarket.Search;
using PagedList;

namespace MakersMarket.Areas.Store.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string brand, string category, string sale, string search, int page = 1, int pageSize = 12)
        {
            // Filter the products
            var filters = new RouteValueDictionary();
            var prodcutsQuery = db.Products.Include(p =>p.Images).AsQueryable();
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
                var searchResults = LuceneSearch.Search(search, "Name").Select(p => p.ID);
                prodcutsQuery = prodcutsQuery.Where(p => searchResults.Contains(p.ID));
            }
            products = prodcutsQuery.OrderByDescending(product => product.Name).Include(p => p.Brand).Include(p => p.Category);

            ViewBag.FilterProductsWith = new System.Func<RouteValueDictionary, string, object, RouteValueDictionary>(FilterProductsWith);
            ViewBag.FilterProductsWithout = new System.Func<RouteValueDictionary, string, RouteValueDictionary>(FilterProductsWithout);

            SelectorViewModel selector = new SelectorViewModel
            {
                Brands = db.Brands,
                Categories = db.Categories,
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
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var relatedProducts = db.Products.Include(p => p.Brand).Include(p => p.Category).Where(p => p.CategoryID == product.CategoryID && p.ID != product.ID).Take(4);
            ProductDetailsViewModel productDetails = new ProductDetailsViewModel
            {
                Product = product,
                RelatedProducts = relatedProducts
            };
            return View(productDetails);
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