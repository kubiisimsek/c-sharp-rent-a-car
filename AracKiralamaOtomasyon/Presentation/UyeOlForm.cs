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
using System.Text.RegularExpressions;

namespace AracKiralamaOtomasyon
{
    public partial class UyeOlForm : Form
    {
        public UyeOlForm()
        {
            InitializeComponent();

        }
        public static string bicim = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"; // Mail kontrolü için
        Regex regex = new Regex(bicim);

        private void CikisButon_Click(object sender, EventArgs e)
        {
            GirisForm yeni = new GirisForm();
            yeni.Show();
            this.Close();
        }

        private void UyeOlButon_Click(object sender, EventArgs e)
        {
            if (SifreTekrarTxtBox.Text == SifreTxtBox.Text)
            {
                E_Uye ekle = new E_Uye();

                ekle.Eposta = EpostaTxtBox.Text;
                ekle.Sifre = SifreTxtBox.Text;
                ekle.TC = TCTxtBox.Text;
                ekle.Ad = AdTxtBox.Text;
                ekle.Soyad = SoyadTxtBox.Text;
                ekle.DogumTarihi = DogumTarihPicker.Value;
                if (radioButton1.Checked == true)
                    ekle.Cinsiyet = Convert.ToChar(radioButton1.Text);
                else
                    ekle.Cinsiyet = Convert.ToChar(radioButton2.Text);

                ekle.Sehir = SehirComboBox.Text.ToString();
                ekle.Adres = AdresTxtBox.Text;
                ekle.Telefon = TelTxtBox.Text;
                ekle.EhliyetSinifi = EhSinifTxtBox.Text;
                ekle.EhliyetYili = Convert.ToInt32(EhYilNumeric.Value);

                int durum = BLL_Uye.UyeEkle(ekle);
                
                if (durum == -1)
                    MessageBox.Show("Lütfen tüm alanları eksiksiz doldurunuz.","Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
                else if(durum == 0)
                    MessageBox.Show("Bu e-posta zaten kayıtlı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                else if(durum == 1)
                    MessageBox.Show("Bu TC No zaten kayıtlı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                else if(durum == 2)
                {
                    MessageBox.Show("Kayıt işlemi başarıyla tamamlandı.\nBilgileriniz e-posta adresinize gönderildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Mail posta = new Mail();
                    posta.MailGonder(EpostaTxtBox.Text,SifreTxtBox.Text,AdTxtBox.Text,SoyadTxtBox.Text);
                    GirisForm yeni = new GirisForm();
                    yeni.Show();
                    this.Close();                   
                }
            }
            else
                MessageBox.Show("Giridğiniz şifreler uyuşmuyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void TCTxtBox_KeyPress(object sender, KeyPressEventArgs e) // TC GİRİLEN TEXTBOX İÇİN HARF GİRİŞİNİ ENGELLEME
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void TelTxtBox_KeyPress(object sender, KeyPressEventArgs e) // TELEFON GİRİLEN TEXTBOX İÇİN HARF GİRİŞİNİ ENGELLEME
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void EhYilTxtBox_KeyPress(object sender, KeyPressEventArgs e) // EHLİYET YILI GİRİLEN TEXTBOX İÇİN HARF GİRİŞİNİ ENGELLEME
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void SifreTxtBox_TextChanged(object sender, EventArgs e) // ŞİFRE 6 KARAKTER OLANA KADAR X İŞARETİ GÖZÜKÜYOR
        {
            sifreKontrolLabel.Visible = true;
            if (SifreTxtBox.Text.Length > 5)
            {
                sifreKontrolLabel.Text = "✓ ";
                sifreKontrolLabel.ForeColor = Color.LimeGreen;
            }
            else
            {
                sifreKontrolLabel.Text = "X";
                sifreKontrolLabel.ForeColor = Color.Red;
            }
           
        }

        private void SifreTekrarTxtBox_TextChanged(object sender, EventArgs e) // ŞİFRE TEKRARI ASIL ŞİFREYE EŞİT OLANA KADAR ÇARPI GÖZÜKÜYOR
        {
            sifreKontrolLabel2.Visible = true;
            if (SifreTekrarTxtBox.Text == SifreTxtBox.Text && SifreTekrarTxtBox.Text != "" && SifreTekrarTxtBox.Text.Length > 5)
            {
                sifreKontrolLabel2.Text = "✓ ";
                sifreKontrolLabel2.ForeColor = Color.LimeGreen;
            }
            else
            {
                sifreKontrolLabel2.Text = "X";
                sifreKontrolLabel2.ForeColor = Color.Red;
            }
        }

        private void EpostaTxtBox_TextChanged(object sender, EventArgs e)
        {
            mailKontrolLabel.Visible = true;
            if (regex.IsMatch(EpostaTxtBox.Text))
            {
                mailKontrolLabel.Text = "✓ ";
                mailKontrolLabel.ForeColor = Color.LimeGreen;
            }
            else
            {                
                mailKontrolLabel.Text = "X ";
                mailKontrolLabel.ForeColor = Color.Red;
            }
        }
    }
}
