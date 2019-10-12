using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralamaOtomasyon.Entity
{
    class E_Grafik
    {
        int[] _Veriler = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //AYLIK VERİLER DEFAULT 0
        string[] _Aylar = { "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" }; //TÜM AYLAR

        public int[] Veriler { get => _Veriler; set => _Veriler = value; }
        public string[] Aylar { get => _Aylar; set => _Aylar = value; }
    }
}
