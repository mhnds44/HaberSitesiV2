using HaberSitesi.Data.Context;
using HaberSitesi.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HaberSitesi.Service
{
    public class GaleriServis
    {
        private HaberSitesiDbContext db;

        public GaleriServis(HaberSitesiDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Galeri> Galeriler()
        {
            return db.Galeri;
        }

        public int Ekle(Galeri galeri)
        {
            db.Galeri.Add(galeri);
            return db.SaveChanges();
        }

        public Galeri Bul(int id)
        {
            return db.Galeri.Find(id);
        }

        public int Guncelle(Galeri galeri)
        {
            db.Entry(galeri).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public bool GaleriSil(int id)
        {
            var silmeBasarilimi = false;
            var galeri = db.Galeri.Find(id);
            try
            {
                db.Galeri.Remove(galeri);
                db.SaveChanges();
                silmeBasarilimi = true;
            }
            catch (Exception ex)
            {
                silmeBasarilimi = false;
            }

            return silmeBasarilimi;
        }

        public bool GaleriVarmi(string Ad)
        {
            bool varmi = db.Galeri
                .Any(x => x.Ad.Trim().ToLower() == Ad.Trim().ToLower());

            return varmi;
        }

        public SayfalanmisListe<Galeri> Galeriler(int page, int rows)
        {
            SayfalanmisListe<Galeri> galeriler = new SayfalanmisListe<Galeri>();
            int pageIndex = page - 1;
            int pageSize = rows;

            galeriler.KayitSayisi = db.Galeri.Count();
            galeriler.KaynakListe = db.Galeri
                 .OrderBy(x => x.Id)
                 .Skip(pageIndex * pageSize)
                 .Take(pageSize)
                 .ToList();

            return galeriler;
        }
    }
}
