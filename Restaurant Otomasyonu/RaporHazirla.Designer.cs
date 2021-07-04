namespace Restaurant
{
    partial class RaporHazirla
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.cboSecim = new System.Windows.Forms.ComboBox();
            this.btnZraporu = new System.Windows.Forms.Button();
            this.btnAylik = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateTimePicker1.Location = new System.Drawing.Point(37, 58);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(204, 26);
            this.dateTimePicker1.TabIndex = 2;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateTimePicker2.Location = new System.Drawing.Point(37, 90);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(204, 26);
            this.dateTimePicker2.TabIndex = 3;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // cboSecim
            // 
            this.cboSecim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSecim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cboSecim.FormattingEnabled = true;
            this.cboSecim.Items.AddRange(new object[] {
            "Satış Raporları",
            "Masa Raporları",
            "Paket Servis Raporları"});
            this.cboSecim.Location = new System.Drawing.Point(37, 24);
            this.cboSecim.Name = "cboSecim";
            this.cboSecim.Size = new System.Drawing.Size(204, 28);
            this.cboSecim.TabIndex = 4;
            // 
            // btnZraporu
            // 
            this.btnZraporu.BackgroundImage = global::Restaurant.Properties.Resources.zraporu;
            this.btnZraporu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnZraporu.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnZraporu.Location = new System.Drawing.Point(176, 156);
            this.btnZraporu.Name = "btnZraporu";
            this.btnZraporu.Size = new System.Drawing.Size(109, 91);
            this.btnZraporu.TabIndex = 1;
            this.btnZraporu.UseVisualStyleBackColor = true;
            this.btnZraporu.Click += new System.EventHandler(this.btnZraporu_Click);
            // 
            // btnAylik
            // 
            this.btnAylik.BackgroundImage = global::Restaurant.Properties.Resources.AylikRapor;
            this.btnAylik.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAylik.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAylik.Location = new System.Drawing.Point(12, 156);
            this.btnAylik.Name = "btnAylik";
            this.btnAylik.Size = new System.Drawing.Size(158, 91);
            this.btnAylik.TabIndex = 0;
            this.btnAylik.UseVisualStyleBackColor = true;
            this.btnAylik.Click += new System.EventHandler(this.btnAylik_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimePicker2);
            this.groupBox1.Controls.Add(this.cboSecim);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 138);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Raporlama";
            // 
            // RaporHazirla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.ClientSize = new System.Drawing.Size(297, 259);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnZraporu);
            this.Controls.Add(this.btnAylik);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "RaporHazirla";
            this.Text = "RaporHazirla";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RaporHazirla_FormClosed);
            this.Load += new System.EventHandler(this.RaporHazirla_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnZraporu;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        public System.Windows.Forms.Button btnAylik;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox cboSecim;
        
    }
}