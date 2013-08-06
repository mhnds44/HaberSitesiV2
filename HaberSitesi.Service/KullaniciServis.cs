using HaberSitesi.Data.Context;
using HaberSitesi.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;

namespace HaberSitesi.Service
{
    public class KullaniciServis
    {
        private HaberSitesiDbContext db;

        public KullaniciServis(HaberSitesiDbContext db)
        {
            this.db = db;
        }

        public KullaniciServis()
        {
            this.db = new HaberSitesiDbContext();
        }

        public int KullaniciOlustur(Kullanici kullanici)
        {
            db.Kullanici.Add(kullanici);
            return db.SaveChanges();
        }


        public bool EpostaDogrula(string eposta)
        {
            bool varmi = db.Kullanici
                .Any(x => x.Eposta == eposta);

            return varmi;
        }

        public bool KullaniciDogrula(string eposta, string sifre)
        {
            var varmi = db.Kullanici
                .Any(x => x.Eposta == eposta && x.Sifre == sifre);

            return varmi;
        }

        public IEnumerable<Kullanici> Kullanicilar()
        {
            return db.Kullanici;
        }

        public Kullanici Bul(int id)
        {
            return db.Kullanici.Find(id);
        }

        public Kullanici Bul(string eposta)
        {
            return db.Kullanici.FirstOrDefault(x => x.Eposta == eposta);
        }

        public SayfalanmisListe<Kullanici> Kullanicilar(int page, int rows)
        {
            SayfalanmisListe<Kullanici> kullanicilar = new SayfalanmisListe<Kullanici>();
            int pageIndex = page - 1;
            int pageSize = rows;

            kullanicilar.KayitSayisi = db.Kullanici.Count();
            kullanicilar.KaynakListe = db.Kullanici
                 .OrderBy(x => x.Id)
                 .Skip(pageIndex * pageSize)
                 .Take(pageSize)
                 .ToList();

            return kullanicilar;
        }

        public Kullanici AktifKullanici(string eposta)
        {
            return db.Kullanici
                .FirstOrDefault(x => x.Eposta == eposta);
        }

        public IEnumerable<Kullanici> RolKullanicilar(string rol)
        {
            var Rol = db.Rol.FirstOrDefault(x => x.Ad == rol);
            return Rol.Kullanicilar;
        }

        public bool KullaniciSil(int id)
        {
            var silmeBasarilimi = false;
            var kullanici = db.Kullanici.Find(id);
            try
            {
                db.Kullanici.Remove(kullanici);
                db.SaveChanges();
                silmeBasarilimi = true;
            }
            catch (Exception ex)
            {
                silmeBasarilimi = false;
            }

            return silmeBasarilimi;
        }

        public void OnayMesajiGonder(string eposta, string onayUrl)
        {
            var kullanici = Bul(eposta);
            string onayKodu = kullanici.OnayKodu.ToString();
            onayUrl += "/Uyelik/EpostaOnay?onayKodu=" + onayKodu;

            var message = new MailMessage("info@belleksizintisi.com", eposta)
            {
                Subject = "Lütfen e-posta adresinizi onaylayınız.",
                Body = onayUrl
            };

            var client = new SmtpClient();

            client.Send(message);
        }

        public Kullanici Bul(Guid onayKodu)
        {
            return db.Kullanici.FirstOrDefault(x => x.OnayKodu == onayKodu);
        }

        public int Guncelle(Kullanici kullanici)
        {
            db.Entry(kullanici).State = EntityState.Modified;
            return db.SaveChanges();
        }
    }
}
