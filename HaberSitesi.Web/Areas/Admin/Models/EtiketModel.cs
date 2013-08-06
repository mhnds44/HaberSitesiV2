using System.ComponentModel.DataAnnotations;

namespace HaberSitesi.Web.Areas.Admin.Models
{
    public class EtiketModel
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Etiket Adı")]
        [System.Web.Mvc.Remote("EtiketVarmi", "Etiket", "Admin")]
        public string Ad { get; set; }
    }
}