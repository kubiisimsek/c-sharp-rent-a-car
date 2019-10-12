using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AracKiralamaOtomasyon.Entity;
using AracKiralamaOtomasyon.Facade;
using System.Data;

namespace AracKiralamaOtomasyon.BLL
{
    class BLL_Sikayet
    {
        public static DataTable TumSikayetler()
        {
            return F_Sikayet.TumSikayetler();
        }
        public static int SikayetGonder(E_Sikayet sikayet)
        {
            if(sikayet.UyeTC.Length==11 && sikayet.SubeID > 0 && sikayet.Konu.Length > 1 && sikayet.SikayetMesaj.Length > 1)
            {
                return F_Sikayet.SikayetGonder(sikayet);
            }
            return -1;
        }

        public static DataTable UyeGecmisSikayet(string TC)
        {
            return F_Sikayet.UyeGecmisSikayet(TC);
        }

    }
}
