namespace MakersMarket.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using MakersMarket.Models;
    using Data.Data;
    using System.Linq;
    public abstract class BaseController : Controller
    {
        private IMakersMarketData data;
        private User userProfile;
        protected IMakersMarketData Data { get; private set; }

        protected User UserProfile { get; private set; }

        protected BaseController(IMakersMarketData data)
        {
            this.Data = data;
        }

        protected BaseController(IMakersMarketData data, User userProfile)
            : this(data)
        {
            this.UserProfile = userProfile;
        }

        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.Name;
                var user = this.Data.Users.All().FirstOrDefault(x => x.UserName == username);
                this.UserProfile = user;
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}