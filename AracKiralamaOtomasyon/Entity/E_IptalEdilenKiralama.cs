using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyon.Entity
{
    class E_IptalEdilenKiralama
    {
        string _TakipKod;
        string _Sebep;
        string _Aciklama;

        public string TakipKod { get => _TakipKod; set => _TakipKod = value; }
        public string Sebep { get => _Sebep; set => _Sebep = value; }
        public string Aciklama { get => _Aciklama; set => _Aciklama = value; }
    }
}
