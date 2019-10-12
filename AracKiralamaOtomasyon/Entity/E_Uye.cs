using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyon.Entity
{
    class E_Uye
    {
        string _Eposta;
        string _Sifre;
        string _TC;
        string _Ad;
        string _Soyad;
        DateTime _DogumTarihi;
        char _Cinsiyet;
        string _Sehir;
        string _Adres;
        string _Telefon;
        string _EhliyetSinifi;
        int _EhliyetYili;
        DateTime _KayitTarihi;

        public string Eposta { get => _Eposta; set => _Eposta = value; }
        public string Sifre { get => _Sifre; set => _Sifre = value; }
        public string TC { get => _TC; set => _TC = value; }
        public string Ad { get => _Ad; set => _Ad = value; }
        public string Soyad { get => _Soyad; set => _Soyad = value; }
        public DateTime DogumTarihi { get => _DogumTarihi; set => _DogumTarihi = value; }
        public char Cinsiyet { get => _Cinsiyet; set => _Cinsiyet = value; }
        public string Sehir { get => _Sehir; set => _Sehir = value; }
        public string Adres { get => _Adres; set => _Adres = value; }
        public string Telefon { get => _Telefon; set => _Telefon = value; }
        public string EhliyetSinifi { get => _EhliyetSinifi; set => _EhliyetSinifi = value; }
        public int EhliyetYili { get => _EhliyetYili; set => _EhliyetYili = value; }
        public DateTime KayitTarihi { get => _KayitTarihi; set => _KayitTarihi = value; }
    }
}
