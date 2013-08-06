using System.Web.Mvc;

namespace HaberSitesi.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Anasayfa", id = UrlParameter.Optional },
                new[] { "HaberSitesi.Web.Areas.Admin.Controllers" }
            );
        }
    }
}
