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
    class F_Sikayet
    {
        public static DataTable TumSikayetler()
        {

            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_TumSikayetler", Baglanti.baglan);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
            }
            finally
            {
                Baglanti.baglan.Close();
            }
            return dt;
        }

        public static int SikayetGonder(E_Sikayet sikayet)
        {
            int donen = 0;
            SqlCommand komut = null;
            try
            {

                komut = new SqlCommand("SP_SikayetGonder", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("SubeID", sikayet.SubeID);
                komut.Parameters.AddWithValue("UyeTC",sikayet.UyeTC);
                komut.Parameters.AddWithValue("Konu",sikayet.Konu);
                komut.Parameters.AddWithValue("Sikayet", sikayet.SikayetMesaj);


                donen = komut.ExecuteNonQuery();


            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
                donen = 0;
            }
            finally
            {
                komut.Connection.Close();
            }
            return donen;
        }

        public static DataTable UyeGecmisSikayet(string TC)
        {
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_UyeGecmisSikayet", Baglanti.baglan);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("TC", TC);
                da.Fill(dt);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
            }
            finally
            {
                Baglanti.baglan.Close();
            }
            return dt;
        }


    }
}
