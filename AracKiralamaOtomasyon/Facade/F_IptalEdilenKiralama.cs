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
    class F_IptalEdilenKiralama
    {
        public static DataTable IptalEdilenKiralamalar()
        {
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_IptalEdilenKiralamalar", Baglanti.baglan);
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

        public static int KiraIptal(E_IptalEdilenKiralama iptal)
        {
            int donen = 0;
            SqlCommand komut = null;
            try
            {

                komut = new SqlCommand("SP_KiraIptal", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("TakipKod", iptal.TakipKod);
                komut.Parameters.AddWithValue("Sebep", iptal.Sebep);
                komut.Parameters.AddWithValue("Aciklama", iptal.Aciklama);

                donen=komut.ExecuteNonQuery();


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

    }
}
