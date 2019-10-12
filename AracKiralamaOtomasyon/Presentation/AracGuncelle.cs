using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AracKiralamaOtomasyon.BLL;
using AracKiralamaOtomasyon.Entity;

namespace AracKiralamaOtomasyon.Presentation
{
    public partial class AracGuncelle : Form
    {
        public AracGuncelle()
        {
            InitializeComponent();
        }

        private void GuncelleButon_Click(object sender, EventArgs e)
        {
            E_Arac arac = new E_Arac();
            arac.Plaka = PlakaLabel.Text;
            arac.Kilometre =Convert.ToDouble( KMTxtBox.Text);
            arac.Ucret = Convert.ToDouble(UcretTxtBox.Text);
            int durum = BLL_Arac.AracGuncelle(arac);
            if (durum == 0 || durum ==-1)
                MessageBox.Show("Güncellerken bir hata oluştu.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if(durum > 0)
            {
                MessageBox.Show(PlakaLabel.Text+" Plakalı araç başarıyla güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }


        }
    }
}
