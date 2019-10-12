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
using System.IO;

namespace AracKiralamaOtomasyon
{
    public partial class YonetimArayuz : Form
    {
        public YonetimArayuz()
        {
            InitializeComponent();

            tabControl1.Appearance = TabAppearance.FlatButtons; tabControl1.ItemSize = new Size(0, 1); tabControl1.SizeMode = TabSizeMode.Fixed; //TABCONTROL MENÜSÜNÜ GÖSTERMEMEK İÇİN

        }
        // EVENTLAR

        private void Textbox_HarfEngelle(object sender,KeyPressEventArgs e) //BELİRLENEN TEXTBOXLAR İÇİN KARAKTER GİRİŞİ ENGELLEME ORTAK EVENT'I
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void AracKMTxtBox_KeyPress(object sender, KeyPressEventArgs e) // KİLOMETRE TEXTBOX'I İÇİN VİRGÜL HARİÇ KARAKTER ENGELLEME
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ','; 
        }
        //-----------------------------------------------------------------------------------------

        // MENÜ GEÇİŞ KODLARI
        private void SikayetMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(SikayetPage);
            TumSikayetlerGridView.ClearSelection();
        }

        private void EklemeIslemMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(EklemeIslemPage);
        }

        private void KullaniciIstatistikMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(KullaniciIstatistikPage);
            TumUyelerGridView.ClearSelection();
        }

        private void AracIstatistikMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(AracIstatistikPage);
            TumAraclarGridView.ClearSelection();
            
        }

        private void SubeIstatistikMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(SubeIstatistikPage);
            TumSubelerGridView.ClearSelection();
        }

        private void CikisMenuItem_Click(object sender, EventArgs e)
        {          
            GirisForm giris = new GirisForm();
            giris.Show();
            this.Close();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           
        }

        private void KiralamalarMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(KiralamaIslemPage);
            TumKiralamaGridView.ClearSelection();
            BitenKiralamaGridView.ClearSelection();
            IptalKiralamaGridView.ClearSelection();
        }
        private void GrafikMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(GrafikPage);
            KiralamaChartVerileri();
            KategoridChartVerileri();
            AracChartVerileri();
            UyeChartVerileri();
        }
        //-----------------------------------------------------------------------------------------

        // -----------------------------------------------------------FORM AÇILDIĞINDA YAPILACAK İŞLEMLER
        private void YonetimArayuz_Load(object sender, EventArgs e)
        {
            KategoriMarkaComboDoldur();
            SubeComboDoldur();
            TumAraclarGridVerileri();
            TumUyeGridVerileri();
            TumSubeGridVerileri();
            UyeIstatistik();
            TumKiralamalarGridVerileri();
            TumSikayetGridVerileri();
            BitenKiralamaGridVerileri();
            IptalEdilenKiralamaGridVerileri();
            
        }

        void TumUyeGridVerileri() //YÖNETİCİ İÇİN TÜM ÜYELER
        {
            TumUyelerGridView.DataSource = null;
            DataTable dt = BLL_Uye.TumUyeler();
            TumUyelerGridView.DataSource = dt;
            TumUyelerGridView.Refresh();
            
        }

        void TumSubeGridVerileri() //YÖNETİCİ İÇİN TÜM ŞUBELER
        {
            TumSubelerGridView.DataSource = null;
            DataTable dt = BLL_Sube.TumSubeler();
            TumSubelerGridView.DataSource = dt;
            TumSubelerGridView.Refresh();
            TumSubelerGridView.ClearSelection();
            
        }

        void TumAraclarGridVerileri() // YÖNETİCİ İÇİN TÜM ARAÇLAR
        {
            TumAraclarGridView.DataSource = null;
            DataTable dt = BLL_Arac.TumAraclar();
            TumAraclarGridView.DataSource = dt;
            TumAraclarGridView.Refresh();
            
        }

        void TumKiralamalarGridVerileri() // YÖNETİCİ İÇİN TÜM KİRALAMALAR
        {
            TumKiralamaGridView.DataSource = null;
            DataTable dt = BLL_Kiralama.TumKiralamalar();
            TumKiralamaGridView.DataSource = dt;
            TumKiralamaGridView.Refresh();
            TumKiralamaGridView.ClearSelection();
        }

        void TumSikayetGridVerileri() // YÖNETİCİ İÇİN TÜM ŞİKAYETLER
        {
            TumSikayetlerGridView.DataSource = null;
            DataTable dt = BLL_Sikayet.TumSikayetler();
            TumSikayetlerGridView.DataSource = dt;
            TumSikayetlerGridView.Refresh();
            TumSikayetlerGridView.ClearSelection();
        }

        void BitenKiralamaGridVerileri() //BİTEN KİRALAMALAR
        {
            BitenKiralamaGridView.DataSource = null;
            DataTable dt = BLL_Kiralama.BitenKiralamalar();
            BitenKiralamaGridView.DataSource = dt;
            BitenKiralamaGridView.Refresh();
            BitenKiralamaGridView.ClearSelection();
        }

        void IptalEdilenKiralamaGridVerileri() // İPTAL EDİLEN KİRALAMALAR
        {
            IptalKiralamaGridView.DataSource = null;
            DataTable dt =BLL.BLL_IptalEdilenKiralama.IptalEdilenKiralamalar();
            IptalKiralamaGridView.DataSource = dt;
            IptalKiralamaGridView.Refresh();
            IptalKiralamaGridView.ClearSelection();
        }

        void KategoriMarkaComboDoldur() //ARAÇ EKLEME KATEGORİ COMBOBOX'I
        {
            KategoriComboBox2.DataSource = null;
            List<E_Arac> ktgr = new List<E_Arac>();
            ktgr = BLL_Arac.Kategoriler();

            KategoriComboBox2.DataSource = ktgr;

            KategoriComboBox2.DisplayMember = "Kategori";
            KategoriComboBox2.ValueMember = "KategoriID";
            KategoriComboBox2.SelectedIndex = -1;
        }

        void SubeComboDoldur() // ŞUBE COMBOBOX'I
        {
            // LİSTELERİ AYRI AYRI TANIMLAMAMIN SEBEBİ DATASOURCE AYNI OLAN COMBOBOXLARDAN 1.SİNDEN BİR DEĞER SEÇİLDİĞİNDE
            // 2.COMBOBOXINDA TEXTİ OTOMATİK İLK COMBOBOXIN TEXTİ OLUYOR.
            SubeComboBox1.DataSource = null;
            SubeComboBox2.DataSource = null;
            SubeComboBox3.DataSource = null;


            List<E_Sube> sube = new List<E_Sube>();
            sube = BLL_Sube.SubeListe();

            List<E_Sube> sube2 = new List<E_Sube>();
            sube2 = BLL_Sube.SubeListe();

            List<E_Sube> sube3 = new List<E_Sube>();
            sube3 = BLL_Sube.SubeListe();


            SubeComboBox1.DataSource = sube;
            SubeComboBox2.DataSource = sube2;
            SubeComboBox3.DataSource = sube3;

            SubeComboBox1.DisplayMember = "Ad";
            SubeComboBox1.ValueMember = "ID";
            SubeComboBox1.SelectedIndex = -1;

            SubeComboBox2.DisplayMember = "Ad";
            SubeComboBox2.ValueMember = "ID";
            SubeComboBox2.SelectedIndex = -1;

            SubeComboBox3.DisplayMember = "Ad";
            SubeComboBox3.ValueMember = "ID";
            SubeComboBox3.SelectedIndex = -1;

        }

        private void KategoriComboBox2_SelectedIndexChanged(object sender, EventArgs e) // Her kategori değişiminde marka listesi yenilenir.
        {
            if (KategoriComboBox2.SelectedValue is int) //KATEGORİCOMBOBOX'INA İLK VERİLER EKLENİRKEN İNT GELMEYEN DEĞER İÇİN KONTROL
            {
                MarkaComboBox.DataSource = null;

                List<E_Arac> marka = new List<E_Arac>();
                int id = Convert.ToInt32(KategoriComboBox2.SelectedValue);
                marka = BLL_Arac.Markalar(id);
                if (marka != null)
                {
                    MarkaComboBox.DataSource = marka;
                    MarkaComboBox.DisplayMember = "Marka";
                    MarkaComboBox.ValueMember = "MarkaID";
                }
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



        //-----------------------------------------------------------------------------------------

        // EKLEME/GÜNCELLEME SAYFASI İŞLEMLERİ

        private void SubeEkleButon_Click(object sender, EventArgs e) // ŞUBE EKLEME BUTONU
        {
            E_Sube sube = new E_Sube();
            sube.Eposta = EkleEpostaTxtBox.Text;
            sube.Sifre = EkleSifreTxtBox.Text;
            sube.Ad = EkleAdTxtBox.Text;
            sube.Sehir = SehirComboBox.Text.ToString();
            sube.Ilce = EkleilceTxtBox.Text;
            sube.Semt = EkleSemtTxtBox.Text;
            sube.Telefon = EkleTelTxtBox.Text;
            sube.Adres = EkleAdresTxtBox.Text;
            int durum = BLL_Sube.SubeEkle(sube);
            if (durum == -1)
                MessageBox.Show("Şube eklenirken bir sorun oluştu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if(durum == 0)
                MessageBox.Show("Eklemek istediğiniz e-posta zaten var.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if(durum == 1)
            {
                MessageBox.Show("Şube Başarıyla eklendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);               
                SubeComboDoldur();
                TumSubeGridVerileri();
                SehirComboBox.SelectedIndex = -1;
                TextBoxTemizle();
            }
                

        }

        private void AracResimButon_Click(object sender, EventArgs e) //OPENFİLEDİALOG CONTROLÜ AÇMA BUTONU
        {
            AracResimFileDg.Title = "Resim Seç";
            AracResimFileDg.InitialDirectory = "C:\\";
            AracResimFileDg.Filter = "Jpeg Dosyası (*.jpg)|*.jpg|Png Dosyası (*.png)|*.png";
            AracResimFileDg.FileName = "";

            if (AracResimFileDg.ShowDialog() == DialogResult.OK)
            {
                ResimAdLabel.Text = AracResimFileDg.SafeFileName;
            }
        }

        private void AracEkleButon_Click(object sender, EventArgs e) // ARAÇ EKLEME İŞLEMİ
        {
            E_Arac arac = new E_Arac();
            if(SubeComboBox1.SelectedIndex > -1 && KategoriComboBox2.SelectedIndex > -1 && MarkaComboBox.SelectedIndex > -1 && YakitComboBox.SelectedIndex > -1 && SanzimanComboBox.SelectedIndex > -1)
            { 
                 arac.SubeID = Convert.ToInt32(SubeComboBox1.SelectedValue);
                 arac.KategoriID = Convert.ToInt32(KategoriComboBox2.SelectedValue);
                 arac.MarkaID = Convert.ToInt32(MarkaComboBox.SelectedValue);
                 arac.Plaka = AracPlakaTxtBox.Text;
                 arac.Model = AracModelTxtBox.Text;
                 arac.ModelYili = Convert.ToInt32(AracYilTxtBox.Text);
                 arac.Hacim = AracHacimTxtBox.Text;
                 arac.Guc = AracGucTxtBox.Text;
                 arac.Yakit = YakitComboBox.SelectedItem.ToString();
                 arac.Sanziman = SanzimanComboBox.SelectedItem.ToString();
                 arac.Kilometre = Convert.ToDouble(AracKMTxtBox.Text);
                 arac.Renk = AracRenkTxtBox.Text;
                 arac.Donanim = AracDonanimTxtBox.Text;
                 arac.Ucret = Convert.ToDouble(AracUcretTxtBox.Text);
                if (ResimAdLabel.Text != "-")
                {
                    FileStream fs = new FileStream(AracResimFileDg.FileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    arac.Resim = br.ReadBytes((int)fs.Length);
                }
                else
                    arac.Resim = null;

                int durum = BLL_Arac.AracEkle(arac);

                if(durum == -1)
                    MessageBox.Show("Lütfen girdiğiniz verileri kontrol ediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if(durum == 0)
                    MessageBox.Show("Bu plaka zaten kayıtlı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if(durum == 1)
                {
                    MessageBox.Show(AracPlakaTxtBox.Text+" Plakalı araç başarıyla eklendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TextBoxTemizle();
                    ResimAdLabel.Text = "-";
                    SubeComboBox1.SelectedIndex = -1;
                    KategoriComboBox2.SelectedIndex = -1;
                    YakitComboBox.SelectedIndex = -1;
                    SanzimanComboBox.SelectedIndex = -1;
                    MarkaComboBox.DataSource = null;
                    TumAraclarGridVerileri();
                }
           }
            else
                MessageBox.Show("Lütfen girdiğiniz verileri kontrol ediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void YoneticiEkleButon_Click(object sender, EventArgs e) // YÖNETİCİ EKLEME İŞLEMİ
        {
            E_Uye uye = new E_Uye();
            uye.Eposta = YEPostaTxtBox.Text;
            uye.Sifre = YSifreTxtBox.Text;
            int durum = BLL_Uye.YoneticiEkle(uye);
            if(durum == 0)
                MessageBox.Show("Bu e-posta zaten kayıtlı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if(durum == -1)
                MessageBox.Show("Girilen verileri kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if(durum == 1)
            {
                MessageBox.Show("Kayıt başarılı.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                YEPostaTxtBox.Clear();
                YSifreTxtBox.Clear();
            }
        }

        private void SubeGuncelleButon_Click(object sender, EventArgs e) //ŞUBE GÜNCELLEME İŞLEMİ
        {
            E_Sube sube = new E_Sube();
            sube.ID = Convert.ToInt32(SubeComboBox2.SelectedValue);
            sube.Eposta = SG_EpostaTxtBox.Text;
            sube.Sifre = SG_SifreTxtBox.Text;
            sube.Ad = SG_AdTxtBox.Text;
            sube.Sehir = SG_SehirTxtBox.Text;
            sube.Ilce = SG_IlceTxtBox.Text;
            sube.Semt = SG_SemtTxtBox.Text;
            sube.Telefon = SG_TelTxtBox.Text;
            sube.Adres = SG_AdresTxtBox.Text;

            int durum = BLL_Sube.SubeGuncelle(sube);
            if (durum == -1)
                MessageBox.Show("Verileri kontrol ederek tekrar girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (durum == -0)
                MessageBox.Show("Şube Güncellenirken bir hata oluştu", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                MessageBox.Show(SG_AdTxtBox.Text+" Şubesi güncellendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SubeComboDoldur();
                TumSubeGridVerileri();
                TumSubeGridVerileri();
                TumSubeGridVerileri();
                TumSubeGridVerileri();
                TumSubeGridVerileri();
                TextBoxTemizle();
            }
        }

        private void SubeComboBox2_SelectedIndexChanged(object sender, EventArgs e) // ŞUBE GÜNCELLEME BÖLÜMÜ INDEXCHANGED İLE VERİ YENİLEME
        {
            if(SubeComboBox2.SelectedValue is int)
            {
                E_Sube sube = new E_Sube();
                sube.ID = Convert.ToInt32(SubeComboBox2.SelectedValue);
                sube = BLL_Sube.SubeBilgiGoster(sube.ID);
                if (sube != null)
                {
                    SubeGuncelleButon.Enabled = true;
                    SG_EpostaTxtBox.Text = sube.Eposta;
                    SG_SifreTxtBox.Text = sube.Sifre;
                    SG_AdTxtBox.Text = sube.Ad;
                    SG_SehirTxtBox.Text = sube.Sehir;
                    SG_IlceTxtBox.Text = sube.Ilce;
                    SG_SemtTxtBox.Text = sube.Semt;
                    SG_TelTxtBox.Text = sube.Telefon;
                    SG_AdresTxtBox.Text = sube.Adres;
                }
            }

        }
        //-----------------------------------------------------------------------------------------

        // ARAÇLARI İNCELE SAYFASI

        private void TumAraclarGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e) // ARAÇ DURUMU MÜSAİT OLAN SATIRLAR YEŞİL,OLMAYANLAR KIRMIZI
        {
            for (int i = 0; i < TumAraclarGridView.Rows.Count; i++)
            {


                if (TumAraclarGridView.Rows[i].Cells["Durum"].Value.ToString() == "Kirada")
                    TumAraclarGridView.Rows[i].DefaultCellStyle.BackColor = Color.OrangeRed;

                else if (TumAraclarGridView.Rows[i].Cells["Durum"].Value.ToString() == "Teslim Bekliyor")
                    TumAraclarGridView.Rows[i].DefaultCellStyle.BackColor = Color.Goldenrod;
                else
                    TumAraclarGridView.Rows[i].DefaultCellStyle.BackColor = Color.ForestGreen;


            }
        }

        private void TumAraclardaAraTxtBox_TextChanged(object sender, EventArgs e) // ARAÇ ARAMAK İÇİN TEXTCHANGED
        {
            TumAraclarGridView.DataSource = null;
            DataTable dt = BLL_Arac.TumAraclardaAra(TumAraclardaAraTxtBox.Text.ToString());
            TumAraclarGridView.DataSource = dt;

            TumAraclarGridView.Refresh();
        }


        private void TumAraclarGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                string aracplaka = TumAraclarGridView.Rows[e.RowIndex].Cells["Plaka"].Value.ToString();
                byte[] resim = BLL_Arac.AracResimGetir(aracplaka);
                if (resim == null)
                    MessageBox.Show("Araç resmi bulunmamaktadır.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    MemoryStream mem = new MemoryStream(resim);
                    AracResimForm resimformu = new AracResimForm();
                    resimformu.AracResimPictureBox.Image = Image.FromStream(mem);
                    resimformu.Text = aracplaka + " - Resim";
                    resimformu.ShowDialog();
                }
            }

        }

        //-----------------------------------------------------------------------------------------

        // ÜYELERİ İNCELE SAYFASI

        E_Uye UyeMail = new E_Uye(); // İKİ AYRI FONKS. KULLANMAK İÇİN

        private void TumUyelerGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) //DATAGRİDVİEW SEÇ BUTONU İŞLEMLERİ
        {
            if(e.ColumnIndex == 0)
            {
                UyeMailGroupBox.Enabled = true;
                UyeMailLabel.Text = TumUyelerGridView.CurrentRow.Cells[1].Value.ToString();
                
                UyeMail.Eposta= TumUyelerGridView.CurrentRow.Cells[1].Value.ToString();
                UyeMail.Ad= TumUyelerGridView.CurrentRow.Cells[3].Value.ToString();
                UyeMail.Soyad = TumUyelerGridView.CurrentRow.Cells[4].Value.ToString();
                
            }
        }

        private void UyeMailGonderButon_Click(object sender, EventArgs e) //ÜYE MAİL GÖNDER BUTONU
        {
            if(!String.IsNullOrEmpty(UyeMailKonuTxtBox.Text) && !String.IsNullOrEmpty(UyeMailMesajTxtBox.Text))
            {
                Mail gonder = new Mail();
                bool durum = gonder.MailGonder(UyeMail.Eposta, UyeMail.Ad, UyeMail.Soyad, UyeMailKonuTxtBox.Text, UyeMailMesajTxtBox.Text);
                if(durum)
                {
                    MessageBox.Show("Mail Gönderildi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UyeMailLabel.Text = "-";
                    UyeMailKonuTxtBox.Clear();
                    UyeMailMesajTxtBox.Clear();
                    UyeMailGroupBox.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UyeAraTxtBox_TextChanged(object sender, EventArgs e) // ÜYE ARAMAK İÇİN TEXTCHANGED
        {
            TumUyelerGridView.DataSource = null;
            DataTable dt = BLL_Uye.UyeAra(UyeAraTxtBox.Text);
            TumUyelerGridView.DataSource = dt;
        }

        void UyeIstatistik() // UYE ISTATISTIK FONKS.
        {
            E_UyeIstatistik uye = BLL_Istatistik.UyeIstatistik();

                USayiLabel.Text = uye.UyeSayi.ToString();
                EUSayiLabel.Text = uye.EUyeSayi.ToString();
                KUSayiLabel.Text = uye.KUyeSayi.ToString();
                KYSayiLabel.Text = uye.KiralamaSayi.ToString();
                SonUyeLabel.Text = uye.SonUye.ToString();

        }


        //-----------------------------------------------------------------------------------------

        //  ŞUBE İNCELE SAYFASI
        E_Sube SubeMsj = new E_Sube();
        private void TumSubelerGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                SubeMesajGroupBox.Enabled = true;
                SubeMsjLabel.Text = TumSubelerGridView.CurrentRow.Cells[1].Value.ToString();

                SubeMsj.Eposta = TumSubelerGridView.CurrentRow.Cells[1].Value.ToString();
                SubeMsj.Ad = TumSubelerGridView.CurrentRow.Cells[2].Value.ToString();
                SubeMsj.Sehir = TumSubelerGridView.CurrentRow.Cells[3].Value.ToString();
                SubeMsj.Ilce = TumSubelerGridView.CurrentRow.Cells[4].Value.ToString();

            }
        }

        private void SubeMsjGndrButon_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(SubeMailKonuTxtBox.Text) && !String.IsNullOrEmpty(SubeMailMsjTxtBox.Text))
            {
                Mail gonder = new Mail();
                bool durum = gonder.MailGonder(SubeMsj.Eposta,SubeMsj.Sehir,SubeMsj.Ilce,SubeMsj.Ad,SubeMailKonuTxtBox.Text, SubeMailMsjTxtBox.Text);
                if (durum)
                {
                    MessageBox.Show("Mail Gönderildi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SubeMsjLabel.Text = "-";
                    SubeMailMsjTxtBox.Clear();
                    SubeMailKonuTxtBox.Clear();
                    SubeMesajGroupBox.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SubeAraTxtBox_TextChanged(object sender, EventArgs e)
        {
            TumSubelerGridView.DataSource = null;
            DataTable dt = BLL_Sube.SubeAra(SubeAraTxtBox.Text);
            TumSubelerGridView.DataSource = dt;
        }

        void SubeIstatıstık(int SubeID)
        {
            E_SubeIstatistik sube = BLL_SubeIstatistik.TekSubeninIstatistik(SubeID);
            SAracSayiLabel.Text = sube.AracSayi.ToString();
            SKiraSayiLabel.Text = sube.KiralamaSayi.ToString();
            SKiraAracLabel.Text = sube.KiradakiAracSayi.ToString();
            SMusaitAracLabel.Text = sube.MusaitAracSayi.ToString();
            SSikayetLabel.Text = sube.SikayetSayi.ToString();
            SUcretLabel.Text = sube.ToplamUcret.ToString() + " TL";
        }

        private void SubeComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SubeComboBox3.SelectedValue is int)
                SubeIstatıstık(Convert.ToInt32(SubeComboBox3.SelectedValue));
        }



        //-----------------------------------------------------------------------------------------
        //  ŞİKAYETLER SAYFASI

        E_Sikayet sikayet = new E_Sikayet(); //KULLANICI BİR SATIRI SEÇİP SATIRI DEĞİŞTİRDİĞİNDE YANLIŞ VERİLERİ ALMAMAK İÇİN GLOBALDE TANIMLAYARAK 2 METODDA KULLANDIM
        private void TumSikayetlerGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                SikayetGroupBox.Enabled = true;
                SikayetMailLabel.Text = TumSikayetlerGridView.CurrentRow.Cells[1].Value.ToString();

                sikayet.UyePosta = TumSikayetlerGridView.CurrentRow.Cells["Üye E-Postası"].Value.ToString();
                sikayet.UyeAd = TumSikayetlerGridView.CurrentRow.Cells["Üye Adı"].Value.ToString();
                sikayet.UyeSoyad = TumSikayetlerGridView.CurrentRow.Cells["Üye Soyadı"].Value.ToString();
                sikayet.SubeAd= TumSikayetlerGridView.CurrentRow.Cells["Şube Adı"].Value.ToString();
                sikayet.Konu = TumSikayetlerGridView.CurrentRow.Cells["Konu"].Value.ToString();
                sikayet.SikayetMesaj= TumSikayetlerGridView.CurrentRow.Cells["Sikayet"].Value.ToString();
                sikayet.Tarih = Convert.ToDateTime(TumSikayetlerGridView.CurrentRow.Cells["Tarih"].Value);

            }
        }

        private void SikayetCevaplaButon_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(SikayetIcerikTxtBox.Text))
            {
                Mail gonder = new Mail();
                bool durum = gonder.MailGonder(sikayet.UyePosta,sikayet.UyeAd,sikayet.UyeSoyad,sikayet.SubeAd,sikayet.SikayetMesaj,sikayet.Tarih,SikayetIcerikTxtBox.Text);
                if (durum)
                {
                    MessageBox.Show("Mail Gönderildi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SikayetMailLabel.Text = "-";
                    SikayetIcerikTxtBox.Clear();
                    SikayetGroupBox.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }




        //-----------------------------------------------------------------------------------------

        //  KİRALAMALAR SAYFASI

        private void TumKiralamaGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e) // ÜYELİK DURUMU ÜYE OLANLAR SARI,OLMAYANLAR KIRMIZI
        {
            for (int i = 0; i < TumKiralamaGridView.Rows.Count; i++)
            {


                if (TumKiralamaGridView.Rows[i].Cells["KiraDurumu"].Value.ToString() == "Kirada")
                {

                   TumKiralamaGridView.Rows[i].DefaultCellStyle.BackColor = Color.ForestGreen;
                }
                else
                {
                    TumKiralamaGridView.Rows[i].DefaultCellStyle.BackColor = Color.Goldenrod;
                }

            }
        }

        //-----------------------------------------------------------------------------------------

        //  GRAFİK İŞLEMLERİ

        void KiralamaChartVerileri()
        {
            int[] veriler = BLL_Grafik.KiralamaVerileri();
            for(int i = 0; i < 12; i++)
            {
                KiralamaChart.Series["Kiralama Sayısı"].Points[i].SetValueY(veriler[i]);
            }
           
            KiralamaChart.DataBind();
        }

        void KategoridChartVerileri()
        {
            DataTable dt = BLL_Grafik.KategoriVerileri();
            KategoriChart.Series["Seri"].XValueMember = "Kategori";
            KategoriChart.Series["Seri"].YValueMembers = "Sayi";
            KategoriChart.DataSource = dt;
            KategoriChart.DataBind();
        }

        void AracChartVerileri()
        {
            DataTable dt = BLL_Grafik.AracVerileri();
            MarkaChart.Series["Seri"].XValueMember = "Marka";
            MarkaChart.Series["Seri"].YValueMembers = "Sayi";
            MarkaChart.DataSource = dt;
            MarkaChart.DataBind();
        }
        void UyeChartVerileri()
        {
            DataTable dt = BLL_Grafik.UyeVerileri();
            UyeChart.Series["Uye"].XValueMember = "Cinsiyet";
            UyeChart.Series["Uye"].YValueMembers = "Sayi";
            UyeChart.DataSource = dt;
            UyeChart.DataBind();

        }


    }
}
