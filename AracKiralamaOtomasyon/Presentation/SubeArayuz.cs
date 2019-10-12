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
    public partial class SubeArayuz : Form
    {
        public SubeArayuz()
        {
            InitializeComponent();

            tabControl1.Appearance = TabAppearance.FlatButtons; tabControl1.ItemSize = new Size(0, 1); tabControl1.SizeMode = TabSizeMode.Fixed; //TABCONTROL MENÜSÜNÜ GÖSTERMEMEK İÇİN
        }

        private void Textbox_HarfEngelle(object sender, KeyPressEventArgs e) //BELİRLENEN TEXTBOXLAR İÇİN KARAKTER GİRİŞİ ENGELLEME ORTAK EVENT'I
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void AracKMTxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',';
        }

        // MENÜ GEÇİŞ KODLARI

        private void AracIslemMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(AracIslemPage);
            AracGridView.ClearSelection();
        }

        private void SubeIslemMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(SubeIslemPage);
            BLL_SubeIstatistik.SubeIstatistikGuncelle(Session.SubeID); //HER GEÇİŞTE İSTATİSTİKLERİ YENİLER
            TekSubeninIstatistikleri();//HER GEÇİŞTE ŞUBE İSTATİSTİKLERİNİ YENİLER
        }

        private void KiraslemMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(KiraIslemPage);
            KiradakilerGridView.ClearSelection();
        }

        private void TeslimIslemMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(AracTeslimPage);
            AracTeslimGridView.ClearSelection();
            TeslimGecmisGridView.ClearSelection();
        }

        private void GecmisMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(GecmisPage);
            TumKiralamaGridView.ClearSelection();
        }

        private void CikisMenuItem_Click(object sender, EventArgs e)
        {
            GirisForm giris = new GirisForm();
            giris.Show();
            this.Close();
        }

        //-----------------------------------------------------------------------------------------
        // GİRİŞTE YAPILACAK İŞLEMLER

        private void SubeArayuz_Load(object sender, EventArgs e) //SUBE ARAYUZ AÇILDIĞINDa
        {
            BLL_SubeIstatistik.SubeIstatistikGuncelle(Session.SubeID);
            SessionIDLabel.Text = Session.UyeAd.ToString();
            SubemdekiAraclar(); //Araç listeleme
            KategoriMarkaComboDoldur(); //Araç eklemek için combobox verileri
            BitenleriGoster();// Biten kiralama listeleme
            TeslimAlinanAraclar();//Teslim alınan araçları listeleme
            SubeKiralamaGoster();//Şubeden yapılan kiralamaları listeleme
            SubeMesajGoster();//Şubeye gelen mesajları listeleme
            SubeBilgisi();//Şubenin bilgilerini listeleme
            TumKiralamalar();

        }

        void SubemdekiAraclar() // ŞUBEDEKİ TÜM ARAÇLAR
        {
            AracGridView.DataSource = null;
            DataTable dt = BLL_Arac.TekSubeninAraclari(Session.SubeID);
            AracGridView.DataSource = dt;
            AracGridView.Refresh();
            AracGridView.ClearSelection();
        }

        void KategoriMarkaComboDoldur() //KATEGORİLER
        {
            KategoriComboBox1.DataSource = null;
            List<E_Arac> ktgr = new List<E_Arac>();
            ktgr = BLL_Arac.Kategoriler();

            KategoriComboBox1.DataSource = ktgr;

            KategoriComboBox1.DisplayMember = "Kategori";
            KategoriComboBox1.ValueMember = "KategoriID";
            KategoriComboBox1.SelectedIndex = -1;
        }

        private void KategoriComboBox1_SelectedIndexChanged(object sender, EventArgs e) // KATEGORİ SEÇİMİNE GÖRE ARAÇLAR
        {
            if (KategoriComboBox1.SelectedValue is int) //KATEGORİCOMBOBOX'INA İLK VERİLER EKLENİRKEN İNT GELMEYEN DEĞER İÇİN KONTROL
            {
                AracMarkaComboBox.DataSource = null;

                List<E_Arac> marka = new List<E_Arac>();
                int id = Convert.ToInt32(KategoriComboBox1.SelectedValue);
                marka = BLL_Arac.Markalar(id);
                if (marka != null)
                {
                    AracMarkaComboBox.DataSource = marka;
                    AracMarkaComboBox.DisplayMember = "Marka";
                    AracMarkaComboBox.ValueMember = "MarkaID";
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
        // ARAÇ İŞLEMLERİ

        private void AracGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e) // DURUMA GÖRE SATIR RENKLENDİRME
        {
            for (int i = 0; i < AracGridView.Rows.Count; i++)
            {
                if (AracGridView.Rows[i].Cells["Durum"].Value.ToString() == "Kirada")
                {
                    AracGridView.Rows[i].DefaultCellStyle.BackColor = Color.OrangeRed;
                }
                else if (AracGridView.Rows[i].Cells["Durum"].Value.ToString() == "Teslim Bekliyor")
                {
                    AracGridView.Rows[i].DefaultCellStyle.BackColor = Color.Goldenrod;
                }
                else
                {
                    AracGridView.Rows[i].DefaultCellStyle.BackColor = Color.ForestGreen;
                }

            }
        }

        private void AracEkleButon_Click(object sender, EventArgs e) // ARAÇ EKLEME BUTONU
        {
            E_Arac arac = new E_Arac();
            if (KategoriComboBox1.SelectedIndex > -1 && AracMarkaComboBox.SelectedIndex > -1 && YakitComboBox.SelectedIndex > -1 && SanzimanComboBox.SelectedIndex > -1)
            {
                arac.SubeID = Session.SubeID;
                arac.KategoriID = Convert.ToInt32(KategoriComboBox1.SelectedValue);
                arac.MarkaID = Convert.ToInt32(AracMarkaComboBox.SelectedValue);
                arac.Plaka = PlakaTxtBox.Text;
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
                    FileStream fs = new FileStream(AracResimDialog.FileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    arac.Resim = br.ReadBytes((int)fs.Length);
                }
                else
                    arac.Resim = null;

                int durum = BLL_Arac.AracEkle(arac);

                if (durum == -1)
                    MessageBox.Show("Lütfen girdiğiniz verileri kontrol ediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (durum == 0)
                    MessageBox.Show("Bu plaka zaten kayıtlı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (durum == 1)
                {
                    MessageBox.Show(PlakaTxtBox.Text + " Plakalı araç başarıyla eklendi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TextBoxTemizle();
                    ResimAdLabel.Text = "-";
                    KategoriComboBox1.SelectedIndex = -1;
                    YakitComboBox.SelectedIndex = -1;
                    SanzimanComboBox.SelectedIndex = -1;
                    AracMarkaComboBox.DataSource = null;
                    SubemdekiAraclar();
                }
            }
            else
                MessageBox.Show("Lütfen girdiğiniz verileri kontrol ediniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AracResimButon_Click(object sender, EventArgs e) // OPENFİLEDİALOG AÇMA BUTONU
        {
            AracResimDialog.Title = "Resim Seç";
            AracResimDialog.InitialDirectory = "C:\\";
            AracResimDialog.Filter = "Jpeg Dosyası (*.jpg)|*.jpg|Png Dosyası (*.png)|*.png";
            AracResimDialog.FileName = "";

            if (AracResimDialog.ShowDialog() == DialogResult.OK)
            {
                ResimAdLabel.Text = AracResimDialog.SafeFileName;
            }
        }

        private void AracGridView_CellContentClick(object sender, DataGridViewCellEventArgs e) // SEÇİLEN ARACI SİLME - ARAÇ RESMİ GÖRÜNTÜLEME
        {
           
            if (e.ColumnIndex == 0)
            {
                string plaka = AracGridView.Rows[e.RowIndex].Cells["Plaka"].Value.ToString();
                byte[] resim = BLL_Arac.AracResimGetir(plaka);
                if (resim == null)
                    MessageBox.Show("Araç resmi bulunmamaktadır.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    MemoryStream mem = new MemoryStream(resim);
                    AracResimForm resimformu = new AracResimForm();
                    resimformu.AracResimPictureBox.Image = Image.FromStream(mem);
                    resimformu.Text = plaka + " - Resim";
                    resimformu.ShowDialog();
                

                }
            }
            else if (e.ColumnIndex == 1)
            {
                string plaka = AracGridView.Rows[e.RowIndex].Cells["Plaka"].Value.ToString();
                if (AracGridView.Rows[e.RowIndex].Cells["Durum"].Value.ToString() == "Kirada")
                {
                    MessageBox.Show("Araç kirada olduğundan işleminiz gerçekleştirilmedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult result = MessageBox.Show(plaka + " Plakalı aracı silmek üzeresiniz!\nAracı sildiğiniz takdirde bu plakaya ait tüm bilgiler(Kiralama,İptal Edilenler,Bitenler,Teslim Alınanlar) silinecek ve geri dönüşü olmayacaktır.\nDevam etmek istiyormusunuz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        int durum = BLL_Arac.SubedenAracSil(plaka);
                        if (durum == 0)
                            MessageBox.Show("İşlem gerçekleştirilirken bir hata oluştu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if (durum > 0)
                        {
                            MessageBox.Show(plaka + " Plakalı araç başarıyla silindi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            SubemdekiAraclar();
}
                    }
                }

            }
            else if (e.ColumnIndex == 2)
            {
                Presentation.AracGuncelle yeni = new Presentation.AracGuncelle();
                yeni.Text = AracGridView.Rows[e.RowIndex].Cells["Plaka"].Value.ToString() + "-Güncelle";
                yeni.PlakaLabel.Text = AracGridView.Rows[e.RowIndex].Cells["Plaka"].Value.ToString();
                yeni.EskiKMLabel.Text = AracGridView.Rows[e.RowIndex].Cells["Kilometre"].Value.ToString() + " KM";
                yeni.EskiUcretLabel.Text = AracGridView.Rows[e.RowIndex].Cells["Ucret"].Value.ToString() + " TL";
                yeni.ShowDialog();
                SubemdekiAraclar();
            }



        }

        //-----------------------------------------------------------------------------------------
        // TESLİM ALMA İŞLEMLERİ

        void BitenleriGoster()
        {
            AracTeslimGridView.DataSource = null;
            AracTeslimGridView.DataSource = BLL_Sube.SubeBitenKiralamalar(Session.SubeID);
            AracTeslimGridView.Refresh();
            AracTeslimGridView.ClearSelection();
        }

        private void AracTeslimGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                string plaka = AracTeslimGridView.Rows[e.RowIndex].Cells["Plaka"].Value.ToString();
                TeslimTCLabel.Text = AracTeslimGridView.Rows[e.RowIndex].Cells["TC"].Value.ToString();
                TeslimTakipLabel.Text = AracTeslimGridView.Rows[e.RowIndex].Cells["TakipKod"].Value.ToString();
                TeslimPlakaLabel.Text = plaka;
                TeslimOncekiKMLabel.Text = BLL_Arac.AracKMGetir(plaka).ToString();
                AracTeslimGroupBox.Enabled = true;
            }
        }

        private void TeslimAlButon_Click(object sender, EventArgs e)
        {
            E_Teslim teslim = new E_Teslim();
            teslim.Plaka = TeslimPlakaLabel.Text;
            teslim.TakipKod = TeslimTakipLabel.Text;
            teslim.Hasar = TeslimHasarTxtBox.Text;
            teslim.YeniKM = Convert.ToDouble(TeslimKMTxtBox.Text);
            int durum = BLL_Teslim.AracTeslimAl(teslim);
            if (durum == -1)
                MessageBox.Show("Araç kilometresi önceki kilometreden küçük olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (durum == 0)
                MessageBox.Show("Teslim alınırken bir sorun oluştu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                MessageBox.Show(TeslimPlakaLabel.Text + " Plakalı araç başarıyla teslim alındı.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TextBoxTemizle();
                TeslimTCLabel.Text = "-";
                TeslimTakipLabel.Text = "-";
                TeslimPlakaLabel.Text = "-";
                TeslimOncekiKMLabel.Text = "-";
                AracTeslimGroupBox.Enabled = false;
                TeslimAlinanAraclar();
                BitenleriGoster();
                SubemdekiAraclar();
            }
        }

        void TeslimAlinanAraclar()
        {
            TeslimGecmisGridView.DataSource = null;
            TeslimGecmisGridView.DataSource = BLL_Teslim.SubeTeslimAlinanAraclar(Session.SubeID);
            TeslimGecmisGridView.Refresh();
            TeslimGecmisGridView.ClearSelection();
        }

        //-----------------------------------------------------------------------------------------
        // KİRALAMA İŞLEMLERİ

        void SubeKiralamaGoster()
        {
            KiradakilerGridView.DataSource = null;
            KiradakilerGridView.DataSource = BLL_Sube.SubeKiralamaGoster(Session.SubeID);
            KiradakilerGridView.Refresh();
            KiradakilerGridView.ClearSelection();
        }

        private void KiradakilerGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            for (int i = 0; i < KiradakilerGridView.Rows.Count; i++)
            {
                if (KiradakilerGridView.Rows[i].Cells["KiraDurumu"].Value.ToString() == "Kirada")
                {
                    KiradakilerGridView.Rows[i].DefaultCellStyle.BackColor = Color.OrangeRed;
                }
                else if (KiradakilerGridView.Rows[i].Cells["KiraDurumu"].Value.ToString() == "Başlamadı")
                {
                    KiradakilerGridView.Rows[i].DefaultCellStyle.BackColor = Color.Goldenrod;
                }
            }
        }

        private void KiradakilerGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0) //İPTAL ETME BUTONU
            {
                iptalGroupBox.Enabled = true;
                iptalTakipkodLabel.Text = KiradakilerGridView.Rows[e.RowIndex].Cells["TakipKod"].Value.ToString();
                iptalPlakaLabel.Text = KiradakilerGridView.Rows[e.RowIndex].Cells["Plaka"].Value.ToString();
                iptalBaslangicLabel.Text = KiradakilerGridView.Rows[e.RowIndex].Cells["BaslangicTarihi"].Value.ToString();
            }
            if (e.ColumnIndex == 1)
            {
                SureUzatGroupBox.Enabled = true;
                UzatTakipKodLabel.Text = KiradakilerGridView.Rows[e.RowIndex].Cells["TakipKod"].Value.ToString();
                uzatPlakaLabel.Text = KiradakilerGridView.Rows[e.RowIndex].Cells["Plaka"].Value.ToString();
                uzatEskiBitisLabel.Text = KiradakilerGridView.Rows[e.RowIndex].Cells["BitisTarihi"].Value.ToString();
                UzatBaslangicLabel.Text = KiradakilerGridView.Rows[e.RowIndex].Cells["BaslangicTarihi"].Value.ToString();
                uzatOdenenLabel.Text = KiradakilerGridView.Rows[e.RowIndex].Cells["OdenenUcret"].Value.ToString();
                uzatTimePicker.MinDate = Convert.ToDateTime(KiradakilerGridView.Rows[e.RowIndex].Cells["BitisTarihi"].Value);
                uzatTimePicker.MaxDate = uzatTimePicker.MinDate.AddDays(30);
            }
        }

        private void uzatTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (KiradakilerGridView.CurrentRow.Cells["Uyelik"].Value.ToString() == "Var")
            {
                double uyeucreti = BLL_Arac.UyeUcretiGetir(uzatPlakaLabel.Text);
                TimeSpan GunHesapla = uzatTimePicker.Value - Convert.ToDateTime(UzatBaslangicLabel.Text);
                int Gun = Convert.ToInt32(GunHesapla.TotalDays);
                double Toplamucret = uyeucreti * Gun;
                double ekucret = Toplamucret - Convert.ToDouble(uzatOdenenLabel.Text);
                uzatEkUcretLabel.Text = ekucret.ToString();
            }
            else
            {
                double uyeolmayanucreti = Convert.ToDouble(KiradakilerGridView.CurrentRow.Cells["GunlukUcret"].Value);
                TimeSpan GunHesapla = uzatTimePicker.Value - Convert.ToDateTime(UzatBaslangicLabel.Text);
                int Gun = Convert.ToInt32(GunHesapla.TotalDays);
                double Toplamucret = uyeolmayanucreti * Gun;
                double ekucret = Toplamucret - Convert.ToDouble(uzatOdenenLabel.Text);
                uzatEkUcretLabel.Text = ekucret.ToString();
            }

        }

        private void iptalButon_Click(object sender, EventArgs e)
        {
            E_IptalEdilenKiralama iptal = new E_IptalEdilenKiralama();
            iptal.TakipKod = iptalTakipkodLabel.Text.ToString();
            iptal.Sebep = iptalSebepTxtBox.Text.ToString();
            iptal.Aciklama = iptalAciklamaTxtBox.Text.ToString();
            int durum = BLL_IptalEdilenKiralama.KiraIptal(iptal);
            if (durum > 0)
            {
                MessageBox.Show(iptal.TakipKod + " Takip kodlu kiralamanız iptal edildi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                KiradakilerGridView.DataSource = null;
                iptalTakipkodLabel.Text = "-";
                iptalBaslangicLabel.Text = "-";
                iptalPlakaLabel.Text = "-";
                iptalGroupBox.Enabled = false;
                iptalSebepTxtBox.Clear();
                iptalAciklamaTxtBox.Clear();
                SubeKiralamaGoster();
                SubemdekiAraclar();
                TumKiralamalar();
            }
            else if (durum == -1)
            {
                MessageBox.Show("Girdiğiniz verileri kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kiralama iptal edilemedi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void uzatButon_Click(object sender, EventArgs e)
        {
            if (uzatEkUcretLabel.Text.ToString() != "0")
            {
                E_Kiralama uzat = new E_Kiralama();
                uzat.TakipKod = UzatTakipKodLabel.Text;
                uzat.BitisTarihi = uzatTimePicker.Value;
                uzat.OdenenUcret = Convert.ToDouble(uzatEkUcretLabel.Text) + Convert.ToDouble(uzatOdenenLabel.Text);
                int durum = BLL_Kiralama.KiraSureUzat(uzat);
                if (durum == 0)
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
                    SubeKiralamaGoster();
                    TumKiralamalar();
                    SureUzatGroupBox.Enabled = false;
                }
            }
        }

        //-----------------------------------------------------------------------------------------
        // ŞUBE İŞLEMLERİ

        void SubeMesajGoster()
        {
            MesajlarGridView.DataSource = null;
            MesajlarGridView.DataSource = BLL_Sube.SubeMesajGoster(Session.SubeID);
            MesajlarGridView.Refresh();
            MesajlarGridView.ClearSelection();
        }

        private void MesajlarGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 0)
            {
                UyeMailLabel.Text = MesajlarGridView.Rows[e.RowIndex].Cells["Eposta"].Value.ToString();
                UyeMailGroupBox.Enabled = true;
            }
        }

        private void UyeMailGonderButon_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(SubeMailKonuTxtBox.Text) && !String.IsNullOrEmpty(SubeMailMesajTxtBox.Text))
            {
                E_Uye uye = new E_Uye();
                uye.Eposta = UyeMailLabel.Text.ToString();
                uye.Ad = MesajlarGridView.CurrentRow.Cells["Ad"].Value.ToString();
                uye.Soyad = MesajlarGridView.CurrentRow.Cells["Soyad"].Value.ToString();
                string konu = SubeMailKonuTxtBox.Text.ToString();
                string mesaj = SubeMailMesajTxtBox.Text.ToString();
                Mail gonder = new Mail();
                bool durum = gonder.SubedenUyeye(uye, konu, mesaj);
                if (durum)
                {
                    MessageBox.Show("Mail Gönderildi.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UyeMailLabel.Text = "-";
                    SubeMailMesajTxtBox.Clear();
                    SubeMailKonuTxtBox.Clear();
                    UyeMailGroupBox.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Lütfen boş alanları doldurunuz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void SubeBilgisi()
        {
            E_Sube sube = BLL_Sube.SubeBilgiGoster(Session.SubeID);
            SubeMailLabel.Text = sube.Eposta;
            SubeAdLabel.Text = sube.Ad;
            SubeSehirLabel.Text = sube.Sehir;
            SubeSemtLabel.Text = sube.Semt;
            SubeTelLabel.Text = sube.Telefon;
            SubeAdresLabel.Text = sube.Adres;

        }

        void TekSubeninIstatistikleri()
        {
            E_SubeIstatistik sube = BLL_SubeIstatistik.TekSubeninIstatistik(Session.SubeID);
            SAracSayiLabel.Text = sube.AracSayi.ToString();
            SKiraSayiLabel.Text = sube.KiralamaSayi.ToString();
            SKiraAracLabel.Text = sube.KiradakiAracSayi.ToString();
            SMusaitAracLabel.Text = sube.MusaitAracSayi.ToString();
            SUcretLabel.Text = sube.ToplamUcret.ToString() + " TL";
        }


        //-----------------------------------------------------------------------------------------
        // TÜM KİRALAMA BİLGİLERİ İŞLEMLERİ

        void TumKiralamalar()
        {
            TumKiralamaGridView.DataSource = null;
            TumKiralamaGridView.DataSource = BLL_Sube.SubeTumKiralamalar(Session.SubeID);
            TumKiralamaGridView.Refresh();
            TumKiralamaGridView.ClearSelection();

        }

        private void TumKiralamaGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            for (int i = 0; i < TumKiralamaGridView.Rows.Count; i++)
            {
                if (TumKiralamaGridView.Rows[i].Cells["KiraDurumu"].Value.ToString() == "Kirada")
                    TumKiralamaGridView.Rows[i].DefaultCellStyle.BackColor = Color.OrangeRed;
                else if (TumKiralamaGridView.Rows[i].Cells["KiraDurumu"].Value.ToString() == "Başlamadı")
                    TumKiralamaGridView.Rows[i].DefaultCellStyle.BackColor = Color.Goldenrod;
                else if (TumKiralamaGridView.Rows[i].Cells["KiraDurumu"].Value.ToString() == "İptal Edildi")
                    TumKiralamaGridView.Rows[i].DefaultCellStyle.BackColor = Color.Crimson;
                else if (TumKiralamaGridView.Rows[i].Cells["KiraDurumu"].Value.ToString() == "Tamamlandı")
                    TumKiralamaGridView.Rows[i].DefaultCellStyle.BackColor = Color.LimeGreen;
                else if (TumKiralamaGridView.Rows[i].Cells["KiraDurumu"].Value.ToString() == "Bitti")
                    TumKiralamaGridView.Rows[i].DefaultCellStyle.BackColor = Color.ForestGreen;
            }
        }
    }
}
