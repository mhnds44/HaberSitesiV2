using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaberSitesi.Domain.DomainModel
{
    [Table("Kullanici")]
    public class Kullanici
    {
        public Kullanici()
        {
            this.Roller = new HashSet<Rol>();
            this.Yorumlar = new HashSet<Yorum>();
            this.OlusturulanHaberler = new HashSet<Haber>();
            this.DegistirilenHaberler = new HashSet<Haber>();
            this.YayinlananHaberler = new HashSet<Haber>();
            this.YazilanHaberler = new HashSet<Haber>();
        }

        public int Id { get; set; }

        [StringLength(150)]
        public string KullaniciAdi { get; set; }

        [StringLength(50)]
        public string Sifre { get; set; }

        [StringLength(150)]
        public string Eposta { get; set; }

        [StringLength(250)]
        public string OrjinalProfilResim { get; set; }

        [StringLength(250)]
        public string KucukProfilResim { get; set; }

        public Guid OnayKodu { get; set; }
        public bool Onayli { get; set; }

        public Nullable<DateTime> KayitTarihi { get; set; }
        public Nullable<DateTime> GuncellemeTarihi { get; set; }

        public virtual ICollection<Rol> Roller { get; set; }
        public virtual ICollection<Yorum> Yorumlar { get; set; }
        public virtual ICollection<Haber> OlusturulanHaberler { get; set; }
        public virtual ICollection<Haber> DegistirilenHaberler { get; set; }
        public virtual ICollection<Haber> YayinlananHaberler { get; set; }
        public virtual ICollection<Haber> YazilanHaberler { get; set; }
    }
}
