namespace AracKiralamaOtomasyon
{
    partial class GirisForm
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.EpostaTxtBox = new System.Windows.Forms.TextBox();
            this.SifreTxtBox = new System.Windows.Forms.TextBox();
            this.GirisYapButon = new System.Windows.Forms.Button();
            this.UyeOlButon = new System.Windows.Forms.Button();
            this.UyeliksizButon = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CikisButon = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(94, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "E-Posta :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.Gainsboro;
            this.label2.Location = new System.Drawing.Point(121, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Şifre :";
            // 
            // EpostaTxtBox
            // 
            this.EpostaTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.EpostaTxtBox.Location = new System.Drawing.Point(173, 122);
            this.EpostaTxtBox.MaxLength = 30;
            this.EpostaTxtBox.Multiline = true;
            this.EpostaTxtBox.Name = "EpostaTxtBox";
            this.EpostaTxtBox.Size = new System.Drawing.Size(160, 23);
            this.EpostaTxtBox.TabIndex = 2;
            // 
            // SifreTxtBox
            // 
            this.SifreTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.SifreTxtBox.Location = new System.Drawing.Point(173, 162);
            this.SifreTxtBox.MaxLength = 20;
            this.SifreTxtBox.Multiline = true;
            this.SifreTxtBox.Name = "SifreTxtBox";
            this.SifreTxtBox.PasswordChar = '*';
            this.SifreTxtBox.Size = new System.Drawing.Size(160, 23);
            this.SifreTxtBox.TabIndex = 3;
            // 
            // GirisYapButon
            // 
            this.GirisYapButon.BackColor = System.Drawing.Color.ForestGreen;
            this.GirisYapButon.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.GirisYapButon.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.GirisYapButon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.GirisYapButon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GirisYapButon.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.GirisYapButon.ForeColor = System.Drawing.Color.White;
            this.GirisYapButon.Location = new System.Drawing.Point(264, 206);
            this.GirisYapButon.Name = "GirisYapButon";
            this.GirisYapButon.Size = new System.Drawing.Size(69, 33);
            this.GirisYapButon.TabIndex = 4;
            this.GirisYapButon.Text = "Giriş Yap";
            this.GirisYapButon.UseVisualStyleBackColor = false;
            this.GirisYapButon.Click += new System.EventHandler(this.GirisYapButon_Click);
            // 
            // UyeOlButon
            // 
            this.UyeOlButon.BackColor = System.Drawing.Color.DarkRed;
            this.UyeOlButon.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.UyeOlButon.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Brown;
            this.UyeOlButon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCoral;
            this.UyeOlButon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UyeOlButon.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.UyeOlButon.ForeColor = System.Drawing.Color.White;
            this.UyeOlButon.Location = new System.Drawing.Point(173, 206);
            this.UyeOlButon.Name = "UyeOlButon";
            this.UyeOlButon.Size = new System.Drawing.Size(69, 33);
            this.UyeOlButon.TabIndex = 5;
            this.UyeOlButon.Text = "Üye Ol";
            this.UyeOlButon.UseVisualStyleBackColor = false;
            this.UyeOlButon.Click += new System.EventHandler(this.UyeOlButon_Click);
            // 
            // UyeliksizButon
            // 
            this.UyeliksizButon.BackColor = System.Drawing.Color.Green;
            this.UyeliksizButon.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.UyeliksizButon.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Green;
            this.UyeliksizButon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.UyeliksizButon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UyeliksizButon.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UyeliksizButon.ForeColor = System.Drawing.Color.White;
            this.UyeliksizButon.Location = new System.Drawing.Point(173, 245);
            this.UyeliksizButon.Name = "UyeliksizButon";
            this.UyeliksizButon.Size = new System.Drawing.Size(160, 33);
            this.UyeliksizButon.TabIndex = 6;
            this.UyeliksizButon.Text = "Üye Olmadan Devam Et >>";
            this.UyeliksizButon.UseVisualStyleBackColor = false;
            this.UyeliksizButon.Click += new System.EventHandler(this.UyeliksizButon_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::AracKiralamaOtomasyon.Properties.Resources.logo;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(107, 58);
            this.panel1.TabIndex = 8;
            // 
            // CikisButon
            // 
            this.CikisButon.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.CikisButon.FlatAppearance.BorderSize = 0;
            this.CikisButon.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Maroon;
            this.CikisButon.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCoral;
            this.CikisButon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CikisButon.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.CikisButon.ForeColor = System.Drawing.Color.Red;
            this.CikisButon.Image = global::AracKiralamaOtomasyon.Properties.Resources.FormCikis;
            this.CikisButon.Location = new System.Drawing.Point(456, 2);
            this.CikisButon.Name = "CikisButon";
            this.CikisButon.Size = new System.Drawing.Size(28, 29);
            this.CikisButon.TabIndex = 7;
            this.CikisButon.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.CikisButon.UseVisualStyleBackColor = true;
            this.CikisButon.Click += new System.EventHandler(this.CikisButon_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Gabriola", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.Gainsboro;
            this.label3.Location = new System.Drawing.Point(185, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 50);
            this.label3.TabIndex = 9;
            this.label3.Text = "Giriş Paneli";
            // 
            // GirisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(99)))), ((int)(((byte)(141)))));
            this.ClientSize = new System.Drawing.Size(484, 304);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CikisButon);
            this.Controls.Add(this.UyeliksizButon);
            this.Controls.Add(this.UyeOlButon);
            this.Controls.Add(this.GirisYapButon);
            this.Controls.Add(this.SifreTxtBox);
            this.Controls.Add(this.EpostaTxtBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GirisForm";
            this.Opacity = 0.95D;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Şimşek Rent A Car";
            this.Load += new System.EventHandler(this.GirisForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox EpostaTxtBox;
        private System.Windows.Forms.TextBox SifreTxtBox;
        private System.Windows.Forms.Button GirisYapButon;
        private System.Windows.Forms.Button UyeOlButon;
        private System.Windows.Forms.Button UyeliksizButon;
        private System.Windows.Forms.Button CikisButon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
    }
}

