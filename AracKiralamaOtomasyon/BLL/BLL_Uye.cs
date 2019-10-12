using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AracKiralamaOtomasyon.Entity;
using AracKiralamaOtomasyon.Facade;
using System.Text.RegularExpressions;
using System.Data;

namespace AracKiralamaOtomasyon.BLL
{
    class BLL_Uye
    {
        public static int UyeEkle(E_Uye uye)
        {
            string bicim = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"; // Mail kontrolü için
            Regex regex = new Regex(bicim);
            if (
                uye.Eposta.Length > 5 &&
                uye.Sifre.Length > 5 &&
                uye.TC.Length == 11 &&
                uye.Ad.Length > 2 &&
                uye.Soyad.Length > 1 &&
                uye.DogumTarihi != null &&               
                uye.Adres.Length > 0 &&
                uye.Telefon.Length >= 10 &&
                uye.EhliyetSinifi.Length > 0 &&
                uye.EhliyetYili > 0 &&
                regex.IsMatch(uye.Eposta)
                )
            {
                return F_Uye.UyeKaydet(uye);
            }

            return -1;
        }

        public static int UyeGiris(E_Uye uye)
        {
            if (uye.Eposta != null && uye.Sifre != null)
            {
                return F_Uye.UyeGiris(uye);
            }
            else
                return -1;
        }

        public static int YoneticiEkle(E_Uye uye)
        {
            if(!String.IsNullOrEmpty(uye.Eposta) && !String.IsNullOrEmpty(uye.Sifre))
            {
                return F_Uye.YoneticiKaydet(uye);
            }

            return -1;
        }

        public static DataTable TumUyeler()
        {
            return F_Uye.TumUyeler();
        }

        public static DataTable UyeAra(string kelime)
        {
            return F_Uye.UyeAra(kelime);
        }

        public static E_Uye UyeBilgiGetir(string eposta,string sifre)
        {
            return F_Uye.UyeBilgiGetir(eposta, sifre);
        }

        public static E_Uye UyeninProfilVerileri(string TC)
        {
            return F_Uye.UyeninProfilVerileri(TC);
        }

        public static int UyeSifreDegistir(E_Uye uye)
        {
            if(uye.Eposta.Length > 1 && uye.Eposta.Length > 5)
            {
                return F_Uye.UyeSifreDegistir(uye);
            }
            return -1;
        }

        public static int UyeProfilGuncelle(E_Uye uye)
        {
            if(uye.TC.Length==11 && uye.Telefon.Length >= 10 && uye.Sehir!=null && uye.Adres.Length > 3 && uye.EhliyetSinifi.Length > 0 && uye.EhliyetYili > 0)
            {
                return F_Uye.UyeProfilGuncelle(uye);
            }
            return -1;
        }
    }
}
