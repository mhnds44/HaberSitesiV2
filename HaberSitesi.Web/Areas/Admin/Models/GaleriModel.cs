using HaberSitesi.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HaberSitesi.Web.Areas.Admin.Models
{
    public class GaleriModel
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Galeri Adı")]
        [System.Web.Mvc.Remote("GaleriVarmi", "Galeri", "Admin")]
        public string Ad { get; set; }

        [Display(Name="Haber")]
        public Nullable<int> HaberId { get; set; }

        public virtual IEnumerable<Haber> Haberler{ get; set; }
    }
}