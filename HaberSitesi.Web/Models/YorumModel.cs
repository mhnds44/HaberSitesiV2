using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HaberSitesi.Web.Models
{
    public class YorumModel
    {
        public int Id { get; set; }
        public int HaberId { get; set; }
        public Nullable<int> KullaniciId { get; set; }

        //[StringLength(100, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Adınız")]
        public string YorumcuAd { get; set; }

        //[StringLength(500, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Yorum")]
        public string YorumMetin { get; set; }

        public DateTime OlusturmaTarihi { get; set; }
        public bool Aktif { get; set; }
        public bool Onayli { get; set; }
        public string IP { get; set; }
    }
}