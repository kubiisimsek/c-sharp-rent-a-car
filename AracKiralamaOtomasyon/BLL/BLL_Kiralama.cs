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
    class BLL_Kiralama
    {
        public static DataTable TumKiralamalar()
        {
            return F_Kiralama.TumKiralamalar();
        }
        
        public static DataTable UyeKiralamaGoster(string TC)
        {
            return F_Kiralama.UyeKiralamaGoster(TC);
        }

        public static DataTable BitenKiralamalar()
        {
            return F_Kiralama.BitenKiralamalar();
        }

        public static DataTable UyeGecmisKiralama(string TC)
        {
            return F_Kiralama.UyeGecmisKiralama(TC);
        }


        public static int TakipKodKontrol(string takipkod)
        {
            return F_Kiralama.TakipKodKontrol(takipkod);
        }

        public static int UyeOlmayanaKirala(E_Uye uye, E_Kiralama kira)
        {
            if(
                uye.TC.Length == 11 &&
                uye.Ad.Length > 1 &&
                uye.Soyad.Length > 1 &&
                uye.Telefon.Length >= 10 &&
                kira.TakipKod.Length==10 &&
                !String.IsNullOrEmpty(kira.Plaka)
                )
            {
                return F_Kiralama.UyeOlmayanaKirala(uye, kira);
            }
            else
            {
                return -1;
            }
        }

        public static int UyeyeKirala(E_Uye uye,E_Kiralama kira)
        {
            if (
                uye.TC.Length == 11 &&
                kira.TakipKod.Length == 10 &&
                !String.IsNullOrEmpty(kira.Plaka)
                )
            {
                return F_Kiralama.UyeyeKirala(uye, kira);
            }
            else
            {
                return -1;
            }
        }

        public static void BitenleriGuncelle()
        {
             F_Kiralama.BitenleriGuncelle();
        }

        public static void BaslayanlariGuncelle()
        {
            F_Kiralama.BaslayanlariGuncelle();
        }

        public static DataTable TakipSorgula(string takipkodu)
        {
            return F_Kiralama.TakipSorgula(takipkodu);
        }

        public static int KiraSureUzat(E_Kiralama uzat)
        {
            return F_Kiralama.KiraSureUzat(uzat);
        }

    }
}
