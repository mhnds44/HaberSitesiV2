using HaberSitesi.Data.Context;
using HaberSitesi.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HaberSitesi.Service
{
    public class RolServis
    {
        private HaberSitesiDbContext db;
        private KullaniciServis kullaniciServis;

        public RolServis(HaberSitesiDbContext db)
        {
            this.db = db;
            this.kullaniciServis = new KullaniciServis(db);
        }

        public RolServis()
        {
            this.db = new HaberSitesiDbContext();
        }

        public bool KullaniciRoldeMi(string eposta, string rolAdi)
        {
            var varmi = false;

            var kullanici = db.Kullanici.FirstOrDefault(x => x.Eposta == eposta);

            if (kullanici != null)
            {
                varmi = kullanici.Roller.Any(x => x.Ad == rolAdi);
            }

            return varmi;
        }

        public string[] KullaniciRolleri(string eposta)
        {
            List<string> roller = new List<string>();
            var kullanici = db.Kullanici.FirstOrDefault(x => x.Eposta == eposta);

            if (kullanici != null)
            {
                foreach (var rol in kullanici.Roller)
                {
                    roller.Add(rol.Ad);
                }
            }

            return roller.ToArray<string>();
        }

        public int Ekle(Rol rol)
        {
            db.Rol.Add(rol);
            return db.SaveChanges();
        }

        public Rol Bul(int id)
        {
            return db.Rol.Find(id);
        }

        public int Guncelle(Rol rol)
        {
            db.Entry(rol).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public bool RolSil(int id)
        {
            var silmeBasarilimi = false;
            var rol = db.Rol.Find(id);
            try
            {
                db.Rol.Remove(rol);
                db.SaveChanges();
                silmeBasarilimi = true;
            }
            catch (Exception ex)
            {
                silmeBasarilimi = false;
            }

            return silmeBasarilimi;
        }

        public bool RolVarmi(string Ad)
        {
            bool varmi = db.Rol
                 .Any(x => x.Ad.Trim().ToLower() == Ad.Trim().ToLower());

            return varmi;
        }

        public IEnumerable<Rol> Roller()
        {
            return db.Rol;
        }

        public IEnumerable<Rol> Roller(int[] roller)
        {
            return db.Rol
                   .Where(x => roller.Contains(x.Id));
        }

        public void KullaniciRolEkle(int kullaniciId, int[] roller)
        {
            var kullanici = kullaniciServis.Bul(kullaniciId);
            var secilenRoller = this.Roller(roller);

            kullanici.Roller.Clear();
            secilenRoller.ToList().ForEach(rol => kullanici.Roller.Add(rol));

            db.SaveChanges();
        }

        public SayfalanmisListe<Rol> Roller(int page, int rows)
        {
            SayfalanmisListe<Rol> roller = new SayfalanmisListe<Rol>();
            int pageIndex = page - 1;
            int pageSize = rows;

            roller.KayitSayisi = db.Rol.Count();
            roller.KaynakListe = db.Rol
                 .OrderBy(x => x.Id)
                 .Skip(pageIndex * pageSize)
                 .Take(pageSize)
                 .ToList();

            return roller;
        }
    }
}
