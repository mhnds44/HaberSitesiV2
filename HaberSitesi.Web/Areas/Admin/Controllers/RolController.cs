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
    public class RolController : AnaController
    {
        private HaberSitesiDbContext db;
        private RolServis rolServis;

        public RolController()
        {
            this.db = new HaberSitesiDbContext();
            this.rolServis = new RolServis(db);
        }

        public ActionResult Roller()
        {
            return View();
        }

        public ActionResult RolEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RolEkle(RolModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Rol rol = Mapper.Map<RolModel, Rol>(model);
                    rolServis.Ekle(rol);

                    return RedirectToAction("Roller");
                }
                catch (Exception ex)
                {

                }
            }
            return View(model);
        }

        public ActionResult RolDuzenle(int id)
        {
            Rol rol = rolServis.Bul(id);
            RolModel model = Mapper.Map<Rol, RolModel>(rol);

            return View(model);
        }

        [HttpPost]
        public ActionResult RolDuzenle(RolModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Rol rol = rolServis.Bul(model.Id);
                    rol = (Rol)Mapper.Map(model, rol, typeof(RolModel), typeof(Rol));
                    rolServis.Guncelle(rol);

                    return RedirectToAction("Roller");
                }
                catch (Exception ex)
                {

                }
            }
            return View(model);
        }

        public ActionResult RolSil(int id)
        {
            rolServis.RolSil(id);
            return RedirectToAction("Roller");
        }

        public JsonResult RolVarmi(string Ad)
        {
            var result = rolServis.RolVarmi(Ad);

            if (result)
            {
                return Json("Girdiğiniz rol sistemde zaten mevcut!", JsonRequestBehavior.AllowGet);
            }

            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RollerJson(int page, int rows, string sort, string order)
        {
            var roller = rolServis.Roller(page, rows);

            var result = new
            {
                total = roller.KayitSayisi,
                rows = roller.KaynakListe.Select(x => new
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
