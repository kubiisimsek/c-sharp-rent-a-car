using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AracKiralamaOtomasyon.Entity;
using AracKiralamaOtomasyon.BLL;

namespace AracKiralamaOtomasyon
{
    public partial class KullaniciArayuz : Form
    {
        public KullaniciArayuz()
        {

            InitializeComponent();
            tabControl1.Appearance = TabAppearance.FlatButtons; tabControl1.ItemSize = new Size(0, 1); tabControl1.SizeMode = TabSizeMode.Fixed;

            switch (Session.Yetki)
            {
                case 0: // ÜYE OLMAYANLAR İÇİN 
                    SessionKBilgiLabel.Text = "Misafir";
                    UcretLabel1.Text = "Ücret :";
                    iletisimMenuItem.Visible = false;
                    gecmisimMenuItem.Visible = false;
                    UyelikIslemMenuItem.Visible = false;
                    UyeOlmayanGroupBox.Visible = true; //KİRALAMA EKRANINDA KULLANICI ÜYE DEĞİLSE KİŞİSEL BİLGİLERİN GİRİLDİĞİ GROUPBOX'IN GÖZÜKMESİ
                    KrediKartıButon.Text = "Devam Et";
                    break;
                case 1: // ÜYELER İÇİN
                    UcretLabel1.Text = "Size Özel Ücret :";
                    SessionKBilgiLabel.Text = Session.UyeAd + " " + Session.UyeSoyad;
                    iletisimMenuItem.Visible = true;
                    gecmisimMenuItem.Visible = true;
                    UyelikIslemMenuItem.Visible = true;
                    UyeOlmayanGroupBox.Visible = false;
                    KrediKartıButon.Text = "Ödemeyi Tamamla";

                    TakipLabel.Visible = false; //ÜYELERE TÜM KİRALAMALAR LİSTELENDİĞİ İÇİN KODLA ARAMA KISMINI GİZLEDİM
                    TakipKodTxtBox.Visible = false;
                    TakipAraButon.Visible = false;
                    break;
            }
            
        }

        // MENÜ GEÇİŞ KODLARI

        private void AracKiralaMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(KiralaPage);
            AracGridView.DataSource = null; //HER GİRİLDİĞİNDE YENİLENMESİ İÇİN
        }

        private void islemTakipMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(TakipPage);
            if(Session.Yetki == 1)
            {
                UyeKiralamaGoster();
            }
        }

        private void CikisMenuItem_Click(object sender, EventArgs e)
        {
            GirisForm Giris = new GirisForm();
            Giris.Show();
            this.Close();
        }

        private void KullaniciArayuz_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void KiralaButon_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(OdemePage);
            byte[] resim = BLL_Arac.AracResimGetir(PlakaLabel.Text);
            if(resim != null)
            {
                MemoryStream mem = new MemoryStream(resim);
                O_PictureBox.Image = Image.FromStream(mem);
            }
        }

        private void gecmisimMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(GecmisPage);
            GecmisKiralamalar();
            GecmisSikayetler();
            GecmisMesajlar();
        }

        private void UyelikIslemMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(UyelikPage);
        }

        private void iletisimMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(IletisimPage);
        }

        //-----------------------------------------------------------------------------------------

        // -----------------------------------------------------------FORM AÇILDIĞINDA YAPILACAK İŞLEMLER

        private void KullaniciArayuz_Load(object sender, EventArgs e)
        {
            SubeComboDoldur();
            TumSubeler();

            if(Session.Yetki == 1)
                ProfilVerileri();
        }

        void SubeComboDoldur() // ŞUBE LİSTESİ
        {
            SubeComboBox.DataSource = null;
            MsjComboBox.DataSource = null;
            SikayetComboBox.DataSource = null;

            List<E_Sube> sube = new List<E_Sube>();
            sube = BLL_Sube.SubeListe();
            SubeComboBox.DataSource = sube;

            SubeComboBox.DisplayMember = "Ad";
            SubeComboBox.ValueMember = "ID";
            SubeComboBox.SelectedIndex = -1;

            List<E_Sube> sube2 = new List<E_Sube>();
            sube2 = BLL_Sube.SubeListe();
            MsjComboBox.DataSource = sube2;

            MsjComboBox.DisplayMember = "Ad";
            MsjComboBox.ValueMember = "ID";
            MsjComboBox.SelectedIndex = -1;


            List<E_Sube> sube3 = new List<E_Sube>();
            sube3 = BLL_Sube.SubeListe();
            SikayetComboBox.DataSource = sube3;

            SikayetComboBox.DisplayMember = "Ad";
            SikayetComboBox.ValueMember = "ID";
            SikayetComboBox.SelectedIndex = -1;

        }

        private void SubeComboBox_SelectedIndexChanged(object sender, EventArgs e) //ŞUBEYE GÖRE ARAÇ GETİRME
        {
            if (SubeComboBox.SelectedValue is int)
            {
                AracGridView.DataSource = null;
                DataTable dt = BLL_Arac.TekSubeninAraclari(Convert.ToInt32(SubeComboBox.SelectedValue));
                AracGridView.DataSource = dt;
                AracGridView.Refresh();
                AracGridView.ClearSelection();
            }


        }

        private void TextBoxTemizle() //TÜM TEXTBOXLARI TEMİZLEME FONKS.
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
        }

        private void Textbox_HarfEngelle(object sender, KeyPressEventArgs e) //BELİRLENEN TEXTBOXLAR İÇİN KARAKTER GİRİŞİ ENGELLEME ORTAK EVENT'I
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        //-----------------------------------------------------------------------------------------

        // Kiralama İşlemleri

        private void AracGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            for (int i = 0; i < AracGridView.Rows.Count; i++)
            {
                if (AracGridView.Rows[i].Cells["Durum"].Value.ToString() == "Kirada")
                    AracGridView.Rows[i].DefaultCellStyle.BackColor = Color.OrangeRed;

                else if (AracGridView.Rows[i].Cells["Durum"].Value.ToString() == "Teslim Bekliyor")
                    AracGridView.Rows[i].DefaultCellStyle.BackColor = Color.Goldenrod;
                else
                    AracGridView.Rows[i].DefaultCellStyle.BackColor = Color.ForestGreen;


            }
        }

        private void AracGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 0)
            {
                if(AracGridView.Rows[e.RowIndex].Cells["Durum"].Value.ToString() == "Kirada")
                    MessageBox.Show("Seçtiğiniz araç kirada olduğundan işlem yapılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if(AracGridView.Rows[e.RowIndex].Cells["Durum"].Value.ToString() == "Teslim Bekliyor")
                    MessageBox.Show("Seçtiğiniz araç henüz teslim edilmediğinden işlem yapılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    AracBilgiGroupBox.Enabled = true;
                    string plaka = AracGridView.Rows[e.RowIndex].Cells["Plaka"].Value.ToString();
                    PlakaLabel.Text = plaka;
                    KategoriLabel.Text = AracGridView.Rows[e.RowIndex].Cells["Kategori"].Value.ToString();
                    MarkaLabel.Text = AracGridView.Rows[e.RowIndex].Cells["Marka"].Value.ToString();
                    ModelLabel.Text = AracGridView.Rows[e.RowIndex].Cells["Model"].Value.ToString();
                    ModelYilLabel.Text = AracGridView.Rows[e.RowIndex].Cells["ModelYili"].Value.ToString();
                    MotorHacmiLabel.Text = AracGridView.Rows[e.RowIndex].Cells["Hacim"].Value.ToString();
                    MotorGucuLabel.Text = AracGridView.Rows[e.RowIndex].Cells["Guc"].Value.ToString();
                    YakitLabel.Text= AracGridView.Rows[e.RowIndex].Cells["Yakit"].Value.ToString();
                    SanzimanLabel.Text = AracGridView.Rows[e.RowIndex].Cells["Sanziman"].Value.ToString();
                    KilometreLabel.Text = AracGridView.Rows[e.RowIndex].Cells["Kilometre"].Value.ToString();
                    RenkLabel.Text = AracGridView.Rows[e.RowIndex].Cells["Renk"].Value.ToString();
                    DonanimLabel.Text = AracGridView.Rows[e.RowIndex].Cells["Donanim"].Value.ToString();
                    if(Session.Yetki == 1)
                        GunlukUcretLabel.Text = BLL_Arac.UyeUcretiGetir(plaka).ToString();
                    else
                        GunlukUcretLabel.Text= AracGridView.Rows[e.RowIndex].Cells["Ucret"].Value.ToString();

                    byte[] resim = BLL_Arac.AracResimGetir(plaka);
                    if(resim != null)
                    {
                        MemoryStream mem = new MemoryStream(resim);
                        AracPictureBox.Image = Image.FromStream(mem);
                    }
                    else
                    {
                        AracPictureBox.Image = null;
                    }



                }
            }
        }
        // ÖDEME SAYFASI  
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedTab == OdemePage) //ÖDEME SAYFASI AÇILDIĞINDA YAPILACAK İŞLEMLER
            {
                O_SubeLabel.Text = SubeComboBox.SelectedValue.ToString();
                O_PlakaLabel.Text = PlakaLabel.Text;
                O_KategoriLabel.Text = KategoriLabel.Text;
                O_MarkaLabel.Text = MarkaLabel.Text;
                O_ModelLabel.Text = ModelLabel.Text;
                O_YilLabel.Text = ModelYilLabel.Text;
                O_HacimLabel.Text = MotorHacmiLabel.Text;
                O_GucLabel.Text = MotorGucuLabel.Text;
                O_YakitLabel.Text = YakitLabel.Text;
                O_SanzimanLabel.Text = SanzimanLabel.Text;
                O_KilometreLabel.Text = KilometreLabel.Text;
                O_RenkLabel.Text = RenkLabel.Text;
                O_DonanimLabel.Text = DonanimLabel.Text;
                O_UcretLabel.Text = GunlukUcretLabel.Text;

                BaslangicTimePicker.MinDate = DateTime.Now;
                BaslangicTimePicker.MaxDate = DateTime.Now.AddDays(7);
                BitisTimePicker.Enabled = false;
            }
            else
            {
                BitisTimePicker.Enabled = false;
                KiraSureLabel.Text = "-";
                ToplamUcretLabel.Text = "-";
                
            }
        }
        //TARİH İŞLEMLERİ
        private void BaslangicTimePicker_ValueChanged(object sender, EventArgs e)
        {
            BitisTimePicker.Enabled = true;

            BitisTimePicker.MinDate = BaslangicTimePicker.Value.AddDays(1);
            BitisTimePicker.MaxDate = BitisTimePicker.MinDate.AddDays(30);
           
        }

        private void BitisTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if(BitisTimePicker.Enabled==true && O_UcretLabel.Text != "-")
            {
                TimeSpan GunHesapla = BitisTimePicker.Value - BaslangicTimePicker.Value;
                int Gun = Convert.ToInt32(GunHesapla.TotalDays);
                KiraSureLabel.Text = Gun.ToString() + " Gün";
                double Toplamucret = Convert.ToDouble(O_UcretLabel.Text) * Gun;

                ToplamUcretLabel.Text = Toplamucret.ToString();
            }

        }

        private void SubedeRadioButon_CheckedChanged(object sender, EventArgs e)
        {
            if (SubedeRadioButon.Checked == true && Session.Yetki == 1) //ÜYEYSE
            {
                KiraBilgiButon.Text = "Kiralamayı Tamamla";
                OnlineOdemeGroupBox.Enabled = false;
            }
            else //DEĞİLSE
            {
                KiraBilgiButon.Text = "Devam Et";
            }
        }

        private void KiraBilgiButon_Click(object sender, EventArgs e)
        {
            if (SubedeRadioButon.Checked == true && BitisTimePicker.Enabled==true && KiraSartCheckBox.Checked == true && Session.Yetki == 1) // ÜYE OLAN
            {
                UyeyeKiralama();
            }
            else if(OnlineRadioButon.Checked==true && BitisTimePicker.Enabled == true && KiraSartCheckBox.Checked == true && Session.Yetki == 1)
            {
                
                OnlineOdemeGroupBox.Enabled = true;
                KartAdSoyadTxtBox.Focus();
            }
            else if (SubedeRadioButon.Checked == true && BitisTimePicker.Enabled == true && KiraSartCheckBox.Checked == true && Session.Yetki==0) // ÜYE OLMAYAN 
            {
                OnlineOdemeGroupBox.Enabled = false;
                UyeOlmayanGroupBox.Enabled = true;
                EpostaTxtBox.Focus();
            }
            else if(OnlineRadioButon.Checked == true && BitisTimePicker.Enabled == true && KiraSartCheckBox.Checked == true && Session.Yetki == 0)
            {
                OnlineOdemeGroupBox.Enabled = true;
                UyeOlmayanGroupBox.Enabled = false;
                EpostaTxtBox.Focus();
            }
            else if(KiraSartCheckBox.Checked == false)
            {
                MessageBox.Show("Kira şartlarını onaylayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("İşlemleri kontrol ederek tekrar deneyiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        void UyeOlmayanKiralama() //ÜYE OLMAYAN KULLANICILARIN KİRALAMA YAPMA FONKS.
        {
            E_Uye uye = new E_Uye();
            E_Kiralama kira = new E_Kiralama();

            kira.TakipKod = TakipkodGenerator.TakipKod(0).ToString();
            kira.Plaka = O_PlakaLabel.Text;
            uye.Eposta = EpostaTxtBox.Text;
            uye.TC = TCTxtBox.Text;
            uye.Ad = AdTxtBox.Text;
            uye.Soyad = SoyadTxtBox.Text;
            uye.DogumTarihi = DogumTarihPicker.Value;
            if (ERadioButon.Checked == true)
                uye.Cinsiyet = 'E';
            else
                uye.Cinsiyet = 'K';

            uye.Adres = AdresTxtBox.Text;
            uye.Telefon = TelTxtBox.Text;
            uye.EhliyetSinifi = EhSinifTxtBox.Text;
            uye.EhliyetYili =Convert.ToInt32(EhYilTxtBox.Text);
            kira.BaslangicTarihi = BaslangicTimePicker.Value;
            kira.BitisTarihi = BitisTimePicker.Value;
            if (OnlineRadioButon.Checked == true)
                kira.OdemeTuru = "Online";
            else
                kira.OdemeTuru = "Şubede";

            kira.OdenenUcret = Convert.ToDouble(ToplamUcretLabel.Text);
            kira.Aciklama = AciklamaTxtBox.Text;

            int durum = BLL_Kiralama.UyeOlmayanaKirala(uye, kira);

            if(durum == -1)
                MessageBox.Show("Girdiğiniz verileri kontrol ederek tekrar deneyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if(durum == -2)
                MessageBox.Show("Kiralama yapılırken bir sorun oluştu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if(durum == 0)
                MessageBox.Show("Bu TC No ile zaten aktif bir kiralama var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if(durum == 1)
            {
                MessageBox.Show(PlakaLabel.Text+" Plakalı araç başarıyla kiralandı.\nTakip Kodunuz:"+kira.TakipKod+"\nKiralama bilgileriniz e-posta adresinize gönderildi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Mail yeni = new Mail();
                yeni.MailGonder(kira.TakipKod,uye, kira, MailIcınAracBilgileri()); //MAİL GÖNDERME
                TextBoxTemizle();
                AracBilgiGroupBox.Enabled = false;
                tabControl1.SelectTab(KiralaPage);
                AracGridView.DataSource = null;
                SubeComboBox.SelectedIndex = -1;
            }

        }

        void UyeyeKiralama() //ÜYE OLAN KULLANICILARIN KİRALAMA YAPMA FONKS.
        {
            E_Uye uye = new E_Uye();
            E_Kiralama kira = new E_Kiralama();
            uye.TC = Session.TC;

            kira.TakipKod = TakipkodGenerator.TakipKod(1).ToString();
            kira.Plaka = O_PlakaLabel.Text;

            kira.BaslangicTarihi = BaslangicTimePicker.Value;
            kira.BitisTarihi = BitisTimePicker.Value;
            if (OnlineRadioButon.Checked == true)
                kira.OdemeTuru = "Online";
            else
                kira.OdemeTuru = "Şubede";

            kira.OdenenUcret = Convert.ToDouble(ToplamUcretLabel.Text);
            kira.Aciklama = AciklamaTxtBox.Text;

            int durum = BLL_Kiralama.UyeyeKirala(uye, kira);

            if (durum == -1)
                MessageBox.Show("Girdiğiniz verileri kontrol ederek tekrar deneyin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if(durum == 0)
                MessageBox.Show("Devam eden bir kiralamanız zaten var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                
                uye.Ad = Session.UyeAd;
                uye.Soyad = Session.UyeSoyad;
                uye.Eposta = Session.Eposta;

                MessageBox.Show(PlakaLabel.Text + " Plakalı araç başarıyla kiralandı.\nTakip Kodunuz:" + kira.TakipKod + "\nKiralama bilgileriniz e-posta adresinize gönderildi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Mail yeni = new Mail();
                yeni.MailGonder(kira.TakipKod,uye, kira, MailIcınAracBilgileri());
                TextBoxTemizle();
                AracBilgiGroupBox.Enabled = false;
                tabControl1.SelectTab(KiralaPage);
                AracGridView.DataSource = null;
                UyeKiralamaGoster();
                SubeComboBox.SelectedIndex = -1;
            }
        }

        private E_Arac MailIcınAracBilgileri()
        {
            E_Arac arac = new E_Arac(); // MAİL İÇİN ARAÇ BİLGİLERİNİ TUTMA
            arac.Marka = O_MarkaLabel.Text; //MAİL İÇİN
            arac.Model = O_ModelLabel.Text; //MAİL İÇİN
            arac.ModelYili = Convert.ToInt32(O_YilLabel.Text); //MAİL İÇİN
            arac.Hacim = O_HacimLabel.Text; //MAİL İÇİN
            arac.Guc = O_GucLabel.Text; //MAİL İÇİN
            arac.Yakit = O_YakitLabel.Text; //MAİL İÇİN
            arac.Sanziman = O_SanzimanLabel.Text; //MAİL İÇİN
            arac.Renk = O_RenkLabel.Text;//MAİL İÇİN
            arac.Donanim = O_DonanimLabel.Text; //MAİL İÇİN

            return arac;
        }

        private void KrediKartıButon_Click(object sender, EventArgs e)
        {
            if(KartAdSoyadTxtBox.Text.Length > 3 && KartNoTxtBox.Text.Length == 16 && CVCTxtBox.Text.Length == 3 && KartBitisGunComboBox.SelectedIndex > 0 && KartBitisAyComboBox.SelectedIndex > 0)
            {
                if(Session.Yetki == 1)
                    UyeyeKiralama();
                else if(Session.Yetki == 0)
                {
                    UyeOlmayanGroupBox.Enabled = true;
                    EpostaTxtBox.Focus();
                }
            }
            else
            {
                MessageBox.Show("Kart bilgilerinizi kontrol ediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UyeOlmayanTamamlaButon_Click(object sender, EventArgs e)
        {
            UyeOlmayanKiralama();
        }

        //-----------------------------------------------------------------------------------------
        // İŞLEM TAKİP SAYFASI

        void UyeKiralamaGoster() //MENÜ DEĞİŞTİRME İÇİNDE BULUNACAK FONKS.
        {
            TakipGridView.DataSource = null;
            DataTable dt = BLL_Kiralama.UyeKiralamaGoster(Session.TC);
            TakipGridView.DataSource = dt;
            TakipGridView.Refresh();
            TakipGridView.ClearSelection();
        }

        private void TakipAraButon_Click(object sender, EventArgs e)
        {
            string takip = TakipKodTxtBox.Text;
            DataTable dt = BLL_Kiralama.TakipSorgula(takip);

            if (TakipKodTxtBox.Text.Length != 10 || dt.Rows.Count == 0)
                MessageBox.Show("Takip kodu bulunamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else
            {
                TakipGridView.DataSource = null;
                TakipGridView.DataSource = dt;
                TakipGridView.Refresh();
                TakipGridView.ClearSelection();
            }
        }

        private void TakipGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(Session.Yetki == 1)
            {
                if (e.ColumnIndex == 0)
                {
                    SureUzatGroupBox.Enabled = false;
                    if(TakipGridView.Rows[e.RowIndex].Cells["KiraDurumu"].Value.ToString() == "Başlamadı")
                    {
                        iptalGroupBox.Enabled = true;
                        iptalTakipkodLabel.Text = TakipGridView.Rows[e.RowIndex].Cells["TakipKod"].Value.ToString();
                        iptalPlakaLabel.Text = TakipGridView.Rows[e.RowIndex].Cells["Plaka"].Value.ToString();
                        iptalBaslangicLabel.Text = Convert.ToDateTime(TakipGridView.Rows[e.RowIndex].Cells["BaslangicTarihi"].Value).ToShortDateString();
                    }
                    else
                    {
                        MessageBox.Show("Kira başladığı için işlem yapılamıyor.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                if(e.ColumnIndex == 1)
                {
                    iptalGroupBox.Enabled = false;
                    SureUzatGroupBox.Enabled = true;
                    UzatTakipKodLabel.Text= TakipGridView.Rows[e.RowIndex].Cells["TakipKod"].Value.ToString();
                    uzatPlakaLabel.Text= TakipGridView.Rows[e.RowIndex].Cells["Plaka"].Value.ToString();
                    uzatEskiBitisLabel.Text = Convert.ToDateTime(TakipGridView.Rows[e.RowIndex].Cells["BitisTarihi"].Value).ToShortDateString();
                    UzatBaslangicLabel.Text = Convert.ToDateTime(TakipGridView.Rows[e.RowIndex].Cells["BaslangicTarihi"].Value).ToShortDateString();
                    uzatOdenenLabel.Text= TakipGridView.Rows[e.RowIndex].Cells["OdenenUcret"].Value.ToString();
                    uzatTimePicker.MinDate = Convert.ToDateTime(TakipGridView.Rows[e.RowIndex].Cells["BitisTarihi"].Value);
                    uzatTimePicker.MaxDate = uzatTimePicker.MinDate.AddDays(30);
                }


            }
            else
            {
                MessageBox.Show("Bu işlemlerden sadece üyeler yararlanabilir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void TakipGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            for (int i = 0; i < TakipGridView.Rows.Count; i++)
            {
                if (TakipGridView.Rows[i].Cells["KiraDurumu"].Value.ToString() == "Başlamadı")
                    TakipGridView.Rows[i].DefaultCellStyle.BackColor = Color.Goldenrod;
                else if(TakipGridView.Rows[i].Cells["KiraDurumu"].Value.ToString() == "Kirada")
                    TakipGridView.Rows[i].DefaultCellStyle.BackColor = Color.ForestGreen;


            }
        }

        private void iptalButon_Click(object sender, EventArgs e)
        {
            if(iptalSartlarCheckBox.Checked == true)
            {
                E_IptalEdilenKiralama iptal = new E_IptalEdilenKiralama();
                iptal.TakipKod = iptalTakipkodLabel.Text.ToString();
                iptal.Sebep = iptalSebepTxtBox.Text.ToString();
                iptal.Aciklama = iptalAciklamaTxtBox.Text.ToString();
                int durum = BLL_IptalEdilenKiralama.KiraIptal(iptal);
                if(durum > 0)
                {
                    MessageBox.Show(iptal.TakipKod + " Takip kodlu kiralamanız iptal edildi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TakipGridView.DataSource = null;
                    TakipKodTxtBox.Clear();
                    iptalTakipkodLabel.Text = "-";
                    iptalBaslangicLabel.Text = "-";
                    iptalPlakaLabel.Text = "-";
                    iptalGroupBox.Enabled = false;
                    iptalSartlarCheckBox.Checked = false;
                    iptalSebepTxtBox.Clear();
                    iptalAciklamaTxtBox.Clear();
                    UyeKiralamaGoster();
                    GecmisKiralamalar();
                }
                else if(durum == -1)
                {
                    MessageBox.Show("Girdiğiniz verileri kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Kiralama iptal edilemedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Kira şartlarını onaylayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void uzatTimePicker_ValueChanged(object sender, EventArgs e)
        {
            double uyeucreti = BLL_Arac.UyeUcretiGetir(uzatPlakaLabel.Text);
            TimeSpan GunHesapla = uzatTimePicker.Value - Convert.ToDateTime(UzatBaslangicLabel.Text);
            int Gun = Convert.ToInt32(GunHesapla.TotalDays);
            double Toplamucret = uyeucreti * Gun;
            double ekucret = Toplamucret - Convert.ToDouble(uzatOdenenLabel.Text);
            uzatEkUcretLabel.Text = ekucret.ToString();
        }

        private void uzatButon_Click(object sender, EventArgs e) //SÜRE UZATMA BUTONU
        {
            if (sureUzatCheckBox.Checked == true)
            {
                if(uzatEkUcretLabel.Text.ToString() != "0")
                {
                    E_Kiralama uzat = new E_Kiralama();
                    uzat.TakipKod = UzatTakipKodLabel.Text;
                    uzat.BitisTarihi = uzatTimePicker.Value;
                    uzat.OdenenUcret = Convert.ToDouble(uzatEkUcretLabel.Text) + Convert.ToDouble(uzatOdenenLabel.Text);
                    int durum = BLL_Kiralama.KiraSureUzat(uzat);
                    if(durum == 0)
                    {
                        MessageBox.Show("Süre uzatılamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(uzatPlakaLabel.Text + " Plakalı aracın kira süresi uzatıldı.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UzatTakipKodLabel.Text = "-";
                        UzatBaslangicLabel.Text = "-";
                        uzatEskiBitisLabel.Text = "-";
                        uzatOdenenLabel.Text = "-";
                        uzatPlakaLabel.Text = "-";
                        uzatEkUcretLabel.Text = "-";
                        sureUzatCheckBox.Checked = false;
                        UyeKiralamaGoster();
                        
                        SureUzatGroupBox.Enabled = false;
                    }

                }

            }
            else
            {
                MessageBox.Show("Uzatma şartlarını onaylayınız.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //-----------------------------------------------------------------------------------------
        // İLETİŞİM SAYFASI

        void TumSubeler() // FORM LOAD İÇİN
        {
            TumSubelerGridView.DataSource = null;
            DataTable dt = BLL_Sube.TumSubeler();
            dt.Columns.Remove("Eposta");
            dt.Columns.Remove("Tarih");
            TumSubelerGridView.DataSource = dt;
            TumSubelerGridView.ClearSelection();
        }

        private void MsjGonderButon_Click(object sender, EventArgs e) //ŞUBEYE MESAJ GÖNDERME BUTONU
        {
            if(MsjComboBox.SelectedIndex > -1)
            {
                E_Mesaj msj = new E_Mesaj();
                msj.SubeID = Convert.ToInt32(MsjComboBox.SelectedValue);
                msj.UyeTC = Session.TC;
                msj.Konu = MsjKonuTxtBox.Text;
                msj.Mesaj = MsjMetniTxtBox.Text;

                int durum = BLL_Mesaj.MesajGonder(msj);
                if(durum == -1)
                {
                    MessageBox.Show("Yıldızlı alanların tümünü doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if(durum == 0)
                {
                    MessageBox.Show("Mesaj gönderilirken bir hata oluştu", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Mesaj başarıyla gönderildi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MsjComboBox.SelectedIndex = -1;
                    MsjKonuTxtBox.Clear();
                    MsjMetniTxtBox.Clear();
                }

            }
        }

        private void SikayetEtButon_Click(object sender, EventArgs e) //YÖNETİCİYE ŞİKAYET İLETMEK İÇİN
        {
            if (SikayetComboBox.SelectedIndex > -1)
            {
                E_Sikayet sikayet = new E_Sikayet();
                sikayet.SubeID = Convert.ToInt32(SikayetComboBox.SelectedValue);
                sikayet.UyeTC = Session.TC;
                sikayet.Konu = SikayetKonuTxtBox.Text;
                sikayet.SikayetMesaj = SikayetMsjTxtBox.Text;

                int durum = BLL_Sikayet.SikayetGonder(sikayet);
                if (durum == -1)
                {
                    MessageBox.Show("Yıldızlı alanların tümünü doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (durum == 0)
                {
                    MessageBox.Show("Şikayet gönderilirken bir hata oluştu", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Şikayet başarıyla iletildi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SikayetComboBox.SelectedIndex = -1;
                    SikayetKonuTxtBox.Clear();
                    SikayetMsjTxtBox.Clear();
                }

            }
        }

        //-----------------------------------------------------------------------------------------
        //GEÇMİŞ SAYFASI

        void GecmisKiralamalar() // ÜYELERİN GEÇMİŞ KİRALAMA GOSTEREN FONKS.
        {
            GecmisKiralamaGridView.DataSource = null;
            DataTable dt = BLL_Kiralama.UyeGecmisKiralama(Session.TC);
            GecmisKiralamaGridView.DataSource = dt;
            GecmisKiralamaGridView.ClearSelection();
        }

        private void GecmisKiralamaGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e) //GEÇMİŞ KİRALAMALARDAN İPTAL OLANLAR SARI,TAMAMLANANLAR YEŞİL YAPMA
        {
            for (int i = 0; i < GecmisKiralamaGridView.Rows.Count; i++)
            {
                if (GecmisKiralamaGridView.Rows[i].Cells["KiraDurumu"].Value.ToString() == "İptal Edildi")
                    GecmisKiralamaGridView.Rows[i].DefaultCellStyle.BackColor = Color.Goldenrod;

                else if (GecmisKiralamaGridView.Rows[i].Cells["KiraDurumu"].Value.ToString() == "Tamamlandı")
                    GecmisKiralamaGridView.Rows[i].DefaultCellStyle.BackColor = Color.ForestGreen;

            }
        }

        void GecmisSikayetler() //ÜYELERİN GEÇMİŞ ŞİKAYERLERİNİ GÖSTEREN FONKS.
        {
            GecmisSikayetGridView.DataSource = null;
            GecmisSikayetGridView.DataSource = BLL_Sikayet.UyeGecmisSikayet(Session.TC);
            GecmisSikayetGridView.ClearSelection();
        }

        void GecmisMesajlar()
        {
            GecmisMesajGridView.DataSource = null;
            GecmisMesajGridView.DataSource = BLL_Mesaj.UyeGecmisMesaj(Session.TC);
            GecmisMesajGridView.ClearSelection();
        }

        //-----------------------------------------------------------------------------------------
        //ÜYE PROFİL SAYFASI

        void ProfilVerileri()
        {
            E_Uye uye = BLL_Uye.UyeninProfilVerileri(Session.TC);
            ProfilEpostaLabel.Text = uye.Eposta.ToString();
            ProfilAdLabel.Text = uye.Ad.ToString();
            ProfilSoyadLabel.Text = uye.Soyad.ToString();
            ProfilDgmTarihLabel.Text = uye.DogumTarihi.ToShortDateString();
            ProfilCinsiyetLabel.Text = uye.Cinsiyet.ToString();
            ProfilSehirLabel.Text=uye.Sehir.ToString();
            ProfilAdresLabel.Text=uye.Adres.ToString();
            ProfilTelLabel.Text=uye.Telefon.ToString();
            ProfilEhSinifLabel.Text=uye.EhliyetSinifi.ToString();
            ProfilEhYilLabel.Text = uye.EhliyetYili.ToString();
            ProfilKytTarihLabel.Text=uye.KayitTarihi.ToString();
        }

        private void SifreGuncelleButon_Click(object sender, EventArgs e)
        {
            if(EskiSifreTxtBox.Text.Length > 5 && YeniSifreTxtBox.Text.Length > 5 && YeniTekrarTxtBox.Text.Length > 5)
            {
                E_Uye kontrol = new E_Uye();
                kontrol.Eposta = Session.Eposta;
                kontrol.Sifre = EskiSifreTxtBox.Text.ToString();
                int durum = BLL_Uye.UyeGiris(kontrol);
                if (durum > 0)
                {
                    if (YeniSifreTxtBox.Text.ToString() == YeniTekrarTxtBox.Text.ToString())
                    {
                        E_Uye uye = new E_Uye();
                        uye.Eposta = Session.Eposta;
                        uye.Sifre = YeniSifreTxtBox.Text.ToString();
                        int durum2 = BLL_Uye.UyeSifreDegistir(uye);
                        if (durum2 == 0 || durum2 == -1)
                        {
                            MessageBox.Show("Şifre değiştirilirken bir sorun oluştu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Şifreniz değiştirildi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TextBoxTemizle();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Yeni şifreniz tekrarıyla uyuşmuyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Eski şifrenizi kontrol ediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Girdiğiniz şifreleri kontrol ediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        private void GuncelleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (GuncelleCheckBox.Checked == true)
            {
                ProfilGuncelleGroupBox.Enabled = true;
                PGTelTxtBox.Text = ProfilTelLabel.Text;
                PGAdresTxtBox.Text = ProfilAdresLabel.Text;
                PGSehirComboBox.SelectedItem = ProfilSehirLabel.Text;
                PGEhSinifTxtBox.Text = ProfilEhSinifLabel.Text;
                PGEhYilNumeric.Value = Convert.ToInt32(ProfilEhYilLabel.Text);
            }
            else
            {
                ProfilGuncelleGroupBox.Enabled = false;
                TextBoxTemizle();
                PGSehirComboBox.SelectedIndex = -1;
                PGEhYilNumeric.Value = 0;
            }
        }

        private void ProfilGuncelleButon_Click(object sender, EventArgs e)
        {
            E_Uye uye = new E_Uye();
            uye.TC = Session.TC;
            uye.Sehir = PGSehirComboBox.SelectedItem.ToString();
            uye.Adres = PGAdresTxtBox.Text;
            uye.Telefon = PGTelTxtBox.Text;
            uye.EhliyetSinifi = PGEhSinifTxtBox.Text;
            uye.EhliyetYili = Convert.ToInt32(PGEhYilNumeric.Value);

            int durum = BLL_Uye.UyeProfilGuncelle(uye);
            if(durum == -1)
                MessageBox.Show("Girdiğiniz verileri kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if(durum == 0)
                MessageBox.Show("Bilgiler güncellenirken bir sorun oluştu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                MessageBox.Show("Güncelleme başarıyla gerçekleştirildi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GuncelleCheckBox.Checked = false;
                ProfilGuncelleGroupBox.Enabled = false;
                TextBoxTemizle();
                PGSehirComboBox.SelectedIndex = -1;
                PGEhYilNumeric.Value = 0;
                ProfilVerileri();
            }
                
        }   
    }
}
