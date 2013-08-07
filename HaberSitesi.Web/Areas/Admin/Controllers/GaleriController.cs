using AutoMapper;
using HaberSitesi.Data.Context;
using HaberSitesi.Domain.DomainModel;
using HaberSitesi.Service;
using HaberSitesi.Web.Areas.Admin.Models;
using HaberSitesi.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace HaberSitesi.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GaleriController : AnaController
    {
        private HaberSitesiDbContext db;
        private GaleriServis galeriServis;
        private HaberServis haberServis;
        private IEnumerable<Haber> haberler;

        public GaleriController()
        {
            this.db = new HaberSitesiDbContext();
            this.galeriServis = new GaleriServis(db);
            this.haberServis = new HaberServis(db);
        }

        public IEnumerable<Haber> Haberler
        {
            get { return haberler ?? (haberler = haberServis.Haberler()); }

            set { haberler = value; }
        }

        public ActionResult Galeriler()
        {
            return View();
        }

        public ActionResult GaleriEkle()
        {
            GaleriModel model = new GaleriModel
            {
                Haberler = Haberler
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult GaleriEkle(GaleriModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Galeri galeri = Mapper.Map<GaleriModel, Galeri>(model);
                    galeriServis.Ekle(galeri);

                    return RedirectToAction("Galeriler");
                }
                catch (Exception ex)
                {
                    model = new GaleriModel
                    {
                        Haberler = Haberler
                    };
                }
            }
            return View(model);
        }

        public ActionResult GaleriDuzenle(int id)
        {
            Galeri galeri = galeriServis.Bul(id);
            GaleriModel model = Mapper.Map<Galeri, GaleriModel>(galeri);
            model.Haberler = Haberler;

            return View(model);
        }

        [HttpPost]
        public ActionResult GaleriDuzenle(GaleriModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Galeri galeri = galeriServis.Bul(model.Id);
                    galeri = (Galeri)Mapper.Map(model, galeri, typeof(GaleriModel), typeof(Galeri));
                    galeriServis.Guncelle(galeri);

                    return RedirectToAction("Galeriler");
                }
                catch (Exception ex)
                {

                }
            }

            model.Haberler = Haberler;
            return View(model);
        }

        public ActionResult GaleriSil(int id)
        {
            galeriServis.GaleriSil(id);
            return RedirectToAction("Galeriler");
        }

        public JsonResult GaleriVarmi(string Ad)
        {
            var result = galeriServis.GaleriVarmi(Ad);

            if (result)
            {
                return Json("Girdiğiniz galeri sistemde zaten mevcut!", JsonRequestBehavior.AllowGet);
            }

            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GalerilerJson(int page, int rows, string sort, string order)
        {
            var galeriler = galeriServis.Galeriler(page, rows);

            var result = new
            {
                total = galeriler.KayitSayisi,
                rows = galeriler.KaynakListe.Select(x => new
                {
                    Id = x.Id,
                    Ad = x.Ad
                })
                  .AsQueryable()
                  .OrderBy(sort + " " + order)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GaleriResimEkle(int id)
        {
            var galeri = galeriServis.Bul(id);
            GaleriResimModel model = new GaleriResimModel
            {
                Galeri = galeri
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult GaleriResimEkle(GaleriResimModel model)
        {
            try
            {
                var galeri = galeriServis.Bul(model.Galeri.Id);

                foreach (var dosya in model.Resimler)
                {
                    // her döngüde seçilen galeri için resim oluştur
                    Resim resim = new Resim();

                    // resmin ismini değiştir.
                    var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(dosya.FileName);

                    // dosya dizinlerinin yollarını oluştur.
                    var orijinalResimDizin = Server.MapPath("~/Images/uploads/Galeri/Orijinal");
                    var buyukResimDizin = Server.MapPath("~/Images/uploads/Galeri/Buyuk");
                    var kucukResimDizin = Server.MapPath("~/Images/uploads/Galeri/Kucuk");

                    // dizin yoksa oluştur.
                    if (!Directory.Exists(orijinalResimDizin))
                    {
                        Directory.CreateDirectory(orijinalResimDizin);
                        Directory.CreateDirectory(buyukResimDizin);
                        Directory.CreateDirectory(kucukResimDizin);
                    }

                    // dosyayı kaydet
                    dosya.SaveAs(Path.Combine(orijinalResimDizin, fileName));

                    // resimleri farklı boyutlarda kaydet.
                    ResimServis.SaveResizedImage(Image.FromFile(Path.Combine(orijinalResimDizin, fileName)), new Size(600, 600), buyukResimDizin, fileName);
                    ResimServis.SaveResizedImage(Image.FromFile(Path.Combine(orijinalResimDizin, fileName)), new Size(200, 200), kucukResimDizin, fileName);

                    // resimin özelliklerini belirle
                    resim.Ad = fileName;
                    resim.Boyut = dosya.ContentLength;
                    resim.Uzanti = dosya.ContentType;
                    resim.OrjinalResim = Path.Combine("Images/uploads/Galeri/Orijinal/", fileName);
                    resim.BuyukResim = Path.Combine("Images/uploads/Galeri/Buyuk/", fileName);
                    resim.KucukResim = Path.Combine("Images/uploads/Galeri/Kucuk/", fileName);

                    // resmi geleriye ekle
                    galeri.Resimler.Add(resim);
                }

                galeriServis.Guncelle(galeri);

                return RedirectToAction("GaleriResimEkle", new { id = galeri.Id });
            }
            catch (Exception ex) { }

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
