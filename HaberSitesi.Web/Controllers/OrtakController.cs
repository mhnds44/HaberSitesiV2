using AutoMapper;
using HaberSitesi.Data.Context;
using HaberSitesi.Domain.DomainModel;
using HaberSitesi.Service;
using HaberSitesi.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HaberSitesi.Web.Controllers
{
    public class OrtakController : AnaController
    {
        private HaberSitesiDbContext db;
        private KategoriServis kategoriServis;
        private HaberServis haberServis;
        private KullaniciServis kullaniciServis;
        private YorumServis yorumServis;

        public OrtakController()
        {
            this.db = new HaberSitesiDbContext();
            this.haberServis = new HaberServis(db);
            this.kategoriServis = new KategoriServis(db);
            this.kullaniciServis = new KullaniciServis(db);
            this.yorumServis = new YorumServis(db);
        }

        public ActionResult _KategorilerMenu()
        {
            var kategoriler = kategoriServis.Kategoriler();

            return PartialView(kategoriler);
        }

        public ActionResult _FooterMenu()
        {
            var kategoriler = kategoriServis.Kategoriler();

            return PartialView(kategoriler);
        }

        public ActionResult _SolManset(int? kategoriId)
        {
            if (kategoriId == null)
            {
                // id si 2 olanlar sol manset haberleri
                var haberler = haberServis.PozisyonHaberler(2, 3)
                    .ToList();

                return PartialView(haberler);
            }
            else
            {

                var haberler = haberServis.KategoriHaberler((int)kategoriId, 3)
                    .ToList();

                return PartialView(haberler);
            }
        }

        public ActionResult _Slider(int? kategoriId)
        {
            if (kategoriId == null)
            {
                // id si 1 olanlar slider haberleri
                var haberler = haberServis.PozisyonHaberler(1, 19)
                    .ToList();

                return PartialView(haberler);
            }
            else
            {
                var haberler = haberServis.KategoriHaberler((int)kategoriId, 19)
                        .ToList();

                return PartialView(haberler);
            }
        }

        public ActionResult _OneCikanlar(int? kategoriId)
        {
            if (kategoriId == null)
            {
                // id si 1 olanlar slider haberleri
                var haberler = haberServis.PozisyonHaberler(1, 7)
                    .ToList();

                return PartialView(haberler);
            }
            else
            {
                var haberler = haberServis.KategoriHaberler((int)kategoriId, 7)
                        .ToList();

                return PartialView(haberler);
            }
        }

        public ActionResult _Yazarlar()
        {
            var yazarlar = haberServis.KoseYazilari(5)
                .ToList();

            return PartialView(yazarlar);
        }

        public ActionResult _KategoriHaberler(int? kategoriId)
        {
            if (kategoriId == null)
            {
                var kategoriler = kategoriServis.AnaKategoriler()
                    .ToList();

                return PartialView(kategoriler);
            }
            else
            {
                List<Kategori> kategoriler = new List<Kategori>();
                kategoriler.Add(kategoriServis.Bul((int)kategoriId));

                return PartialView(kategoriler);
            }
        }

        public ActionResult _EnCokOkunanlar(int? kategoriId)
        {
            if (kategoriId == null)
            {
                var haberler = haberServis.EnCokOkunanHaberler(1, 5)
                    .ToList();

                return PartialView(haberler);
            }
            else
            {
                var haberler = haberServis.EnCokOkunanHaberler((int)kategoriId, 1, 5)
                    .ToList();

                ViewBag.Kategori = kategoriServis.Bul((int)kategoriId).Ad;

                return PartialView(haberler);
            }
        }

        public ActionResult _YorumEkle(int haberId)
        {
            var model = new YorumModel
            {
                HaberId = haberId
            };

            return PartialView();
        }

        [HttpPost]
        public ActionResult _YorumEkle(YorumModel model)
        {
            model.Aktif = true;
            model.IP = Request.ServerVariables["REMOTE_ADDR"];
            model.Onayli = true;
            model.OlusturmaTarihi = DateTime.Now;

            if (Request.IsAuthenticated)
            {
                model.KullaniciId = kullaniciServis.AktifKullanici(User.Identity.Name).Id;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Yorum yorum = Mapper.Map<YorumModel, Yorum>(model);
                    yorumServis.Ekle(yorum);

                    return Json(new { success = true, haberId = model.HaberId });
                }
                catch (Exception ex) { }
            }

            return PartialView(model);
        }

        public ActionResult _Yorumlar(int haberId, int kayitSayisi = 0)
        {
            kayitSayisi += 10;
            var yorumlar = yorumServis.HaberYorumlar(haberId, kayitSayisi);

            ViewBag.HaberId = haberId;
            ViewBag.ToplamKayit = yorumServis.HaberYorumSayisi(haberId);
            return PartialView(yorumlar);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
