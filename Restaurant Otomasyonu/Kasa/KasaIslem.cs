using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Restaurant.Kasa
{
    public class KasaIslem
    {


        public int id { get; set; }
        public Kasaİslemleri yukle { get; set; }
      
        public KasaIslem()
        {
           
        }

        public void Ekle() //Kaydet İşlemini Yapıyoruz
        {
            try
            {
                
                if (yukle.cboKasa.SelectedIndex == 0)
                {


                    using (SqlCommand ekle = new SqlCommand("INSERT INTO Kasa (Aciklama,OdemeTipi,Tarih,Tutar,Durum,Onay) VALUES (@a,@ot,@t,@tu,@d,@o)", Baglanti.bag()))
                    {
                        ekle.Parameters.AddWithValue("@a", yukle.txtAciklama.Text);
                        ekle.Parameters.AddWithValue("@ot", yukle.txtOdemeSekli.Text);
                        ekle.Parameters.AddWithValue("@t", yukle.dtpTarih.Value);
                        ekle.Parameters.AddWithValue("@tu", yukle.txtOdeme.Text);
                        ekle.Parameters.AddWithValue("@d", 1);
                        ekle.Parameters.AddWithValue("@o", 1);
                        ekle.ExecuteNonQuery();
                        MessageBox.Show("Bilgiler Kaydedildi", "Kaydet", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ekle.Dispose();

                    }
                }

                else if (yukle.cboKasa.SelectedIndex == 1)
                {

                    using (SqlCommand ekle = new SqlCommand("INSERT INTO Kasa (Aciklama,OdemeTipi,Tarih,Tutar,Durum,Onay) VALUES (@a,@ot,@t,@tu,@d,@o)", Baglanti.bag()))
                    {
                        ekle.Parameters.AddWithValue("@a", yukle.txtAciklama.Text);
                        ekle.Parameters.AddWithValue("@ot", yukle.txtOdemeSekli.Text);
                        ekle.Parameters.AddWithValue("@t", yukle.dtpTarih.Value);
                        ekle.Parameters.AddWithValue("@tu", yukle.txtOdeme.Text);
                        ekle.Parameters.AddWithValue("@d", 0);
                        ekle.Parameters.AddWithValue("@o", 1);
                        ekle.ExecuteNonQuery();
                        MessageBox.Show("Bilgiler Kaydedildi", "Kaydet", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ekle.Dispose();
                    }
                }
            }
            catch (Exception) { MessageBox.Show("Seçilen Veri Üzerine Değişiklik Yapamazsınız", ""); }
        }

        
    

        public void Sil() /// Silme İşlemi Yapılacak
        {
            try
            {

                using (SqlCommand sil = new SqlCommand("UPDATE Kasa SET Onay=2,SilinmeSebebi=@s WHERE KasaID=@id", Baglanti.bag()))
                {
                    sil.Parameters.AddWithValue("@id", id);
                    sil.Parameters.AddWithValue("@s",yukle.txtSilme.Text);
                    sil.ExecuteNonQuery();
                    sil.Dispose();
                    MessageBox.Show("Seçilen Veri Silinmiştir", "Silme İşlemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch(Exception) {; }

          }
        public void Güncelle()
        {
            using (SqlCommand g = new SqlCommand("UPDATE Kasa SET Aciklama=@a,OdemeTipi=@ö,Tarih=@t,Tutar=@tt WHERE KasaID=@id", Baglanti.bag()))
            {
                try
                {
                    g.Parameters.AddWithValue("@id", id);
                    g.Parameters.AddWithValue("@a", yukle.txtAciklama.Text);
                    g.Parameters.AddWithValue("@ö", yukle.txtOdemeSekli.Text);
                    g.Parameters.AddWithValue("@t", Convert.ToDateTime(yukle.dtpTarih.Text));
                    g.Parameters.AddWithValue("@tt", Convert.ToDecimal(yukle.txtOdeme.Text));
                    g.ExecuteNonQuery();
                }
                catch (Exception) {; }
            }
        }

    }
    }
