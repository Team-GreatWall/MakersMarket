namespace MakersMarket.Web.Areas.LoggedUser.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Data.Data;
    using MakersMarket.Models;
    using PagedList;
    public class ShopController : LoggedUserController
    {
        public ShopController(IMakersMarketData data)
            : base(data)
        {
        }
        public ActionResult Index(int page = 1, int pageSize = 12)
        {
            var userId = this.UserProfile.Id;
            return View(this.Data.Shops.All().Where(s => s.User.Id == userId).OrderByDescending(category => category.Name).ToPagedList(page, pageSize));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shop = this.Data.Shops.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Shop model)
        {
            var userId = this.UserProfile.Id;
            ViewData["UserId"] = userId;
            model.UserId = userId;
            if (ModelState.IsValid)
            {
                this.Data.Shops.Add(model);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shop shop = this.Data.Shops.Find(id);
            if (shop == null)
            {
                return HttpNotFound();
            }
            return View(shop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description")] Shop shop)
        {
            if (ModelState.IsValid)
            {
                this.Data.Shops.Update(shop);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "ID")] Shop shop)
        {
            this.Data.Shops.Delete(shop);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
