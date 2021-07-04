using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Restaurant.Kasa
{
   public class Silinmis
    {


        public DataTable dv = new DataTable();
        public Kasaİslemleri Sil { get; set; }


        public DataTable d = new DataTable();
        public Kasaİslemleri Del { get; set; }



        public void Silinen()
        {
            try
            {
                KasaOku k = new KasaOku();
                if (Sil.cboKasa.SelectedIndex == 0)
                {
                    using (SqlDataAdapter s = new SqlDataAdapter("SELECT KasaID,Aciklama,OdemeTipi,Tarih,Tutar  FROM Kasa WHERE Onay=2 AND Durum=1", Baglanti.bag()))
                    {
                        dv.Clear();
                        k.dt.Clear();
                        s.Fill(dv);
                        Sil.dataGridView1.DataSource = dv;
                        s.Dispose();
                    }
                }

                else if (Del.cboKasa.SelectedIndex == 1)
                {
                    using (SqlDataAdapter ss = new SqlDataAdapter("SELECT KasaID,Aciklama,OdemeTipi,Tarih,Tutar  FROM Kasa WHERE Onay=2 AND Durum=0", Baglanti.bag()))
                    {
                        d.Clear();
                        k.db.Clear();
                        ss.Fill(d);
                        Del.dataGridView2.DataSource = d;
                        ss.Dispose();
                    }
                }
            }
            catch(Exception hata) {  MessageBox.Show(hata.ToString()) ; }
            }
    }
}
