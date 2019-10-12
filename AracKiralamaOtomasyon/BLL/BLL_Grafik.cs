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
    class BLL_Grafik
    {
        public static int[] KiralamaVerileri()
        {
            return F_Grafik.KiralamaVerileri();
        }

        public static DataTable KategoriVerileri()
        {
            return F_Grafik.KategoriVerileri();
        }

        public static DataTable AracVerileri()
        {
            return F_Grafik.AracVerileri();
        }

        public static DataTable UyeVerileri()
        {
            return F_Grafik.UyeVerileri();
        }
    }
}
