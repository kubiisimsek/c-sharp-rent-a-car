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
    class F_Arac
    {

        public static int AracEkle(E_Arac arac)
        {
            int donen = 0;
            SqlCommand komut = null;
            try
            {
                komut = new SqlCommand("SP_AracEkle", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("Plaka", arac.Plaka);
                komut.Parameters.AddWithValue("SubeID", arac.SubeID);
                komut.Parameters.AddWithValue("Renk", arac.Renk);
                komut.Parameters.AddWithValue("Kilometre", arac.Kilometre);
                komut.Parameters.AddWithValue("Donanim", arac.Donanim);
                komut.Parameters.AddWithValue("Resim", arac.Resim);

                komut.Parameters.AddWithValue("MarkaID", arac.MarkaID);
                komut.Parameters.AddWithValue("KategoriID", arac.KategoriID);
                komut.Parameters.AddWithValue("Model", arac.Model);
                komut.Parameters.AddWithValue("ModelYili", arac.ModelYili);
                komut.Parameters.AddWithValue("Yakit", arac.Yakit);
                komut.Parameters.AddWithValue("Hacim", arac.Hacim);
                komut.Parameters.AddWithValue("Guc", arac.Guc);
                komut.Parameters.AddWithValue("Sanziman", arac.Sanziman);
                komut.Parameters.AddWithValue("Ucret", arac.Ucret);

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


        public static List<E_Arac> Kategoriler()
        {
            List<E_Arac> kategori = null;
            SqlCommand komut = null;
            try
            {
                komut = new SqlCommand("SELECT ID,Kategori FROM AracKategori", Baglanti.baglan);
                komut.Connection.Open();
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.HasRows)
                {
                    kategori = new List<E_Arac>();
                    while (oku.Read())
                    {
                        E_Arac ktgr = new E_Arac();
                        ktgr.KategoriID = Convert.ToInt32(oku["ID"]);
                        ktgr.Kategori = oku["Kategori"].ToString();
                        kategori.Add(ktgr);
                    }
                }

                oku.Close();

            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
                kategori = null;
            }
            finally
            {
                komut.Connection.Close();
            }
            return kategori;
        }

        public static List<E_Arac> Markalar(int KategoriID) //KATEGORİYE GÖRE MARKA LİSTELEME
        {
            List<E_Arac> marka = null;
            SqlCommand komut = null;
            try
            {
                
                komut = new SqlCommand("SP_MarkaListele", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("KategoriID", KategoriID);
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.HasRows)
                {
                    marka = new List<E_Arac>();
                    while (oku.Read())
                    {
                        E_Arac m = new E_Arac();
                        m.Marka = oku["Marka"].ToString();
                        m.MarkaID = Convert.ToInt32(oku["ID"]);
                        marka.Add(m);
                    }
                }

                oku.Close();

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
                marka = null;
            }
            finally
            {
                komut.Connection.Close();
            }
            return marka;
        }


        public static DataTable TumAraclar()
        {

            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da= new SqlDataAdapter("SELECT * FROM AracGoster", Baglanti.baglan);
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

        public static DataTable TumAraclardaAra(string kelime)
        {

            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da=new SqlDataAdapter("SP_AracAra", Baglanti.baglan);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("AramaMetni",kelime);            
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

        public static DataTable TekSubeninAraclari(int ID)
        {

            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_TekSubeninAraclari", Baglanti.baglan);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("ID", ID);
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


        public static int SubedenAracSil(string plaka)
        {
            SqlCommand komut = null;
            int donen;
            try
            {
                komut = new SqlCommand("SP_SubedenAracSil", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("Plaka", plaka);
                donen=komut.ExecuteNonQuery();
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

        public static double UyeUcretiGetir(string Plaka)
        {
            SqlCommand komut = null;
            double ucret = 0;
            try
            {
                komut = new SqlCommand("SP_UyeUcretGetir", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("Plaka", Plaka);
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    ucret = Convert.ToDouble(oku["UyeUcret"]);
                }
                oku.Close();

            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
                ucret = 0;
            }
            finally
            {
                komut.Connection.Close();
            }
            return ucret;

        }

        public static double AracKMGetir(string plaka)
        {
            SqlCommand komut = null;
            double km = 0;
            try
            {
                komut = new SqlCommand("SP_AracKMGetir", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("Plaka", plaka);
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    km = Convert.ToDouble(oku["Kilometre"]);
                }
                oku.Close();

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
                km = 0;
            }
            finally
            {
                komut.Connection.Close();
            }
            return km;
        }


        public static byte[] AracResimGetir(string Plaka)
        {
            SqlCommand komut = null;
            byte[] resim = null;
            try
            {
                komut = new SqlCommand("SP_AracResimGoster", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("Plaka", Plaka);
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    if (oku["Resim"] != DBNull.Value)
                        resim = (Byte[])oku["Resim"];
                    else
                        resim = null;
                }

                oku.Close();

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Bir hata oluştu.Hata: " + e.ToString(), "Hata");
                resim = null;
            }
            finally
            {
                komut.Connection.Close();
            }
            return resim;
        }

        public static int AracGuncelle(E_Arac arac)
        {
            SqlCommand komut = null;
            int donen;
            try
            {
                komut = new SqlCommand("SP_AracGuncelle", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                komut.Parameters.AddWithValue("Plaka",arac.Plaka);
                komut.Parameters.AddWithValue("Ucret", arac.Ucret);
                komut.Parameters.AddWithValue("KM", arac.Kilometre);
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


    }
}
