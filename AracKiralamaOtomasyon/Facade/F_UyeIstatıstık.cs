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
    class F_UyeIstatıstık
    {
        public static E_UyeIstatistik UyeIstatistik()
        {
            E_UyeIstatistik Uye = new E_UyeIstatistik();
            SqlCommand komut = null;
            try
            {
                komut = new SqlCommand("SP_UyeIstatistikleri", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();

                SqlDataReader oku = komut.ExecuteReader();


                if (oku.Read())
                {
                    Uye.UyeSayi = Convert.ToInt32(oku["UyeSayi"]);
                }
                oku.NextResult();
                if (oku.Read())
                {
                    Uye.EUyeSayi = Convert.ToInt32(oku["EUyeSayi"]);
                }
                oku.NextResult();
                if (oku.Read())
                {
                    Uye.KUyeSayi = Convert.ToInt32(oku["KUyeSayi"]);
                }
                oku.NextResult();
                if (oku.Read())
                {
                    Uye.KiralamaSayi = Convert.ToInt32(oku["UyeKiralama"]);
                }
                oku.NextResult();
                if (oku.Read())
                {
                    Uye.SonUye = oku["Ad"].ToString() + " " + oku["Soyad"].ToString();
                }
                else
                {
                    Uye.SonUye = "-";
                }

                oku.Close();

            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
                Uye = null;

            }
            finally
            {
                komut.Connection.Close();
            }
            return Uye;
        }

       
    }
}
