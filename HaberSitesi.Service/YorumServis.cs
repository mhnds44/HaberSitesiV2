using HaberSitesi.Data.Context;
using HaberSitesi.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberSitesi.Service
{
    public class YorumServis
    {
        private HaberSitesiDbContext db;

        public YorumServis(HaberSitesiDbContext db)
        {
            this.db = db;
        }

        public int Ekle(Yorum yorum)
        {
            db.Yorum.Add(yorum);
            return db.SaveChanges();
        }

        public IEnumerable<Yorum> TumHaberYorumlar(int haberId, int kayitSayisi)
        {
            return db.Yorum
                .Where(x => x.HaberId == haberId)
                .OrderByDescending(x => x.Id)
                .Take(kayitSayisi);
        }

        public IEnumerable<Yorum> HaberYorumlar(int haberId)
        {
            return db.Yorum
                .Where(x => x.HaberId == haberId && x.Onayli && x.Aktif)
                .OrderByDescending(x => x.Id);
        }

        public IEnumerable<Yorum> HaberYorumlar(int haberId, int kayitSayisi)
        {
            return db.Yorum
                .Where(x => x.HaberId == haberId && x.Onayli && x.Aktif)
                .OrderByDescending(x => x.Id)
                .Take(kayitSayisi);
        }

        public int HaberYorumSayisi(int haberId)
        {
            return db.Yorum.Where(x => x.HaberId == haberId && x.Onayli && x.Aktif)
                .Count();
        }
    }
}
