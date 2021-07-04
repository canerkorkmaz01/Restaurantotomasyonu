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
    public partial class Rezervasyon : Form
    {
        public Rezervasyon()
        {
            InitializeComponent();
           
        }
        DataTable dt = new DataTable();
        //public void dtTemizle() //Başka Forumdan 
        //{
        //    dt.Clear();
        //}
       
        public void oku() //Rezervasyonlardaki Bütün Tabloları Okumak İçin Kullanılan Metot
        {
            using (SqlDataAdapter r = new SqlDataAdapter("SELECT * FROM Rezervasyon WHERE Durum=1", Baglanti.bag()))
            {
               
                r.Fill(dt);
                dataGridView1.DataSource = dt;
                ////dataGridView1.Columns[0].Visible = false;
                ////dataGridView1.Columns[6].Visible = false;
                ////dataGridView1.Columns[5].Visible = false;
                ////dataGridView1.Columns[9].Visible = false;
                ////dataGridView1.Columns[10].Visible = false;
                //dataGridView1.Columns[4].Width = 120;
                //dataGridView1.Columns[8].Width = 95;
                grid();


                r.Dispose();

            }

        }

        private void bilgioku() // DatagridView Adres Rezervasyon Kayıt Tarihi Ve Rezervasyon Tarih Bilgisini Tutmak İçin Kullanılır
        {
            try
            {
                maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                maskedTextBox2.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                txtAdres.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Müşteri Bilgisi Bulunamadı..!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        DataTable db = new DataTable();// Combobox MasaID lerin Saklandıgı Veri

        private void Masalar()// Combobox da Durumu 0 Olan Masaların Bilgisini Okutuyoruz
        {
            using (SqlDataAdapter ads = new SqlDataAdapter("SELECT MasaID FROM Masalar WHERE Durumu=0", Baglanti.bag()))
            {
                ads.Fill(db);
                cboMasalar.DataSource = db;
                cboMasalar.ValueMember = "MasaID";        
                
            }
         }
        
        private void RezerveMasa() //Rezervasyon ID Seçilen Masaların MasaID Okumak İçin Kullanılır
        {
            try
            {
                using (SqlCommand rmasa = new SqlCommand("SELECT MasaID FROM Masalar WHERE RezervasyonID=@r", Baglanti.bag()))
                {
                    rmasa.Parameters.AddWithValue("@r", dataGridView1.CurrentRow.Cells[0].Value);
                    SqlDataReader oku = rmasa.ExecuteReader();
                    while (oku.Read())
                    {
                        txtMasa.Text = oku["MasaID"].ToString();
                    }

                }
            }
            catch (Exception) {; }
        }

        private void Uzunluk()
        {
            txtMadi.MaxLength = 15;
            txtMSoyadi.MaxLength = 15;
            txtTelefon.MaxLength = 10;
            txtAdres.MaxLength = 50;

        }

        private void Rezervasyon_Load(object sender, EventArgs e) //Formun Load Açılışta Combobox daki Verileri Açılışta Combobox İçine Eklemek için
        {
            Masalar();
        }

        private void grid()
        {
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "M Adı";
            dataGridView1.Columns[2].HeaderText = "M Soyadı";
            dataGridView1.Columns[3].HeaderText = "M Ünvanı";
            dataGridView1.Columns[8].HeaderText = "K Sayısı";
            dataGridView1.Columns[4].HeaderText = "Telefon";
            dataGridView1.Columns[7].HeaderText = "E-Mail";
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[4].Width = 120;
            dataGridView1.Columns[8].Width = 95;
        }

        private void btnRzv_Click(object sender, EventArgs e) //dataGridView Kolon İsimleri Değiştiriyoruz - oku() Metodunu ve dt.clear() Metodlarını Kullanıyoruz
        {
            dt.Clear();
            oku();
            grid();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)// dataGridView1 Cell Click Olayı
        {
            txtMasa.Text = " ";
            RezerveMasa();
            bilgioku();
            Durumu = 0;
            durum();
            btnRezervasyonAc.Enabled = true;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)// dataGridView Açılışta Seçili Gelmesini Engelliyoruz
        {
            dataGridView1.ClearSelection();
        }

        private void txtMadi_TextChanged(object sender, EventArgs e) // Müşteri Adında Arama Yapmak İçin Kullanılır
        {
            if (txtMadi.Text.Trim() == "") { dt.Clear(); oku(); grid(); }
            else
            {
                using (SqlDataAdapter arama = new SqlDataAdapter("SELECT * FROM Rezervasyon WHERE Durum=1 AND MusteriAdi  LIKE '%' +@Madi+ '%'  ", Baglanti.bag()))
                {
                    arama.SelectCommand.Parameters.AddWithValue("@Madi", txtMadi.Text);
                    DataTable db = new DataTable();
                    arama.Fill(db);
                    dataGridView1.DataSource = db;
                    grid();
                    arama.Dispose();
                }
            }
        }

        private void txtMSoyadi_TextChanged(object sender, EventArgs e)// Müşteri Soyadında Arama Yapmak İçin Kullanılır
        {
            if (txtMSoyadi.Text.Trim() == "") { dt.Clear(); oku(); grid(); }
            else
            {
                using (SqlDataAdapter arama = new SqlDataAdapter("SELECT * FROM Rezervasyon WHERE Durum=1 AND MusteriSoyadi LIKE '%' +@Msoyadi+ '%'", Baglanti.bag()))
                {
                    arama.SelectCommand.Parameters.AddWithValue("@Msoyadi", txtMSoyadi.Text);
                    DataTable db = new DataTable();
                    arama.Fill(db);
                    dataGridView1.DataSource = db;
                    grid();
                    arama.Dispose();
                }
            }
        }

        private void txtTelefon_TextChanged(object sender, EventArgs e) // Müşteri Telefonunda Arama Yapmak İçin Kullanılır
        {
            if (txtTelefon.Text.Trim() == "") { dt.Clear(); oku(); grid(); }
            else
            {
                using (SqlDataAdapter arama = new SqlDataAdapter("SELECT * FROM Rezervasyon WHERE Durum=1 AND Telefonu LIKE '%' +@Mtelefon+ '%'", Baglanti.bag()))
                {
                    arama.SelectCommand.Parameters.AddWithValue("@Mtelefon", txtTelefon.Text);
                    DataTable db = new DataTable();
                    arama.Fill(db);
                    dataGridView1.DataSource = db;
                    grid();
                    arama.Dispose();
                }
            }
        }

        private void txtAdress_TextChanged(object sender, EventArgs e) // Müşteri Adres Bilgisinde Arama Yapmak İçin Kullanılır
        {
            if (txtAdress.Text.Trim() == "") { dt.Clear(); oku(); grid(); }
            else
            {
                using (SqlDataAdapter arama = new SqlDataAdapter("SELECT * FROM Rezervasyon WHERE Durum=1 AND Adresi LIKE '%' +@Madres+ '%'", Baglanti.bag()))
                {
                    arama.SelectCommand.Parameters.AddWithValue("@Madres", txtAdress.Text);
                    DataTable db = new DataTable();
                    arama.Fill(db);
                    dataGridView1.DataSource = db;
                    grid();
                    arama.Dispose();
                }
            }
        }

        private void MasaRezerve() // Masayı Rezerve Etmek İçin Kullanılır
        {
            try
            {
                using (SqlCommand rezerve = new SqlCommand("UPDATE Masalar SET Durumu=@d,RezervasyonID=@r WHERE MasaID=@m", Baglanti.bag()))
                {
                    rezerve.Parameters.AddWithValue("@m", cboMasalar.SelectedValue);
                    rezerve.Parameters.AddWithValue("@d", 2);
                    rezerve.Parameters.AddWithValue("@r", Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
                    rezerve.ExecuteNonQuery();
                    MessageBox.Show("Rezervasyon İşlemi Yapılmıştır.", "Rezervasyon Ekle", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rezerve.Dispose();
                }
            }
            catch(Exception)
            {
                MessageBox.Show("Lütenn Rezervasyon Yapılacak Masayı Seçiniz..","Masa İşlemi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void dtpTarih_ValueChanged(object sender, EventArgs e) // Müşteri Tarih Bilgisinde Arama Yapmak İçin Kullanılır
        {

            if (dtpTarih.Value != null)
            {
                using (SqlDataAdapter arama = new SqlDataAdapter(@"SELECT * FROM Rezervasyon WHERE Tarih >= @baslangic AND Tarih < @bitis AND Durum=1", Baglanti.bag()))
                {
                    arama.SelectCommand.Parameters.AddWithValue("@baslangic", dtpTarih.Value.Date);
                    arama.SelectCommand.Parameters.AddWithValue("@bitis", dtpTarih.Value.Date.AddDays(1));
                    DataTable db = new DataTable();
                    arama.Fill(db);
                    dataGridView1.DataSource = db;
                    arama.Dispose();
                }
            }

            else
            {
                dt.Clear(); oku(); 
            }
        }

        int Durumu;
        private void durum() // Açık Rezervasyon Tekrar Rezerve Edilmesini Engellemek
        {
            try
            {
                using (SqlCommand duru = new SqlCommand("SELECT Durumu FROM Masalar Durumu WHERE RezervasyonID=@d", Baglanti.bag()))
                {
                    duru.Parameters.AddWithValue("@d", dataGridView1.CurrentRow.Cells[0].Value);

                    SqlDataReader oku = duru.ExecuteReader();
                    while (oku.Read())
                    {
                       
                        Durumu = Convert.ToInt32(oku["Durumu"]);
                       
                    }
                    oku.Dispose();
                    duru.Dispose();
                }
            }
             catch(Exception)
            {
                MessageBox.Show("Rezervasyon Yapılacak Müşterileri Seçiniz", "Hatalı Rezervasyon İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
       
        private void btnMüsteri_Click(object sender, EventArgs e)// Müsteri Ekleme
        {
            MusteriEkle m = new MusteriEkle();
            m.Show();
            m.btnGüncelle.Enabled = false;
            m.btnSil.Enabled = false;
            this.Dispose();
        }
        
        private void btnGüncelle_Click(object sender, EventArgs e)
        {
                MusteriEkle m = new MusteriEkle();
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Lütfen Müşteri Seçin..!","Hata",MessageBoxButtons.OK,MessageBoxIcon.Information);
                   
                }
                else
                {
                    m.btnKaydet.Enabled = false;
                    m.btnSil.Enabled = false;
                    m.Show();
                    m.g=(int) dataGridView1.CurrentRow.Cells[0].Value;
                    m.txtMadi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    m.txtMSoyadi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    m.txtMÜnvani.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    m.mkdTel.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    m.dateTimePicker1.Value =(DateTime) dataGridView1.CurrentRow.Cells[5].Value;
                    m.txtMAdresi.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                    m.txtMEmail.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                    m.txtKSayisi.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                    this.Dispose();
                }

        } // Müşteri Güncelleme

        private void btnSil_Click(object sender, EventArgs e) // Müsteri Silme
        {
            MusteriEkle m = new MusteriEkle();
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen Müşteri Seçin..!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                m.btnGüncelle.Enabled = false;
                m. btnKaydet.Enabled = false;
                m.Show();
                m.g = (int)dataGridView1.CurrentRow.Cells[0].Value;
                m.txtMadi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                m.txtMSoyadi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                m.txtMÜnvani.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                m.mkdTel.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                m.dateTimePicker1.Value = (DateTime)dataGridView1.CurrentRow.Cells[5].Value;
                m.txtMAdresi.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                m.txtMEmail.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                m.txtKSayisi.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();

                m.txtMadi.Enabled = false;
                m.txtMSoyadi.Enabled = false;
                m.txtMÜnvani.Enabled = false;
                m.mkdTel.Enabled = false;
                m.dateTimePicker1.Enabled = false;
                m.txtMAdresi.Enabled = false;
                m.txtMEmail.Enabled = false;
                m.txtKSayisi.Enabled = false;
                this.Dispose();
            }
        }

        private void btnRezervasyonAc_Click(object sender, EventArgs e)// Rezervasyon Oluşturmak için Kullanılır
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen Müşteri Seçiniz..", "Müşteri İşlemleri", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Durumu == 2)
            {
                MessageBox.Show("Masa Rezerveli", "Rezervasyon İşlemi Hatalı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if(dataGridView1.SelectedRows!=null)
            {
                MasaRezerve();
                db.Clear();
                Masalar();
                txtMasa.Text = " ";
                Rezervasyon_Load(sender, e);
                btnRezervasyonAc.Enabled = false;
            }
           
        }

        private void btnGeri_Click(object sender, EventArgs e) // Menu Formuna Dönüp Rezervasyon Formunu Kapatmak İçin Kullanılır
        {
            Menu m = new Menu();
            m.Show();
            this.Dispose();
        }

        private void Rezervasyon_FormClosing(object sender, FormClosingEventArgs e)
        {
            Menu m = new Menu();
            m.Show();
            this.Dispose();
        }

        private void txtTelefon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtAdress_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);

        }

        private void txtMSoyadi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }

        private void txtMadi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }
    }

    }

