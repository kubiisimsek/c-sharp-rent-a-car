using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AracKiralamaOtomasyon.Entity;
using AracKiralamaOtomasyon.Facade;



namespace AracKiralamaOtomasyon.BLL
{
    class BLL_SubeIstatistik
    {
        
        public static E_SubeIstatistik TekSubeninIstatistik(int SubeID)
        {
            return F_SubeIstatıstık.TekSubeninIstatistik(SubeID);
        }
        public static void SubeIstatistikGuncelle(int subeID)
        {
            F_SubeIstatıstık.SubeIstatistikGuncelle(subeID);
        }
    }
}
