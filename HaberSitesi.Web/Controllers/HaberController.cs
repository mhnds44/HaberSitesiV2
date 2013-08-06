using HaberSitesi.Data.Context;
using HaberSitesi.Service;
using System.Web.Mvc;

namespace HaberSitesi.Web.Controllers
{
    public class HaberController : AnaController
    {
        private HaberSitesiDbContext db;
        private HaberServis haberServis;

        public HaberController()
        {
            this.db = new HaberSitesiDbContext();
            this.haberServis = new HaberServis(db);
        }

        public ActionResult HaberDetay(int id)
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
