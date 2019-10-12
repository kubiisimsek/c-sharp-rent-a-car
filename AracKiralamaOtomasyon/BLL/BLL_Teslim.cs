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
    class BLL_Teslim
    {
        public static int AracTeslimAl(E_Teslim teslim)
        {
            if (teslim.YeniKM > BLL_Arac.AracKMGetir(teslim.Plaka))
            {
                return F_Teslim.AracTeslimAl(teslim);
            }

            return -1;
        }

        public static DataTable SubeTeslimAlinanAraclar(int SubeID)
        {
            return F_Teslim.SubeTeslimAlinanAraclar(SubeID);
        }
    }
}
