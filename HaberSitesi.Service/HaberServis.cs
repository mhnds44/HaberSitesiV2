using HaberSitesi.Data.Context;
using HaberSitesi.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HaberSitesi.Service
{
    public class HaberServis
    {
        private HaberSitesiDbContext db;
        private KategoriServis kategoriServis;

        public HaberServis(HaberSitesiDbContext db)
        {
            this.db = db;
            this.kategoriServis = new KategoriServis(db);
        }

        public IEnumerable<Haber> Haberler()
        {
            return db.Haber
                .Where(x=>x.HaberTipId == 1);
        }

        public IEnumerable<HaberPozisyon> HaberPozisyonlar()
        {
            return db.HaberPozisyon;
        }

        public SayfalanmisListe<Haber> Haberler(int page, int rows)
        {
            SayfalanmisListe<Haber> haberler = new SayfalanmisListe<Haber>();
            int pageIndex = page - 1;
            int pageSize = rows;

            haberler.KayitSayisi = db.Haber.Count();
            haberler.KaynakListe = db.Haber
                 .Where(x => x.HaberTipId == 1)
                 .OrderBy(x => x.Id)
                 .Skip(pageIndex * pageSize)
                 .Take(pageSize)
                 .ToList();

            return haberler;
        }

        public int Ekle(Haber haber)
        {
            db.Haber.Add(haber);
            return db.SaveChanges();
        }

        public Haber Bul(int haberId)
        {
            return db.Haber.Find(haberId);
        }

        public int Guncelle(Haber haber)
        {
            db.Entry(haber).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public bool HaberSil(int id)
        {
            var silmeBasarilimi = false;
            var haber = db.Haber.Find(id);
            try
            {
                db.Haber.Remove(haber);
                db.SaveChanges();
                silmeBasarilimi = true;
            }
            catch (Exception ex)
            {
                silmeBasarilimi = false;
            }

            return silmeBasarilimi;
        }

        public void HaberYayinDegistir(int id, bool durum, int aktifKullaniciId)
        {
            var haber = db.Haber.Find(id);
            haber.Yayinda = !durum;
            haber.YayinlanmaTarihi = DateTime.Now;
            haber.YayinlamaKullaniciId = aktifKullaniciId;
            db.SaveChanges();
        }

        public SayfalanmisListe<Haber> KoseYazilari(int page, int rows)
        {
            SayfalanmisListe<Haber> koseYazilari = new SayfalanmisListe<Haber>();
            int pageIndex = page - 1;
            int pageSize = rows;

            koseYazilari.KayitSayisi = db.Haber.Count();
            koseYazilari.KaynakListe = db.Haber
                 .Where(x => x.HaberTipId == 2)
                 .OrderBy(x => x.Id)
                 .Skip(pageIndex * pageSize)
                 .Take(pageSize)
                 .ToList();

            return koseYazilari;
        }

        public IEnumerable<Haber> KoseYazilari(int kayitSayisi)
        {
            return db.Haber
                .Where(x => x.HaberTipId == 2 && x.Yayinda)
                .OrderByDescending(x => x.YayinlanmaTarihi)
                .Take(kayitSayisi);
        }

        public IEnumerable<Haber> PozisyonHaberler(int pozisyonId, int kayitSayisi)
        {
            return db.Haber
                .Where(x => x.HaberPozisyonId == pozisyonId && x.Yayinda)
                .OrderByDescending(x => x.YayinlanmaTarihi)
                .Take(kayitSayisi);
        }

        public void OkunmaSayisiArtir(Haber haber)
        {
            haber.OkunmaSayisi++;
            db.Entry(haber).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IEnumerable<Haber> EnCokOkunanHaberler(int haberTipId, int kayitSayisi)
        {
            return db.Haber
                .Where(x => x.HaberTipId == haberTipId && x.Yayinda)
                .OrderByDescending(x => x.OkunmaSayisi)
                .Take(kayitSayisi);
        }

        public IEnumerable<Haber> KategoriHaberler(int kategoriId, int kayitSayisi)
        {
            var kategori = kategoriServis.Bul(kategoriId);

            return kategori.Haberler
                .Where(x => x.Yayinda && x.HaberTipId == 1)
                .OrderByDescending(x => x.YayinlanmaTarihi)
                .Take(kayitSayisi);
        }

        public IEnumerable<Haber> EnCokOkunanHaberler(int kategoriId, int haberTipId, int kayitSayisi)
        {
            var kategori = kategoriServis.Bul(kategoriId);

            return kategori.Haberler
               .Where(x => x.HaberTipId == haberTipId && x.Yayinda)
               .OrderByDescending(x => x.OkunmaSayisi)
               .Take(kayitSayisi);
        }
    }
}
