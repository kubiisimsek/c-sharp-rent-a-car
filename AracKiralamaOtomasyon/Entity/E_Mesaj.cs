using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyon.Entity
{
    class E_Mesaj
    {
        int _SubeID;
        string _SubeAd;
        string _UyeTC;
        string _Konu;
        string _Mesaj;
        DateTime _Tarih;


        public int SubeID { get => _SubeID; set => _SubeID = value; }
        public string UyeTC { get => _UyeTC; set => _UyeTC = value; }
        public string Mesaj { get => _Mesaj; set => _Mesaj = value; }
        public string Konu { get => _Konu; set => _Konu = value; }
        public string SubeAd { get => _SubeAd; set => _SubeAd = value; }
        public DateTime Tarih { get => _Tarih; set => _Tarih = value; }
    }
}
