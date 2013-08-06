using HaberSitesi.Domain.DomainModel;
using System.Data.Entity.ModelConfiguration;

namespace HaberSitesi.Data.DbContextMapping
{
    public class KullaniciMapping : EntityTypeConfiguration<Kullanici>
    {
        public KullaniciMapping()
        {
            // kullanici-rol ara tablosu oluşturma
            HasMany(h => h.Roller).
            WithMany(e => e.Kullanicilar).
            Map(
                m =>
                {
                    m.MapLeftKey("KullaniciId");
                    m.MapRightKey("RolId");
                    m.ToTable("KullaniciRol");
                }
            );
        }
    }
}
