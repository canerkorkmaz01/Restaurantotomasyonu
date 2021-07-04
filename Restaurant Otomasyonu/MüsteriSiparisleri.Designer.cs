namespace Restaurant
{
    partial class MusteriSiparisleri
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MusteriSiparisleri));
            this.btnSil = new System.Windows.Forms.Button();
            this.btnGüncelle = new System.Windows.Forms.Button();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMadi = new System.Windows.Forms.TextBox();
            this.txtMAdresi = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMSoyadi = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMÜnvani = new System.Windows.Forms.TextBox();
            this.mkdTel = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSil
            // 
            this.btnSil.BackgroundImage = global::Restaurant.Properties.Resources.sil;
            this.btnSil.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSil.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSil.Location = new System.Drawing.Point(352, 430);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(176, 76);
            this.btnSil.TabIndex = 6;
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btnGüncelle
            // 
            this.btnGüncelle.BackgroundImage = global::Restaurant.Properties.Resources.mg;
            this.btnGüncelle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnGüncelle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGüncelle.Location = new System.Drawing.Point(534, 430);
            this.btnGüncelle.Name = "btnGüncelle";
            this.btnGüncelle.Size = new System.Drawing.Size(176, 76);
            this.btnGüncelle.TabIndex = 7;
            this.btnGüncelle.UseVisualStyleBackColor = true;
            this.btnGüncelle.Click += new System.EventHandler(this.btnGüncelle_Click);
            // 
            // btnKaydet
            // 
            this.btnKaydet.BackgroundImage = global::Restaurant.Properties.Resources.kaydet;
            this.btnKaydet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnKaydet.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnKaydet.Location = new System.Drawing.Point(170, 430);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(176, 76);
            this.btnKaydet.TabIndex = 8;
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtMadi);
            this.groupBox1.Controls.Add(this.txtMAdresi);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtMSoyadi);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtMÜnvani);
            this.groupBox1.Controls.Add(this.mkdTel);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(170, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(540, 392);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Müşteri Ekle";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(51, 293);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Müşteri Adresi";
            // 
            // txtMadi
            // 
            this.txtMadi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtMadi.ForeColor = System.Drawing.Color.Black;
            this.txtMadi.Location = new System.Drawing.Point(219, 76);
            this.txtMadi.Name = "txtMadi";
            this.txtMadi.Size = new System.Drawing.Size(195, 26);
            this.txtMadi.TabIndex = 1;
            this.txtMadi.TextChanged += new System.EventHandler(this.txtMadi_TextChanged);
            this.txtMadi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMadi_KeyPress);
            // 
            // txtMAdresi
            // 
            this.txtMAdresi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtMAdresi.ForeColor = System.Drawing.Color.Black;
            this.txtMAdresi.Location = new System.Drawing.Point(219, 204);
            this.txtMAdresi.Multiline = true;
            this.txtMAdresi.Name = "txtMAdresi";
            this.txtMAdresi.Size = new System.Drawing.Size(195, 109);
            this.txtMAdresi.TabIndex = 7;
            this.txtMAdresi.TextChanged += new System.EventHandler(this.txtMAdresi_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(51, 175);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "Müşteri Telefonu";
            // 
            // txtMSoyadi
            // 
            this.txtMSoyadi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtMSoyadi.ForeColor = System.Drawing.Color.Black;
            this.txtMSoyadi.Location = new System.Drawing.Point(219, 108);
            this.txtMSoyadi.Name = "txtMSoyadi";
            this.txtMSoyadi.Size = new System.Drawing.Size(195, 26);
            this.txtMSoyadi.TabIndex = 2;
            this.txtMSoyadi.TextChanged += new System.EventHandler(this.txtMSoyadi_TextChanged);
            this.txtMSoyadi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMSoyadi_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(51, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "Müşteri Ünvanı ";
            // 
            // txtMÜnvani
            // 
            this.txtMÜnvani.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtMÜnvani.ForeColor = System.Drawing.Color.Black;
            this.txtMÜnvani.Location = new System.Drawing.Point(219, 140);
            this.txtMÜnvani.Name = "txtMÜnvani";
            this.txtMÜnvani.Size = new System.Drawing.Size(195, 26);
            this.txtMÜnvani.TabIndex = 3;
            this.txtMÜnvani.TextChanged += new System.EventHandler(this.txtMÜnvani_TextChanged);
            this.txtMÜnvani.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMÜnvani_KeyPress);
            // 
            // mkdTel
            // 
            this.mkdTel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.mkdTel.ForeColor = System.Drawing.Color.Black;
            this.mkdTel.Location = new System.Drawing.Point(219, 172);
            this.mkdTel.Mask = "000 000 00 00";
            this.mkdTel.Name = "mkdTel";
            this.mkdTel.Size = new System.Drawing.Size(195, 26);
            this.mkdTel.TabIndex = 5;
            this.mkdTel.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(51, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Müşteri Soyadı  ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(51, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Müşteri Adı       ";
            // 
            // MusteriSiparisleri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.ClientSize = new System.Drawing.Size(913, 529);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.btnGüncelle);
            this.Controls.Add(this.btnKaydet);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MusteriSiparisleri";
            this.Text = "Müşteri Ekle";
            this.Load += new System.EventHandler(this.MusteriSiparisleri_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnSil;
        public System.Windows.Forms.Button btnGüncelle;
        public System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtMadi;
        public System.Windows.Forms.TextBox txtMAdresi;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtMSoyadi;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtMÜnvani;
        public System.Windows.Forms.MaskedTextBox mkdTel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.GroupBox groupBox1;
    }
}