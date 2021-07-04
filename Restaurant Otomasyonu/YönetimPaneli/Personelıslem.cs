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
   public class Personelıslem
    {
        private YonetimPaneli islem;

        public Personelıslem (YonetimPaneli islemler)
        {
            islem = islemler;
        }


        public void Kaydet() //Yeni Personel Eklemek İçin Kullanılır
        {

            try
            {
                using (SqlCommand kaydet = new SqlCommand(@"INSERT INTO Personeller 
            (Adi,Soyadi,Gorevi,KullaniciAdi,Sifre,Telefon,TipId) 
                              VALUES (@a,@s,@g,@k,@p,@tt,@t)", Baglanti.bag()))
                {
                    kaydet.Parameters.AddWithValue("@a", islem.txtAdi.Text);
                    kaydet.Parameters.AddWithValue("@s", islem.txtSoyadi.Text);
                    kaydet.Parameters.AddWithValue("@g", islem.txtGorevi.Text);
                    kaydet.Parameters.AddWithValue("@k", islem.txtUsers.Text);
                    kaydet.Parameters.AddWithValue("@p", islem.txtPassword.Text);
                    kaydet.Parameters.AddWithValue("@tt", islem.mkdTel.Text);
                    kaydet.Parameters.AddWithValue("@t",Convert.ToInt32(islem.yetki));
                    kaydet.ExecuteNonQuery();

                    MessageBox.Show("Personel Kaydı Yapılmıştır", "Personel Kaydet", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
        }
            catch (Exception hata) { MessageBox.Show(hata.ToString()); }
}

        public void Degistir()// Personel Bilgisini Güncellemek İçin Kulalnılır
        {
            YonetimPaneli yp = new YonetimPaneli();
            try
            {
                using (SqlCommand degistir = new SqlCommand(@"UPDATE Personeller SET
             Adi=@a,Soyadi=@s,Gorevi=@g,KullaniciAdi=@k,Sifre=@p,Telefon=@tt,TipId=@d 
                   WHERE Personelid=@id ", Baglanti.bag()))
                {
                    degistir.Parameters.AddWithValue("@id", islem.dataGridView1.CurrentRow.Cells[0].Value);
                    degistir.Parameters.AddWithValue("@a", islem.txtAdi.Text);
                    degistir.Parameters.AddWithValue("@s", islem.txtSoyadi.Text);
                    degistir.Parameters.AddWithValue("@g", islem.txtGorevi.Text);
                    degistir.Parameters.AddWithValue("@k", islem.txtUsers.Text);
                    degistir.Parameters.AddWithValue("@p", islem.txtPassword.Text);
                    degistir.Parameters.AddWithValue("@tt", islem.mkdTel.Text);
                    degistir.Parameters.AddWithValue("@t", Convert.ToInt32(islem.yetki)); // combobox yetki verilecek
                    degistir.ExecuteNonQuery();
                    MessageBox.Show("Personel Kaydı Güncelleştirilmiştir", "Personel Bilgi Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception hata) { MessageBox.Show(hata.ToString()); }
            }

        public void Sil() //Personel Silmek İçin Kullanılır
       {
            try
            {
                if (islem.dataGridView1.Rows.Count==0)
                {
                    MessageBox.Show("Tabloda Silinecek Veri Bulunamadı..", "Veri Silme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    using (SqlCommand sil = new SqlCommand("UPDATE Personeller SET TipId=0 WHERE Personelid=@id", Baglanti.bag()))
                    {

                        sil.Parameters.AddWithValue("@id", islem.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        sil.ExecuteNonQuery();
                        
                    }

                    MessageBox.Show("Silme İşlemi Başarılı", "Silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.ToString());
            }
        }
    }
}
