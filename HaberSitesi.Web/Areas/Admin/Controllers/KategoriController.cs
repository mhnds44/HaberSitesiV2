using AutoMapper;
using HaberSitesi.Data.Context;
using HaberSitesi.Domain.DomainModel;
using HaberSitesi.Service;
using HaberSitesi.Utilities;
using HaberSitesi.Web.Areas.Admin.Models;
using HaberSitesi.Web.Controllers;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace HaberSitesi.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KategoriController : AnaController
    {
        private HaberSitesiDbContext db;
        private KategoriServis kategoriServis;

        public KategoriController()
        {
            this.db = new HaberSitesiDbContext();
            this.kategoriServis = new KategoriServis(db);
        }

        public ActionResult Kategoriler()
        {
            return View();
        }

        public ActionResult KategoriEkle()
        {
            return View(new KategoriModel());
        }

        [HttpPost]
        public ActionResult KategoriEkle(KategoriModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Kategori kategori = Mapper.Map<KategoriModel, Kategori>(model);
                    kategori.SeoAd = StringIslemleri.ToSeoUrl(model.Ad);
                    kategoriServis.Ekle(kategori);

                    return RedirectToAction("Kategoriler");
                }
                catch (Exception ex)
                {

                }
            }
            return View(model);
        }

        public ActionResult KategoriDuzenle(int id)
        {
            Kategori kategori = kategoriServis.Bul(id);
            KategoriModel model = Mapper.Map<Kategori, KategoriModel>(kategori);

            return View(model);
        }

        [HttpPost]
        public ActionResult KategoriDuzenle(KategoriModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Kategori kategori = kategoriServis.Bul(model.Id);
                    kategori = (Kategori)Mapper.Map(model, kategori, typeof(KategoriModel), typeof(Kategori));
                    kategori.SeoAd = StringIslemleri.ToSeoUrl(model.Ad);
                    kategoriServis.Guncelle(kategori);

                    return RedirectToAction("Kategoriler");
                }
                catch (Exception ex)
                {

                }
            }
            return View(model);
        }

        public ActionResult KategoriSil(int id)
        {
            kategoriServis.KategoriSil(id);
            return RedirectToAction("Kategoriler");
        }

        public JsonResult KategoriVarmi(string Ad)
        {
            var result = kategoriServis.KategoriVarmi(Ad);

            if (result)
            {
                return Json("Girdiğiniz kategori sistemde zaten mevcut!", JsonRequestBehavior.AllowGet);
            }

            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult KategorilerJson(int page, int rows, string sort, string order)
        {
            var kategoriler = kategoriServis.Kategoriler(page, rows);

            var result = new
            {
                total = kategoriler.KayitSayisi,
                rows = kategoriler.KaynakListe.Select(x => new
                {
                    Id = x.Id,
                    Ad = x.Ad,
                    Aciklama = x.Aciklama,
                    AnaMenu = x.AnaMenu,
                    SiraNo = x.SiraNo,
                    SeoAd = x.SeoAd
                })
                  .AsQueryable()
                  .OrderBy(sort + " " + order)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult KategoriDuzeyDegistir(int id, bool durum)
        {
            kategoriServis.DuzeyDegistir(id, !durum);

            return RedirectToAction("Kategoriler");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}
