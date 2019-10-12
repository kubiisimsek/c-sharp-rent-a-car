using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AracKiralamaOtomasyon.BLL;


namespace AracKiralamaOtomasyon
{
    class TakipkodGenerator
    {
        public static string TakipKod(int yetki)
        {
            string randomkod;

            while (true)
            {
               
                Random kod = new Random();
                int takipkod = kod.Next(100000000, 999999999);
                if(yetki == 1)
                {
                   randomkod = "U" + takipkod.ToString(); //ÜYELER İÇİN
                }
                else
                {
                    randomkod = "M" + takipkod.ToString(); //MİSAFİRLER İÇİN
                }

                int donen = BLL_Kiralama.TakipKodKontrol(randomkod);


                if (donen == 0)
                {
                    continue;
                }
                else if(donen == 1)
                {
                    break;
                }

            }
            return randomkod;
        }
    }
}
