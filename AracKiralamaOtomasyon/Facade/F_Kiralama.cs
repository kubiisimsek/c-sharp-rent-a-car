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
    class F_Kiralama
    {
        public static DataTable TumKiralamalar()
        {
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_TumKiralamaGoster", Baglanti.baglan);
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

        public static DataTable UyeKiralamaGoster(string TC) //ÜYENİN O ANKİ KİRALAMASINI GÖSTERME
        {
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_UyeKiralamaGoster", Baglanti.baglan);
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

        public static DataTable UyeGecmisKiralama(string TC) //ÜYE İÇİN GEÇMİŞ KİRALAMALAR GÖSTERME
        {
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_UyeGecmisKiralama", Baglanti.baglan);
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

        public static DataTable BitenKiralamalar()
        {
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_BitenKiralamaGoster", Baglanti.baglan);
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




        public static int UyeOlmayanaKirala(E_Uye uye,E_Kiralama kira)
        {
            int donen = -2;
            SqlCommand komut = null;
            try
            {
                komut = new SqlCommand("SP_UyeOlmayanaKirala", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("TakipKod", kira.TakipKod);
                komut.Parameters.AddWithValue("Plaka", kira.Plaka);
                komut.Parameters.AddWithValue("TC", uye.TC);
                komut.Parameters.AddWithValue("Ad", uye.Ad);
                komut.Parameters.AddWithValue("Soyad", uye.Soyad);
                komut.Parameters.AddWithValue("DogumTarihi", uye.DogumTarihi);
                komut.Parameters.AddWithValue("Cinsiyet", uye.Cinsiyet);
                komut.Parameters.AddWithValue("Adres", uye.Adres);
                komut.Parameters.AddWithValue("Telefon", uye.Telefon);
                komut.Parameters.AddWithValue("EhliyetSinifi", uye.EhliyetSinifi);
                komut.Parameters.AddWithValue("EhliyetYili", uye.EhliyetYili);
                komut.Parameters.AddWithValue("BaslangicTarihi",kira.BaslangicTarihi);
                komut.Parameters.AddWithValue("BitisTarihi",kira.BitisTarihi);
                komut.Parameters.AddWithValue("OdemeTuru",kira.OdemeTuru);
                komut.Parameters.AddWithValue("OdenenUcret",kira.OdenenUcret);
                komut.Parameters.AddWithValue("Aciklama", kira.Aciklama);

                SqlParameter cevap = komut.Parameters.Add("@return_value", SqlDbType.Int);
                cevap.Direction = ParameterDirection.ReturnValue;

                komut.ExecuteNonQuery();
                donen = (Int32)cevap.Value;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
                donen = -2;
            }
            finally
            {
                komut.Connection.Close();
            }
            return donen;
        }

        public static int UyeyeKirala(E_Uye uye,E_Kiralama kira)
        {
            int donen = 0;
            SqlCommand komut = null;
            try
            {
                komut = new SqlCommand("SP_UyeyeKirala", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("TakipKod", kira.TakipKod);
                komut.Parameters.AddWithValue("Plaka", kira.Plaka);
                komut.Parameters.AddWithValue("UyeTC", uye.TC);
                komut.Parameters.AddWithValue("BaslangicTarihi", kira.BaslangicTarihi);
                komut.Parameters.AddWithValue("BitisTarihi", kira.BitisTarihi);
                komut.Parameters.AddWithValue("OdemeTuru", kira.OdemeTuru);
                komut.Parameters.AddWithValue("OdenenUcret", kira.OdenenUcret);
                komut.Parameters.AddWithValue("Aciklama", kira.Aciklama);

                SqlParameter cevap = komut.Parameters.Add("@return_value", SqlDbType.Int);
                cevap.Direction = ParameterDirection.ReturnValue;

                komut.ExecuteNonQuery();
                donen = (Int32)cevap.Value;
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



        public static int TakipKodKontrol(string takipkod)
        {
            int donen = 0;
            SqlCommand komut = null;
            try
            {
                komut = new SqlCommand("SP_TakipKodKontrol", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("TakipKod", takipkod);

                SqlParameter cevap = komut.Parameters.Add("@return_value", SqlDbType.Int);
                cevap.Direction = ParameterDirection.ReturnValue;

                komut.ExecuteNonQuery();
                donen = (Int32)cevap.Value;
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

        public static void BitenleriGuncelle()
        {
            SqlCommand komut = null;
            try
            {
                komut = new SqlCommand("SP_BitenleriGuncelle", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
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

        public static void BaslayanlariGuncelle()
        {
            SqlCommand komut = null;
            try
            {
                komut = new SqlCommand("SP_BaslayanlariGuncelle", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
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

        public static DataTable TakipSorgula(string takipkodu)
        {
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_TakipSorgula", Baglanti.baglan);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("TakipKod", takipkodu);

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

        public static int KiraSureUzat(E_Kiralama uzat)
        {
            int donen = 0;
            SqlCommand komut = null;
            try
            {
                komut = new SqlCommand("SP_KiraSureUzat", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("TakipKod",uzat.TakipKod);
                komut.Parameters.AddWithValue("YeniTarih", uzat.BitisTarihi);
                komut.Parameters.AddWithValue("YeniUcret", uzat.OdenenUcret);

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

    }
}
