using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaberSitesi.Domain.DomainModel
{
    [Table("Resim")]
    public class Resim
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Ad { get; set; }

        [StringLength(25)]
        public string Uzanti { get; set; }
        public int Boyut { get; set; }

        [StringLength(300)]
        public string OrjinalResim { get; set; }

        [StringLength(300)]
        public string BuyukResim { get; set; }

        [StringLength(300)]
        public string KucukResim { get; set; }

        public int GaleriId { get; set; }

        [ForeignKey("GaleriId")]
        public virtual Galeri Galeri { get; set; }
    }
}
