using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using AracKiralamaOtomasyon.Entity;

namespace AracKiralamaOtomasyon
{
    class Mail
    {
        SmtpClient client = new SmtpClient();
        MailMessage mesaj = new MailMessage();

        public Mail()
        {
            this.client.Port = 587;
            this.client.Host = "mail.kubisimsek.com";
            this.client.EnableSsl = false;
            this.client.Credentials = new NetworkCredential("rentcar@kubisimsek.com", "mailsifre123");

            this.mesaj.From = new MailAddress("rentcar@kubisimsek.com", "Şimşek Rent A Car");
            this.mesaj.IsBodyHtml = true;
        }
     
        public void MailGonder(string mail,string sifre,string ad,string soyad) // ÜYE KAYDI BİLGİLENDİRME MAİLİ
        {
            this.mesaj.To.Add(mail);
            this.mesaj.Subject = "Kayıt Bilgilendirme";
            this.mesaj.Body = @"<h3>Merhaba," + ad + " " + soyad + "</h3><br><h4>Sistemimize kayıt olduğun için teşekkür ederiz." +
                "</h4><br><h4>Giriş bilgilerin,</h4>" +
                "<br><h5>E-Posta:"+mail+"" +
                "<br><h5>Şifre:" + sifre + "" +
                "<br><h3>Uygulamaya giriş yapıp istediğin aracı kiralayabilirsin.Görüşmek üzere !</h3>";

            try
            {
                this.client.Send(this.mesaj);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Mail gönderilirken bir sorun oluştu.","Uyarı", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        public bool MailGonder(string mail,string ad,string soyad,string konu,string icerik) // YÖNETİCİDEN ÜYEYE MAİL
        {
            bool durum;
            this.mesaj.To.Add(mail);
            this.mesaj.Subject = konu;
            this.mesaj.Body = @"<h3>Merhaba," + ad + " " + soyad + "</h3>" +
                "<br><h4>"+ icerik+ "</h4>" +
                "<br><h3>Şimşek Rent A Car Yönetim Ekibi</h3>";

            try
            {
                this.client.Send(this.mesaj);
                durum = true;
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Mail gönderilirken bir sorun oluştu.", "Uyarı",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Warning);
                durum = false;
            }

            return durum;
        }

        public bool MailGonder(string mail,string sehir,string ilce,string ad,string konu, string icerik) // YÖNETİCİDEN ŞUBEYE MAİL
        {
            bool durum;
            this.mesaj.To.Add(mail);
            this.mesaj.Subject = konu;
            this.mesaj.Body = @"<h3>Merhaba," + sehir+"-"+ilce+" "+ad+ "</h3>" +
                "<br><h4>" + icerik + "</h4>" +
                "<br><h3>Şimşek Rent A Car Yönetim Ekibi</h3>";

            try
            {
                this.client.Send(this.mesaj);
                durum = true;
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Mail gönderilirken bir sorun oluştu.", "Uyarı", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                durum = false;
            }

            return durum;
        }

        public bool MailGonder(string mail,string ad,string soyad,string subeAd,string sikayet,DateTime tarih,string gidecekMesaj) // YÖNETİCİDEN ŞUBEYE MAİL 2
        {
            bool durum;
            this.mesaj.To.Add(mail);
            this.mesaj.Subject = subeAd+" Şikayeti Hakkında Bilgilendirme";
            this.mesaj.Body = @"<h3>Merhaba," + ad + " " + soyad + "</h3>" +
                "<br><h5>" +"Şikayetiniz:"+ sikayet + "</h5>" +
                "<br><h4>" + gidecekMesaj + "</h4>" +
                "<br><h3>Şimşek Rent A Car Yönetim Ekibi</h3>";

            try
            {
                this.client.Send(this.mesaj);
                durum = true;
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Mail gönderilirken bir sorun oluştu.", "Uyarı", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                durum = false;
            }

            return durum;
        }

        public bool MailGonder(string TakipKod,E_Uye uye, E_Kiralama kira,E_Arac arac) //ARAÇ KİRALAYAN ÜYE BİLGİLENDİRME MAİL
        {
            bool durum;
            try
            {          
            this.mesaj.To.Add(uye.Eposta);
            this.mesaj.Subject = "Kiralama İşlemi E-Fatura";
            this.mesaj.Body = @"<h3>Merhaba," + uye.Ad + " " + uye.Soyad + "</h3>" +
                "<br><h4>" +kira.Plaka +" Plakalı Araç Kiralaması İçin E-Fatura Bilgileriniz Aşağıdadır." +"</h4>" +
                "<br>Takip Kodu:" + TakipKod +
                "<br>Araç Plaka:" +kira.Plaka +
                "<br>Araç Bilgileri:" + arac.Marka +"-"+arac.ModelYili+"-"+arac.Model+"-" + arac.Hacim + "-" + arac.Guc + "HP-" + arac.Yakit + "-" + arac.Sanziman + "-" + arac.Renk + "-" + arac.Donanim +
                "<br>Kiralayan Bilgileri:" + uye.Ad +" "+uye.Soyad + 
                "<br>Kiralama Başlangıç Tarihi:" +kira.BaslangicTarihi.ToShortDateString() +
                "<br>Kiralama Bitiş Tarihi:" + kira.BitisTarihi.ToShortDateString() + 
                "<br>Ödeme Türü:" + kira.OdemeTuru +
                "<br>Ödenen Ücret:" + kira.OdenenUcret.ToString()+ "TL"+
                "<br><h4>Kiralamanız için teşekkür eder iyi günler dileriz.</h4>"+
                "<br><h3>Şimşek Rent A Car</h3>";


                this.client.Send(this.mesaj);
                durum = true;
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Mail gönderilirken bir sorun oluştu.", "Uyarı", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                durum = false;
            }

            return durum;
        }

        public bool SubedenUyeye(E_Uye uye,string konu,string mesaj) //ŞUBEDEN ÜYEYE MAİL
        {
            
            bool durum;
            this.mesaj.To.Add(uye.Eposta);
            this.mesaj.Subject =konu;
            this.mesaj.Body = @"<h3>Merhaba," + uye.Ad+ " " + uye.Soyad+ "</h3>" +
                "<br><h4>" + mesaj + "</h4>" +
                "<br><h3>Şimşek Rent A Car - "+Session.UyeAd+" Şubesi</h3>";

            try
            {
                this.client.Send(this.mesaj);
                durum = true;
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Mail gönderilirken bir sorun oluştu.", "Uyarı", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                durum = false;
            }

            return durum;
        }

    }
}
