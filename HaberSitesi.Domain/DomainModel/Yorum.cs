using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberSitesi.Domain.DomainModel
{
    [Table("Yorum")]
    public class Yorum
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string YorumcuAd { get; set; }

        [StringLength(500)]
        public string YorumMetin { get; set; }

        public DateTime OlusturmaTarihi { get; set; }
        public bool Onayli { get; set; }
        public bool Aktif { get; set; }

        [StringLength(32)]
        public string IP { get; set; }

        public Nullable<int> KullaniciId { get; set; }
        public int HaberId { get; set; }

        [ForeignKey("KullaniciId")]
        public virtual Kullanici Kullanici { get; set; }

        [ForeignKey("HaberId")]
        public virtual Haber Haber { get; set; }
    }
}
