using AutoMapper;
using HaberSitesi.Data.Context;
using HaberSitesi.Domain.DomainModel;
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
    public class EtiketController : AnaController
    {
        private HaberSitesiDbContext db;
        private EtiketServis etiketServis;

        public EtiketController()
        {
            this.db = new HaberSitesiDbContext();
            this.etiketServis = new EtiketServis(db);
        }

        public ActionResult Etiketler()
        {
            return View();
        }

        public ActionResult EtiketEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EtiketEkle(EtiketModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Etiket etiket = Mapper.Map<EtiketModel, Etiket>(model);
                    etiketServis.Ekle(etiket);

                    return RedirectToAction("Etiketler");
                }
                catch (Exception ex)
                {

                }
            }
            return View(model);
        }

        public ActionResult EtiketDuzenle(int id)
        {
            Etiket etiket = etiketServis.Bul(id);
            EtiketModel model = Mapper.Map<Etiket, EtiketModel>(etiket);

            return View(model);
        }

        [HttpPost]
        public ActionResult EtiketDuzenle(EtiketModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Etiket etiket = etiketServis.Bul(model.Id);
                    etiket = (Etiket)Mapper.Map(model, etiket, typeof(EtiketModel), typeof(Etiket));
                    etiketServis.Guncelle(etiket);

                    return RedirectToAction("Etiketler");
                }
                catch (Exception ex)
                {

                }
            }
            return View(model);
        }

        public ActionResult EtiketSil(int id)
        {
            etiketServis.EtiketSil(id);
            return RedirectToAction("Etiketler");
        }

        public JsonResult EtiketVarmi(string Ad)
        {
            var result = etiketServis.EtiketVarmi(Ad);

            if (result)
            {
                return Json("Girdiğiniz etiket sistemde zaten mevcut!", JsonRequestBehavior.AllowGet);
            }

            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EtiketlerJson(int page, int rows, string sort, string order)
        {
            var etiketler = etiketServis.Etiketler(page, rows);

            var result = new
            {
                total = etiketler.KayitSayisi,
                rows = etiketler.KaynakListe.Select(x => new
                {
                    Id = x.Id,
                    Ad = x.Ad
                })
                .AsQueryable()
                .OrderBy(sort + " " + order)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
