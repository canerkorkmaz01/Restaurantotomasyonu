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
using System.Diagnostics;

namespace Restaurant
{
    public partial class Ürünler : Form
    {

        public Ürünler()
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

        byte OdemeDurumu;
        private void OdemeKontrol() // Ödeme Butonu Eğer 1 veya 3 İse Ödeme Butonun Aktif Ediyor 
        {
            
            using (SqlCommand odeme=new SqlCommand("SELECT Durum FROM MasaSiparisleri WHERE MasaID=@ıd AND Durum=1 OR Durum=3",Baglanti.bag()))
            {
                odeme.Parameters.AddWithValue("@ıd", Araclar.MasaId);
                SqlDataReader oku = odeme.ExecuteReader();

                while(oku.Read())
                {
                    OdemeDurumu =(byte)oku["Durum"];
                }
            }
        }

       byte rmo;
        private void RezervasyonOku() //Masanın Rezerveli Olup Olmadıgı Bilgisini Veren Metot
        {

            using(SqlCommand rezervasyon=new SqlCommand("SELECT Durumu FROM Masalar WHERE MasaID=@m ",Baglanti.bag()))
            {
                rezervasyon.Parameters.AddWithValue("@m", Araclar.MasaId);
                SqlDataReader oku = rezervasyon.ExecuteReader();
                 
                while (oku.Read())
                {
                    rmo = Convert.ToByte(oku["Durumu"]);
                }

            }
        }

        int RNO;
        private void RNOku() //RezervasyonID Bilgisini Okumak İçin Kullanılır Bilgiler() Metoduna Gönderilir
        {
            
            using (SqlCommand rm = new SqlCommand("SELECT  RezervasyonID FROM  Masalar  WHERE MasaID=@m ", Baglanti.bag()))
            {
                rm.Parameters.AddWithValue("@m", Araclar.MasaId);
                SqlDataReader oku = rm.ExecuteReader();
                while (oku.Read())
                {
                    //    RNO = Convert.ToInt32(oku["RezervasyonID"]);
                    if ( rmo==2)
                    {
                        RNO = Convert.ToInt32(oku["RezervasyonID"]);

                    }

                    else if(rmo==3)
                    {
                        RNO = Convert.ToInt32(oku["RezervasyonID"]);
                    }
                }
                rm.Dispose();
                oku.Dispose();
            }
        }

        private void Bilgiler()// Rezervasyon Yapılan Kişilerin Bilgilerini Ürünlerde görmek İçin
        {
            using (SqlCommand b = new SqlCommand("SELECT MusteriAdi,MusteriSoyadi,Tarih FROM Rezervasyon WHERE  RezervasyonID=@r AND Durum=1", Baglanti.bag()))
            {
                b.Parameters.AddWithValue("@r", RNO);
                
                SqlDataReader oku = b.ExecuteReader();

                while (oku.Read())
                {
                    label6.Text = oku["MusteriAdi"].ToString();
                    label7.Text = oku["MusteriSoyadi"].ToString();
                    maskedTextBox1.Text = oku["Tarih"].ToString();
                    
                }
            }
        }
        private void Siparisİptal()//Siparişlerin İptal Edildi Metot
        {
            
            using (SqlCommand siptal = new SqlCommand("UPDATE MasaSiparisleri SET Durum=4 WHERE MasaID=@s", Baglanti.bag()))
             {
                 siptal.Parameters.AddWithValue("@s", Araclar.MasaId);
                 siptal.ExecuteNonQuery();
                 siptal.Dispose();
             }

             using(SqlCommand d=new SqlCommand("UPDATE Masalar SET Durumu=0,rezervasyonID=0 WHERE  MasaID=@2 ",Baglanti.bag()))
             {

                 d.Parameters.AddWithValue("@2", Araclar.MasaId);
                 d.ExecuteNonQuery();
                 d.Dispose();
             }

            using (SqlCommand i = new SqlCommand("UPDATE Rezervasyon SET Durum=2 WHERE  RezervasyonID=@2 ",Baglanti.bag()))
            {
                i.Parameters.AddWithValue("@2", RNO);
                i.ExecuteNonQuery();
                i.Dispose();
            }

            //using (SqlCommand ip = new SqlCommand("UPDATE Masalar SET RezervasyonID=0 WHERE MasaID=@1 AND RezervasyonID=@2 ", Baglanti.bag()))
            //{
            //    ip.Parameters.AddWithValue("@1",Araclar.MasaId);
            //    ip.Parameters.AddWithValue("@2", RNO);
            //    ip.ExecuteNonQuery();
            //    ip.Dispose();
            //}
            MessageBox.Show("Siparişler İptal Edildi", "Sipariş İptal", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        int rezer;
        private void Rezerve()// Rezervasyon Bilgisini Okuma Metot
        {
            
            using ( SqlCommand rezerve=new SqlCommand("SELECT Durumu FROM Masalar WHERE MasaID=@r",Baglanti.bag()))
            {
                rezerve.Parameters.AddWithValue("@r", Araclar.MasaId);
                SqlDataReader oku = rezerve.ExecuteReader();
                while(oku.Read())
                {
                    rezer = Convert.ToInt32(oku["Durumu"]);
                }
                
            }
        }

        private void MasaAc()//Masa Açma İşlemleri Yapmak İçin Kullanılan Metot
        {
           
                using (SqlCommand cmd = new SqlCommand("UPDATE Masalar SET Durumu=1 WHERE MasaID=@ıd", Baglanti.bag()))
                
                {
                    cmd.Parameters.AddWithValue("@ıd", Araclar.MasaId);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }

        }

        int RNo;
        private void RNoOku() //Rezervasyon No Oku 
        {
           
            using (SqlCommand rm = new SqlCommand("SELECT RezervasyonID FROM  Masalar WHERE MasaID=@m AND Durumu=2 OR Durumu=3", Baglanti.bag()))
            {
                rm.Parameters.AddWithValue("@m", Araclar.MasaId);
                SqlDataReader oku = rm.ExecuteReader();
                while (oku.Read())
                {
                    RNo = Convert.ToInt32(oku["RezervasyonID"]);
                }
                rm.Dispose();
                oku.Dispose();
            }
        }


        private void RezerveliMasaAc()//Rezerveli Masalar için
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE Masalar SET Durumu=@Acık WHERE  MasaID=@ıd", Baglanti.bag()))
           
            {
                cmd.Parameters.AddWithValue("@Acık", 3);
                cmd.Parameters.AddWithValue("@ıd", Araclar.MasaId);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                
            }

            using (SqlCommand com = new SqlCommand("UPDATE MasaSiparisleri SET Tarih=@t, Durum=@Acık WHERE MasaID=@ıd ", Baglanti.bag()))
             {
                 com.Parameters.AddWithValue("@Acık", 3);
                 com.Parameters.AddWithValue("@ıd", Araclar.MasaId);
                com.Parameters.AddWithValue("@t",DateTime.Now);
                 com.ExecuteNonQuery();
                 com.Dispose();
             }

        }
       

        private void RezerveliMasa() //Masanın Durumunu Açık Rezerve Yapmak İçin Kulalnılan Metot
        {
           
            using (SqlCommand cmd = new SqlCommand("UPDATE Masalar SET Durumu=@Acık WHERE MasaID=@ıd", Baglanti.bag()))
            {
                cmd.Parameters.AddWithValue("@ıd", Araclar.MasaId);
                cmd.Parameters.AddWithValue("@Acık", 3);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

            using (SqlCommand com = new SqlCommand("UPDATE MasaSiparisleri SET  Durum=3,RezervasyonID=@r WHERE  MasaID=@ıd AND Durum=3 ", Baglanti.bag()))
            {
                com.Parameters.AddWithValue("@ıd", Araclar.MasaId);
                com.Parameters.AddWithValue("@r", RNo);
                com.ExecuteNonQuery();
                com.Dispose();
            }
        }


        private void grid()// dataGridView2 Kolonların Adını Ve Boyutunu Değiştirmek İçin Kullanılır
        {
            this.dataGridView2.Columns[1].Width = 150;
            this.dataGridView2.Columns[0].Visible = false;
            this.dataGridView2.Columns[1].HeaderText = "Ürün Adı";
            this.dataGridView2.Columns[2].HeaderText = "Fiyatı";
            this.dataGridView2.Columns[3].HeaderText = "Adeti";
            this.dataGridView2.Columns[4].HeaderText = "Tutarı";

        }



        DataTable data = new DataTable(); //dataGridView2 deki Bilgilerin Okundugu Datatabale
        public void Oku() // Veritabanındaki Kaydedilen Ürünleri dataGridView2 Okumak İçin Kullanılan Metot
        {

            using (SqlDataAdapter oku = new SqlDataAdapter("SELECT SiparisID, UrunAdi,Fiyati,Adeti,Tutari FROM MasaSiparisleri WHERE MasaID=@p AND Durum=1", Baglanti.bag()))
            {
                data.Clear();
                oku.SelectCommand.Parameters.AddWithValue("@p", Araclar.MasaId);
                oku.Fill(data);
                dataGridView2.DataSource = data;
                grid();
                oku.Dispose();

            }
           

        }
        DataTable veri = new DataTable();
        public void RezerveOku() // Veritabanındaki Kaydedilen Ürünleri dataGridView2 Okumak İçin Kullanılan Metot
        {
            using (SqlDataAdapter oku = new SqlDataAdapter("SELECT SiparisID,UrunAdi,Fiyati,Adeti,Tutari FROM MasaSiparisleri WHERE MasaID=@p AND Durum=3", Baglanti.bag()))
            {
                veri.Clear();
                oku.SelectCommand.Parameters.AddWithValue("@p", Araclar.MasaId);
                oku.Fill(veri);
                dataGridView2.DataSource = veri;
                grid();
                oku.Dispose();
            }
        }

        int mdb;
        private void MasaDurumBilgisiOku()
        {
          
            using (SqlCommand m=new SqlCommand("SELECT * FROM Masalar Durumu WHERE MasaID=@ıd ",Baglanti.bag()))
            {
                m.Parameters.AddWithValue("@ıd", Araclar.MasaId);

                SqlDataReader oku = m.ExecuteReader();

                while(oku.Read())
                {
                    if (oku.GetByte(3) == 2)
                    {
                        mdb = 3;
                    }

                    else if (oku.GetByte(3) == 3)
                    {
                        mdb = 3;
                    }
                    else mdb = 1;
                }
                m.Dispose();
                oku.Close();
            }
        }



        int UrunID;
        public void SiparislereAktar() //dataGridView1 Seçilen Ürünleri Veritabanına Aktarmak İçin Kullanılan Metot
        {
          
            try
            {
                using (SqlCommand ekle = new SqlCommand("INSERT INTO MasaSiparisleri (UrunAdi,Fiyati,Adeti,Tutari,Durum,IslemId,MasaID,UrunID,Tarih)values(@urun,@fiyat,@adt,@tut,@no,@p,@m,@u,@t) ", Baglanti.bag()))
                {

                    int adet = Convert.ToInt32(txtAdet.Text);
                    decimal fiyat = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[1].Value);

                    ekle.Parameters.AddWithValue("@urun", dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    ekle.Parameters.AddWithValue("@fiyat", fiyat);
                    ekle.Parameters.AddWithValue("@no",mdb); //ilk Eklenen Durum Bilgisi
                    ekle.Parameters.AddWithValue("@adt", adet);
                    ekle.Parameters.AddWithValue("@tut", adet * fiyat);
                    ekle.Parameters.AddWithValue("@p",Islem.PersonelID);
                    ekle.Parameters.AddWithValue("@m", Araclar.MasaId);
                    ekle.Parameters.AddWithValue("@u", UrunID);
                    ekle.Parameters.AddWithValue("@t",DateTime.Now);
                    ekle.ExecuteNonQuery();
                    ekle.Dispose();

                    
                }

            }
            catch (Exception )
            {
                //MessageBox.Show(hata.ToString());
                MessageBox.Show("Lütfen Adet Miktarı Girin", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void DurumBilgisi() // MasaSiparislerinden Durumu 1 ve 3 Olan Siparisleri dataGridView2 de Göster
        {
            
            using (SqlCommand durum = new SqlCommand("SELECT * FROM MasaSiparisleri Durum WHERE MasaID=@m ", Baglanti.bag()))
            {
               
                durum.Parameters.AddWithValue("@m", Araclar.MasaId);
                SqlDataReader oku = durum.ExecuteReader();

                while (oku.Read())
                {
                    if (oku.GetByte(9) == 1)
                    {
                        Oku();
                        
                    }
                    else if ( oku.GetByte(9) == 3)
                    {
                        RezerveOku();
                    }
                }
                durum.Dispose();
                oku.Close();
            }
        }

        private void Ürünler_Load(object sender, EventArgs e) //İşlemlerin Eventları ve Masa Formu Açılırken Çalıaşcak Metotlerı İçerir
        {
           txtAdet.MaxLength = 10;
            data.Clear();
            veri.Clear();
            DurumBilgisi();
            RezervasyonOku();
            label6.Visible = false;
            label7.Visible = false;
            RNOku();
            Bilgiler();
            OdemeKontrol();
            MasaDurumBilgisiOku();
            label1.Text = ("Masa No:" + Araclar.MasaId.ToString());

            if (rmo == 2)
            {
                label6.Visible = true;
                label7.Visible = true;
                label2.Text = "Rezerveli Masa";
                label3.Text = "Müşteri Adı";
                label4.Text = "Müşteri Soyadı";
                label5.Text = "R Tarih";
                maskedTextBox1.Visible = Enabled;
            }
            else if (rmo == 3)
            {
                label6.Visible = true;
                label7.Visible = true;
                label2.Text = "Açık Rezerveli Masa";
                label3.Text = "Müşteri Adı";
                label4.Text = "Müşteri Soyadı";
                label5.Text = "R Tarih";
                maskedTextBox1.Visible = Enabled;
            }

            Rezerve();

            if (OdemeDurumu == 1 || OdemeDurumu == 3)
            {
                btnOdeme.Enabled = true;
            }
            else btnOdeme.Enabled = false;

            txtAciklama.MaxLength = 300;
        }

        private void btnAnaYemek_Click(object sender, EventArgs e) //Ana Yemekleri Çagıran Metot
        {
            dt.Clear();
            Urunler(1);
        }

        private void btnİçecek_Click(object sender, EventArgs e) // İçecekleri Çagıran Metot
        {
            dt.Clear();
            Urunler(2);
        }

        private void btnTatlılar_Click(object sender, EventArgs e) // Tatlıları Çagıran Metot
        {
            dt.Clear();
            Urunler(3);
        }
        
        private void btnSalata_Click(object sender, EventArgs e) //Salataları Çagıran Metot 
        {
            dt.Clear();
            Urunler(4);
        }

        private void btnFastFood_Click(object sender, EventArgs e) //FastFood Çagıran Metot
        {
            dt.Clear();
            Urunler(5);
        }

        private void btnCorba_Click(object sender, EventArgs e) // Çorbaları Çagıran Metot
        {
            dt.Clear();
            Urunler(6);
        }

        private void btnMakarna_Click(object sender, EventArgs e) // Makarnaları Çağıran Metot
        {
            dt.Clear();
            Urunler(7);
        }

        private void btnAraSıcak_Click(object sender, EventArgs e) //AraSıcakları ÇAgıran Metot
        {
            dt.Clear();
            Urunler(8);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) //dataGridView1 Açılışta Seçili Gelmesini Engelleyen Events
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) //dataGridView2 Açılışta Seçili Gelmesini Engelleyen Events
        {
            dataGridView2.ClearSelection();
        }

       

        private void Aciklama()
        {
            using (SqlCommand a = new SqlCommand("UPDATE MasaSiparisleri SET Aciklama=@Ac WHERE MasaID=@id AND Durum=1 OR Durum=3 ", Baglanti.bag()))
            {
                a.Parameters.AddWithValue("@id", Araclar.MasaId);
                a.Parameters.AddWithValue("@Ac", txtAciklama.Text);
                a.ExecuteNonQuery();
                MessageBox.Show("Açıklama eklenmişitr", "Açıklama Ekle");
                a.Dispose();
            }
        }


        Masalar ms = new Masalar();

        private void btnSiparis_Click(object sender, EventArgs e) //Siparislerin Verildiği Buton 
        {


            if (dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("Lütfen Sipraiş Ekleyin");
            }
            else if (rezer == 2)
            {
                RNoOku();
                RezerveliMasa();
                MessageBox.Show("Siparişler Verildi", "Masa Sipariş", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnOdeme.Enabled = true;
                Masalar frmMasa = (Masalar)Application.OpenForms["Masalar"];
                if (frmMasa != null)
                frmMasa.MasaOlustur();
            }
            else if (rezer == 3)
            {
                RNoOku();
                RezerveliMasa();
                MessageBox.Show("Siparişler Verildi", "Masa Sipariş", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnOdeme.Enabled = true;
                veri.Clear();
                DurumBilgisi();
                Masalar frmMasa = (Masalar)Application.OpenForms["Masalar"];
                if (frmMasa != null)
                    frmMasa.MasaOlustur();
            }

            else
            {
                MasaAc();
                MessageBox.Show("Siparişler Verildi", "Masa Sipariş", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Masalar frmMasa = (Masalar)Application.OpenForms["Masalar"];
                if (frmMasa != null)
                frmMasa.MasaOlustur();
            }

        }

        //HesapOdeme odeme = null;
        private void btnOdeme_Click(object sender, EventArgs e)// Ödeme Butonun Click Olayı
        {
            //odeme = new HesapOdeme();
            //if (odeme == null || odeme.Disposing || odeme.IsDisposed)
            //{
            HesapOdeme o = new HesapOdeme();
            o.Show();
            this.Dispose();
            //}
                
            //else
            //{ odeme.Activate(); }
            
        }

        private void btnİptal_Click(object sender, EventArgs e)//Siparislerini İptal İşlemini Gerçekleştiren Buton click Olayı
        {
            if (dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("İptal Edilecek Sipariş Bulunamadı", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Siparisİptal();
                btnOdeme.Enabled = false;
                data.Clear();
                Ürünler_Load(sender, e);
                Masalar frmMasa = (Masalar)Application.OpenForms["Masalar"];
                if (frmMasa != null)
                frmMasa.MasaOlustur();
            }
        }

        private void btnGeri_Click(object sender, EventArgs e) //Bir Önceki Forma Dönmek İçin Kullanılır 
        {
            Masalar m = new Masalar();
            m.Show();
            this.Dispose();
        }

        private void Ürünler_FormClosing(object sender, FormClosingEventArgs e) // Form Kapanırken Yapıalcak İşlemler 
        {
            Masalar m = new Masalar();
            m.Show();
            this.Dispose();
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)//dataGridView1 Click Olayına Tıklandıgında Yapıalcak İşlemler
        {
            UrunID = (int)dataGridView1.CurrentRow.Cells[2].Value;
            SiparislereAktar();
            data.Clear();
            veri.Clear();
            DurumBilgisi();
            RezervasyonOku();
        }

       
        private void GridSil(int index)
        {
            
            try
            {
                using (SqlCommand urun = new SqlCommand("UPDATE MasaSiparisleri SET Durum=4,RezervasyonID=0 WHERE SiparisID=@id", Baglanti.bag()))
                {
                    int value = 0;
                    int.TryParse(dataGridView2.Rows[index].Cells[0].Value.ToString(),out value);    
                    urun.Parameters.AddWithValue("@id",value); //Convert.ToInt32(dataGridView2.Rows[index].Cells[0].Value)
                    urun.ExecuteNonQuery();
                    urun.Parameters.Clear();
                    data.Clear();
                    DurumBilgisi();
                    urun.Dispose();
                }
            }
            catch (Exception) { ; }
        }
        private void btnClear_Click(object sender, EventArgs e)// Adetin Girildiği TextBox Temizlemek İçin Kullanılan Buton
        {
            txtAdet.Text = "";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
            data.Clear();
            veri.Clear();
            int index = e.RowIndex;
            veri.Clear();
            DurumBilgisi();
            GridSil(index);

            Ürünler_Load(sender, e);
            
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                txtAdet.Text = txtAdet.Text.Substring(1, txtAdet.Text.Length - 1);
            }
            catch(Exception) {; }
        }

        private void btnAciklama_Click(object sender, EventArgs e)
        {
            Aciklama();
        }

        
    }
}

     
