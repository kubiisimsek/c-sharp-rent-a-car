using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyon.Entity
{
    class E_Sube
    {
        int _ID;
        string _Eposta;
        string _Sifre;
        string _Ad;
        string _Sehir;
        string _Ilce;
        string _Semt;
        string _Telefon;
        string _Adres;

        public string Eposta { get => _Eposta; set => _Eposta = value; }
        public string Sifre { get => _Sifre; set => _Sifre = value; }
        public string Ad { get => _Ad; set => _Ad = value; }
        public string Sehir { get => _Sehir; set => _Sehir = value; }
        public string Ilce { get => _Ilce; set => _Ilce = value; }
        public string Semt { get => _Semt; set => _Semt = value; }
        public string Telefon { get => _Telefon; set => _Telefon = value; }
        public string Adres { get => _Adres; set => _Adres = value; }
        public int ID { get => _ID; set => _ID = value; }
    }
}
