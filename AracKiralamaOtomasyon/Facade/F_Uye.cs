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
    class F_Uye
    {
        
        public static int UyeKaydet(E_Uye uye)
        {
            int donen = 0;
            SqlCommand komut=null;
            try
            {

                komut = new SqlCommand("SP_UyeEkle",Baglanti.baglan);               
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("Eposta", uye.Eposta);
                komut.Parameters.AddWithValue("Sifre", uye.Sifre);
                komut.Parameters.AddWithValue("TC", uye.TC);
                komut.Parameters.AddWithValue("Ad", uye.Ad);
                komut.Parameters.AddWithValue("Soyad", uye.Soyad);
                komut.Parameters.AddWithValue("DogumTarihi", uye.DogumTarihi);
                komut.Parameters.AddWithValue("Cinsiyet", uye.Cinsiyet);
                komut.Parameters.AddWithValue("Sehir", uye.Sehir);
                komut.Parameters.AddWithValue("Adres", uye.Adres);
                komut.Parameters.AddWithValue("Telefon", uye.Telefon);
                komut.Parameters.AddWithValue("EhliyetSinifi", uye.EhliyetSinifi);
                komut.Parameters.AddWithValue("EhliyetYili", uye.EhliyetYili);

                SqlParameter cevap = komut.Parameters.Add("@return_value", SqlDbType.Int);
                cevap.Direction = ParameterDirection.ReturnValue;

                komut.ExecuteNonQuery();

                donen = (Int32)cevap.Value;

            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
            }
            finally
            {
                komut.Connection.Close();
            }
            return donen;
        }


        public static int UyeGiris(E_Uye uye)
        {
            int donen = 0;
            SqlCommand komut = null;
            try
            {
                komut = new SqlCommand("SP_UyeGiris", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("Eposta", uye.Eposta);
                komut.Parameters.AddWithValue("Sifre", uye.Sifre);

                SqlParameter cevap = komut.Parameters.Add("@return_value", SqlDbType.Int);
                cevap.Direction = ParameterDirection.ReturnValue;

                komut.ExecuteNonQuery();
                donen = (Int32)cevap.Value;
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
            }
            finally
            {
                komut.Connection.Close();
            }
            return donen;

        }

        public static int YoneticiKaydet(E_Uye uye)
        {
            int donen = 0;
            SqlCommand komut = null;
            try
            {

                komut = new SqlCommand("SP_YoneticiEkle", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("Eposta", uye.Eposta);
                komut.Parameters.AddWithValue("Sifre", uye.Sifre);

                SqlParameter cevap = komut.Parameters.Add("@return_value", SqlDbType.Int);
                cevap.Direction = ParameterDirection.ReturnValue;

                komut.ExecuteNonQuery();

                donen = (Int32)cevap.Value;

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
            }
            finally
            {
                komut.Connection.Close();
            }
            return donen;
        }

        public static DataTable TumUyeler()
        {

            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SELECT * FROM UyeGoster", Baglanti.baglan);
                da.Fill(dt);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
                dt = null;
            }
            finally
            {
                Baglanti.baglan.Close();
            }
            return dt;
        }


        public static DataTable UyeAra(string kelime)
        {

            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_UyeAra", Baglanti.baglan);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("AramaMetni", kelime);
                da.Fill(dt);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
                dt = null;
            }
            finally
            {
                Baglanti.baglan.Close();
            }
            return dt;
        }


        public static E_Uye UyeBilgiGetir(string eposta,string sifre) //SESSION İÇİN
        {
            SqlCommand komut = null;
            E_Uye uye = null;
            try
            {
                komut = new SqlCommand("SP_UyeBilgiGetir", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("Eposta", eposta);
                komut.Parameters.AddWithValue("Sifre", sifre);
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    uye = new E_Uye();
                    uye.Ad = oku["Ad"].ToString();
                    uye.Soyad = oku["Soyad"].ToString();
                    uye.TC = oku["TC"].ToString();
                }
                oku.Close();

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");

            }
            finally
            {
                komut.Connection.Close();
            }
            return uye;
        }

        public static E_Uye UyeninProfilVerileri(string TC) //ÜYE PROFİLİ İÇİN
        {
            SqlCommand komut = null;
            E_Uye uye = null;
            try
            {
                komut = new SqlCommand("SP_UyeninProfilVerileri", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("TC", TC);

                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    uye = new E_Uye();
                    uye.Eposta = oku["Eposta"].ToString();
                    uye.Ad = oku["Ad"].ToString();
                    uye.Soyad = oku["Soyad"].ToString();
                    uye.DogumTarihi = Convert.ToDateTime(oku["DogumTarihi"]);
                    uye.Cinsiyet = Convert.ToChar(oku["Cinsiyet"]);
                    uye.Sehir = oku["Sehir"].ToString();
                    uye.Adres = oku["Adres"].ToString();
                    uye.Telefon = oku["Telefon"].ToString();
                    uye.EhliyetSinifi = oku["EhliyetSinifi"].ToString();
                    uye.EhliyetYili = Convert.ToInt32(oku["EhliyetYili"]);
                    uye.KayitTarihi = Convert.ToDateTime(oku["KayıtTarihi"]);

                }
                oku.Close();

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");

            }
            finally
            {
                komut.Connection.Close();
            }
            return uye;
        }

        public static int UyeSifreDegistir(E_Uye uye)
        {
            int donen = 0;
            SqlCommand komut = null;
            try
            {

                komut = new SqlCommand("SP_UyeSifreYenile", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("Eposta", uye.Eposta);
                komut.Parameters.AddWithValue("YeniSifre", uye.Sifre);

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

        public static int UyeProfilGuncelle(E_Uye uye)
        {
            int donen = 0;
            SqlCommand komut = null;
            try
            {

                komut = new SqlCommand("SP_UyeProfilGuncelle", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("TC", uye.TC);
                komut.Parameters.AddWithValue("Sehir", uye.Sehir);
                komut.Parameters.AddWithValue("Adres", uye.Adres);
                komut.Parameters.AddWithValue("Tel", uye.Telefon);
                komut.Parameters.AddWithValue("EhSinif", uye.EhliyetSinifi);
                komut.Parameters.AddWithValue("EhYil", uye.EhliyetYili);

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
