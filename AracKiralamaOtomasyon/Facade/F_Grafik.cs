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
    class F_Grafik
    {
        public static int[] KiralamaVerileri()
        {
            int[] dizi = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,0 };
            
            SqlCommand komut = null;
            try
            {
                komut = new SqlCommand("SP_GrafikKiralamaVerileri", Baglanti.baglan);
                komut.CommandType = CommandType.StoredProcedure;
                komut.Connection.Open();
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.HasRows)
                {
                    
                    while (oku.Read())
                    {
                        int ay = Convert.ToInt32(oku["Ay"]);
                        switch (ay)
                        {
                            case 1:
                                dizi[0]= Convert.ToInt32(oku["Sayi"]);
                                break;
                            case 2:
                                dizi[1] = Convert.ToInt32(oku["Sayi"]);
                                break;
                            case 3:
                                dizi[2] = Convert.ToInt32(oku["Sayi"]);
                                break;
                            case 4:
                                dizi[3] = Convert.ToInt32(oku["Sayi"]);
                                break;
                            case 5:
                                dizi[4] = Convert.ToInt32(oku["Sayi"]);
                                break;
                            case 6:
                                dizi[5] = Convert.ToInt32(oku["Sayi"]);
                                break;
                            case 7:
                                dizi[6] = Convert.ToInt32(oku["Sayi"]);
                                break;
                            case 8:
                                dizi[7] = Convert.ToInt32(oku["Sayi"]);
                                break;
                            case 9:
                                dizi[8] = Convert.ToInt32(oku["Sayi"]);
                                break;
                            case 10:
                                dizi[9] = Convert.ToInt32(oku["Sayi"]);
                                break;
                            case 11:
                                dizi[10] = Convert.ToInt32(oku["Sayi"]);
                                break;
                            case 12:
                                dizi[11] = Convert.ToInt32(oku["Sayi"]);
                                break;

                        }


                    }
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
            return dizi;
        }

        public static DataTable KategoriVerileri()
        {
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_GrafikKategoriVerileri", Baglanti.baglan);
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

        public static DataTable AracVerileri()
        {
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_GrafikAracVerileri", Baglanti.baglan);
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

        public static DataTable UyeVerileri()
        {
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                Baglanti.baglan.Open();
                da = new SqlDataAdapter("SP_GrafikUyeVerileri", Baglanti.baglan);
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

    }
}
