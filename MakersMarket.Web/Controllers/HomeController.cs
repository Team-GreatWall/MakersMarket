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
        public ActionResult Index()
        {
            return View();
        }

        public HomeController(IMakersMarketData data) 
            : base(data)
        {

        }
    }
}