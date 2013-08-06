using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HaberSitesi.Domain.DomainModel
{
    [Table("Ayarlar")]
    public class Ayarlar
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string UygulamaAdi { get; set; }
    }
}

