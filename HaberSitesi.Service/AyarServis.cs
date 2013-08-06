using HaberSitesi.Data.Context;
using HaberSitesi.Domain.DomainModel;
using System.Linq;

namespace HaberSitesi.Service
{
    public class AyarServis
    {
        private HaberSitesiDbContext db;

        public AyarServis(HaberSitesiDbContext db)
        {
            this.db = db;
        }

        public Ayarlar Ayarlar()
        {
            var ayarlar = db.Ayarlar.FirstOrDefault();
            return ayarlar;
        }
    }
}
