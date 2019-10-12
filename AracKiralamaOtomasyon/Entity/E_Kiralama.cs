using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyon.Entity
{
    class E_Kiralama
    {
        string _TakipKod;
        string _TC;
        string _Plaka;
        int _OdemeID;
        DateTime _BaslangicTarihi;
        DateTime _BitisTarihi;
        string _Aciklama;
        string _KiraDurumu;
        string _Uyelik;
        string _OdemeTuru;
        double _OdenenUcret;
        DateTime _IslemTarih;

        public string TakipKod { get => _TakipKod; set => _TakipKod = value; }
        public string TC { get => _TC; set => _TC = value; }
        public string Plaka { get => _Plaka; set => _Plaka = value; }
        public int OdemeID { get => _OdemeID; set => _OdemeID = value; }
        public DateTime BaslangicTarihi { get => _BaslangicTarihi; set => _BaslangicTarihi = value; }
        public DateTime BitisTarihi { get => _BitisTarihi; set => _BitisTarihi = value; }
        public string Aciklama { get => _Aciklama; set => _Aciklama = value; }
        public string KiraDurumu { get => _KiraDurumu; set => _KiraDurumu = value; }
        public string Uyelik { get => _Uyelik; set => _Uyelik = value; }
        public DateTime IslemTarih { get => _IslemTarih; set => _IslemTarih = value; }
        public string Sebep { get => _Sebep; set => _Sebep = value; }
        public string OdemeTuru { get => _OdemeTuru; set => _OdemeTuru = value; }
        public double OdenenUcret { get => _OdenenUcret; set => _OdenenUcret = value; }

        string _Sebep; // İptal için
        
    }
}
