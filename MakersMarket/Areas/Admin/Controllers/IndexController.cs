using System.Web.Mvc;

namespace MakersMarket.Areas.Admin.Controllers
{
    public class IndexController : AdminController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}