using System.Web.Mvc;
using MakersMarket.Models;

namespace MakersMarket.Areas.LoggedUser.Controllers
{
    [Authorize]
    public class LoggedUserController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}