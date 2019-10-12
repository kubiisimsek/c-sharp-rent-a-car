using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AracKiralamaOtomasyon.Entity;
using AracKiralamaOtomasyon.BLL;

namespace AracKiralamaOtomasyon
{
    public partial class GirisForm : Form
    {
        public GirisForm()
        {
            InitializeComponent();
        }


        private void GirisForm_Load(object sender, EventArgs e)
        {
            BLL_Kiralama.BitenleriGuncelle();
            BLL_Kiralama.BaslayanlariGuncelle();
        }



        private void GirisYapButon_Click(object sender, EventArgs e)
        {
            E_Uye uye = new E_Uye();
            uye.Eposta = EpostaTxtBox.Text;
            uye.Sifre = SifreTxtBox.Text;
            int durum = BLL_Uye.UyeGiris(uye);

            if(durum == 0 || durum == -1)
                MessageBox.Show("E-posta yada şifrenizi kontrol edin.");
            else if (durum == 1) //DÖNEN YETKİ 1 İSE MÜŞTERİ
            {
                E_Uye bilgi=BLL_Uye.UyeBilgiGetir(uye.Eposta, uye.Sifre);

                Session.TC = bilgi.TC;
                Session.UyeAd = bilgi.Ad;
                Session.UyeSoyad = bilgi.Soyad;
                Session.Eposta = uye.Eposta;
                Session.Yetki = 1;


                KullaniciArayuz kullanici = new KullaniciArayuz();
                kullanici.Show();
                this.Hide();
            }
            else if(durum == 2) //DÖNEN YETKİ 2 İSE ŞUBE
            {
                E_Sube bilgi = BLL_Sube.SubeIDGetir(uye.Eposta, uye.Sifre);
                Session.UyeAd = bilgi.Ad;
                Session.SubeID = bilgi.ID;
                SubeArayuz sube = new SubeArayuz();
                sube.Show();
                this.Hide();
            }
            else if(durum == 3) //DÖNEN YETKİ 3 İSE YÖNETİCİ
            {
                YonetimArayuz yonetim = new YonetimArayuz();
                yonetim.Show();
                this.Hide();
            }


        }

        private void UyeliksizButon_Click(object sender, EventArgs e)
        {
            Session.Yetki = 0;
            KullaniciArayuz kullanici = new KullaniciArayuz();
            kullanici.Show();
            this.Hide();
        }

        private void UyeOlButon_Click(object sender, EventArgs e)
        {
            UyeOlForm uyeol = new UyeOlForm();
            this.Hide();
            uyeol.ShowDialog();
            
        }
        private void CikisButon_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
