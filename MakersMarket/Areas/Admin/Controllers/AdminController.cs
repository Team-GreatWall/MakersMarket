using System.Web.Mvc;
using MakersMarket.Models;

namespace MakersMarket.Areas.Admin.Controllers
{
    [Authorize(Roles = "AppAdmin")]
    public class AdminController : Controller
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