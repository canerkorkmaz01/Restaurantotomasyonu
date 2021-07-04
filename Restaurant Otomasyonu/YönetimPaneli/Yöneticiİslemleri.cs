using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Restaurant.YönetimPaneli
{


    public class Yöneticiİslemleri
    {

        private YonetimPaneli İslemler;// Field Çagıracagımız Class ın İsmini Veriyoruz

        public Yöneticiİslemleri(YonetimPaneli islem) // Yapıcı Metot İle Class İçin Parametre İle Kullanıyoruz
        {
            İslemler = islem;
        }

        public void Oku() // Grid Satırlarını Okumak İçin 
        {
            try
            {
                İslemler.txtAd.Text = İslemler.dataGridView2.CurrentRow.Cells[1].Value.ToString();
                İslemler.txtSoyad.Text = İslemler.dataGridView2.CurrentRow.Cells[2].Value.ToString();
                İslemler.txtGörev.Text = İslemler.dataGridView2.CurrentRow.Cells[3].Value.ToString();
                İslemler.txtUser.Text = İslemler.dataGridView2.CurrentRow.Cells[4].Value.ToString();
                İslemler.txtPass.Text = İslemler.dataGridView2.CurrentRow.Cells[5].Value.ToString();
                İslemler.mkdTell.Text= İslemler.dataGridView2.CurrentRow.Cells[6].Value.ToString();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.ToString());

                //MessageBox.Show("Tablo'da Hiç Bir Kayıt Bulunamadı","Hata Mesajı",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }




        public void Kaydet() // Yeni Bir Yönetici eklemek İçin Kullanılır
        {
            //try
            //{
                using (SqlCommand kaydet = new SqlCommand(@"INSERT INTO Personeller 
            (Adi,Soyadi,Gorevi,KullaniciAdi,Sifre,TipId,Telefon) 
                              VALUES (@a,@s,@g,@k,@p,@t,@tt)", Baglanti.bag()))
                {
                    kaydet.Parameters.AddWithValue("@a", İslemler.txtAd.Text);
                    kaydet.Parameters.AddWithValue("@s", İslemler.txtSoyad.Text);
                    kaydet.Parameters.AddWithValue("@g", İslemler.txtGörev.Text);
                    kaydet.Parameters.AddWithValue("@k", İslemler.txtUser.Text);
                    kaydet.Parameters.AddWithValue("@p", İslemler.txtPass.Text);
                    kaydet.Parameters.AddWithValue("@t", 1);
                    kaydet.Parameters.AddWithValue("@tt", İslemler.mkdTell.Text);
                    kaydet.ExecuteNonQuery();
                    kaydet.Dispose();
 
                    MessageBox.Show("Yönetici Kaydı Yapılmıştır", "Yönetici Kaydet", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            //}
            //catch (Exception hata) { MessageBox.Show(hata.ToString()); }
        }


        public void Güncelle()// Yönetici Bilgilerini güncellemek İçin Kullanılır
        {
            using (SqlCommand g = new SqlCommand(@"UPDATE Personeller SET 
                       Adi=@a,Soyadi=@s,Gorevi=@g,KullaniciAdi=@k,Sifre=@p,TipId=@t,Telefon=@tt
                       WHERE Personelid=@id", Baglanti.bag()))
            {
                g.Parameters.AddWithValue("@id",İslemler.dataGridView2.CurrentRow.Cells[0].Value);
                g.Parameters.AddWithValue("@a", İslemler.txtAd.Text);
                g.Parameters.AddWithValue("@s", İslemler.txtSoyad.Text);
                g.Parameters.AddWithValue("@g", İslemler.txtGörev.Text);
                g.Parameters.AddWithValue("@k", İslemler.txtUser.Text);
                g.Parameters.AddWithValue("@p", İslemler.txtPass.Text);
                g.Parameters.AddWithValue("@tt", İslemler.mkdTell.Text);
                g.Parameters.AddWithValue("@t", 1);
                g.ExecuteNonQuery();
                g.Dispose();
                MessageBox.Show("Yönetici Bilgileri Güncelleştirilmiştir", "Yönetici Bilgi Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public void Sil() //Yönetici Silmek İçin Kullanılır
        {
            try
            {
                if (İslemler.dataGridView2.Rows.Count ==0)
                {
                    MessageBox.Show("Tabloda Silinecek Veri Bulunamadı..", "Veri Silme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    using (SqlCommand sil = new SqlCommand("UPDATE Personeller SET TipId=0 WHERE Personelid=@id", Baglanti.bag()))
                    {

                        sil.Parameters.AddWithValue("@id", İslemler.dataGridView2.CurrentRow.Cells[0].Value.ToString());
                        sil.ExecuteNonQuery();
                        sil.Parameters.Clear();
                        sil.Dispose();
                    }

                    MessageBox.Show("Silme İşlemi Başarılı", "Silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.ToString());
            }

          
        }
        public void MasaEkle() // Yeni Masa Eklemek İçin Kullanılır
        {
            try
            {
                int Masa = Convert.ToInt32(İslemler.txtOlustur.Text);
                int Masa2 = Convert.ToInt32(İslemler.txtOluştur2.Text);
                int toplam = Masa + 1;
                int kat;
                if (İslemler.cboKat.SelectedIndex == 0) { kat = 1; }
                else if (İslemler.cboKat.SelectedIndex == 1) { kat = 2; }
                else { kat = 3; }

                using (SqlCommand s = new SqlCommand("DBCC CHECKIDENT ('Masalar', RESEED, @masa)", Baglanti.bag()))
                {
                    s.Parameters.AddWithValue("@masa", Masa);
                    s.ExecuteNonQuery();
                }

                for (int i = Masa; i < Masa2; i++)
                {

                    using (SqlCommand btn = new SqlCommand("insert into Masalar (Kapasitesi,MasaAdi,Durumu,KatId) values(@k,@m,@d,@kıd)", Baglanti.bag()))
                    {

                        btn.Parameters.AddWithValue("@k", 4);
                        btn.Parameters.AddWithValue("@m", "Masa" + " " + toplam++);
                        btn.Parameters.AddWithValue("@d", 0);
                        btn.Parameters.AddWithValue("@kıd", kat);
                        btn.ExecuteNonQuery();
                        btn.Dispose();
                        
                    }

                }
                MessageBox.Show("Masa Eklendi", "Masa Ekle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            { MessageBox.Show("Lütfen 2 Sayı Aralığında Bir Değer Giriniz", "Hatalı Değer", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        }

        public void Kaldır()
        {
            try
            {
                int kat;
                if (İslemler.cboKat2.SelectedIndex == 0) { kat = 1; }
                else if (İslemler.cboKat2.SelectedIndex == 1) { kat = 2; }
                else { kat = 3; }
                using (SqlCommand del = new SqlCommand(@"DELETE FROM Masalar WHERE  MasaID 
                        >= @Masa1 AND MasaID <=@Masa2 AND KatId=@id   ", Baglanti.bag()))
                {
                    del.Parameters.AddWithValue("@Masa1", İslemler.txtKaldir.Text);
                    del.Parameters.AddWithValue("@Masa2", İslemler.txtKaldir2.Text);
                    del.Parameters.AddWithValue("@id", kat);
                    del.ExecuteNonQuery();
                    del.Dispose();
                }
            }
            catch (Exception hata) { MessageBox.Show(hata.ToString()); }
        }
       
    }
}
