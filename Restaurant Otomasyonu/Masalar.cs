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
using System.Collections;
using System.Globalization;

namespace Restaurant
{
    public partial class Masalar : Form
    {
        SiparisOku okunan;
        public Masalar()
        {
            InitializeComponent();
            okunan = new SiparisOku();
            btnTümü.Text = "\nTümü";

         

        }

        private void MasaOku() //Boş Masaların Bilgisinin Okunduğu Metot
        {
            using (SqlCommand masaoku = new SqlCommand("SELECT MasaID FROM Masalar WHERE Durumu=0 ", Baglanti.bag()))
            {
                SqlDataReader oku = masaoku.ExecuteReader();

                while (oku.Read())
                {
                    cboMasa.Items.Add(oku["MasaID"]);
                }

                oku.Dispose();
                masaoku.Dispose();

            }

        }

        private void MasaTasi()//Taşınacak Masaların Bilgisi
        {

            using (SqlCommand masa = new SqlCommand("SELECT MasaID FROM Masalar  WHERE Durumu=1 OR Durumu=2 OR Durumu=3", Baglanti.bag()))
            {
                SqlDataReader oku = masa.ExecuteReader();
                while (oku.Read())
                {
                    cboTasinacak.Items.Add(oku["MasaID"]);
                    cboTasinacak.Sorted = true;
                }
                masa.Dispose();
                oku.Dispose();
                
            }
        }

        public IList<SiparisOku> MasaSorgu(string querystring, int durum) // Açık Masanın SiparisId Bilgisini Okutuyoruz 
        {
            IList<SiparisOku> listele = new List<SiparisOku>();

                using (SqlDataAdapter da = new SqlDataAdapter(new SqlCommand(querystring, Baglanti.bag())))
                {
                    da.SelectCommand.Parameters.AddWithValue("@durum", durum);
                    DataTable tbl = new DataTable();
                    tbl.Locale = CultureInfo.InvariantCulture;
                    da.Fill(tbl);
                    foreach (DataRow item in tbl.Rows)
                    {
                        listele.Add(new SiparisOku
                        {
                            MasaID = item.Field<int>("MasaID"),
                            SiparisID = item.Field<int>("SiparisID")
                        });
                    }
                    da.Dispose();

            }
            return listele;
        }

        private void Masa() // Açık Masalar İçin Masa Taşıma İşlemi  Class Gelecek // Class İçinde Uygulanıyor
        {

            IList<SiparisOku> listeler = MasaSorgu(@"Select * From MasaSiparisleri Where Durum = @durum", 1);
            foreach (var item in listeler)
            {
                using (SqlCommand m = new SqlCommand("UPDATE MasaSiparisleri SET MasaID=@ıd WHERE MasaID=@m AND SiparisID=@s AND Durum=1", Baglanti.bag()))
                {
                    int cbo = (int)cboMasa.SelectedItem;
                    int cb = (int)cboTasinacak.SelectedItem;
                    m.Parameters.AddWithValue("@s", item.SiparisID);
                    m.Parameters.AddWithValue("@ıd", cbo); //MasaSiparişleri Durumunu Güncelliyor 
                    m.Parameters.AddWithValue("@m", cb);
                    m.ExecuteNonQuery();
                    m.Dispose();
                    

                }
            }


            using (SqlCommand mm = new SqlCommand("UPDATE Masalar SET Durumu=0  WHERE MasaID=@ıd  ", Baglanti.bag()))
            {
                int cb = (int)cboTasinacak.SelectedItem;
                mm.Parameters.AddWithValue("@ıd",cb);
                mm.ExecuteNonQuery();
                mm.Dispose();
            }
            //Siparis.Clear();
        }

        private void MasaDurumu() //Masa Taşıma İşlemi//işlem yapıalcak
        {

            using (SqlCommand mm = new SqlCommand("UPDATE Masalar SET Durumu=1  WHERE MasaID=@ıd  ", Baglanti.bag()))
            {
                int cbo = (int)cboMasa.SelectedItem;
                mm.Parameters.AddWithValue("@ıd", cbo);
                mm.ExecuteNonQuery();
                mm.Dispose();
            }
            using (SqlCommand mm = new SqlCommand("UPDATE MasaSiparisleri SET Durum=1  WHERE MasaID=@ıd AND SiparisID=@s", Baglanti.bag()))
            {
                int cbo = (int)cboMasa.SelectedItem;
                mm.Parameters.AddWithValue("@s", SiparisBilgisi.Siparisno);
                mm.Parameters.AddWithValue("@ıd", cbo);
                mm.ExecuteNonQuery();
                mm.Dispose();
            }

        }

        byte d = 0;
        private void MDurum() //Masa Durumunu Okumak İçin Kullanılan Metot
         {
            try
            {
                using (SqlCommand m = new SqlCommand("SELECT Durumu FROM Masalar WHERE MasaID=@t ", Baglanti.bag()))
                {
                    m.Parameters.AddWithValue("@t", cboTasinacak.SelectedItem);
                    SqlDataReader oku = m.ExecuteReader();

                    while (oku.Read())
                    {
                        d = Convert.ToByte(oku["Durumu"]);
                    }
                    m.Dispose();
                    oku.Dispose();
                }
            }
            catch (Exception) {; }

         }
 //---------------------------------------------------------------------------------------------------------------------
        //List<int> rz = new List<int>();

        decimal rz = 0m;
        private void RezervasyonNo() //Rezervasyon No Okutarak Rezervasyonlu Masaların Taşıma İşlemi İçin RezervasyonNo Bilgisini Okuyor
        {
            using (SqlCommand m = new SqlCommand("SELECT RezervasyonID FROM Masalar WHERE MasaID=@t  ", Baglanti.bag()))
            {
                m.Parameters.AddWithValue("@t", cboTasinacak.SelectedItem);
                SqlDataReader oku = m.ExecuteReader();

                while (oku.Read())
                {
                    rz = Convert.ToDecimal(oku["RezervasyonID"]);
                }
                m.Dispose();
                oku.Dispose();
            }
        }

        private void Rezerve() //Rezerveli Masanın Taşıma İşemlemni Yapan Metot
        {
            int cb = (int)cboTasinacak.SelectedItem;
          

                using (SqlCommand r = new SqlCommand("UPDATE Masalar SET Durumu=2, RezervasyonID=@r WHERE MasaID=@m ", Baglanti.bag()))
                {
                    int cbo = (int)cboMasa.SelectedItem;
                    r.Parameters.AddWithValue("@m", cbo);
                    r.Parameters.AddWithValue("@r", rz);
                    r.ExecuteNonQuery();
                    r.Dispose();
                }
            

            using (SqlCommand r = new SqlCommand("UPDATE Masalar SET Durumu=0 , RezervasyonID=@r WHERE MasaID=@m ", Baglanti.bag()))
            {
                r.Parameters.AddWithValue("@m", cb);
                r.Parameters.AddWithValue("@r", 0);//DBNull.Value Veritabanındaki Kolonu NULL Yapmak İçin Kullanılır...
                r.ExecuteNonQuery();
                r.Dispose();
            }
        }
 //----------------------------------------------------------------------------------------------------------------------

        List<int> RezervasyonId = new List<int>();

        private void RezervasyonID() //Rezervasyon No Okutarak Rezervasyonlu Masaların Taşıma İşlemi İçin RezervasyonNo Bilgisini Okuyor
        {
            using (SqlCommand m = new SqlCommand("SELECT RezervasyonID FROM MasaSiparisleri WHERE MasaID=@t AND Durum=3", Baglanti.bag()))
            {
                m.Parameters.AddWithValue("@t", cboTasinacak.SelectedItem);
                SqlDataReader oku = m.ExecuteReader();

                while (oku.Read())
                {
                    RezervasyonId.Add((int)oku["RezervasyonID"]);
                }
                m.Dispose();
                oku.Dispose();
            }
        }

        private void AcıkRezerve() //Açık Rezerveli Masanın Taşıma İşlemini Yapan Metot
        {
            
            foreach (int AcikRezerve in RezervasyonId)
            {
                
                using (SqlCommand r = new SqlCommand("UPDATE Masalar SET Durumu=3, RezervasyonID=@r WHERE MasaID=@m ", Baglanti.bag()))
                {
                    int cb = (int)cboTasinacak.SelectedItem;
                    int cbo = (int)cboMasa.SelectedItem;
                    r.Parameters.AddWithValue("@m", cbo);
                    r.Parameters.AddWithValue("@r", AcikRezerve);
                    r.ExecuteNonQuery();
                    r.Dispose();
                }
            }
            using (SqlCommand r = new SqlCommand("UPDATE Masalar SET Durumu=0 , RezervasyonID=@r WHERE MasaID=@m ", Baglanti.bag()))
            {
                int cb = (int)cboTasinacak.SelectedItem;
                r.Parameters.AddWithValue("@m", cb);
                r.Parameters.AddWithValue("@r", DBNull.Value);
                r.ExecuteNonQuery();
                r.Dispose();
            }
        }

        

        private void Rezervasyonıd() 
            {
               foreach (int item in RezervasyonId)
            {
                using (SqlCommand m = new SqlCommand("UPDATE MasaSiparisleri SET MasaID=@ıd WHERE RezervasyonID=@s AND MasaID=@m ", Baglanti.bag()))
                {
                    int cbo = (int)cboMasa.SelectedItem;
                    int cb = (int)cboTasinacak.SelectedItem;
                    m.Parameters.AddWithValue("@s", item);
                    m.Parameters.AddWithValue("@m", cb);// Durumu 1 2 3 Olan ÇAık rezerveli Açık rezerveli MAsaları getirir cb
                    m.Parameters.AddWithValue("@ıd", cbo); //MasaSiparişleri Durumunu Güncelliyor 
                    m.ExecuteNonQuery();
                    m.Dispose();
                }
}

            }

        public void MasaOlustur() // Dinamik Masaların Oluşturulduğu Metot
        {

            flowLayoutPanel1.Controls.Clear();
            SqlCommand m = new SqlCommand("select MasaID,Durumu from Masalar Durumu order by MasaID ", Baglanti.bag());
            {
                SqlDataReader oku = m.ExecuteReader();
                Bitmap bmp = new Bitmap("dosya/Masalar.png");
                while (oku.Read())
                {
                    Button btn = new Button();
                    btn.ForeColor = Color.Black;
                    btn.Text = String.Concat("Masa:" + oku["MasaID"]);
                    btn.Size = new System.Drawing.Size(120, 60);
                    btn.Font = new Font(btn.Font.Name, btn.Font.Size, FontStyle.Bold);
                    btn.ForeColor = Color.Black;
                    btn.Tag = Convert.ToInt32(oku["MasaID"]);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.BackColor = Color.White;

                    //btn.ImageList = imgList;
                    //btn.ImageIndex = 0;
                    ////btn.BackgroundImage = imgList.Images[0];
                    //btn.TextImageRelation = TextImageRelation.ImageAboveText;
                    ////btn.BackgroundImageLayout = ImageLayout.None;
                    if (oku.GetByte(1) == 0)
                    {
                        btn.BackgroundImage = bmp;
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else if (oku.GetByte(1) == 1)
                    {
                        Bitmap x = new Bitmap("dosya/Acık.png");
                        btn.BackgroundImage = x;
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else if (oku.GetByte(1) == 2)
                    {
                        Bitmap x = new Bitmap("dosya/rezerve.png");
                        btn.BackgroundImage = x;
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else if (oku.GetByte(1) == 3)
                    {
                        Bitmap x = new Bitmap("dosya/açıkrezerve.png");
                        btn.BackgroundImage = x;
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else if (oku.GetByte(1) == 4)
                    {
                        Bitmap x = new Bitmap("dosya/Masalar.png");
                        btn.BackgroundImage = x;
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    btn.Click += btn_Click;
                    flowLayoutPanel1.Controls.Add(btn);

                }
                m.Dispose();
                oku.Dispose();
            }
        }

        Ürünler u = null;

        public void btn_Click(object sender, EventArgs e) //Dinamik Masaların Tagını Tutup Ürünler Formunda Bilgisini Tutan 
        {
            

            if (u == null || u.Disposing || u.IsDisposed)
            {

                u = new Ürünler();
                u.Tag = Araclar.MasaId = (int)((sender as Button).Tag);
                u.Show();
                Button b = (Button)(sender as Button);
                b.ImageIndex = 1;
            }
            else
            {
                u.Activate();
            }
            this.Dispose();

        }

        private void KatGetir() // Kat Bilgisini Getiren Metot
        {
            string s = @"SELECT katId,katTanimi FROM katlar";

            using (SqlDataAdapter a = new SqlDataAdapter(s, Baglanti.bag()))
            {
                DataTable dtable = new DataTable();
                a.Fill(dtable);
                comboBox1.DataSource = dtable;
                comboBox1.ValueMember = "KatTanimi";
                comboBox1.ValueMember = "KatId";
                a.Dispose();
            }

        }
        PersonelBilgisi p = new PersonelBilgisi();
        private void Masalar_Load(object sender, EventArgs e) // Masanın Açılışında Çalışacak Metot
        {

          


            MasaSorgu("SELECT * FROM MasaSiparisleri WHERE Durum=@durum", 1);
           
            
           
            p.islemOku();
            p.Personel();

            timer1.Start();
            //this.Text ="Masalar"+"                     "+ p.Ad + " " + p.Soyad + " " + p.Görevi +"       " + DateTime.Now.ToLongDateString()+"         "+ "ŞİRİNOĞLU BAKLAVA";

            MasaOlustur();
            KatGetir();
            MasaTasi();
            MasaOku();

            //RezerveliMasaTasi();
            //btnTümü.Text = "\n Tümü";
            SiparisBilgisi s = new SiparisBilgisi();
            s.SiparisNo();
            //s.SiparisN();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //Kat Bilgisi İçin Kullanılır
        {
            try
            {
                int secilenKatId = (int)comboBox1.SelectedValue;
                KattakileriGetir(secilenKatId);
            }
            catch (Exception)
            {
                ;
            }
        }

        private void KattakileriGetir(int id) //Kata Bigisne Göre Masaları Getiren Metot
        {
            flowLayoutPanel1.Controls.Clear();
            SqlCommand m = new SqlCommand("SELECT MasaID,Durumu FROM Masalar Durumu WHERE katId=@i ORDER BY MasaID ", Baglanti.bag());
            {
                m.Parameters.AddWithValue("@i", id);
                SqlDataReader oku = m.ExecuteReader();
                Bitmap bmp = new Bitmap("dosya/Masalar.png");
                while (oku.Read())
                {
                    Button btn = new Button();
                    btn.ForeColor = Color.Black;
                    btn.Text = String.Concat("Masa:" + oku["MasaID"]);
                    btn.Size = new System.Drawing.Size(154, 66);
                    btn.Font = new Font(btn.Font.Name, btn.Font.Size, FontStyle.Bold);
                    btn.ForeColor = Color.Black;
                    btn.Tag = Convert.ToInt32(oku["MasaID"]);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.BackColor = Color.White;

                    //btn.ImageList = imgList;
                    //btn.ImageIndex = 0;
                    ////btn.BackgroundImage = imgList.Images[0];
                    //btn.TextImageRelation = TextImageRelation.ImageAboveText;
                    ////btn.BackgroundImageLayout = ImageLayout.None;
                    if (oku.GetByte(1) == 0)
                    {
                        btn.BackgroundImage = bmp;
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else if (oku.GetByte(1) == 1)
                    {
                        Bitmap x = new Bitmap("dosya/Acık.png");
                        btn.BackgroundImage = x;
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else if (oku.GetByte(1) == 2)
                    {
                        Bitmap x = new Bitmap("dosya/rezerve.png");
                        btn.BackgroundImage = x;
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else if (oku.GetByte(1) == 3)
                    {
                        Bitmap x = new Bitmap("dosya/açıkrezerve.png");
                        btn.BackgroundImage = x;
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                    }

                    else if (oku.GetByte(1) == 4)
                    {
                        Bitmap x = new Bitmap("dosya/Masalar.png");
                        btn.BackgroundImage = x;
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    btn.Click += btn_Click;
                    flowLayoutPanel1.Controls.Add(btn);
                }
                oku.Dispose();
                m.Dispose();
            }
        }

        private void Masalar_FormClosing(object sender, FormClosingEventArgs e) // Form Kapatılırken Menü formuna Dönmek İçin Kullanılır
        {
            Menu m = new Menu();
            m.Show();
            this.Dispose();
        }

        private void btnGeri_Click(object sender, EventArgs e) // Buton Click Olayına Menü Formuna Dönmek İçin Kullanılır
        {
            Menu m = new Menu();
            m.Show();
            this.Dispose();
        }

        private void btnTümü_Click(object sender, EventArgs e)//Tüm Masaları Getiren Button Click
        {
            MasaOlustur();
        }

        private void btnTasi_Click(object sender, EventArgs e) // Taşıma İşlemini Gerçekleştiren Buton Click Olayı
        {
           

            MDurum();
            if (d == 1)
            {
                
                Masa();
                MasaDurumu();
                Masalar frmMasa = (Masalar)Application.OpenForms["Masalar"];
                if (frmMasa != null)
                frmMasa.MasaOlustur();

                cboTasinacak.Items.Clear();
                MasaTasi();

                cboMasa.Items.Clear();
                MasaOku();
               
            }
            else if(d==2)
            {
                RezervasyonNo();
                Rezerve();
                Masalar frmMasa = (Masalar)Application.OpenForms["Masalar"];
                if (frmMasa != null)
                frmMasa.MasaOlustur();

                cboTasinacak.Items.Clear();
                MasaTasi();

                cboMasa.Items.Clear();
                MasaOku();
            }
            else if(d==3)
            {
                RezervasyonID();
                RezervasyonNo();
                AcıkRezerve();
                Rezervasyonıd();
                Masalar frmMasa = (Masalar)Application.OpenForms["Masalar"];
                if (frmMasa != null)
                    frmMasa.MasaOlustur();

                cboTasinacak.Items.Clear();
                MasaTasi();

                cboMasa.Items.Clear();
                MasaOku();
            }
        }

        private void chcBos_CheckedChanged(object sender, EventArgs e) //chcbos Checked Olayına Tıkladıgında Boş Masaları Getir 
        {
            using (SqlCommand bos = new SqlCommand("SELECT MasaID,Durumu FROM Masalar  WHERE Durumu=0 ", Baglanti.bag()))
            {

                flowLayoutPanel1.Controls.Clear();
                SqlDataReader oku = bos.ExecuteReader();
                if (chcBos.Checked == true)
                {
                    chcDolu.Enabled = false;
                    Bitmap bmp = new Bitmap("dosya/Masalar.png");
                    while (oku.Read())
                    {
                        Button btn = new Button();
                        btn.ForeColor = Color.Black;
                        btn.Text = String.Concat("Masa:" + oku["MasaID"]);
                        btn.Size = new System.Drawing.Size(120, 60);
                        btn.Font = new Font(btn.Font.Name, btn.Font.Size, FontStyle.Bold);
                        btn.ForeColor = Color.Black;
                        btn.Tag = Convert.ToInt32(oku["MasaID"]);
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.BackColor = Color.White;

                        if (oku.GetByte(1) == 0)
                        {
                            btn.BackgroundImage = bmp;
                            btn.BackgroundImageLayout = ImageLayout.Stretch;
                        }

                        else if (oku.GetByte(1) == 4)
                        {
                            Bitmap x = new Bitmap("dosya/Masalar.png");
                            btn.BackgroundImage = x;
                            btn.BackgroundImageLayout = ImageLayout.Stretch;
                        }

                        btn.Click += btn_Click;
                        flowLayoutPanel1.Controls.Add(btn);
                    }
                    oku.Close();
                    bos.Dispose();
                }

                else if (chcBos.Checked == false)
                {
                    MasaOlustur();
                    chcDolu.Enabled = true;
                } 
            }

        }

        private void chcDolu_CheckedChanged(object sender, EventArgs e)//chcD Checked Olayına Tıkladıgında Dolu Masaları Getir
        {
            using (SqlCommand dolu = new SqlCommand("SELECT MasaID,Durumu FROM Masalar  WHERE Durumu=1 OR Durumu=2 OR Durumu=3 ", Baglanti.bag()))
                    {

                        flowLayoutPanel1.Controls.Clear();
                        SqlDataReader ok = dolu.ExecuteReader();
                if (chcDolu.Checked == true)
                {
                    chcBos.Enabled = false;
                    while (ok.Read())
                    {
                        Button btn = new Button();
                        btn.ForeColor = Color.Black;
                        btn.Text = String.Concat("Masa:" + ok["MasaID"]);
                        btn.Size = new System.Drawing.Size(120, 60);
                        btn.Font = new Font(btn.Font.Name, btn.Font.Size, FontStyle.Bold);
                        btn.ForeColor = Color.Black;
                        btn.Tag = Convert.ToInt32(ok["MasaID"]);
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.BackColor = Color.White;

                        if (ok.GetByte(1) == 1)
                        {
                            Bitmap x = new Bitmap("dosya/Acık.png");
                            btn.BackgroundImage = x;
                            btn.BackgroundImageLayout = ImageLayout.Stretch;
                        }

                        else if (ok.GetByte(1) == 2)
                        {

                            Bitmap x = new Bitmap("dosya/rezerve.png");
                            btn.BackgroundImage = x;
                            btn.BackgroundImageLayout = ImageLayout.Stretch;
                        }


                        else if (ok.GetByte(1) == 3)
                        {
                            Bitmap y = new Bitmap("dosya/açıkrezerve.png");
                            btn.BackgroundImage = y;
                            btn.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        else if (ok.GetByte(1) == 4)
                        {
                            Bitmap x = new Bitmap("dosya/Masalar.png");
                            btn.BackgroundImage = x;
                            btn.BackgroundImageLayout = ImageLayout.Stretch;
                        }

                        btn.Click += btn_Click;
                        flowLayoutPanel1.Controls.Add(btn);
                    }
                    ok.Dispose();
                    dolu.Connection.Close();

                }

                else if (chcDolu.Checked == false)
                {
                    MasaOlustur();
                    chcBos.Enabled = true;
                }
                    }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //PersonelBilgisi p = new PersonelBilgisi();
           
           
            this.Text = "Masalar" + "                                 "+" Kullanıcı Adı" + p.Ad + " " + p.Soyad + " " +"Görevi"+ p.Görevi + "       " + DateTime.Now.ToLongDateString() + "  "+ DateTime.Now.ToLongTimeString() +"        "+ "ŞİRİNOĞLU BAKLAVA"; 
        }

       
    }

    }
