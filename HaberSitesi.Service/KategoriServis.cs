using HaberSitesi.Data.Context;
using HaberSitesi.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HaberSitesi.Service
{
    public class KategoriServis
    {
        private HaberSitesiDbContext db;

        public KategoriServis(HaberSitesiDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Kategori> Kategoriler()
        {
            return db.Kategori
                .OrderBy(x=>x.SiraNo);
        }

        public int Ekle(Kategori kategori)
        {
            db.Kategori.Add(kategori);
            return db.SaveChanges();
        }

        public void DuzeyDegistir(int id, bool durum)
        {
            Kategori kategori = db.Kategori.Find(id);

            kategori.AnaMenu = durum;
            db.SaveChanges();
        }

        public Kategori Bul(int id)
        {
            return db.Kategori.Find(id);
        }

        public Kategori Bul(string seoAd)
        {
            return db.Kategori
                .FirstOrDefault(x => x.SeoAd == seoAd);
        }

        public int Guncelle(Kategori kategori)
        {
            db.Entry(kategori).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public bool KategoriSil(int id)
        {
            var silmeBasarilimi = false;
            var kategori = db.Kategori.Find(id);
            try
            {
                db.Kategori.Remove(kategori);
                db.SaveChanges();
                silmeBasarilimi = true;
            }
            catch (Exception ex)
            {
                silmeBasarilimi = false;
            }

            return silmeBasarilimi;
        }

        public bool KategoriVarmi(string Ad)
        {
            bool varmi = db.Kategori
                .Any(x => x.Ad.Trim().ToLower() == Ad.Trim().ToLower());

            return varmi;
        }

        public SayfalanmisListe<Kategori> Kategoriler(int page, int rows)
        {
            SayfalanmisListe<Kategori> kategoriler = new SayfalanmisListe<Kategori>();
            int pageIndex = page - 1;
            int pageSize = rows;

            kategoriler.KayitSayisi = db.Kategori.Count();
            kategoriler.KaynakListe = db.Kategori
                 .OrderBy(x => x.Id)
                 .Skip(pageIndex * pageSize)
                 .Take(pageSize)
                 .ToList();

            return kategoriler;
        }

        public IEnumerable<Kategori> AnaKategoriler()
        {
            return db.Kategori
                .Where(x => x.AnaMenu)
                .OrderBy(x => x.SiraNo);
        }
    }
}
