using HaberSitesi.Data.Context;
using HaberSitesi.Domain.DomainModel;
using HaberSitesi.Service;
using HaberSitesi.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using HaberSitesi.Web.Areas.Admin.Models;
using AutoMapper;
using HaberSitesi.Utilities;

namespace HaberSitesi.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KoseYazisiController : AnaController
    {
        private HaberSitesiDbContext db;
        private HaberServis haberServis;
        private KategoriServis kategoriServis;
        private Kullanici aktifKullanici;
        private KullaniciServis kullaniciServis;

        public KoseYazisiController()
        {
            this.db = new HaberSitesiDbContext();
            this.haberServis = new HaberServis(db);
            this.kategoriServis = new KategoriServis(db);
            this.kullaniciServis = new KullaniciServis(db);
        }

        public Kullanici AktifKullanici
        {
            get
            {
                return aktifKullanici ?? (aktifKullanici = kullaniciServis.AktifKullanici(User.Identity.Name));
            }
            set
            {
                aktifKullanici = value;
            }
        }

        public ActionResult KoseYazilari()
        {
            return View();
        }

        public ActionResult KoseYazisiEkle()
        {
            KoseYazisiModel model = new KoseYazisiModel
            {
                Kategoriler = kategoriServis.Kategoriler(),
                Yazarlar = kullaniciServis.RolKullanicilar("Yazar"),
                Yayinda = true
            };

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult KoseYazisiEkle(KoseYazisiModel model)
        {
            try
            {
                Haber haber = Mapper.Map<KoseYazisiModel, Haber>(model);

                haber.DegistirmeKullaniciId = AktifKullanici.Id;
                haber.OlusturmaKullaniciId = AktifKullanici.Id;
                haber.YayinlamaKullaniciId = AktifKullanici.Id;
                haber.DegistirmeTarihi = DateTime.Now;
                haber.OlusturmaTarihi = DateTime.Now;
                haber.YayinlanmaTarihi = DateTime.Now;
                haber.OkunmaSayisi = 0;
                haber.YorumSayisi = 0;
                haber.HaberTipId = 2;
                haber.SeoBaslik = StringIslemleri.ToSeoUrl(model.Baslik);

                haberServis.Ekle(haber);

                return RedirectToAction("KoseYazilari");
            }
            catch (Exception ex)
            {
                model.Kategoriler = kategoriServis.Kategoriler();
                model.Yazarlar = kullaniciServis.RolKullanicilar("Yazar");
            }

            return View(model);
        }

        public ActionResult KoseYazisiDuzenle(int id)
        {
            Haber haber = haberServis.Bul(id);
            KoseYazisiModel model = Mapper.Map<Haber, KoseYazisiModel>(haber);

            model.Kategoriler = kategoriServis.Kategoriler();
            model.Yazarlar = kullaniciServis.RolKullanicilar("Yazar");

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult KoseYazisiDuzenle(KoseYazisiModel model)
        {
            try
            {
                var haber = haberServis.Bul(model.Id);

                haber.Baslik = model.Baslik;
                haber.Icerik = model.Icerik;
                haber.KategoriId = model.KategoriId;
                haber.Yayinda = model.Yayinda;
                haber.YazarId = model.YazarId;
                haber.DegistirmeKullaniciId = AktifKullanici.Id;
                haber.DegistirmeTarihi = DateTime.Now;
                haber.SeoBaslik = StringIslemleri.ToSeoUrl(model.Baslik);

                haberServis.Guncelle(haber);

                return RedirectToAction("KoseYazilari");
            }
            catch (Exception ex)
            {
                model.Kategoriler = kategoriServis.Kategoriler();
                model.Yazarlar = kullaniciServis.RolKullanicilar("Yazar");
            }

            return View(model);
        }

        public ActionResult KoseYazisiSil(int id)
        {
            haberServis.HaberSil(id);
            return RedirectToAction("KoseYazilari");
        }

        public ActionResult KoseYazilariJson(int page, int rows, string sort, string order)
        {
            var koseYazilari = haberServis.KoseYazilari(page, rows);

            var result = new
            {
                total = koseYazilari.KayitSayisi,
                rows = koseYazilari.KaynakListe.Select(x => new
                {
                    Id = x.Id,
                    Baslik = x.Baslik,
                    Kategori = x.Kategori.Ad,
                    Resim = x.Yazar.KucukProfilResim,
                    Yayinda = x.Yayinda
                })
                .AsQueryable()
                .OrderBy(sort + " " + order)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult KoseYazisiYayinDegistir(int id, bool durum)
        {
            haberServis.HaberYayinDegistir(id, durum, AktifKullanici.Id);

            return RedirectToAction("KoseYazilari");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
