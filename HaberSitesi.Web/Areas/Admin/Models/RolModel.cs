using System.ComponentModel.DataAnnotations;

namespace HaberSitesi.Web.Areas.Admin.Models
{
    public class RolModel
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "{0} alanı en fazla {1}, en az {2} karakter uzunluğunda olmalıdır!", MinimumLength = 3)]
        [Required(ErrorMessage = "{0} alanı gereklidir!")]
        [Display(Name = "Rol Adı")]
        [System.Web.Mvc.Remote("RolVarmi", "Rol", "Admin")]
        public string Ad { get; set; }
    }
}