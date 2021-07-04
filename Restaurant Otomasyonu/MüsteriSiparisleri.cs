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
    public partial class MusteriSiparisleri : Form
    {
        public MusteriSiparisleri()
        {
            InitializeComponent();
        }

        private void Temizle()
        {
            txtMadi.Text = "";
            txtMSoyadi.Text = "";
            txtMAdresi.Text = "";
            txtMÜnvani.Text = "";
            mkdTel.Text = "";
        }


        
        private void KayıtKontrol()// Aynı Kayıttan Varmı Yokmu Denetler 
        {
            using (SqlCommand kk=new SqlCommand("SELECT * FROM MusteriBilgileri WHERE Telefonu=@tel",Baglanti.bag()))
            {
                kk.Parameters.AddWithValue("@tel",mkdTel.Text);

                SqlDataReader oku = kk.ExecuteReader();

                if (oku.Read())
                {
                    if (oku.GetString(3) == mkdTel.Text)
                    {
                        MessageBox.Show("Telefon Numarası Kayıtlı", "Kayıt Hatası", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }


                else  { Kaydet(); }

                
            }

        }


        private void Kaydet() //Bilgileri Veritabanına Kaydeder
        {
            try {

                using (SqlCommand k = new SqlCommand("INSERT INTO MusteriBilgileri (MusteriAdi,MusteriSoyadi,Telefonu,Unvanı,Adresi,Durum) VALUES (@madi,@ms,@tel,@u,@a,@d)", Baglanti.bag()))
                {

                    if (txtMadi.Text == "") { MessageBox.Show("Müşteri Adı Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (txtMSoyadi.Text == "") { MessageBox.Show("Müşteri Soyadı Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (txtMÜnvani.Text == "") { MessageBox.Show("Müşteri Ünvanı Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (mkdTel.Text == "") { MessageBox.Show("Müşteri Telefonu Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (txtMAdresi.Text == "") { MessageBox.Show("Müşteri Adresi Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                    else
                    {
                        k.Parameters.AddWithValue("@madi", txtMadi.Text);
                        k.Parameters.AddWithValue("@ms", txtMSoyadi.Text);
                        k.Parameters.AddWithValue("@tel", mkdTel.Text);
                        k.Parameters.AddWithValue("@u", txtMÜnvani.Text);
                        k.Parameters.AddWithValue("@a", txtMAdresi.Text);
                        k.Parameters.AddWithValue("@d", "1");
                        k.ExecuteNonQuery();
                        MessageBox.Show("Müşteri  Bilgileri Eklendi", "Müşteri Ekle", MessageBoxButtons.OK, MessageBoxIcon.None);
                        k.Dispose();
                    }
                }
            }
            catch (Exception) {; }
        }


        private void Guncelle() //Bilgileri Veritabanına Kaydeder
        {
            try
            {

                using (SqlCommand g = new SqlCommand("UPDATE MusteriBilgileri SET MusteriAdi=@madi,MusteriSoyadi=@ms,Telefonu=@tel,Unvanı=@u,Adresi=@a,Durum=@d WHERE MusteriID=@id ", Baglanti.bag()))
                {

                    if (txtMadi.Text == "") { MessageBox.Show("Müşteri Adı Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (txtMSoyadi.Text == "") { MessageBox.Show("Müşteri Soyadı Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (txtMÜnvani.Text == "") { MessageBox.Show("Müşteri Ünvanı Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (mkdTel.Text == "") { MessageBox.Show("Müşteri Telefonu Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (txtMAdresi.Text == "") { MessageBox.Show("Müşteri Adresi Boş Geçilmez", "Adı Hata", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                    else
                    {
                        g.Parameters.AddWithValue("@id", Müsteri.MusteriID);
                        g.Parameters.AddWithValue("@madi", txtMadi.Text);
                        g.Parameters.AddWithValue("@ms", txtMSoyadi.Text);
                        g.Parameters.AddWithValue("@tel", mkdTel.Text);
                        g.Parameters.AddWithValue("@u", txtMÜnvani.Text);
                        g.Parameters.AddWithValue("@a", txtMAdresi.Text);
                        g.Parameters.AddWithValue("@d", "1");
                        g.ExecuteNonQuery();
                        MessageBox.Show("Müşteri  Bilgileri Güncellendi", "Müşteri Güncelle", MessageBoxButtons.OK, MessageBoxIcon.None);
                        g.Dispose();
                    }
                }
            }
            catch (Exception) {; }
        }




        private void txtMadi_TextChanged(object sender, EventArgs e)
        {
            var konum = txtMadi.SelectionStart;
            string veri = txtMadi.Text.ToLower();
            txtMadi.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(veri);
            txtMadi.SelectionStart = konum;
            //Adi Bilgisine Girilen İlk Harf Büyük Yazırma 
        }

        private void txtMSoyadi_TextChanged(object sender, EventArgs e)
        {
            var konum = txtMSoyadi.SelectionStart;
            string veri = txtMSoyadi.Text.ToUpper();
            txtMSoyadi.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(veri);
            txtMSoyadi.SelectionStart = konum;
            //Adi Bilgisine Girilen İlk Harf Büyük Yazırma 
        }

        private void txtMÜnvani_TextChanged(object sender, EventArgs e)
        {
            var konum = txtMÜnvani.SelectionStart;
            string veri = txtMÜnvani.Text.ToLower();
            txtMÜnvani.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(veri);
            txtMÜnvani.SelectionStart = konum;
            //Adi Bilgisine Girilen İlk Harf Büyük Yazırma 
        }

        private void txtMAdresi_TextChanged(object sender, EventArgs e)
        {
            var konum = txtMAdresi.SelectionStart;
            string veri = txtMAdresi.Text.ToLower();
            txtMAdresi.Text = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(veri);
            txtMAdresi.SelectionStart = konum;
            //Adi Bilgisine Girilen İlk Harf Büyük Yazırma 
        }
       
        private void Sil()
        {
            
            using (SqlCommand sil=new SqlCommand("UPDATE MusteriBilgileri SET Durum=2 WHERE MusteriID=@id ",Baglanti.bag()))
            {
                sil.Parameters.AddWithValue("@id", Müsteri.MusteriID);
                sil.ExecuteNonQuery();

                MessageBox.Show("Müşteri Başarıyla Silinmiştir","Müşteri Sil",MessageBoxButtons.OK,MessageBoxIcon.Information);
                sil.Dispose();

            }

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            
            KayıtKontrol();
            Temizle();
            
        }

        private void MusteriSiparisleri_Load(object sender, EventArgs e)
        {

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            Sil();
            Temizle();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            Guncelle();
            Temizle();
        }

        private void txtMadi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
            && !char.IsSeparator(e.KeyChar);
        }

        private void txtMSoyadi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
            && !char.IsSeparator(e.KeyChar);
        }

        private void txtMÜnvani_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
            && !char.IsSeparator(e.KeyChar);
        }
    }
}
