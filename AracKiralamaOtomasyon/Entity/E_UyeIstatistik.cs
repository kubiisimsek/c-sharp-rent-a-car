using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyon.Entity
{
    class E_UyeIstatistik
    {
        int _UyeSayi;
        int _EUyeSayi;
        int _KUyeSayi;
        int _KiralamaSayi;
        string _SonUye;

        public int UyeSayi { get => _UyeSayi; set => _UyeSayi = value; }
        public int EUyeSayi { get => _EUyeSayi; set => _EUyeSayi = value; }
        public int KUyeSayi { get => _KUyeSayi; set => _KUyeSayi = value; }
        public int KiralamaSayi { get => _KiralamaSayi; set => _KiralamaSayi = value; }
        public string SonUye { get => _SonUye; set => _SonUye = value; }

    }
}
