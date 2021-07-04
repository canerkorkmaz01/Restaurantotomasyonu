using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Restaurant
{
    public partial class YonetimPaneli : Form
    {
        public YonetimPaneli()
        {
            InitializeComponent();
            //tabPage2.BackColor = System.Drawing.ColorTranslator.FromHtml("#2f2f2f");
            //tabPage1.BackColor = System.Drawing.ColorTranslator.FromHtml("#2f2f2f");

           
        }
      
        public void oku() // Grid Satırlarını Okumak İçin 
        {
            try
            {
                txtUsers.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                txtPassword.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                txtAdi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtSoyadi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtGorevi.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                mkdTel.Text= dataGridView1.CurrentRow.Cells[8].Value.ToString();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.ToString());

                //MessageBox.Show("Tablo'da Hiç Bir Kayıt Bulunamadı","Hata Mesajı",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }
        public void Kgirisi()
        {
            string users = txtUser.Text;
            string passs = txtPass.Text;


            using (SqlCommand cmd = new SqlCommand("update  YönetimPaneli set UserName=@kullanici,PassWord=@parola ", Baglanti.bag()))
            {
                cmd.Parameters.AddWithValue("@kullanici", users);
                cmd.Parameters.AddWithValue("@parola", passs);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Bilgiler Güncellendi", "Panel Güncelleme");
            }

        }
     

        private void btnGuncel_Click(object sender, System.EventArgs e)
        {
            Kgirisi();
        }

        private void YonetimPaneli_Load(object sender, System.EventArgs e)
        {
            YönetimPaneli.PersonelBilgileri pb = new YönetimPaneli.PersonelBilgileri(this);
            pb.PersonelOku();
            txtOlustur.MaxLength = 3;
            txtOluştur2.MaxLength = 3;
            txtKaldir.MaxLength = 3;
            txtKaldir2.MaxLength = 3;
            cboKat.SelectedIndex = 0;
            cboYetki.SelectedIndex = 0;
            cboKat2.SelectedIndex = 0;
        }
            
        Menu m = new Menu();
        private void btnGeri_Click(object sender, System.EventArgs e)
        {
            m.Show();
            this.Hide();
        }

        private void YonetimPaneli_FormClosing(object sender, FormClosingEventArgs e)
        {
            m.Show();
            this.Hide();
        }
        private void btnEkle_Click(object sender, System.EventArgs e)
        {
            if (txtUsers.Text == "") MessageBox.Show("Personel Adı Boş Geçilmez", "Kullanıcı Adı Boş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtPassword.Text == "") MessageBox.Show("Personel Şifresi Boş Geçilmez", "Şifre Boş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtAdi.Text == "") MessageBox.Show("Personel İsimi Boş Geçilmez", "Adı Boş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtSoyadi.Text == "") MessageBox.Show("Personel Soyadı Boş Geçilmez", "Soyadı Boş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtGorevi.Text == "") MessageBox.Show("Personel Görevi Boş Geçilmez", "Görevi Boş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (mkdTel.Text == "") MessageBox.Show("Personel Durumu Boş Geçilmez", "Durum Boş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
           
            else
            {
                YönetimPaneli.Personelıslem pi = new YönetimPaneli.Personelıslem(this);
                pi.Kaydet();
                YonetimPaneli_Load(sender, e);
            }
        }
        private void btnBack_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            m.Show();
        }

        public byte tap { get; set; }
        private void tabControl1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();

            if (CPersoneller.tab == 1)
            {
                if (tabControl1.SelectedTab == tabPage1)
                {
                    tabControl1.SelectedTab = tabPage1;
                    MessageBox.Show("Yönetici Yetkilerine Sahip Değilsiniz", "Yetki Hatası");
                }
                if (tabControl1.SelectedTab == tabPage1)
                {
                    tabPage1.Enabled = false;


                }
                CPersoneller.tab = 0;
        }
            else {; }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            oku();
        }

        private void btnGuncelle_Click(object sender, System.EventArgs e)
        {
            if (txtUsers.Text == "") MessageBox.Show("Personel Adı Boş Geçilmez", "Kullanıcı Adı Boş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtPassword.Text == "") MessageBox.Show("Personel Şifresi Boş Geçilmez", "Şifre Boş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtAdi.Text == "") MessageBox.Show("Personel İsimi Boş Geçilmez", "Adı Boş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtSoyadi.Text == "") MessageBox.Show("Personel Soyadı Boş Geçilmez", "Soyadı Boş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtGorevi.Text == "") MessageBox.Show("Personel Görevi Boş Geçilmez", "Görevi Boş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (mkdTel.Text == "") MessageBox.Show("Personel Durumu Boş Geçilmez", "Durum Boş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
            else
            {
                YönetimPaneli.Personelıslem bi = new YönetimPaneli.Personelıslem(this);
                bi.Degistir();
                YonetimPaneli_Load(sender, e);
            }
        }

        private void btnSil_Click(object sender, System.EventArgs e)
        {
            YönetimPaneli.Personelıslem pi = new YönetimPaneli.Personelıslem(this);
            pi.Sil();
            YonetimPaneli_Load(sender, e);
        }
        private void btnTemizle_Click(object sender, System.EventArgs e)
        {
            txtAdi.Text = "";
            txtGorevi.Text = "";
            txtPass.Text = "";
            txtPassword.Text = "";
            txtSoyadi.Text = "";
            txtUsers.Text = ""; 
        }
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            YönetimPaneli.PersonelBilgileri pb = new YönetimPaneli.PersonelBilgileri(this);
            pb.Arama();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            YönetimPaneli.Yöneticiİslemleri yi = new YönetimPaneli.Yöneticiİslemleri(this);
            yi.MasaEkle();
        }

        public int yetki  { get; set; }

        private void rbYardimci_CheckedChanged(object sender, EventArgs e)
        {
            yetki = 2;
        }

        private void drPer_CheckedChanged(object sender, EventArgs e)
        {
            yetki = 3;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            YönetimPaneli.Yöneticiİslemleri yi = new YönetimPaneli.Yöneticiİslemleri(this);
            yi.Kaydet();
            YonetimPaneli_Load(sender, e);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            YönetimPaneli.Yöneticiİslemleri yi = new YönetimPaneli.Yöneticiİslemleri(this);
            yi.Güncelle();
            YonetimPaneli_Load(sender, e);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            YönetimPaneli.Yöneticiİslemleri yi = new YönetimPaneli.Yöneticiİslemleri(this);
            yi.Sil();
            YonetimPaneli_Load(sender, e);
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            YönetimPaneli.Yöneticiİslemleri yi = new YönetimPaneli.Yöneticiİslemleri(this);
            yi.Oku();
            YonetimPaneli_Load(sender, e);
        }

        private void btnGetir_Click(object sender, EventArgs e)
        {
            YönetimPaneli.YöneticiBilgileri yb = new YönetimPaneli.YöneticiBilgileri(this);
            yb.YetkiliBilgileri();
            YonetimPaneli_Load(sender, e);
        }

        private void btnKaldir_Click(object sender, EventArgs e)
        {
            YönetimPaneli.Yöneticiİslemleri yi = new YönetimPaneli.Yöneticiİslemleri(this);
            yi.Kaldır();
            YonetimPaneli_Load(sender, e);
        }

       
    }
}
