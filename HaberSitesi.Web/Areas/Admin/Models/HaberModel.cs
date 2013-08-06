using HaberSitesi.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace HaberSitesi.Web.Areas.Admin.Models
{
    public class HaberModel
    {
        public int Id { get; set; }

        [StringLength(150, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 5)]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Başlık")]
        public string Baslik { get; set; }

        [StringLength(400, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 5)]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Açıklama")]
        [DataType(DataType.MultilineText)]
        public string Aciklama { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "İçerik")]
        [DataType(DataType.MultilineText)]
        public string Icerik { get; set; }

        [ScaffoldColumn(false)]
        public string TumEtiketler { get; set; }

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

        [StringLength(50, ErrorMessage = "{0} alanı en fazla {1} karakter uzunluğunda olmalıdır!")]
        [Display(Name = "Kaynak")]
        public string Kaynak { get; set; }

        [Display(Name = "Yayında?")]
        public bool Yayinda { get; set; }

        [StringLength(250)]
        [ScaffoldColumn(false)]
        public string OrjinalProfilResim { get; set; }

        [StringLength(250)]
        [ScaffoldColumn(false)]
        public string BuyukProfilResim { get; set; }

        [StringLength(250)]
        [ScaffoldColumn(false)]
        public string KucukProfilResim { get; set; }

        [ScaffoldColumn(false)]
        public int HaberTipId { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Kategori")]
        public int KategoriId { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Haber Pozisyon")]
        public Nullable<int> HaberPozisyonId { get; set; }

        [Display(Name = "Resim")]
        public HttpPostedFileBase Resim { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Etiketler")]
        public int[] SecilenEtiketler { get; set; }

        public IEnumerable<Kategori> Kategoriler { get; set; }
        public IEnumerable<HaberPozisyon> HaberPozisyonlar { get; set; }
        public IEnumerable<Etiket> Etiketler { get; set; }
    }
}