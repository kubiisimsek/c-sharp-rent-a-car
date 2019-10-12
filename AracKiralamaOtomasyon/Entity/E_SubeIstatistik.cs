using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyon.Entity
{
    class E_SubeIstatistik
    {

        int _SubeID;
        int _AracSayi;
        int _ToplamKiraSayi;
        int _KiradakiAracSayi;
        int _MusaitAracSayi;
        int _SikayetSayi;
        int _KiralamaSayi;
        double _ToplamUcret;

        public int SubeID { get => _SubeID; set => _SubeID = value; }
        public int AracSayi { get => _AracSayi; set => _AracSayi = value; }
        public int ToplamKiraSayi { get => _ToplamKiraSayi; set => _ToplamKiraSayi = value; }
        public int KiradakiAracSayi { get => _KiradakiAracSayi; set => _KiradakiAracSayi = value; }
        public int MusaitAracSayi { get => _MusaitAracSayi; set => _MusaitAracSayi = value; }
        public int SikayetSayi { get => _SikayetSayi; set => _SikayetSayi = value; }
        public int KiralamaSayi { get => _KiralamaSayi; set => _KiralamaSayi = value; }
        public double ToplamUcret { get => _ToplamUcret; set => _ToplamUcret = value; }
    }
}
