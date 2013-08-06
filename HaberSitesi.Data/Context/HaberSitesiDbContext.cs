
using HaberSitesi.Data.DbContextMapping;
using HaberSitesi.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
namespace HaberSitesi.Data.Context
{
    public class HaberSitesiDbContext : DbContext
    {
        public HaberSitesiDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer(new SeedHaberSitesiDbContext());
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Ayarlar> Ayarlar { get; set; }
        public DbSet<Etiket> Etiket { get; set; }
        public DbSet<Galeri> Galeri { get; set; }
        public DbSet<Haber> Haber { get; set; }
        public DbSet<HaberPozisyon> HaberPozisyon { get; set; }
        public DbSet<HaberTipi> HaberTipi { get; set; }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Resim> Resim { get; set; }
        public DbSet<Yorum> Yorum { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new KullaniciMapping());
            modelBuilder.Configurations.Add(new HaberMapping());

            base.OnModelCreating(modelBuilder);
        }

        public class SeedHaberSitesiDbContext : CreateDatabaseIfNotExists<HaberSitesiDbContext>
        {
            protected override void Seed(HaberSitesiDbContext context)
            {
                Kullanici kullanici = new Kullanici { KullaniciAdi = "Yönetici", Sifre = "Admin", Eposta = "admin@gmail.com", OnayKodu = Guid.NewGuid(), Onayli = true };

                List<Rol> roller = new List<Rol>
                {
                    new Rol { Ad = "Admin" },
                    new Rol { Ad = "Moderator" },
                    new Rol { Ad = "Editor" },
                    new Rol { Ad = "Yazar" }
                };

                // roller
                foreach (var item in roller)
                {
                    kullanici.Roller.Add(item);
                }

                // kullanıcı
                context.Kullanici.Add(kullanici);

                // haber tipleri
                context.HaberTipi.Add(new HaberTipi { Ad = "Haber", Id = 1 });
                context.HaberTipi.Add(new HaberTipi { Ad = "Köşe Yazısı", Id = 2 });

                // haber posizyonları
                context.HaberPozisyon.Add(new HaberPozisyon { Ad = "Slider", Id = 1 });
                context.HaberPozisyon.Add(new HaberPozisyon { Ad = "Manşet Sol", Id = 2 });

                // kategoriler
                context.Kategori.Add(new Kategori { Ad = "GÜNDEM", SeoAd = "gundem", SiraNo = 0, AnaMenu = true });
                context.Kategori.Add(new Kategori { Ad = "DÜNYA", SeoAd = "dunya", SiraNo = 1, AnaMenu = true });
                context.Kategori.Add(new Kategori { Ad = "EKONOMİ", SeoAd = "ekonomi", SiraNo = 2, AnaMenu = true });
                context.Kategori.Add(new Kategori { Ad = "SİYASET", SeoAd = "siyaset", SiraNo = 3, AnaMenu = true });
                context.Kategori.Add(new Kategori { Ad = "SPOR", SeoAd = "spor", SiraNo = 4, AnaMenu = true });
                context.Kategori.Add(new Kategori { Ad = "EĞİTİM", SeoAd = "egitim", SiraNo = 5, AnaMenu = true });
                context.Kategori.Add(new Kategori { Ad = "TEKNOLOJİ", SeoAd = "teknoloji", SiraNo = 6, AnaMenu = true });
                context.Kategori.Add(new Kategori { Ad = "KÜLTÜR", SeoAd = "kultur", SiraNo = 7, AnaMenu = true });
                context.Kategori.Add(new Kategori { Ad = "AİLE-SAĞLIK", SeoAd = "aile-saglik", SiraNo = 8, AnaMenu = true });
                context.Kategori.Add(new Kategori { Ad = "MAGAZİN", SeoAd = "magazin", SiraNo = 9, AnaMenu = true });

                // örnek etiketler
                context.Etiket.Add(new Etiket { Ad = "haber" });
                context.Etiket.Add(new Etiket { Ad = "yazılım" });
                context.Etiket.Add(new Etiket { Ad = "bilişim" });
                context.Etiket.Add(new Etiket { Ad = "gündem" });
                context.Etiket.Add(new Etiket { Ad = "istanbul" });
                context.Etiket.Add(new Etiket { Ad = "iç politika" });
                context.Etiket.Add(new Etiket { Ad = "başbakan" });
                context.Etiket.Add(new Etiket { Ad = "ekonomi" });
                context.Etiket.Add(new Etiket { Ad = "enerji" });

                base.Seed(context);
            }
        }
    }
}
