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
    public class Arama
    {
        DataTable ta = new DataTable();
        public Kasaİslemleri Tarih { get; set; }

        DataTable at = new DataTable();
        public Kasaİslemleri Tarih2 { get; set; }

      
        public void TarihArama()
        {
            try
            {

                if (Tarih.cboKasa.SelectedIndex == 0)
                {

                    using (SqlDataAdapter arama = new SqlDataAdapter(@"SELECT KasaID,Aciklama,OdemeTipi,Tarih,Tutar 
                    FROM Kasa 
                    WHERE Tarih >= @baslangic AND Tarih < @bitis AND 
                        Durum=1 AND 
                        Onay=1", Baglanti.bag()))
                    {
                        arama.SelectCommand.Parameters.AddWithValue("@baslangic", Tarih.dtpTarih1.Value.Date);
                        arama.SelectCommand.Parameters.AddWithValue("@bitis", Tarih2.dtpTarih2.Value.Date.AddDays(1));
                        arama.Fill(ta);
                        Tarih.dataGridView1.DataSource = ta.DefaultView;
                        arama.Dispose();
                    }


                }
                else if (Tarih.cboKasa.SelectedIndex == 1)
                {
                    using (SqlDataAdapter arama = new SqlDataAdapter(@"SELECT KasaID,Aciklama,OdemeTipi,Tarih,Tutar 
                    FROM Kasa 
                    WHERE Tarih >= @baslangic AND Tarih < @bitis AND 
                        Durum=0 AND 
                        Onay=1", Baglanti.bag()))
                    {
                        arama.SelectCommand.Parameters.AddWithValue("@baslangic", Tarih.dtpTarih1.Value.Date);
                        arama.SelectCommand.Parameters.AddWithValue("@bitis", Tarih2.dtpTarih2.Value.Date.AddDays(1));
                        arama.Fill(at);
                        Tarih2.dataGridView2.DataSource = at.DefaultView;
                        arama.Dispose();
                    }
                }
            }
            catch (Exception hata) { MessageBox.Show(hata.ToString()); }
         }
    }
}