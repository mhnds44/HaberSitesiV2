using HaberSitesi.Service;
using System.Web.Mvc;

namespace HaberSitesi.Web.Controllers
{
    public class AnaController : Controller
    {
        private KullaniciServis kullaniciServis;

        public AnaController()
        {
            this.kullaniciServis = new KullaniciServis();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.AktifKullanici = kullaniciServis.AktifKullanici(KullaniciEposta);
            base.OnActionExecuting(filterContext);
        }

        protected bool KullaniciAktifMi
        {
            get
            {
                return System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        protected string KullaniciEposta
        {
            get
            {
                return KullaniciAktifMi ? System.Web.HttpContext.Current.User.Identity.Name : null;
            }
        }
    }
}
