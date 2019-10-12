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
    class F_Sube
    {
        public static int SubeEkle(E_Sube sube)
        {
            int donen = 0;
            SqlCommand komut = null;
            try
            {

                komut = new SqlCommand("SP_SubeEkle", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("Eposta", sube.Eposta);
                komut.Parameters.AddWithValue("Sifre", sube.Sifre);
                komut.Parameters.AddWithValue("Ad", sube.Ad);
                komut.Parameters.AddWithValue("Sehir", sube.Sehir);
                komut.Parameters.AddWithValue("Ilce", sube.Ilce);
                komut.Parameters.AddWithValue("Semt", sube.Semt);
                komut.Parameters.AddWithValue("Telefon", sube.Telefon);
                komut.Parameters.AddWithValue("Adres", sube.Adres);

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

        public static int SubeGuncelle(E_Sube sube)
        {
            int donen=0;
            SqlCommand komut = null;
            try
            {

                komut = new SqlCommand("SP_SubeGuncelle", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("ID", sube.ID);
                komut.Parameters.AddWithValue("Eposta", sube.Eposta);
                komut.Parameters.AddWithValue("Sifre", sube.Sifre);
                komut.Parameters.AddWithValue("Ad", sube.Ad);
                komut.Parameters.AddWithValue("Sehir", sube.Sehir);
                komut.Parameters.AddWithValue("Ilce", sube.Ilce);
                komut.Parameters.AddWithValue("Semt", sube.Semt);
                komut.Parameters.AddWithValue("Telefon", sube.Telefon);
                komut.Parameters.AddWithValue("Adres", sube.Adres);

                donen=komut.ExecuteNonQuery();

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

        public static E_Sube SubeBilgiGoster(int ID)
        {
            E_Sube subebilgi = null;
            SqlCommand komut = null;
            try
            {
                komut = new SqlCommand("SP_SubeBilgiGoster", Baglanti.baglan);
                komut.Parameters.AddWithValue("ID",ID);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    subebilgi = new E_Sube();
                    
                    subebilgi.Eposta = oku["Eposta"].ToString();
                    subebilgi.Sifre = oku["Sifre"].ToString();
                    subebilgi.Ad = oku["Ad"].ToString();
                    subebilgi.Sehir = oku["Sehir"].ToString();
                    subebilgi.Ilce = oku["Ilce"].ToString();
                    subebilgi.Semt = oku["Semt"].ToString();
                    subebilgi.Telefon = oku["Telefon"].ToString();
                    subebilgi.Adres = oku["Adres"].ToString();                    
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
                subebilgi = null;
            }
            finally
            {
                komut.Connection.Close();
            }
            return subebilgi;

        }

        public static List<E_Sube> SubeListe()
        {
            List<E_Sube> subeler = null;
            SqlCommand komut = null;
            try
            {
                komut = new SqlCommand("SELECT ID,Ad,Sehir,Ilce FROM SubeBilgi", Baglanti.baglan);
                komut.Connection.Open();
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.HasRows)
                {
                    subeler = new List<E_Sube>();
                    while (oku.Read())
                    {
                        E_Sube subebilgi = new E_Sube();
                        subebilgi.ID = Convert.ToInt32(oku["ID"]);
                        subebilgi.Ad = oku["Sehir"].ToString() +" - " + oku["Ilce"].ToString() + " -> " + oku["Ad"].ToString();
                        subeler.Add(subebilgi);
                    }
                }

                oku.Close();

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
                subeler = null;
            }
            finally
            {
                komut.Connection.Close();
            }
            return subeler;
        }


        public static DataTable TumSubeler()
        {

            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SELECT * FROM SubeGoster", Baglanti.baglan);
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

        public static DataTable SubeAra(string kelime)
        {

            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_SubeAra", Baglanti.baglan);
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

        public static E_Sube SubeIDGetir(string Eposta, string Sifre)
        {
            E_Sube sube = new E_Sube();
            SqlCommand komut = null;
            try
            {
                komut = new SqlCommand("SP_SubeIDGetir", Baglanti.baglan);
                komut.Connection.Open();
                komut.CommandType = CommandType.StoredProcedure;
                komut.Parameters.AddWithValue("Eposta", Eposta);
                komut.Parameters.AddWithValue("Sifre", Sifre);

                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    sube.ID = Convert.ToInt32(oku["ID"]);
                    sube.Ad = oku["Ad"].ToString();
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

        public static DataTable SubeBitenKiralamalar(int SubeID)
        {

            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_SubeBitenKiralamaGoster", Baglanti.baglan);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("SubeID", SubeID);
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

        public static DataTable SubeKiralamaGoster(int SubeID)
        {

            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_SubeKiralamaGoster", Baglanti.baglan);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("SubeID", SubeID);
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

        public static DataTable SubeTumKiralamalar(int SubeID)
        {

            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_SubeTumKiralamalar", Baglanti.baglan);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("SubeID", SubeID);
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

        public static DataTable SubeMesajGoster(int SubeID)
        {

            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_SubeMesajGoster", Baglanti.baglan);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("SubeID", SubeID);
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



    }
}
