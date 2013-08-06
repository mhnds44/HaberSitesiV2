using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaberSitesi.Domain.DomainModel
{
    public class SayfalanmisListe<T>
    {
        public List<T> KaynakListe { get; set; }
        public int KayitSayisi { get; set; }
    }
}
