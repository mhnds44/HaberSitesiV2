using HaberSitesi.Data.Context;
using HaberSitesi.Service;
using HaberSitesi.Web.Areas.Admin.Models;
using HaberSitesi.Web.Controllers;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace HaberSitesi.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KullaniciController : AnaController
    {
        private HaberSitesiDbContext db;
        private KullaniciServis kullaniciServis;
        private RolServis rolServis;

        public KullaniciController()
        {
            this.db = new HaberSitesiDbContext();
            this.kullaniciServis = new KullaniciServis(db);
            this.rolServis = new RolServis(db);
        }

        public ActionResult Kullanicilar()
        {
            return View();
        }

        public ActionResult KullaniciRolDuzenle(int id)
        {
            var kullanici = kullaniciServis.Bul(id);
            var secilenRoller = kullanici.Roller.Select(x => x.Id).ToArray();

            KullaniciRolModel model = new KullaniciRolModel
            {
                Kullanici = kullanici,
                Roller = rolServis.Roller(),
                SecilenRoller = secilenRoller
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult KullaniciRolDuzenle(KullaniciRolModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    rolServis.KullaniciRolEkle(model.Kullanici.Id, model.SecilenRoller);

                    return RedirectToAction("Kullanicilar");
                }
                catch (Exception ex)
                {
                    model = new KullaniciRolModel
                    {
                        Kullanici = kullaniciServis.Bul(model.Kullanici.Id),
                        Roller = rolServis.Roller()
                    };
                }
            }

            return View(model);
        }

        public ActionResult KullanicilarJson(int page, int rows, string sort, string order)
        {
            var kullanicilar = kullaniciServis.Kullanicilar(page, rows);

            var result = new
            {
                total = kullanicilar.KayitSayisi,
                rows = kullanicilar.KaynakListe.Select(x => new
                {
                    Id = x.Id,
                    KullaniciAdi = x.KullaniciAdi,
                    Eposta = x.Eposta,
                    Resim = x.KucukProfilResim,
                    Roller = String.Join(",", rolServis.KullaniciRolleri(x.Eposta))
                })
                  .AsQueryable()
                  .OrderBy(sort + " " + order)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult KullaniciSil(int id)
        {
            kullaniciServis.KullaniciSil(id);
            return RedirectToAction("Kullanicilar");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
