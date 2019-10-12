using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AracKiralamaOtomasyon.Entity
{
    class E_Arac
    {
        int _SubeID;
        string _SubeAd;
        string _Plaka;
        string _Renk;
        double _Kilometre;
        string _Donanim;
        byte[] _Resim;
        int _MarkaID;
        string _Marka;
        int _KategoriID;
        string _Kategori;
        string _Model;
        int _ModelYili;
        string _Yakit;
        string _Hacim;
        string _Guc;
        string _Sanziman;
        double _Ucret;
        string _Durum;

        public string Plaka { get => _Plaka; set => _Plaka = value; }
        public string Renk { get => _Renk; set => _Renk = value; }
        public double Kilometre { get => _Kilometre; set => _Kilometre = value; }
        public string Donanim { get => _Donanim; set => _Donanim = value; }
        public byte[] Resim { get => _Resim; set => _Resim = value; }
        public int MarkaID { get => _MarkaID; set => _MarkaID = value; }
        public int KategoriID { get => _KategoriID; set => _KategoriID = value; }
        public string Model { get => _Model; set => _Model = value; }
        public string Yakit { get => _Yakit; set => _Yakit = value; }
        public string Hacim { get => _Hacim; set => _Hacim = value; }
        public string Guc { get => _Guc; set => _Guc = value; }
        public string Sanziman { get => _Sanziman; set => _Sanziman = value; }
        public string Marka { get => _Marka; set => _Marka = value; }
        public string Kategori { get => _Kategori; set => _Kategori = value; }
        public int SubeID { get => _SubeID; set => _SubeID = value; }
        public int ModelYili { get => _ModelYili; set => _ModelYili = value; }
        public double Ucret { get => _Ucret; set => _Ucret = value; }
        public string SubeAd { get => _SubeAd; set => _SubeAd = value; }
        public string Durum { get => _Durum; set => _Durum = value; }
    }
}
