using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MakersMarket.Data.Data;

namespace MakersMarket.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IMakersMarketData data) 
            : base(data)
        {

        }

        public ActionResult Index()
        {
            return this.RedirectToAction("Index", new { area = "Store", controller = "Shop" });
        }
    }
}