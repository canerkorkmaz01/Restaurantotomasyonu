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
    public partial class PaketServis : Form
    {
        public PaketServis()
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
        DataTable data = new DataTable();

        private void MusteriOku()
        {
            try
            {
                using (SqlDataAdapter m = new SqlDataAdapter("SELECT * FROM MusteriBilgileri WHERE Durum=1", Baglanti.bag()))
                {

                    m.Fill(data);
                    dataGridView2.DataSource = data;
                }
            }
            catch (Exception hata) { MessageBox.Show(hata.ToString()); }
        }
        Müsteri mu = new Müsteri();
        int UrunID;
        DataTable db = new DataTable();
        private void UrunOku()
        {
            try
            {
                using (SqlDataAdapter m = new SqlDataAdapter("SELECT PaketID,UrunAdi,Fiyati,Adeti,Tutari FROM PaketServis WHERE MusteriID=@id AND Durum=1 ", Baglanti.bag()))
                {
                    m.SelectCommand.Parameters.AddWithValue("@id", UrunID);
                    m.Fill(db);
                    dataGridView3.DataSource = db;
                    m.Dispose();
                }
            }
            catch (Exception hata) { MessageBox.Show(hata.ToString()); }
        }

        private void grid3()
        {
            dataGridView3.Columns[0].Visible = false;
            dataGridView3.Columns[1].HeaderText = "Ürün Adı";
            dataGridView3.Columns[1].Width = 160;
            dataGridView3.Columns[2].HeaderText = "Fiyatı";
            dataGridView3.Columns[2].Width = 120;
            dataGridView3.Columns[3].HeaderText = "Adeti";
            dataGridView3.Columns[3].Width = 70;
            dataGridView3.Columns[4].HeaderText = "Tutarı";
            dataGridView3.Columns[4].Width = 120;
        }



        public void UrunEkle() //dataGridView1 Seçilen Ürünleri Veritabanına Aktarmak İçin Kullanılan Metot
        {
            try
            {
                using (SqlCommand ekle = new SqlCommand("INSERT INTO PaketServis (UrunAdi,MusteriID,Fiyati,Adeti,Tutari,Durum,Tarih)values(@u,@id,@f,@a,@t,@d,@ta)", Baglanti.bag()))
                {

                    int adet = Convert.ToInt32(txtAdet.Text);
                    decimal fiyat = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[1].Value);

                    ekle.Parameters.AddWithValue("@u", dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    ekle.Parameters.AddWithValue("@id", UrunID);
                    ekle.Parameters.AddWithValue("@f", fiyat);
                    ekle.Parameters.AddWithValue("@a", adet);
                    ekle.Parameters.AddWithValue("@t", adet * fiyat);
                    ekle.Parameters.AddWithValue("@d", "1");
                    ekle.Parameters.AddWithValue("@ta",DateTime.Now.Date.AddHours(00).AddMinutes(00).AddSeconds(00).AddMilliseconds(000));
                    ekle.ExecuteNonQuery();
                    ekle.Dispose();
                }

            }
            catch (Exception )
            {

                MessageBox.Show("Lütfen Adet Miktarı Girin", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void GridSil(int index)// PaketServis Durumunu 2 Yapıyor
        {
            try
            {
                using (SqlCommand urun = new SqlCommand("UPDATE PaketServis SET Durum=2  WHERE PaketID=@id", Baglanti.bag()))
                {
                    int value = 0;
                    int.TryParse(dataGridView3.Rows[index].Cells[0].Value.ToString(), out value);
                    urun.Parameters.AddWithValue("@id", value); //Convert.ToInt32(dataGridView2.Rows[index].Cells[0].Value)
                    urun.ExecuteNonQuery();
                    urun.Parameters.Clear();
                    urun.Dispose();
                }
            }
            catch (Exception) {; }
        }

        private void GridDel(int index)// Müsteri Bilgilerinin Durumunu Degiştiriyor 2 Yapıyor
        {
            try
            {
                using (SqlCommand urun = new SqlCommand("UPDATE MusteriBilgileri SET Durum=2  WHERE MusteriID=@id", Baglanti.bag()))
                {
                    int value = 0;
                    int.TryParse(dataGridView3.Rows[index].Cells[0].Value.ToString(), out value);
                    urun.Parameters.AddWithValue("@id", value); //Convert.ToInt32(dataGridView2.Rows[index].Cells[0].Value)

                    urun.ExecuteNonQuery();
                    urun.Parameters.Clear();
                    urun.Dispose();
                }
            }
            catch (Exception) {; }
        }

        public void gridview()
        {
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[1].HeaderText = " Adı";
            dataGridView2.Columns[2].HeaderText = "Soyadı";
            dataGridView2.Columns[3].HeaderText = "Telefonu";
            dataGridView2.Columns[4].HeaderText = "Tarih";
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[7].Visible = false;
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

        private void Uzunluk()
        {
            txtAd.MaxLength = 15;
            txtSoyad.MaxLength = 15;
            txtTel.MaxLength = 10;

        }


        private void PaketServis_Load(object sender, EventArgs e)
        {

            Application.OpenForms.Cast<Form>().Where(frm => frm != this).ToList().ForEach(frm => frm.Hide());


        }

        private void btnMüsteriGetir_Click(object sender, EventArgs e)
        {
            data.Clear();
            MusteriOku();
            gridview();
            //grid2();

        }

        private void btnAnaYemek_Click(object sender, EventArgs e)
        {
            dt.Clear();
            Urunler(1);
        }

        private void btnİçecek_Click(object sender, EventArgs e)
        {
            dt.Clear();
            Urunler(2);
        }

        private void btnTatlılar_Click(object sender, EventArgs e)
        {
            dt.Clear();
            Urunler(3);
        }

        private void btnSalata_Click(object sender, EventArgs e)
        {
            dt.Clear();
            Urunler(4);
        }

        private void btnFastFood_Click(object sender, EventArgs e)
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

        private void btnAraSıcak_Click(object sender, EventArgs e)
        {
            dt.Clear();
            Urunler(8);
        }


        int deger;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (deger == 1)
            {
                db.Clear();
                UrunEkle();
                UrunOku();
                grid3();
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {

            txtAdet.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                txtAdet.Text = txtAdet.Text.Substring(0, txtAdet.Text.Length - 1);
            }

            catch (Exception) {; }
        }

        double toplam = 0;
        private void Hesapla()
        {
            try
            {
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    toplam += Convert.ToDouble(dataGridView3.Rows[i].Cells[4].Value);

                }
                fiyati.Text = "";
                fiyati.Text = toplam.ToString("N2") + " " + "TL";
            }
            catch {; }
        }




        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                deger = 1;
                UrunID = (int)dataGridView2.CurrentRow.Cells[0].Value;


                db.Clear();
                UrunOku();
                txtAdres.Text = dataGridView2.CurrentRow.Cells[6].Value.ToString();
                grid3();
                Müsteri.MusteriID = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);
              
                toplam = 0;
                Hesapla();
                Müsteri.MusteriID = (int)dataGridView2.CurrentRow.Cells[0].Value;
            }
            catch (Exception) {; }

        }

        private void İptal()
        {

            using (SqlCommand i = new SqlCommand("UPDATE MusteriBilgileri SET Durum=3 WHERE MusteriID=@id", Baglanti.bag()))
            {

                i.Parameters.AddWithValue("@id", Müsteri.MusteriID);
                i.ExecuteNonQuery();
                i.Dispose();
            }

            using (SqlCommand ip = new SqlCommand("UPDATE PaketServis SET Durum=3 WHERE MusteriID=@id", Baglanti.bag()))
            {

                ip.Parameters.AddWithValue("@id", Müsteri.MusteriID);
                ip.ExecuteNonQuery();
                ip.Dispose();
            }
        }

        private void Siparis()
        {

            using (SqlCommand i = new SqlCommand("UPDATE MusteriBilgileri SET Durum=0 WHERE MusteriID=@id", Baglanti.bag()))
            {

                i.Parameters.AddWithValue("@id", Müsteri.MusteriID);
                i.ExecuteNonQuery();
                i.Dispose();
            }

            using (SqlCommand ip = new SqlCommand("UPDATE PaketServis SET Durum=0 WHERE MusteriID=@id", Baglanti.bag()))
            {

                ip.Parameters.AddWithValue("@id", Müsteri.MusteriID);
                ip.ExecuteNonQuery();
                ip.Dispose();
            }
        }

        private void OdemeTuru()// Veritabanına Ödeme Bilgisi Eklemek İçin Kullanılır
        {
            string odeme = null;


            if (rdbtn1.Checked == true)
            {
                odeme = "Nakit";
            }

            else if (rdbtn2.Checked == true)
            {
                odeme = "Kredi Kartı";
            }

            else if (rdbtn3.Checked == true)
            {
                odeme = "Ticket";
            }

            try
            {
                using (SqlCommand odemeBilgisi = new SqlCommand("UPDATE PaketServis SET OdemeTuru=@od WHERE Durum=1 AND MusteriID=@id", Baglanti.bag()))
                {
                    odemeBilgisi.Parameters.AddWithValue("@id", Müsteri.MusteriID);
                    odemeBilgisi.Parameters.AddWithValue("@od", odeme);
                    odemeBilgisi.ExecuteNonQuery();
                    odemeBilgisi.Dispose();
                }
            }
            catch (Exception) {; }
        }

        private void dataGridView3_DoubleClick(object sender, DataGridViewCellEventArgs e) //grid double click olayı
        {


            db.Clear();
            dt.Clear();
            int index = e.RowIndex;
            dt.Clear();
            UrunOku();
            GridSil(index);
            GridDel(index);

            PaketServis_Load(sender, e);
        }


        MusteriSiparisleri ms = null;
        public void form() // Formun 2 Defa Açılmasını Engellemek İçin Kullanılan Metot
        {
            if (ms == null || ms.Disposing || ms.IsDisposed)
            {
                ms = new MusteriSiparisleri();
                ms.Show();
            }
            else { ms.Activate(); }
        }

        private void btnSiparis_Click(object sender, EventArgs e) //Siparislerin Ödemelerini Tamamlamak İçin
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Müşteri Seçininiz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (rdbtn1.Checked == false && rdbtn2.Checked == false && rdbtn3.Checked == false)
            {
                MessageBox.Show("Lütfen Ödeme Türünü Girniz ", "Ödeme Türü", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (dataGridView3.Rows.Count == 0)
            {
                MessageBox.Show("Müşteriye Ait Ürün Bulunamadı", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                OdemeTuru();
                Siparis();
                data.Clear();
                MusteriOku();
                db.Clear();
                UrunOku();
                txtAdres.Text = "";
            }
        }

        private void PaketServis_FormClosed(object sender, FormClosedEventArgs e)
        {
            Menu m = new Restaurant.Menu();
            m.Show();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Menu m = new Restaurant.Menu();
            m.Show();
            this.Dispose();

        }

        private void Bilgiler() // Seçilen DataGridView İçindeki Bilgileri MüsteriSiparisleri İçine Ekliyor
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Müşteri Seçininiz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {

                try
                {
                    form();
                    ms.txtMadi.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                    ms.txtMSoyadi.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                    ms.mkdTel.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
                    ms.txtMÜnvani.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
                    ms.txtMAdresi.Text = dataGridView2.CurrentRow.Cells[6].Value.ToString();
                }

                catch (Exception) {; }
            }
        }


        private void btnSil_Click(object sender, EventArgs e)
        {
            
            

            try
            {

                Bilgiler();
                ms.txtMadi.Enabled = false;
                ms.txtMSoyadi.Enabled = false;
                ms.txtMAdresi.Enabled = false;
                ms.txtMÜnvani.Enabled = false;
                ms.btnGüncelle.Enabled = false;
                ms.btnKaydet.Enabled = false;
            }
            catch (Exception) {; }
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {


            try
            {

                Bilgiler();
                ms.btnKaydet.Enabled = false;
                ms.btnSil.Enabled = false;
            }
            catch (Exception) {; }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            form();
            try
            {
                ms.btnGüncelle.Enabled = false;
                ms.btnSil.Enabled = false;
            }
            catch (Exception) {; }
        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count == 0)
            {
                MessageBox.Show("İptal Edilecek Müşteri Bulunamadı", "Hata");
            }

            else
            {
                İptal();
                data.Clear();
                MusteriOku();
                gridview();
            }

        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView3_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView3.ClearSelection();
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView2.ClearSelection();
        }

        private void AdArama() // Ad Kısmında Aramak yapmak İçin Kullanılır
        {
            Uzunluk();
            if (txtAd.Text.Trim() == "")
            {
                data.Clear();
                MusteriOku();
                gridview();
            }

            else {

                using (SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM MusteriBilgileri WHERE Durum=1 AND MusteriAdi LIKE '%' +@ad+ '%'", Baglanti.bag()))
                {
                    ad.SelectCommand.Parameters.AddWithValue("@ad", txtAd.Text);

                    DataTable dt = new DataTable();
                    data.Clear();
                    ad.Fill(dt);

                    dataGridView2.DataSource = dt;
                    gridview();
                    ad.Dispose();


                }
            }
        }

        private void SoyadArama() // Soyad Kısmında Aramak yapmak İçin Kullanılır
        {
            Uzunluk();
            if (txtSoyad.Text.Trim() == "")
            {
                data.Clear();
                MusteriOku();
                gridview();
            }

            else {

                using (SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM MusteriBilgileri WHERE Durum=1 AND MusteriSoyadi LIKE '%' +@soyad+ '%'", Baglanti.bag()))
                {
                    ad.SelectCommand.Parameters.AddWithValue("@soyad", txtSoyad.Text);

                    DataTable dt = new DataTable();
                    data.Clear();
                    ad.Fill(dt);

                    dataGridView2.DataSource = dt;
                    gridview();
                    ad.Dispose();


                }
            }
        }


        private void TelefonArama() //Telefon Kısmında Aramak yapmak İçin Kullanılır
        {
            Uzunluk();
            if (txtTel.Text.Trim() == "")
            {
                data.Clear();
                MusteriOku();
                gridview();
            }

            else {

                using (SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM MusteriBilgileri WHERE Durum=1 AND Telefonu LIKE '%' +@tel+ '%'", Baglanti.bag()))
                {
                    ad.SelectCommand.Parameters.AddWithValue("@tel", txtTel.Text);

                    DataTable dt = new DataTable();
                    data.Clear();
                    ad.Fill(dt);

                    dataGridView2.DataSource = dt;
                    gridview();
                    ad.Dispose();


                }
            }
        }

        private void txtAd_TextChanged_1(object sender, EventArgs e)
        {
            AdArama();
        }

        private void txtSoyad_TextChanged(object sender, EventArgs e)
        {
            SoyadArama();
        }

        private void txtTel_TextChanged(object sender, EventArgs e)
        {
            TelefonArama();
        }

        private void Tarih()
        {
            using (SqlDataAdapter arama = new SqlDataAdapter(@"SELECT * FROM MusteriBilgileri WHERE Tarih >= @baslangic AND Tarih < @bitis AND Durum=1", Baglanti.bag()))
            {
                arama.SelectCommand.Parameters.AddWithValue("@baslangic", dtpTarih.Value.Date);
                arama.SelectCommand.Parameters.AddWithValue("@bitis", dtpTarih.Value.Date.AddDays(1));
                DataTable db = new DataTable();
                arama.Fill(db);
                dataGridView2.DataSource = db;
                gridview();
                arama.Dispose();
            }
        }

        private void dtpTarih_ValueChanged(object sender, EventArgs e)
        {

            if (dtpTarih.Value != null)
            {
                Tarih();
            }
            else
            {
                data.Clear();
                MusteriOku();
            }

        }

        private void txtAdres_TextChanged(object sender, EventArgs e)
        {

        }

        private void Yazdır()
        {
            try
            {
                Müsteri.MusteriID = (int)dataGridView2.CurrentRow.Cells[0].Value;

                using (SqlDataAdapter yazdır = new SqlDataAdapter("SELECT MusteriAdi,MusteriSoyadi,Telefonu,Tarih,Adresi FROM MusteriBilgileri WHERE MusteriID=@id AND Durum=1", Baglanti.bag()))
                {
                    yazdır.SelectCommand.Parameters.AddWithValue("@id", Müsteri.MusteriID);
                    DataTable dt = new DataTable();
                    yazdır.Fill(dt);
                    //Rapor r = new Rapor();
                    //r.DataSource = dt;

                    //using (ReportPrintTool printTool = new ReportPrintTool(r))
                    //{
                    //    r.xrLabel11.Text = fiyati.Text;
                    //    r.PageHeight = 329;
                    //    r.PageWidth = 283;
                    //    printTool.ShowPreviewDialog();
                    //    r.DesignerOptions.ShowPrintingWarnings = false;
                    //    r.ShowPrintMarginsWarning = false;
                    //}
                    //yazdır.Dispose();
                }

            }

            catch (Exception)
            {
                MessageBox.Show("Lütfen Yazdırılacak Bir Müşteri Seçiniz", "Müsteri Seçilmedi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnYazdır_Click(object sender, EventArgs e)
        {
            //Yazdır();
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void txtAdet_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            yazdır y = new yazdır();
            y.MüsteriBilgileri();
            y.Nesne = this;

            int sayac = 0;
            int i = 0;

            for (; i < y.Adres.Length; i++)

            {
                if (y.Adres.Substring(i, 1) == " ")
                {
                    sayac++;
                    if (sayac == 3) break;

                }

            }
            StringBuilder sb = new StringBuilder();
            sb.Append(y.Adres.Substring(0, i) + Environment.NewLine + y.Adres.Substring(i).TrimStart());
            e.Graphics.DrawString("Şirioglu Baklava", new Font("Arial", 30, FontStyle.Regular), Brushes.Black, new Point(25, 60));
            e.Graphics.DrawString("Adı :" + y.Adi, new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(25, 120));
            e.Graphics.DrawString("Soyadı :" + y.Soyadi, new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(25, 160));
            e.Graphics.DrawString("Telefon :" + y.Telefon, new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(25, 200));
            e.Graphics.DrawString("Tarih :" + y.Tarih, new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(25, 240));
            e.Graphics.DrawString("Adres :" + sb, new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(25, 280));
            e.Graphics.DrawString("Tutarı :" +fiyati.Text, new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(25, 340));

            // e.Graphics.DrawString(label1.Text, new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(25, 320));
        }
    }
}

  

