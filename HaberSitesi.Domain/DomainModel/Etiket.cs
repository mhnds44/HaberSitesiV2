using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaberSitesi.Domain.DomainModel
{
    [Table("Etiket")]
    public class Etiket
    {
        public Etiket()
        {
            this.Haberler = new HashSet<Haber>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Ad { get; set; }

        public virtual ICollection<Haber> Haberler { get; set; }
    }
}
