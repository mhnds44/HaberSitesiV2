using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaberSitesi.Domain.DomainModel
{
    [Table("Rol")]
    public class Rol
    {
        public Rol()
        {
            this.Kullanicilar = new HashSet<Kullanici>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Ad { get; set; }

        public virtual ICollection<Kullanici> Kullanicilar { get; set; }
    }
}
