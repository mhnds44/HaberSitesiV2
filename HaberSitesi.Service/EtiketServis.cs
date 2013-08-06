using HaberSitesi.Data.Context;
using HaberSitesi.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HaberSitesi.Service
{
    public class EtiketServis
    {
        private HaberSitesiDbContext db;

        public EtiketServis(HaberSitesiDbContext db)
        {
            this.db = db;
        }

        public int Ekle(Etiket etiket)
        {
            db.Etiket.Add(etiket);
            return db.SaveChanges();
        }

        public Etiket Bul(int id)
        {
            return db.Etiket.Find(id);
        }

        public int Guncelle(Etiket etiket)
        {
            db.Entry(etiket).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public bool EtiketSil(int id)
        {
            var silmeBasarilimi = false;
            var etiket = db.Etiket.Find(id);
            try
            {
                db.Etiket.Remove(etiket);
                db.SaveChanges();
                silmeBasarilimi = true;
            }
            catch (Exception ex)
            {
                silmeBasarilimi = false;
            }

            return silmeBasarilimi;
        }

        public bool EtiketVarmi(string Ad)
        {
            bool varmi = db.Etiket
                .Any(x => x.Ad.Trim().ToLower() == Ad.Trim().ToLower());

            return varmi;
        }

        public IEnumerable<Etiket> Etiketler()
        {
            return db.Etiket;
        }

        public SayfalanmisListe<Etiket> Etiketler(int page, int rows)
        {
            SayfalanmisListe<Etiket> etiketler = new SayfalanmisListe<Etiket>();
            int pageIndex = page - 1;
            int pageSize = rows;

            etiketler.KayitSayisi = db.Etiket.Count();
            etiketler.KaynakListe = db.Etiket
                .OrderBy(x => x.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList();

            return etiketler;
        }

        public IEnumerable<Etiket> Etiketler(int[] ids)
        {
            return db.Etiket
                .Where(x => ids.Contains(x.Id));
        }
    }
}
