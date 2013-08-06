using HaberSitesi.Domain.DomainModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaberSitesi.Web.Areas.Admin.Models
{
    public class KullaniciRolModel
    {
        public Kullanici Kullanici { get; set; }

        [Display(Name = "Roller")]
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        public int[] SecilenRoller { get; set; }

        public IEnumerable<Rol> Roller { get; set; }
    }
}