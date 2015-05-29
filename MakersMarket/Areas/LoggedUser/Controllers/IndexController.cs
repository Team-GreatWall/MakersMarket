using System.Web.Mvc;

namespace MakersMarket.Areas.LoggedUser.Controllers
{
    public class IndexController : LoggedUserController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}