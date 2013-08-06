using HaberSitesi.Data.Context;
using HaberSitesi.Service;
using System.Web.Mvc;

namespace HaberSitesi.Web.Controllers
{
    public class KoseYazisiController : AnaController
    {
        private HaberSitesiDbContext db;
        private HaberServis haberServis;

        public KoseYazisiController()
        {
            this.db = new HaberSitesiDbContext();
            this.haberServis = new HaberServis(db);
        }

        public ActionResult KoseYazisiDetay(int id)
        {
            var haber = haberServis.Bul(id);
            haberServis.OkunmaSayisiArtir(haber);

            return View(haber);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
