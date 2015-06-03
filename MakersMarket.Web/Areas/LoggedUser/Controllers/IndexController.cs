namespace MakersMarket.Web.Areas.LoggedUser.Controllers
{
    using System.Web.Mvc;
    using Data.Data;
    public class IndexController : LoggedUserController
    {
        public IndexController(IMakersMarketData data)
            : base(data)
        {
        }

    }
}