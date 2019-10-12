using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AracKiralamaOtomasyon.Entity;
using System.Data.SqlClient;
using System.Data;
namespace AracKiralamaOtomasyon.Facade
{
    class F_SubeIstatıstık
    {


        public static E_SubeIstatistik TekSubeninIstatistik(int SubeID)
        {
            E_SubeIstatistik sube = new E_SubeIstatistik();
            SqlCommand komut = null;
            try
            {
                komut = new SqlCommand("SP_TekSubeIstatistik", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("SubeID", SubeID);

                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    sube.AracSayi = Convert.ToInt32(oku["AracSayisi"]);
                    sube.KiralamaSayi= Convert.ToInt32(oku["KiralamaSayisi"]);
                    sube.KiradakiAracSayi= Convert.ToInt32(oku["KiradakiAracSayisi"]);
                    sube.MusaitAracSayi= Convert.ToInt32(oku["MusaitAracSayisi"]);
                    if (oku["ToplamAlinanUcret"] != DBNull.Value)
                        sube.ToplamUcret = Convert.ToDouble(oku["ToplamAlinanUcret"]);
                    else
                        sube.ToplamUcret = 0;

                    sube.SikayetSayi = Convert.ToInt32(oku["SikayetSayisi"]);
                }


            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
            }
            finally
            {
                komut.Connection.Close();
            }
            return sube;
        }

        public static void SubeIstatistikGuncelle(int SubeID)
        {
            SqlCommand komut = null;
            try
            {
                komut = new SqlCommand("SP_IstatistikGuncelle", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("SubeID", SubeID);
                komut.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
            }
            finally
            {
                komut.Connection.Close();
            }

        }

    }
}
