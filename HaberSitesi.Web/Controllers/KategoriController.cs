using HaberSitesi.Data.Context;
using HaberSitesi.Service;
using System.Web.Mvc;

namespace HaberSitesi.Web.Controllers
{
    public class KategoriController : AnaController
    {
        private HaberSitesiDbContext db;
        private KategoriServis kategoriServis;

        public KategoriController()
        {
            this.db = new HaberSitesiDbContext();
            this.kategoriServis = new KategoriServis(db);
        }

        public ActionResult KategoriDetay(string kategoriAd)
        {
            var kategori = kategoriServis.Bul(kategoriAd);

            return View(kategori);
        }

    }
}
