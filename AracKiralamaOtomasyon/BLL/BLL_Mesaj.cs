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
    class BLL_Mesaj
    {
        public static int MesajGonder(E_Mesaj msj)
        {
            if(msj.SubeID > 0 && msj.UyeTC.Length==11 && msj.Konu.Length > 1 && msj.Mesaj.Length > 1)
            {
                return F_Mesaj.MesajGonder(msj);
            }
            return -1;
        }

        public static DataTable UyeGecmisMesaj(string TC)
        {
            return F_Mesaj.UyeGecmisMesaj(TC);
        }


    }
}
