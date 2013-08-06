using System.ComponentModel.DataAnnotations;

namespace HaberSitesi.Web.Areas.Admin.Models
{
    public class KategoriModel
    {
        public KategoriModel()
        {
            this.SiraNo = 0;
        }

        public int Id { get; set; }

        //[System.Web.Mvc.Remote("KategoriVarmi", "Kategori", "Admin")]
        [StringLength(50, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Kategori Adı")]
        public string Ad { get; set; }

        [StringLength(250, ErrorMessage = "{0} alanı en fazla {1} karakter uzunluğunda olmalıdır!")]
        [Display(Name = "Açıklama")]
        [DataType(DataType.MultilineText)]
        public string Aciklama { get; set; }

        [Display(Name = "Ana Menu?")]
        public bool AnaMenu { get; set; }

        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Sıra No")]
        public int SiraNo { get; set; }
    }
}