namespace AracKiralamaOtomasyon
{
    partial class AracResimForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AracResimPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.AracResimPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AracResimPictureBox
            // 
            this.AracResimPictureBox.Location = new System.Drawing.Point(2, 1);
            this.AracResimPictureBox.Name = "AracResimPictureBox";
            this.AracResimPictureBox.Size = new System.Drawing.Size(901, 595);
            this.AracResimPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AracResimPictureBox.TabIndex = 0;
            this.AracResimPictureBox.TabStop = false;
            // 
            // AracResimForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(906, 598);
            this.Controls.Add(this.AracResimPictureBox);
            this.MaximizeBox = false;
            this.Name = "AracResimForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AracResimForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AracResimPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox AracResimPictureBox;
    }
}