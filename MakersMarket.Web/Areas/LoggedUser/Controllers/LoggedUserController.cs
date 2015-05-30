namespace MakersMarket.Web.Areas.LoggedUser.Controllers
{
    using System.Web.Mvc;
    using Web.Controllers;
    using Data.Data;
    [Authorize]
    public class LoggedUserController : BaseController
    {
        public LoggedUserController(IMakersMarketData data) : base(data)
        {
        }
    }
}