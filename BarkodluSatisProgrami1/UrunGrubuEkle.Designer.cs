namespace BarkodluSatisProgrami1
{
    partial class UrunGrubuEkle
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
            this.label6 = new System.Windows.Forms.Label();
            this.txtUrunGrubuAdi = new System.Windows.Forms.TextBox();
            this.listBoxUrunGrupAdi = new System.Windows.Forms.ListBox();
            this.btnSil = new System.Windows.Forms.Button();
            this.btnEkle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.ForeColor = System.Drawing.Color.DarkCyan;
            this.label6.Location = new System.Drawing.Point(32, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(177, 26);
            this.label6.TabIndex = 8;
            this.label6.Text = "Ürün Grubu Adı";
            // 
            // txtUrunGrubuAdi
            // 
            this.txtUrunGrubuAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtUrunGrubuAdi.Location = new System.Drawing.Point(37, 62);
            this.txtUrunGrubuAdi.Multiline = true;
            this.txtUrunGrubuAdi.Name = "txtUrunGrubuAdi";
            this.txtUrunGrubuAdi.Size = new System.Drawing.Size(322, 32);
            this.txtUrunGrubuAdi.TabIndex = 0;
            this.txtUrunGrubuAdi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // listBoxUrunGrupAdi
            // 
            this.listBoxUrunGrupAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listBoxUrunGrupAdi.FormattingEnabled = true;
            this.listBoxUrunGrupAdi.ItemHeight = 20;
            this.listBoxUrunGrupAdi.Location = new System.Drawing.Point(37, 110);
            this.listBoxUrunGrupAdi.Name = "listBoxUrunGrupAdi";
            this.listBoxUrunGrupAdi.Size = new System.Drawing.Size(322, 204);
            this.listBoxUrunGrupAdi.TabIndex = 9;
            // 
            // btnSil
            // 
            this.btnSil.BackColor = System.Drawing.Color.Peru;
            this.btnSil.FlatAppearance.BorderColor = System.Drawing.Color.Peru;
            this.btnSil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSil.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSil.ForeColor = System.Drawing.Color.White;
            this.btnSil.Image = global::BarkodluSatisProgrami1.Properties.Resources.cancel24;
            this.btnSil.Location = new System.Drawing.Point(37, 328);
            this.btnSil.Margin = new System.Windows.Forms.Padding(1);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(152, 83);
            this.btnSil.TabIndex = 2;
            this.btnSil.Text = "SİL";
            this.btnSil.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSil.UseVisualStyleBackColor = false;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btnEkle
            // 
            this.btnEkle.BackColor = System.Drawing.Color.Tomato;
            this.btnEkle.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            this.btnEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEkle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnEkle.ForeColor = System.Drawing.Color.White;
            this.btnEkle.Image = global::BarkodluSatisProgrami1.Properties.Resources.Ekle24;
            this.btnEkle.Location = new System.Drawing.Point(207, 328);
            this.btnEkle.Margin = new System.Windows.Forms.Padding(1);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(152, 83);
            this.btnEkle.TabIndex = 1;
            this.btnEkle.Text = "EKLE";
            this.btnEkle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEkle.UseVisualStyleBackColor = false;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // UrunGrubuEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(402, 435);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.btnEkle);
            this.Controls.Add(this.listBoxUrunGrupAdi);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtUrunGrubuAdi);
            this.Name = "UrunGrubuEkle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ürün Grubu İşlemleri";
            this.Load += new System.EventHandler(this.UrunGrubuEkle_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUrunGrubuAdi;
        private System.Windows.Forms.ListBox listBoxUrunGrupAdi;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Button btnSil;
    }
}