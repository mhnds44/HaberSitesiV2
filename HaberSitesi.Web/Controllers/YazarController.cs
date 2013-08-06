using HaberSitesi.Data.Context;
using HaberSitesi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HaberSitesi.Web.Controllers
{
    public class YazarController : AnaController
    {
        private HaberSitesiDbContext db;
        private KullaniciServis kullaniciServis;

        public YazarController()
        {
            this.db = new HaberSitesiDbContext();
            this.kullaniciServis = new KullaniciServis(db);
        }

        public ActionResult Yazarlar()
        {
            var yazarlar = kullaniciServis.RolKullanicilar("Yazar")
                .Where(x => x.YazilanHaberler.Any());

            return View(yazarlar);
        }

    }
}
