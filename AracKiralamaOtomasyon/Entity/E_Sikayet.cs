using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyon.Entity
{
    class E_Sikayet
    {

        int _SubeID; // KULLANICI ŞİKAYET İŞLEMLERİ İÇİN
        string _UyeTC;
        string _Konu;
        string _SikayetMesaj;

        public int SubeID { get => _SubeID; set => _SubeID = value; }
        public string UyeTC { get => _UyeTC; set => _UyeTC = value; }
        public string Konu { get => _Konu; set => _Konu = value; }
        public string SikayetMesaj { get => _SikayetMesaj; set => _SikayetMesaj = value; }


        string _UyePosta; //YÖNETİCİ ŞİKAYET İŞLEMLERİ İÇİN
        string _UyeAd;
        string _UyeSoyad;
        string _SubeAd;
        DateTime _Tarih;
        public string UyePosta { get => _UyePosta; set => _UyePosta = value; }
        public string UyeAd { get => _UyeAd; set => _UyeAd = value; }
        public string UyeSoyad { get => _UyeSoyad; set => _UyeSoyad = value; }
        public string SubeAd { get => _SubeAd; set => _SubeAd = value; }
        public DateTime Tarih { get => _Tarih; set => _Tarih = value; }
    }
}
