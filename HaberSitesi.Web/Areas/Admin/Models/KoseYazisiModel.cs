using HaberSitesi.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace HaberSitesi.Web.Areas.Admin.Models
{
    public class KoseYazisiModel
    {
        public int Id { get; set; }

        [StringLength(150, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 5)]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Başlık")]
        public string Baslik { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "İçerik")]
        [DataType(DataType.MultilineText)]
        public string Icerik { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<DateTime> OlusturmaTarihi { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<DateTime> DegistirmeTarihi { get; set; }

        [ScaffoldColumn(false)]
        public Nullable<DateTime> YayinlanmaTarihi { get; set; }

        [ScaffoldColumn(false)]
        public int OlusturmaKullaniciId { get; set; }

        [ScaffoldColumn(false)]
        public int DegistirmeKullaniciId { get; set; }

        [ScaffoldColumn(false)]
        public int YayinlamaKullaniciId { get; set; }

        [Display(Name = "Yazar")]
        public Nullable<int> YazarId { get; set; }

        [Display(Name = "Yayında?")]
        public bool Yayinda { get; set; }

        [ScaffoldColumn(false)]
        public int HaberTipId { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Kategori")]
        public int KategoriId { get; set; }

        public IEnumerable<Kategori> Kategoriler { get; set; }
        public IEnumerable<Kullanici> Yazarlar { get; set; }
    }
}