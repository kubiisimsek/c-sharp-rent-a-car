using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyon.Entity
{
    class E_Teslim
    {
        string _TakipKod;
        string _TC;
        string _Plaka;
        double _YeniKM;
        string _Hasar;

        public string TakipKod { get => _TakipKod; set => _TakipKod = value; }
        public string TC { get => _TC; set => _TC = value; }
        public string Plaka { get => _Plaka; set => _Plaka = value; }
        public double YeniKM { get => _YeniKM; set => _YeniKM = value; }
        public string Hasar { get => _Hasar; set => _Hasar = value; }
    }
}
