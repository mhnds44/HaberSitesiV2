using System.ComponentModel.DataAnnotations;

namespace HaberSitesi.Web.Models
{
    public class Uyelik
    {
    }

    public class KayitModel
    {
        [StringLength(150, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 5)]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }

        [StringLength(50, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 5)]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Sifre { get; set; }

        [Compare("Sifre", ErrorMessage = "İki şifre eşleşmiyor!")]
        [Display(Name = "Şifre Tekrar")]
        [DataType(DataType.Password)]
        public string SifreTekrar { get; set; }

        [StringLength(150, ErrorMessage = "{0} alanı en fazla {1} karakter uzunluğunda olmalıdır!")]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} geçersiz!")]
        [Display(Name = "E-Posta")]
        [System.Web.Mvc.Remote("EmailVarmi", "Uyelik")]
        public string Eposta { get; set; }
    }

    public class GirisModel
    {
        [StringLength(50, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 5)]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Sifre { get; set; }

        [StringLength(150, ErrorMessage = "{0} alanı en fazla {1} karakter uzunluğunda olmalıdır!")]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} geçersiz!")]
        [Display(Name = "E Posta")]
        public string Eposta { get; set; }

        [Display(Name = "Beni Hatırla?")]
        public bool BeniHatirla { get; set; }
    }
}