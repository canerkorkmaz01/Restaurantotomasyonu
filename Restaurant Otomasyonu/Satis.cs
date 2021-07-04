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
    public partial class Satis : Form
    {
        public Satis()
        {
            InitializeComponent();
            Olaylar();
           
        }

        DataTable dt = new DataTable();// dt Datatable Ürünler Bilgisini Tutuyor 

        public void Urunler(int id) // Urunlerin Çagrıldı Metot
        {
            try
            {
                using (SqlDataAdapter read = new SqlDataAdapter("SELECT UrunAdi,UrunFiyatı,UrunId FROM Urunler WHERE GrupID=@ıd ", Baglanti.bag()))
                {
                    read.SelectCommand.Parameters.AddWithValue("@ıd", id);
                    read.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[0].HeaderText = "Ürün Adı";
                    dataGridView1.Columns[1].HeaderText = "Fiyatı";
                    dataGridView1.Columns[2].HeaderText = "Ürün No";
                    read.Dispose();
                }
            }
            catch (Exception hata)
            { MessageBox.Show(hata.ToString()); }

        }

        DataTable db = new DataTable();

        private void Oku() /// Buradan Devam Edilecek
        {
            using (SqlDataAdapter oku=new SqlDataAdapter ("SELECT SatisID,UrunAdi,Fiyati,Adeti,Tutari FROM Satislar WHERE Durum=1",Baglanti.bag()))
            {
                oku.Fill(db);
                dataGridView2.DataSource = db;
                oku.Dispose();
            }
        }

        string Odeme = null;
        private void Satıs()
       {
            try
            {
                if (rdmNakit.Checked == true) { Odeme = rdmNakit.Text; }
                else if (rdmKredi.Checked == true) { Odeme = rdmKredi.Text; }
                else if (rdmIkram.Checked == true) { Odeme = rdmIkram.Text; }
                else if (rdmSodexo.Checked == true) { Odeme = rdmNakit.Text; }

                using (SqlCommand s=new SqlCommand("UPDATE Satislar SET  OdemeTuru=@o WHERE Durum=1",Baglanti.bag()))
                {
                    s.Parameters.AddWithValue("@o",Odeme);
                    //s.Parameters.AddWithValue("@t", DateTime.Now.Date.AddHours(00).AddMinutes(00).AddSeconds(00).AddMilliseconds(000));
                    s.ExecuteNonQuery();
                }
                
            }
            catch (Exception hata) { MessageBox.Show(hata.ToString()); }
        }

        public void Satislar() //dataGridView1 Seçilen Ürünleri Veritabanına Aktarmak İçin Kullanılan Metot
        {
            try
            {
                using (SqlCommand ekle = new SqlCommand("INSERT INTO Satislar (UrunAdi,Fiyati,Adeti,Tutari,Durum,Tarih) VALUES (@urun,@fiyat,@adt,@tut,@d,@t) ", Baglanti.bag()))
                {

                    int adet = Convert.ToInt32(txtAdet.Text);
                    decimal fiyat = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[1].Value);
                    ekle.Parameters.AddWithValue("@urun", dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    ekle.Parameters.AddWithValue("@fiyat", fiyat);
                    ekle.Parameters.AddWithValue("@adt", adet);
                    ekle.Parameters.AddWithValue("@tut", adet * fiyat);
                    ekle.Parameters.AddWithValue("@d", 1);
                    ekle.Parameters.AddWithValue("@t", DateTime.Now.Date.AddHours(00).AddMinutes(00).AddSeconds(00).AddMilliseconds(000));
                    ekle.ExecuteNonQuery();
                   
                    ekle.Dispose();
                }
        }
            catch (Exception)
            {

                MessageBox.Show("Lütfen Adet Miktarı Girin", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
       }

        private void Ode()
        {
            using (SqlCommand o=new SqlCommand("UPDATE Satislar SET Durum=0 WHERE Durum=1",Baglanti.bag()))
            {
                o.ExecuteNonQuery();
            }
        }


        private void islem(object sender, EventArgs e)// Siparisler Eklenirken Adeti Ekleyecegimiz Metot
        {

            Button btn = sender as Button;

            switch (btn.Name)
            {
                case "btn1":

                    txtAdet.Text += (1).ToString();

                    break;
                case "btn2":

                    txtAdet.Text += (2).ToString();

                    break;
                case "btn3":

                    txtAdet.Text += (3).ToString();
                    break;
                case "btn4":

                    txtAdet.Text += (4).ToString();
                    break;
                case "btn5":

                    txtAdet.Text += (5).ToString();
                    break;
                case "btn6":

                    txtAdet.Text += (6).ToString();
                    break;
                case "btn7":

                    txtAdet.Text += (7).ToString();
                    break;
                case "btn8":

                    txtAdet.Text += (8).ToString();
                    break;
                case "btn9":

                    txtAdet.Text += (9).ToString();
                    break;
                case "btn0":

                    txtAdet.Text += (0).ToString();
                    break;
            }


        }// işlem yapmak için 

        private void Olaylar()
        {
            btn1.Click += new EventHandler(islem);
            btn2.Click += new EventHandler(islem);
            btn3.Click += new EventHandler(islem);
            btn4.Click += new EventHandler(islem);
            btn5.Click += new EventHandler(islem);
            btn6.Click += new EventHandler(islem);
            btn7.Click += new EventHandler(islem);
            btn8.Click += new EventHandler(islem);
            btn9.Click += new EventHandler(islem);
            btn0.Click += new EventHandler(islem);
        }

        private void Satis_Load(object sender, EventArgs e)
        {
            Oku();
            Toplam();
            groupBox6.Visible = false;
            grid();
            txtAdet.MaxLength = 10;
            txtIndirim.MaxLength = 4;
        }

        private void btnAnaYemekler_Click(object sender, EventArgs e)
        {
            dt.Clear();
            Urunler(1);
        }

        private void btnİçecek_Click(object sender, EventArgs e)
        {
            dt.Clear();
            Urunler(2);
        }

        private void btnTatlilar_Click(object sender, EventArgs e)
        {
            dt.Clear();
            Urunler(3);
        }

        private void btnSalata_Click(object sender, EventArgs e)
        {
            dt.Clear();
            Urunler(4);
        }

        private void btnFastfood_Click(object sender, EventArgs e)
        {
            dt.Clear();
            Urunler(5);
        }

        private void btnCorba_Click(object sender, EventArgs e)
        {
            dt.Clear();
            Urunler(6);
        }

        private void btnMakarna_Click(object sender, EventArgs e)
        {
            dt.Clear();
            Urunler(7);
        }

        private void btnArasicak_Click(object sender, EventArgs e)
        {
            dt.Clear();
            Urunler(8);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        float toplam ;
        private void Toplam()
        {
            toplam = 0f;

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                toplam += Convert.ToInt32(dataGridView2.Rows[i].Cells[4].Value);
                label2.Text = toplam.ToString("N2");
            }
        }
       
        private void SecilenToplam()
        {
            try
            {
                float toplam = 0f;

                foreach (DataGridViewRow row in dataGridView2.SelectedRows)

                    toplam += Convert.ToSingle(row.Cells[4].Value);

                label4.Text = toplam.ToString("N2");
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.ToString());
            }

        }

        private void Miktar() //iskonto Burdan ekleniyor........
        {
            try
            {
                using (SqlCommand m = new SqlCommand("INSERT INTO İndirimler(Satisindirim) VALUES (@t) ", Baglanti.bag()))
                {
                    m.Parameters.AddWithValue("@t", Convert.ToDecimal(txtIndirim.Text));
                    m.ExecuteNonQuery();
                    m.Dispose();
                }
            }
            catch (Exception) {;}
        }

        private void btnOdeme_Click(object sender, EventArgs e)
        {
            if (rdmNakit.Checked == false && rdmKredi.Checked == false && rdmIkram.Checked == false && rdmSodexo.Checked == false)
            {
                MessageBox.Show("Lütfen Ödeme Türünü Girniz ", "Ödeme Türü", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Miktar();
                Satıs();
                Ode();
                db.Clear();
                label4.Text = "";
                label2.Text = "";

                MessageBox.Show("Satış Tamamlanmıştır", "Satış İşlemi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Satislar();
            db.Clear();
            Oku();

            if (dataGridView2.Rows.Count==0)
            {
                ;
            }
            else
            {
                Toplam();
            }
           
        }

       

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                groupBox6.Visible = true;

            }
            else if (checkBox1.Checked==false)
            {
                groupBox6.Visible = false;
                label6.Text = "";
                txtIndirim.Text = "";
            }
        }

        private void txtIndirim_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtIndirim.Text != "")
                {
                    float tutar = 0f;
                    float indirim = 0f;

                    indirim = Convert.ToSingle(txtIndirim.Text);
                    tutar = toplam - float.Parse(txtIndirim.Text);

                    label2.Text = tutar.ToString("N2");

                    label6.Text = indirim.ToString("N2"); 
                }

                else if(txtIndirim.Text == "" )
                {
                    label2.Text = toplam.ToString("N2");
                }
            }
            catch (Exception) { }
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Menu m = new Menu();
            m.Show();
            this.Dispose();

        }

        private void Satis_FormClosed(object sender, FormClosedEventArgs e)
        {
           Menu m = new Menu();
            m.Show();
            this.Dispose();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                txtAdet.Text = txtAdet.Text.Substring(0, txtAdet.TextLength - 1);
            }

            catch (Exception) { }
        }

        private void GridSil(int index)
        {
            try
            {
                using (SqlCommand urun = new SqlCommand("UPDATE Satislar SET Durum=2 WHERE SatisID=@id", Baglanti.bag()))
                {
                    int value = 0;
                    int.TryParse(dataGridView2.Rows[index].Cells[0].Value.ToString(), out value);
                    urun.Parameters.AddWithValue("@id",value); //Convert.ToInt32(dataGridView2.Rows[index].Cells[0].Value)
                    urun.ExecuteNonQuery();
                    urun.Parameters.Clear();
                    db.Clear();
                    Oku();
                    urun.Dispose();
                }
            }
            catch (Exception) {;}
        }

        private void grid()
        {
            try
            {
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.Columns[1].HeaderText = "Ürün Adı";
                dataGridView2.Columns[2].HeaderText = "Fiyatı";
                dataGridView2.Columns[3].HeaderText = "Adeti";
                dataGridView2.Columns[4].HeaderText = "Tutarı";
            }
            catch (Exception) {;}
        }        

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SecilenToplam();
            grid();
        }

        private void dataGridView2_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            db.Clear();
            int index = e.RowIndex;
            db.Clear();
            Oku();
            GridSil(index);
            
            //Satis_Load(sender, e);
        }

        private void İptal()
        {
            using (SqlCommand i=new SqlCommand("UPDATE Satislar SET Durum=3 WHERE Durum=1",Baglanti.bag()))
            {
                i.ExecuteNonQuery();
                i.Dispose();
                MessageBox.Show("Siparişler İptal Edilmiştir","Sipariş İptal",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count == 0)
            {

                MessageBox.Show("İptal Edilecek Sipariş Bulunamadı","Sipariş İptal Hata",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                İptal();
                db.Clear();
                Oku();
                label4.Text = "";
                label2.Text = "";
            }
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView2.ClearSelection();
        }

        private void txtAdet_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtIndirim_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
