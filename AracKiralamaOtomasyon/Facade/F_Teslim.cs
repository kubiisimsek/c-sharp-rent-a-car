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
    class F_Teslim
    {
        public static int AracTeslimAl(E_Teslim teslim)
        {
            SqlCommand komut = null;
            int donen;
            try
            {
                komut = new SqlCommand("SP_AracTeslimAl", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("TakipKod", teslim.TakipKod);
                komut.Parameters.AddWithValue("HasarKontrol",teslim.Hasar);
                komut.Parameters.AddWithValue("KM", teslim.YeniKM);
                donen = komut.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
                donen = 0;
            }
            finally
            {
                Baglanti.baglan.Close();
            }
            return donen;
        }


        public static DataTable SubeTeslimAlinanAraclar(int SubeID)
        {
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_SubeTeslimAlinanAraclar", Baglanti.baglan);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("SubeID", SubeID);
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
