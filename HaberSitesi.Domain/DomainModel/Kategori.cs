using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaberSitesi.Domain.DomainModel
{
    [Table("Kategori")]
    public class Kategori
    {
        public Kategori()
        {
            this.Haberler = new HashSet<Haber>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Ad { get; set; }

        [StringLength(50)]
        public string SeoAd { get; set; }

        [StringLength(250)]
        public string Aciklama { get; set; }

        public bool AnaMenu { get; set; }
        public int SiraNo { get; set; }

        public virtual ICollection<Haber> Haberler { get; set; }
    }
}
