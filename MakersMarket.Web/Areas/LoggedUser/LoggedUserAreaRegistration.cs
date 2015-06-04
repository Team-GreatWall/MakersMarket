using System.Web.Mvc;

namespace MakersMarket.Web.Areas.LoggedUser
{
    public class LoggedUserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LoggedUser";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            context.MapRoute(
                "DeletePhoto",
                "LoggedUser/{controller}/{action}/{id}/{photoId}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "MakersMarket.Web.Areas.LoggedUser.Controllers" }
            );

            context.MapRoute(
                "LoggedUser_default",
                "LoggedUser/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "MakersMarket.Web.Areas.LoggedUser.Controllers" }
            );
        }
    }
}