namespace MakersMarket.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Data.Data;
    public class IndexController : AdminController
    {
        public IndexController(IMakersMarketData data)
            : base(data)
        {
        }
        public ActionResult Index()
        {
            return View();
        }
        
    }
}