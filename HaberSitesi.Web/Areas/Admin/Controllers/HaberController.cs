using AutoMapper;
using HaberSitesi.Data.Context;
using HaberSitesi.Domain.DomainModel;
using HaberSitesi.Service;
using HaberSitesi.Utilities;
using HaberSitesi.Web.Areas.Admin.Models;
using HaberSitesi.Web.Controllers;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace HaberSitesi.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HaberController : AnaController
    {
        private HaberSitesiDbContext db;
        private HaberServis haberServis;
        private EtiketServis etiketServis;
        private KategoriServis kategoriServis;
        private Kullanici aktifKullanici;
        private KullaniciServis kullaniciServis;

        public HaberController()
        {
            this.db = new HaberSitesiDbContext();
            this.haberServis = new HaberServis(db);
            this.etiketServis = new EtiketServis(db);
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

        public ActionResult Haberler()
        {
            return View();
        }

        public ActionResult HaberEkle()
        {
            HaberModel model = new HaberModel
            {
                Etiketler = etiketServis.Etiketler(),
                Kategoriler = kategoriServis.Kategoriler(),
                HaberPozisyonlar = haberServis.HaberPozisyonlar(),
                Yayinda = true
            };

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult HaberEkle(HaberModel model)
        {
            try
            {
                Haber haber = Mapper.Map<HaberModel, Haber>(model);
                var dosya = model.Resim;
                var etiketler = etiketServis.Etiketler(model.SecilenEtiketler).ToList();

                if (dosya != null && dosya.ContentLength > 0)
                {
                    // resmin ismini değiştir.
                    var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(dosya.FileName);

                    // dosya dizinlerinin yollarını oluştur.
                    var orijinalResimDizin = Server.MapPath("~/Images/uploads/Haber/Orijinal");
                    var buyukResimDizin = Server.MapPath("~/Images/uploads/Haber/Buyuk");
                    var kucukResimDizin = Server.MapPath("~/Images/uploads/Haber/Kucuk");

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

                    haber.OrjinalProfilResim = Path.Combine("Images/uploads/Haber/Orijinal/", fileName);
                    haber.BuyukProfilResim = Path.Combine("Images/uploads/Haber/Buyuk/", fileName);
                    haber.KucukProfilResim = Path.Combine("Images/uploads/Haber/Kucuk/", fileName);
                }
                haber.DegistirmeKullaniciId = AktifKullanici.Id;
                haber.OlusturmaKullaniciId = AktifKullanici.Id;
                haber.YayinlamaKullaniciId = AktifKullanici.Id;
                haber.DegistirmeTarihi = DateTime.Now;
                haber.OlusturmaTarihi = DateTime.Now;
                haber.YayinlanmaTarihi = DateTime.Now;
                haber.OkunmaSayisi = 0;
                haber.YorumSayisi = 0;
                haber.HaberTipId = 1;
                haber.SeoBaslik = StringIslemleri.ToSeoUrl(model.Baslik);
                haber.TumEtiketler = string.Join(", ", etiketler.Select(x => x.Ad));
                etiketler.ForEach(x => haber.Etiketler.Add(x));

                haberServis.Ekle(haber);

                return RedirectToAction("Haberler");
            }
            catch (Exception ex)
            {
                model.Etiketler = etiketServis.Etiketler();
                model.Kategoriler = kategoriServis.Kategoriler();
                model.HaberPozisyonlar = haberServis.HaberPozisyonlar();
            }

            return View(model);
        }

        public ActionResult HaberDuzenle(int id)
        {
            Haber haber = haberServis.Bul(id);
            HaberModel model = Mapper.Map<Haber, HaberModel>(haber);
            var secilenEtiketler = haber.Etiketler.Select(x => x.Id).ToArray();

            model.Etiketler = etiketServis.Etiketler();
            model.Kategoriler = kategoriServis.Kategoriler();
            model.HaberPozisyonlar = haberServis.HaberPozisyonlar();
            model.SecilenEtiketler = secilenEtiketler;

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult HaberDuzenle(HaberModel model)
        {
            try
            {
                var haber = haberServis.Bul(model.Id);
                var dosya = model.Resim;
                var etiketler = etiketServis.Etiketler(model.SecilenEtiketler).ToList();

                if (dosya != null && dosya.ContentLength > 0)
                {
                    // resmin ismini değiştir.
                    var fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(dosya.FileName);

                    // dosya dizinlerinin yollarını oluştur.
                    var orijinalResimDizin = Server.MapPath("~/Images/uploads/Haber/Orijinal");
                    var buyukResimDizin = Server.MapPath("~/Images/uploads/Haber/Buyuk");
                    var kucukResimDizin = Server.MapPath("~/Images/uploads/Haber/Kucuk");

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

                    haber.OrjinalProfilResim = Path.Combine("Images/uploads/Haber/Orijinal/", fileName);
                    haber.BuyukProfilResim = Path.Combine("Images/uploads/Haber/Buyuk/", fileName);
                    haber.KucukProfilResim = Path.Combine("Images/uploads/Haber/Kucuk/", fileName);
                }

                haber.Aciklama = model.Aciklama;
                haber.Baslik = model.Baslik;
                haber.HaberPozisyonId = model.HaberPozisyonId;
                haber.Icerik = model.Icerik;
                haber.KategoriId = model.KategoriId;
                haber.Kaynak = model.Kaynak;
                haber.Yayinda = model.Yayinda;
                haber.DegistirmeKullaniciId = AktifKullanici.Id;
                haber.DegistirmeTarihi = DateTime.Now;
                haber.SeoBaslik = StringIslemleri.ToSeoUrl(model.Baslik);
                haber.TumEtiketler = string.Join(", ", etiketler.Select(x => x.Ad));
                etiketler.ForEach(x => haber.Etiketler.Add(x));

                haberServis.Guncelle(haber);

                return RedirectToAction("Haberler");
            }
            catch (Exception ex)
            {
                model.Etiketler = etiketServis.Etiketler();
                model.Kategoriler = kategoriServis.Kategoriler();
                model.HaberPozisyonlar = haberServis.HaberPozisyonlar();
            }

            return View(model);
        }

        public ActionResult HaberSil(int id)
        {
            haberServis.HaberSil(id);
            return RedirectToAction("Haberler");
        }

        public ActionResult HaberlerJson(int page, int rows, string sort, string order)
        {
            var haberler = haberServis.Haberler(page, rows);

            var result = new
            {
                total = haberler.KayitSayisi,
                rows = haberler.KaynakListe.Select(x => new
                {
                    Id = x.Id,
                    Baslik = x.Baslik,
                    Kategori = x.Kategori.Ad,
                    Pozisyon = x.HaberPozisyon.Ad,
                    Resim = x.KucukProfilResim,
                    Yayinda = x.Yayinda
                })
                .AsQueryable()
                .OrderBy(sort + " " + order)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HaberYayinDegistir(int id, bool durum)
        {
            haberServis.HaberYayinDegistir(id, durum, AktifKullanici.Id);

            return RedirectToAction("Haberler");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
