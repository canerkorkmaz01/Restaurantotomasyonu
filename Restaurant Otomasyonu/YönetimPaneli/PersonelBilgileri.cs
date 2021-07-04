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
    public class PersonelBilgileri
    {

        private YonetimPaneli Form;
        public PersonelBilgileri(YonetimPaneli frm)
        {
            Form = frm;
        }
        public void PersonelOku()// Personel Tablosunu Okumak İçin Kullanılır

        {
            DataTable dt = new DataTable();
            try
            { 
           using (SqlDataAdapter oku = new SqlDataAdapter(@"SELECT Personelid,Adi,Soyadi,
                     Gorevi,KullaniciAdi,Sifre,Tarih,TipId,Telefon FROM Personeller
                     WHERE  TipId > =2 AND TipId <=5 ", Baglanti.bag()))
            {
                
                oku.Fill(dt);
               Form.dataGridView1.DataSource = dt;
            }
                Form. dataGridView1.Columns[0].Visible = false;
                Form.dataGridView1.Columns[1].HeaderText = "ADI";
                Form.dataGridView1.Columns[2].HeaderText = "SOYADI";
                Form.dataGridView1.Columns[3].HeaderText = "GÖREVİ";
                Form.dataGridView1.Columns[4].HeaderText = "KULLANICI ADI";
                Form.dataGridView1.Columns[5].HeaderText = "SİFRE";
                Form.dataGridView1.Columns[6].HeaderText = "TARİH";
                Form.dataGridView1.Columns[7].HeaderText = "YETKİ";
                Form.dataGridView1.Columns[8].HeaderText = "TELEFON";


            }
            catch (Exception hata) { MessageBox.Show(hata.ToString()); }
        }

        public void Arama()// Personel Aramak İçin Kullanılır
        {
            try
            {
                using (SqlDataAdapter ara = new SqlDataAdapter(@"SELECT Adi,Soyadi,
                     Gorevi, KullaniciAdi, Sifre, Tarih, TipId FROM Personeller
                     WHERE Adi LIKE '%' + @a+ '%' ", Baglanti.bag()))
                {
                    if (Form.textBox1.Text.Trim() == "") { PersonelOku(); }

                    else
                    {
                        ara.SelectCommand.Parameters.AddWithValue("@a", Form.textBox1.Text);

                        DataTable db = new DataTable();
                        ara.Fill(db);
                        Form.dataGridView1.DataSource = db;

                    }
                }
            }
            catch (Exception hata) { MessageBox.Show(hata.ToString()); }
        }
    }
}
 
