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
    class BLL_Sube
    {
        public static int SubeEkle(E_Sube sube)
        {
            if(
                !String.IsNullOrEmpty(sube.Eposta) &&
                !String.IsNullOrEmpty(sube.Sifre) &&
                !String.IsNullOrEmpty(sube.Ad) &&
                !String.IsNullOrEmpty(sube.Sehir) &&
                !String.IsNullOrEmpty(sube.Ilce) &&
                !String.IsNullOrEmpty(sube.Semt) &&
                !String.IsNullOrEmpty(sube.Telefon) &&
                !String.IsNullOrEmpty(sube.Adres)
                )
            {
                return F_Sube.SubeEkle(sube);
            }
            return -1;
        }

        public static int SubeGuncelle(E_Sube sube)
        {
           if (
                !String.IsNullOrEmpty(sube.Eposta) &&
                !String.IsNullOrEmpty(sube.Sifre) &&
                !String.IsNullOrEmpty(sube.Ad) &&
                !String.IsNullOrEmpty(sube.Sehir) &&
                !String.IsNullOrEmpty(sube.Ilce) &&
                !String.IsNullOrEmpty(sube.Semt) &&
                !String.IsNullOrEmpty(sube.Telefon) &&
                !String.IsNullOrEmpty(sube.Adres)
                )
            {
                return F_Sube.SubeGuncelle(sube);
            }
            return -1;
        }

        public static E_Sube SubeBilgiGoster(int ID)
        {
            if (ID > 0)
                return F_Sube.SubeBilgiGoster(ID);
            else
                return null;
        }

        public static List<E_Sube> SubeListe()
        {
            return F_Sube.SubeListe();
        }

        public static DataTable TumSubeler()
        {
            return F_Sube.TumSubeler();
        }

        public static DataTable SubeAra(string kelime)
        {
            return F_Sube.SubeAra(kelime);
        }

        public static E_Sube SubeIDGetir(string Eposta,String Sifre)
        {
            return F_Sube.SubeIDGetir(Eposta, Sifre);
        }

        public static DataTable SubeBitenKiralamalar(int SubeID)
        {
            return F_Sube.SubeBitenKiralamalar(SubeID);         
        }

        public static DataTable SubeKiralamaGoster(int SubeID)
        {
            return F_Sube.SubeKiralamaGoster(SubeID);
        }

        public static DataTable SubeTumKiralamalar(int SubeID)
        {
            return F_Sube.SubeTumKiralamalar(SubeID);
        }

        public static DataTable SubeMesajGoster(int SubeID)
        {
            return F_Sube.SubeMesajGoster(SubeID);
        }


    }
}
