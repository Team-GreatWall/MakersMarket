namespace MakersMarket.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Web.Controllers;
    using Data.Data;

    [Authorize(Roles = "AppAdmin")]
    public class AdminController : BaseController
    {
        public AdminController(IMakersMarketData data)
            : base(data)
        {
        }
        
    }
}