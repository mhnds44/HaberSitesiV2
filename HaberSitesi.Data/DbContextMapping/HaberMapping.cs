using HaberSitesi.Domain.DomainModel;
using System.Data.Entity.ModelConfiguration;

namespace HaberSitesi.Data.DbContextMapping
{
    public class HaberMapping : EntityTypeConfiguration<Haber>
    {
        public HaberMapping()
        {
            HasMany(h => h.Etiketler).
            WithMany(e => e.Haberler).
            Map(
                m =>
                {
                    m.MapLeftKey("HaberId");
                    m.MapRightKey("EtiketId");
                    m.ToTable("HaberEtiket");
                }
            );

            // haber-yorum ayarı. yorum tablosu
            // hem haber tablosuna hemde kullanıcı tablosuna
            // bağlı. varsayılan olarak delete-on-cascade
            // tanımlı oldugundan, kullanıcı silindiginde mi
            // yoksa haber silinde mi yorumlar silinecek onun 
            // ayarı. kullanıcı silinince yorum kalsın diyoruz.
            HasMany(x => x.Yorumlar)
                .WithRequired(x => x.Haber)
                .HasForeignKey(x => x.HaberId)
                .WillCascadeOnDelete(false);

            // birden fazla foreign_key için
            HasRequired(m => m.OlusturmaKullanici)
                    .WithMany(t => t.OlusturulanHaberler)
                    .HasForeignKey(m => m.OlusturmaKullaniciId)
                    .WillCascadeOnDelete(false);
            HasRequired(m => m.DegistirmeKullanici)
                    .WithMany(t => t.DegistirilenHaberler)
                    .HasForeignKey(m => m.DegistirmeKullaniciId)
                    .WillCascadeOnDelete(false);
            HasRequired(m => m.YayinlamaKullanici)
                    .WithMany(t => t.YayinlananHaberler)
                    .HasForeignKey(m => m.YayinlamaKullaniciId)
                    .WillCascadeOnDelete(false);
        }
    }
}
