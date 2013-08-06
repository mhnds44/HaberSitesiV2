using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaberSitesi.Domain.DomainModel
{
    [Table("Galeri")]
    public class Galeri
    {
        public Galeri()
        {
            this.Resimler = new HashSet<Resim>();
        }

        public int Id { get; set; }

        [StringLength(250)]
        public string Ad { get; set; }

        public Nullable<int> HaberId { get; set; }

        [ForeignKey("HaberId")]
        public virtual Haber Haber { get; set; }

        public virtual ICollection<Resim> Resimler { get; set; }
    }
}
