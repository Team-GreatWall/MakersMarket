using System.Web.Mvc;

namespace MakersMarket.Areas.LoggedUser
{
    public class LogedUserAreaRegistration : AreaRegistration 
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
                "LoggedUser_default",
                "LoggedUser/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "MakersMarket.Areas.LoggedUser.Controllers" }
            );
        }
    }
}