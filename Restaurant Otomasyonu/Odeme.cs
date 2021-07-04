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
    public partial class HesapOdeme : Form
    {
        
        public HesapOdeme()
        {
            
            InitializeComponent();
            groupBox1.Visible = false;
        }
      
        public DateTime? tarihsaat(int say) // Müşterinin Ne Kadar Oturdugunu Bilgisini Almak İçin Kulllanılan Metod
        {
            Araclar a = new Araclar();
            DateTime? tarih = null;

            using (SqlCommand trh = new SqlCommand("SELECT max(Tarih) as sonTarih FROM MasaSiparisleri WHERE MasaID=@t AND Durum=1 OR Durum=3", Baglanti.bag()))
            {
                trh.Parameters.AddWithValue("@t", Araclar.MasaId);

                var x = trh.ExecuteScalar();
                if (x != DBNull.Value) tarih = (DateTime?)x;

                trh.Connection.Close();
            }

            return tarih;
        }

        private void zaman() // Tarih Saat Metodun Kullanımı
        {
            
            var mno = Araclar.MasaId;
            var acilis = tarihsaat(mno);
            if (acilis.HasValue)
            {
                var fark = DateTime.Now - acilis.Value;
                label15.Text = string.Format("{0} Gün, {1} Saat, {2} Dakika",
                (int)fark.TotalDays, fark.Hours, fark.Minutes, mno);
            }
        }

        byte drm;
        private void durum()
        {
            
            using ( SqlCommand d=new SqlCommand("SELECT Durum FROM MasaSiparisleri Durum WHERE MasaID=@m ",Baglanti.bag()))
            {
                d.Parameters.AddWithValue("@m", Araclar.MasaId);
                SqlDataReader oku = d.ExecuteReader();

                while (oku.Read())
                {
                    drm = Convert.ToByte(oku["Durum"]);

                }
                d.Dispose();
                oku.Dispose();
            }
        }


        private void grid()
        {

            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[0].Width = 200;

            dataGridView1.Columns[0].HeaderText = "Ürün Adı";
            dataGridView1.Columns[1].HeaderText = "Fiyatı";
            dataGridView1.Columns[2].HeaderText = "Adeti";
            dataGridView1.Columns[3].HeaderText = "Tutarı";
            dataGridView1.Columns[4].HeaderText = "Ürün No";
        }


        int Topla = 0;

        private void hesapoku()// dataGridView Tabloların Adını Değiştirme  ve dataGridView deki Ürünlerin Fiyatlarının Toplamını Alma
        {

            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                Topla += Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
            }
            label8.Text = Topla.ToString("N2");

           
        }

        private void Miktar() //iskonto Burdan ekleniyor........
        {
            try
            {
                using (SqlCommand m = new SqlCommand("INSERT INTO İndirimler(Masaindirim) VALUES (@t) ", Baglanti.bag()))
                {
                    m.Parameters.AddWithValue("@t", Convert.ToDecimal(txtIndirim.Text));
                    m.ExecuteNonQuery();
                    m.Dispose();
                }
            }
            catch (Exception) {; }
        }

        private void hesapyaz() // Ödemenin Hangi Şekilde Yapılacagını Hazırlayan Metot Sodexo
        {
            string odeme=null;
            if (rdbtn1.Checked == false && rdbtn2.Checked == false && rdbtn3.Checked == false && rdbtn4.Checked==false)
            {
                MessageBox.Show("Lütfen Ödeme Türünü Girniz ","Ödeme Türü",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                
                if (rdbtn1.Checked == true)
                {
                    odeme = "Nakit";
                }
                else if (rdbtn2.Checked == true) { odeme = "Kredi Kartı"; }

                else if (rdbtn3.Checked == true) { odeme = "Sodexo"; }

                else odeme = "İkram";
            }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                try
                {
                    SqlCommand hesap = new SqlCommand("INSERT INTO Odemeler (UrunID,UrunAdi,Fiyati,Adet,Tutarı,OdemeTuru) values (@UrunID,@UrunAdi,@Fiyati,@Adet,@Tutarı,@OdemeTuru)", Baglanti.bag());
                    hesap.Parameters.AddWithValue("@UrunID", dataGridView1.Rows[i].Cells[4].Value);
                    hesap.Parameters.AddWithValue("@UrunAdi", dataGridView1.Rows[i].Cells[0].Value);
                    hesap.Parameters.AddWithValue("@Fiyati", dataGridView1.Rows[i].Cells[1].Value);
                    hesap.Parameters.AddWithValue("@Adet", dataGridView1.Rows[i].Cells[2].Value);
                    hesap.Parameters.AddWithValue("@Tutarı", dataGridView1.Rows[i].Cells[3].Value);
                    hesap.Parameters.AddWithValue("@OdemeTuru", odeme);
                    hesap.ExecuteNonQuery();
                    hesap.Dispose();

                }

                catch (Exception hata)
                {
                    string str = hata.Message;
                    //MessageBox.Show(hata.ToString());
                }
            }
        }

        DataTable data = new DataTable();// Siparişi Verilen Tabloların Bilgisi DataTable Tutuluyor

        public void oku() // Siparişi Verilen Ürünleri dataGridView Gösterme
        {
            
            if (Araclar.MasaId != default(int))
                using (SqlDataAdapter oku = new SqlDataAdapter("SELECT UrunAdi,Fiyati,Adeti,Tutari,UrunID FROM MasaSiparisleri WHERE MasaID=@p AND Durum=1", Baglanti.bag()))
                {
                    oku.SelectCommand.Parameters.AddWithValue("@p", Araclar.MasaId);
                    oku.Fill(data);
                    dataGridView1.DataSource = data;
                    
                    label17.Text = (String.Format("{0}:{1}", "Masa No", Araclar.MasaId));
                    oku.Dispose();
                    
                }
        }
        DataTable db = new DataTable();
        public void Roku() // Siparişi Verilen Ürünleri dataGridView Gösterme
        {
            
            if (Araclar.MasaId != default(int))
                using (SqlDataAdapter oku = new SqlDataAdapter("SELECT UrunAdi,Fiyati,Adeti,Tutari,UrunID FROM MasaSiparisleri WHERE MasaID=@p AND Durum=3 ", Baglanti.bag()))
                {
                    oku.SelectCommand.Parameters.AddWithValue("@p", Araclar.MasaId);
                    oku.Fill(db);
                    dataGridView1.DataSource = db;

                    label17.Text = (String.Format("{0}:{1}", "Masa No", Araclar.MasaId));
                    oku.Dispose();

                }

        }
        int RNo;
        private void ROKU() //Rezervasyonlardan RezervasyonID Okumak İçin Kullanılır 
        {
            
            using (SqlCommand rn = new SqlCommand("SELECT RezervasyonID FROM  Masalar WHERE MasaID=@m AND Durumu=3 ", Baglanti.bag()))
            {
                rn.Parameters.AddWithValue("@m", Araclar.MasaId);
                SqlDataReader oku = rn.ExecuteReader();

                while ( oku.Read())
                {
                    RNo = Convert.ToInt32(oku["RezervasyonID"]);
                }
            }
        }

        public void masakapat() //Masa Kapatmak İçin Masanın Durumunu Değiştirmek İçin Kullanılır
        {
         
            try
            {
                using (SqlCommand kapat = new SqlCommand("UPDATE Masalar SET Durumu=0 WHERE MasaID=@p", Baglanti.bag()))
               
                {
                    kapat.Parameters.AddWithValue("@p", Araclar.MasaId);
                    kapat.ExecuteNonQuery();                 
                    dataGridView1.DataSource = null;
                    kapat.Dispose();
                    
                }
                 using (SqlCommand kpt = new SqlCommand("UPDATE MasaSiparisleri SET Tarih=@t,Durum=0 WHERE MasaID=@p ", Baglanti.bag()))
                 {
                     kpt.Parameters.AddWithValue("@p", Araclar.MasaId);
                     kpt.Parameters.AddWithValue("@t",DateTime.Now.Date.AddHours(00).AddMinutes(00).AddSeconds(00).AddMilliseconds(000));
                     kpt.ExecuteNonQuery();
                     kpt.Dispose();
                 }

                 using (SqlCommand rn = new SqlCommand("UPDATE Rezervasyon SET Durum=0 WHERE RezervasyonID=@r ", Baglanti.bag()))
                 {
                     rn.Parameters.AddWithValue("@r", RNo);
                     rn.ExecuteNonQuery();
                     rn.Dispose();
                 }

                using (SqlCommand ri = new SqlCommand("UPDATE Masalar SET RezervasyonID=0 WHERE MasaID=@r ", Baglanti.bag()))
                {
                    ri.Parameters.AddWithValue("@r", Araclar.MasaId);
                    ri.ExecuteNonQuery();
                    ri.Dispose();
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.ToString());
            }

        }

        private void MasalarDurumu() //Masalardan Rezervasyon No Sıfır Yapmak İçin Kullanılan Metot
        {
            Araclar a = new Araclar();
            using (SqlCommand rn = new SqlCommand("UPDATE Masalar SET RezervasyonID=0 WHERE MasaID=@m AND RezervasyonID=@r ", Baglanti.bag()))
            {
                rn.Parameters.AddWithValue("@m", Araclar.MasaId);
                rn.Parameters.AddWithValue("@r", RNo);
                rn.ExecuteNonQuery();
                rn.Dispose();
            }
        }

        private void Odeme_Load(object sender, EventArgs e) //Hesap Kapat Butonun Pasif Edilmiştir indirim Textbox 4 Rakamı Girilmesine İzin Verilmiştir
        {
            btnHesapKapat.Enabled = false;
            txtIndirim.MaxLength = 4;
            durum();
            ROKU();
        }

        private void btnOzet_Click(object sender, EventArgs e) //oku () ve hesapoku() zaman() Metodlarının Kullanıldıgı Kısım dataGridView de Hesap Özetini Görebiliriz
        {
            if (drm == 1)
            {
                oku();
                hesapoku();
                zaman();
                grid();
            }
            else if (drm == 3)
            {
               
                zaman();
                Roku();
                hesapoku();
                grid();

            }

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Sipariş Bulunamadı","Hata",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            btnHesapKapat.Enabled = true;
            btnOzet.Enabled = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e) //dataGridView Seçilen Satırlardaki Kolonlarda Fiyatların Toplamlarını Almak İçin Kullanılır
        {
            try
            {
                float toplam = 0f;

                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    toplam += Convert.ToSingle(row.Cells[3].Value);

                label16.Text = toplam.ToString("N2");
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.ToString());
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) // dataGridView Seçili Gelmesini Engelliyoruz
        {
            dataGridView1.ClearSelection();
        }

        decimal toplam = 0m;
        decimal sayi = 0m;
        private void txtIndirim_TextChanged(object sender, EventArgs e) //İndirim TextBox daki Yapaıclak İşlemlerin Ypaıldıgı Metod
        {
            try
            {
                sayi = Convert.ToDecimal(txtIndirim.Text);
                label2.Text = sayi.ToString("N2");
                toplam = Topla - sayi;
                label10.Text = toplam.ToString("N2");
               
            }
            catch (Exception)
            {
                ;
                label2.Text = "0";
                label10.Text = "0";
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------
        private void btnHesapKapat_Click(object sender, EventArgs e)// Hesap Kapatma İşlemleri hesapyaz() masakapat() Masa Butonun Rengini Değiştirme
        {
            if (dataGridView1.Rows.Count == 0) MessageBox.Show("Hesap Kapatılacak Sipariş Bulunamadı", "Sipariş İşlemi Hatalı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                Miktar();
                hesapyaz();
                masakapat();
                MasalarDurumu();
                data.Clear();
                db.Clear();
                Masalar frmMasa = (Masalar)Application.OpenForms["Masalar"];
                if (frmMasa != null)
                    frmMasa.MasaOlustur();

                using (Islem i = new Islem())
                {
                    i.IslemAdi = "Hesap Kapatıldı.";
                    i.Tarih = DateTime.Now;
                    i.Kaydet();
                }

                MessageBox.Show("Hesap Ödemesi Tamamlandı", "Ödeme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            label8.Text = "0";
            label15.Text = "";
            label2.Text="0";
            label10.Text="0";
            label16.Text = "0,00";
        }

        private void HesapOdeme_FormClosing(object sender, FormClosingEventArgs e)// Form Kapatılırken Aktif Formu Kapatıp Ürünler Formunu Açar
        {
            Ürünler ü = new Ürünler();
            ü.Show();
            this.Dispose();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) //İndirimde İşlemi Yapmak İçin Gizli Olan checkBox1 True Yada False Yapıldıgı Kısım
        {
            if (checkBox1.Checked == true)
            {
                groupBox1.Visible = true;
            }
            else groupBox1.Visible = false; txtIndirim.Text = "";         
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Masalar m = new Masalar();
            m.Show();
            this.Dispose();
        }

        private void txtIndirim_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
