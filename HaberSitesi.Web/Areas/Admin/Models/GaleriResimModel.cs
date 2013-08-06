
using HaberSitesi.Domain.DomainModel;
using System.Collections.Generic;
using System.Web;
namespace HaberSitesi.Web.Areas.Admin.Models
{
    public class GaleriResimModel
    {
        public Galeri Galeri{ get; set; }

        public IEnumerable<HttpPostedFileBase> Resimler { get; set; }
    }
}