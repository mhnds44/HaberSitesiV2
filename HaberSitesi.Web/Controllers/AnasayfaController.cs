using HaberSitesi.Data.Context;
using System.Web.Mvc;

namespace HaberSitesi.Web.Controllers
{
    public class AnasayfaController : AnaController
    {
        private HaberSitesiDbContext db;

        public AnasayfaController()
        {
            this.db = new HaberSitesiDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Hakkimizda()
        {
            return View();
        }

        public ActionResult Iletisim()
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
