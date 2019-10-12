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
    class F_Mesaj
    {
        public static int MesajGonder(E_Mesaj msj)
        {
            int donen = 0;
            SqlCommand komut = null;
            try
            {

                komut = new SqlCommand("SP_MesajGonder", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("SubeID", msj.SubeID);
                komut.Parameters.AddWithValue("UyeTC", msj.UyeTC);
                komut.Parameters.AddWithValue("Konu", msj.Konu);
                komut.Parameters.AddWithValue("Mesaj", msj.Mesaj);


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

        public static DataTable UyeGecmisMesaj(string TC)
        {
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_UyeGecmisMesaj", Baglanti.baglan);
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
