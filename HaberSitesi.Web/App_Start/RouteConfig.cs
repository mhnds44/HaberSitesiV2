using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HaberSitesi.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "KategoriDetay",
                url: "KategoriDetay/{kategoriAd}",
                defaults: new { controller = "Kategori", action = "KategoriDetay", kategoriAd = UrlParameter.Optional },
                namespaces: new[] { "HaberSitesi.Web.Controllers" }
            );

            routes.MapRoute(
                name: "HaberDetay",
                url: "HaberDetay/{kategori}/{id}/{seoBaslik}",
                defaults: new { controller = "Haber", action = "HaberDetay", kategori = UrlParameter.Optional, id = UrlParameter.Optional, seoBaslik = UrlParameter.Optional },
                namespaces: new[] { "HaberSitesi.Web.Controllers" }
            );

            routes.MapRoute(
                name: "KoseYazisiDetay",
                url: "KoseYazisiDetay/{kategori}/{id}/{seoBaslik}",
                defaults: new { controller = "KoseYazisi", action = "KoseYazisiDetay", kategori = UrlParameter.Optional, id = UrlParameter.Optional, seoBaslik = UrlParameter.Optional },
                namespaces: new[] { "HaberSitesi.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Anasayfa", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "HaberSitesi.Web.Controllers" }
            );
        }
    }
}