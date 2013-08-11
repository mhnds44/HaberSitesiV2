using HaberSitesi.Data.Context;
using HaberSitesi.Domain.DomainModel;
using HaberSitesi.Service;
using HaberSitesi.Web.Models;
using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security;

namespace HaberSitesi.Web.Controllers
{
    public class UyelikController : AnaController
    {
        private HaberSitesiDbContext db;
        private KullaniciServis kullaniciServis;

        public UyelikController()
        {
            this.db = new HaberSitesiDbContext();
            this.kullaniciServis = new KullaniciServis(db);
        }

        public ActionResult GirisYap(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult GirisYap(GirisModel model, string ReturnUrl)
        {
            if (ModelState.IsValid && kullaniciServis.KullaniciDogrula(model.Eposta, model.Sifre))
            {
                var kullanici = kullaniciServis.Bul(model.Eposta);
                if (!kullanici.Onayli)
                {
                    TempData["epostaOnayMesaj"] = "E-posta adresiniz onaylı değildir. Lütfen e-posta adresinizdeki linki kullanarak e-posta adresinizi onaylayınız.";

                    return View();
                }
                FormsAuthentication.SetAuthCookie(model.Eposta, model.BeniHatirla);
                return RedirectToLocal(ReturnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı ve ya şifre geçersiz!");
            }

            return View(model);
        }

        public ActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KayitOl(KayitModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var now = DateTime.Now;
                    Kullanici kullanici = new Kullanici
                    {
                        Eposta = model.Eposta,
                        GuncellemeTarihi = now,
                        KayitTarihi = now,
                        KullaniciAdi = model.KullaniciAdi,
                        Sifre = model.Sifre,
                        Onayli = false,
                        OnayKodu = Guid.NewGuid(),
                        OrjinalProfilResim = "Images/Icons/Buyuk/no_profile_image.png",
                        KucukProfilResim = "Images/Icons/Kucuk/no_profile_image.png"
                    };

                    kullaniciServis.KullaniciOlustur(kullanici);
                    kullaniciServis.OnayMesajiGonder(kullanici.Eposta, Request.Url.GetLeftPart(UriPartial.Authority));
                    return RedirectToAction("Index", "Anasayfa");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Kullanıcı oluşturma başarısız!");
                }
            }

            return View(model);
        }

        public JsonResult EmailVarmi(string Eposta)
        {
            var result = kullaniciServis.EpostaDogrula(Eposta);

            if (result)
            {
                return Json("Girdiğiniz e-posta adresi sistemde zaten mevcut!", JsonRequestBehavior.AllowGet);
            }

            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Cikis()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Anasayfa");
        }

        public ActionResult EpostaOnay(Guid onayKodu)
        {
            if (string.IsNullOrEmpty(onayKodu.ToString()) || (!Regex.IsMatch(onayKodu.ToString(),
                   @"[0-9a-f]{8}\-([0-9a-f]{4}\-){3}[0-9a-f]{12}")))
            {
                TempData["epostaOnayMesaj"] = "Hesap geçerli değil. Lütfen e-posta adresinizdeki linke tekrar tıklayınız.";

                return View();
            }
            else
            {
                var kullanici = kullaniciServis.Bul(onayKodu);

                if (!kullanici.Onayli)
                {
                    kullanici.Onayli = true;
                    kullaniciServis.Guncelle(kullanici);
                    FormsAuthentication.SetAuthCookie(kullanici.Eposta, true);
                    TempData["epostaOnayMesaj"] = "E-posta adresinizi onayladığınız için teşekkürler. Artık sitemize üyesiniz.";

                    return RedirectToAction("HosGeldiniz");
                }
                else
                {
                    TempData["epostaOnayMesaj"] = "E-posta adresiniz zaten onaylı. Giriş yapabilirsiniz.";

                    return RedirectToAction("GirisYap");
                }
            }
        }

        public ActionResult HosGeldiniz()
        {
            return View();
        }

        #region yardımcı fonksiyonlar
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Anasayfa");
            }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
