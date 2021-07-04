using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
namespace Restaurant.Kasa
{
    public class KasaOku
    {
        public Kasaİslemleri Form { get;  set; }
        public KasaOku( )
        {

        }
        public DataTable dt = new DataTable();
     //   public Kasaİslemleri Form { get; set; }

        public DataTable db = new DataTable();
      //  public Kasaİslemleri Form { get; set; }

        
       
        public void KasaRead()
        {
            try
            {
                using (SqlDataAdapter k = new SqlDataAdapter("SELECT KasaID,Aciklama,OdemeTipi,Tarih,Tutar FROM Kasa WHERE Durum=1 AND Onay=1 ", Baglanti.bag()))
                {
                    k.Fill(dt);
                    Form.dataGridView1.DataSource = dt;
                    k.Dispose();
                }
            }
            catch (Exception hata) { MessageBox.Show(hata.ToString()); }
        }



        public void KOku()
        {
            try
            {
                using (SqlDataAdapter k = new SqlDataAdapter("SELECT KasaID,Aciklama,OdemeTipi,Tarih,Tutar FROM Kasa WHERE Durum=0 AND Onay=1 ", Baglanti.bag()))
                {
                    k.Fill(db);
                    Form.dataGridView2.DataSource = db;
                    k.Dispose();
                }
            }
            catch (Exception hata) { MessageBox.Show(hata.ToString()); }
        }

    }
}
