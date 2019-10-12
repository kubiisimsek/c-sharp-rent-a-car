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
    class BLL_Arac
    {
        public static int AracEkle(E_Arac arac)
        {
            int parsekontrol = 0;
            double parsekontrol2 = 0.0;
            if (
                arac.SubeID > 0 &&//
                arac.KategoriID > 0 &&//
                arac.MarkaID > 0 &&//
                arac.Plaka.Length >= 5 && //
                arac.Model.Length > 1 &&
                arac.ModelYili > 1900 && arac.ModelYili < 2100 &&//
                arac.Hacim.Length > 1 &&
                arac.Guc.Length > 1 &&
                !String.IsNullOrEmpty(arac.Sanziman) &&//
                arac.Kilometre > 0 &&
                arac.Renk.Length > 1 &&
                double.TryParse(arac.Ucret.ToString(), out parsekontrol2) &&
                double.TryParse(arac.Kilometre.ToString(),out parsekontrol2) &&
                int.TryParse(arac.ModelYili.ToString(), out parsekontrol)
                )
            {
                return F_Arac.AracEkle(arac);
            }
            else
               return -1;

        }

        public static DataTable TumAraclar()
        {
            return F_Arac.TumAraclar();
        }

        public static DataTable TumAraclardaAra(string kelime)
        {
            return F_Arac.TumAraclardaAra(kelime);
        }

        public static List<E_Arac> Kategoriler()
        {
            return F_Arac.Kategoriler();
        }


        public static List<E_Arac> Markalar(int KategoriID)
        {
            if(KategoriID > 0)
                return F_Arac.Markalar(KategoriID);
            else
            {
                return null;
            }
        }

        public static DataTable TekSubeninAraclari(int ID)
        {
            return F_Arac.TekSubeninAraclari(ID);
        }

        public static int SubedenAracSil(string plaka)
        {
            return F_Arac.SubedenAracSil(plaka);
        }

        public static double UyeUcretiGetir(string Plaka)
        {
            return F_Arac.UyeUcretiGetir(Plaka);
        }

        public static double AracKMGetir(string Plaka)
        {
            return F_Arac.AracKMGetir(Plaka);

        }

        public static byte[] AracResimGetir(string Plaka)
        {
            if(Plaka.Length > 4)
                return F_Arac.AracResimGetir(Plaka);

            return null;
        }

        public static int AracGuncelle (E_Arac arac)
        {
            double parsekontrol2 = 0.0;
            if (double.TryParse(arac.Ucret.ToString(), out parsekontrol2) && double.TryParse(arac.Kilometre.ToString(), out parsekontrol2))
            {
                return F_Arac.AracGuncelle(arac);
            }
            else
                return -1;
        }

    }
}
