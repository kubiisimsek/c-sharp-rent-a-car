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
    class BLL_IptalEdilenKiralama
    {
        public static DataTable IptalEdilenKiralamalar()
        {
            return F_IptalEdilenKiralama.IptalEdilenKiralamalar();
        }

        public static int KiraIptal(E_IptalEdilenKiralama kira)
        {
            if(kira.TakipKod.Length==10 && kira.Sebep.Length > 1 && kira.Aciklama.Length > 1)
            {
                return F_IptalEdilenKiralama.KiraIptal(kira);
            }
            return -1;
        }
    }
}
