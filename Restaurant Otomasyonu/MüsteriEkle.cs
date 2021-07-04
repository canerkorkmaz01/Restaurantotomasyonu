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
    
    public partial class MusteriEkle : Form
    {
        public int g; //Rezervasyon ID Bilgisini Tutan Field Değişkeni

        public MusteriEkle()
        {
            InitializeComponent();
        }
     
        private void temizle() // Textbox Temizleme Metodu
        {
            txtMadi.Text = "";
            txtMSoyadi.Text = "";
            txtMÜnvani.Text = "";
            mkdTel.Text = "";
            dateTimePicker1.Text = "";
            txtMAdresi.Text = "";
            txtMEmail.Text = "";
            txtKSayisi.Text = "";
        }

        private void txtKSayisi_KeyPress(object sender, KeyPressEventArgs e) // Textbox Metin Girişini Yasaklama
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtMadi_KeyPress(object sender, KeyPressEventArgs e) // Textbox Sayı Girişini Yasaklama
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar);

        }

        private void txtMSoyadi_KeyPress(object sender, KeyPressEventArgs e) // Textbox Sayı Girişini Yasaklama
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
            && !char.IsSeparator(e.KeyChar);

        }

        private void txtMÜnvani_KeyPress(object sender, KeyPressEventArgs e) // Textbox Sayı Girişini Yasaklama
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
            && !char.IsSeparator(e.KeyChar);

        }

        private void DurumDegistir() //Masa Durumunu Degiştirme Metodu
        {
            using (SqlCommand durum = new SqlCommand("UPDATE Masalar SET Durumu=@d,RezervasyonID=@r WHERE RezervasyonID=@id ", Baglanti.bag()))
            {
                
                durum.Parameters.AddWithValue("@id", g);
                durum.Parameters.AddWithValue("@d",0);
                durum.Parameters.AddWithValue("@r",0);
                durum.ExecuteNonQuery();
                durum.Dispose();
            }
        }

        private void Sil()
        {

            using (SqlCommand update = new SqlCommand("UPDATE Rezervasyon SET Durum=@d WHERE RezervasyonID=@ID", Baglanti.bag()))
            {
                update.Parameters.AddWithValue("@ID", g);
                update.Parameters.AddWithValue("@d", 3);
                update.ExecuteNonQuery();
                MessageBox.Show("Müşteri Rezervasyon Bilgileri Silindi", "Müşteri Sil", MessageBoxButtons.OK, MessageBoxIcon.None);
                temizle();
                update.Dispose();
            }

            //using (SqlCommand sil = new SqlCommand("DELETE FROM Rezervasyon WHERE RezervasyonID=@ID", Baglanti.bag()))
            //{
            //    sil.Parameters.AddWithValue("@ID", g);
            //    sil.ExecuteNonQuery();
            //    MessageBox.Show("Müşteri Rezervasyon Bilgileri Silindi", "Müşteri Sil", MessageBoxButtons.OK, MessageBoxIcon.None);
            //    temizle();
            //    sil.Dispose();
            //}
        } // Silme Metodu

        private void Guncelle()
        {
            try
            {
                using (SqlCommand guncelle = new SqlCommand("UPDATE Rezervasyon SET MusteriAdi=@ma, MusteriSoyadi=@ms, MusteriUnvani=@mu,Telefonu=@t,Tarih=@trh ,Adresi=@a,Email=@e,Kisi=@k,Durum=@d WHERE RezervasyonID=@ID", Baglanti.bag()))
                {
                    if (txtMadi.Text == "") { MessageBox.Show("Müşteri Adı Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (txtMSoyadi.Text == "") { MessageBox.Show("Müşteri Soyadı Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (txtMÜnvani.Text == "") { MessageBox.Show("Müşteri Ünvanı Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (txtMAdresi.Text == "") { MessageBox.Show("Müşteri Adresi Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (txtKSayisi.Text == "") { MessageBox.Show("Müşteri Kisi Sayısı Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (String.IsNullOrEmpty(mkdTel.Text))
                    {
                        MessageBox.Show("Telefon Numarası Boş Geçilemez", "Telefon Numarası Boş", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else if (String.IsNullOrEmpty(dateTimePicker1.Text)) MessageBox.Show("Tarih Saat Boş Geçilemez", "Tarih Saat Boş", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    else
                    {

                        guncelle.Parameters.AddWithValue("@ID", g);
                        guncelle.Parameters.AddWithValue("@ma", txtMadi.Text);
                        guncelle.Parameters.AddWithValue("@ms", txtMSoyadi.Text);
                        guncelle.Parameters.AddWithValue("@mu", txtMÜnvani.Text);
                        guncelle.Parameters.AddWithValue("@t", mkdTel.Text.ToString());
                        guncelle.Parameters.AddWithValue("@trh",dateTimePicker1.Value);
                        guncelle.Parameters.AddWithValue("@a", txtMAdresi.Text);
                        guncelle.Parameters.AddWithValue("@e", txtMEmail.Text);
                        guncelle.Parameters.AddWithValue("@k",txtKSayisi.Text);
                        guncelle.Parameters.AddWithValue("@d", 1);
                        guncelle.ExecuteNonQuery();
                        MessageBox.Show("Müşteri Rezervasyon Bilgileri Güncellendi", "Müşteri Güncelle", MessageBoxButtons.OK, MessageBoxIcon.None);
                        guncelle.Dispose();
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Hatalı Tarih Saat Bilgisi", "Tarih Hatası", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        } // Güncelle Metodu mkdTarih.Text

        private void KayıtKontrol()// Aynı Kayıttan Varmı Yokmu Denetler 
        {
            using (SqlCommand kk = new SqlCommand("SELECT * FROM Rezervasyon WHERE Telefonu=@tel", Baglanti.bag()))
            {
                kk.Parameters.AddWithValue("@tel", mkdTel.Text);

                SqlDataReader oku = kk.ExecuteReader();

                if (oku.Read())
                {
                    if (oku.GetString(4) == mkdTel.Text)
                    {
                        MessageBox.Show("Telefon Numarası Kayıtlı", "Kayıt Hatası", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else { Kaydet(); }

            }

        }



        private void Kaydet()
        {
            try
            {
                using (SqlCommand kaydet = new SqlCommand("INSERT INTO Rezervasyon (MusteriAdi,MusteriSoyadi,MusteriUnvani,Telefonu,Tarih,Adresi,Email,Kisi,Durum) values  (@ma,@ms,@mu,@t,@trh,@a,@e,@k,@d)", Baglanti.bag()))
                {
                   
                    if (txtMadi.Text == "") { MessageBox.Show("Müşteri Adı Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (txtMSoyadi.Text == "") { MessageBox.Show("Müşteri Soyadı Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (txtMÜnvani.Text == "") { MessageBox.Show("Müşteri Ünvanı Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (txtMAdresi.Text == "") { MessageBox.Show("Müşteri Adresi Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (txtKSayisi.Text == "") { MessageBox.Show("Müşteri Kisi Sayısı Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (String.IsNullOrEmpty(mkdTel.Text))
                    {
                        MessageBox.Show("Telefon Numarası Boş Geçilemez", "Telefon Numarası Boş", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else if (String.IsNullOrEmpty(dateTimePicker1.Text)) MessageBox.Show("Tarih Saat Boş Geçilemez", "Tarih Saat Boş", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        //"dd/MM/yyyy hh:mm"
                        kaydet.Parameters.AddWithValue("@ma", txtMadi.Text);
                        kaydet.Parameters.AddWithValue("@ms", txtMSoyadi.Text);
                        kaydet.Parameters.AddWithValue("@mu", txtMÜnvani.Text);
                        kaydet.Parameters.AddWithValue("@t",mkdTel.Text.ToString());
                        kaydet.Parameters.AddWithValue("@trh",dateTimePicker1.Value);
                        kaydet.Parameters.AddWithValue("@a", txtMAdresi.Text);
                        kaydet.Parameters.AddWithValue("@e", txtMEmail.Text);
                        kaydet.Parameters.AddWithValue("@k", txtKSayisi.Text);
                        kaydet.Parameters.AddWithValue("@d", 1);
                        kaydet.ExecuteNonQuery();
                        MessageBox.Show("Müşteri Rezervasyon Bilgileri Eklendi", "Müşteri Ekle", MessageBoxButtons.OK, MessageBoxIcon.None);
                        kaydet.Dispose();
                        temizle();
                    }


                }
                //tarih saat formatı:  "dd/MM/yyyy hh:mm:ss"
            }
            catch (Exception )
            {
               // MessageBox.Show(hata.ToString());
                MessageBox.Show("Hatalı Tarih Saat Bilgisi", "Tarih Hatası"); 
            }
        } // Kaydet Metodu

        private void btnKaydet_Click(object sender, EventArgs e) //Kaydet Metodunu Çagıran Buton
        {
            KayıtKontrol();
            temizle();
        }

        private void btnGüncelle_Click(object sender, EventArgs e) //Güncelle Metodunu Çagıran Buton
        {
            Guncelle();
        } 
          
        private void MusteriEkle_Load(object sender, EventArgs e) // Form Load Olayına Textbox Maksimum Deger Uzunluğu Verme 
        {
            txtKSayisi.MaxLength = 4;
            txtMadi.MaxLength = 17;
            txtMSoyadi.MaxLength = 17;
            txtMÜnvani.MaxLength = 20;
            txtMAdresi.MaxLength = 100;
            txtMEmail.MaxLength = 40; 
        } 

        private void btnSil_Click(object sender, EventArgs e) // Sil Metodunu Çagıran Buton
        {
            DurumDegistir();
            Sil();
            
        } 
       
        private void btnGeri_Click(object sender, EventArgs e)
        {
            Rezervasyon r = new Rezervasyon();
            r.Show();
            this.Dispose();
        }

        private void txtMadi_TextChanged(object sender, EventArgs e)
        {
            var konum = txtMadi.SelectionStart;
            string veri = txtMadi.Text.ToLower();
            txtMadi.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(veri);
            txtMadi.SelectionStart = konum;
            //Adi Bilgisine Girilen İlk Harf Büyük Yazırma 
        }

        private void txtMÜnvani_TextChanged(object sender, EventArgs e)
        {
            var konum = txtMÜnvani.SelectionStart;
            string veri = txtMÜnvani.Text.ToLower();
            txtMÜnvani.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(veri);
            txtMÜnvani.SelectionStart = konum;
        } // Soyadı Bilgisine Girilen İlk Harf Büyük Yazırma 

        private void txtMAdresi_TextChanged(object sender, EventArgs e)// Adres Bilgisine Girilen İlk Harf Büyük Yazırma 
        {
            var konum = txtMAdresi.SelectionStart;
            string veri = txtMAdresi.Text.ToLower();
            txtMAdresi.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(veri);
            txtMAdresi.SelectionStart = konum;
        }

        private void txtMSoyadi_TextChanged(object sender, EventArgs e) //Soyadı Bilgisinin Bütün Harflerini Büyük Yazdırma 
        {
            var konum = txtMSoyadi.SelectionStart;
            string veri = txtMSoyadi.Text.ToUpper();
            txtMSoyadi.Text = veri;
            txtMSoyadi.SelectionStart = konum;
        }

        private void MusteriEkle_FormClosed(object sender, FormClosedEventArgs e)
        {
            Rezervasyon r = new Rezervasyon();
            r.Show();
            this.Dispose();
        }
    }
    }

