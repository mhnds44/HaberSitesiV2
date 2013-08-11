using HaberSitesi.Data.Context;
using HaberSitesi.Web.Controllers;
using System.Web.Mvc;

namespace HaberSitesi.Web.Areas.Admin.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : AnaController
    {
        private HaberSitesiDbContext db;

        public AdminController()
        {
            this.db = new HaberSitesiDbContext();
        }

        public ActionResult Anasayfa()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
