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
    public class KapatılanHesaplar
    {
        public DataTable hp = new DataTable();
        public Kasaİslemleri Hesaplar { get; set; }

        public DataTable ht = new DataTable();
        public Kasaİslemleri Hesapp { get; set; }
        public void Hesap()
        {
            KasaOku k = new KasaOku();
            Silinmis s = new Silinmis();

            try
            {
                if (Hesaplar.cboKasa.SelectedIndex == 0)
                {

                    using (SqlDataAdapter hesap = new SqlDataAdapter("SELECT KasaID,Aciklama,OdemeTipi,Tarih,Tutar FROM Kasa WHERE Onay=0 AND Durum=1", Baglanti.bag()))
                    {
                        k.dt.Clear();
                        s.dv.Clear();
                        hp.Clear();
                        hesap.Fill(hp);
                        Hesaplar.dataGridView1.DataSource = hp;
                        hesap.Dispose();
                    }
                }

                else if (Hesaplar.cboKasa.SelectedIndex == 1)
                {
                    using (SqlDataAdapter hesap = new SqlDataAdapter("SELECT KasaID,Aciklama,OdemeTipi,Tarih,Tutar FROM Kasa WHERE Onay=0 AND Durum=0", Baglanti.bag()))
                    {
                        k.db.Clear();
                        s.d.Clear();
                        ht.Clear();
                        hesap.Fill(ht);
                        Hesaplar.dataGridView2.DataSource = ht;
                        hesap.Dispose();
                    }

                }
            }
            catch(Exception hata) { MessageBox.Show(hata.ToString()); }
            }
        }

    }


