using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace Restaurant.YönetimPaneli
{
   public class YöneticiBilgileri
    {
        private YonetimPaneli Yetki;

         public YöneticiBilgileri (YonetimPaneli Yetkili )
        {
            Yetki = Yetkili;
        }

      public  DataTable dt = new DataTable();
        public void YetkiliBilgileri() // Yönetici Bilgilerini Göstermek İçin
        {
            using (SqlDataAdapter yk= new SqlDataAdapter(@"SELECT Personelid,Adi,Soyadi,
                     Gorevi, KullaniciAdi, Sifre, Tarih, TipId,Telefon FROM
                     Personeller WHERE TipId=1 ", Baglanti.bag()))
            {
                
                yk.Fill(dt);
                Yetki.dataGridView2.DataSource = dt;
            }
            Yetki.dataGridView2.Columns[0].Visible = false;
            Yetki.dataGridView2.Columns[1].HeaderText = "ADI";
            Yetki.dataGridView2.Columns[2].HeaderText = "SOYADI";
            Yetki.dataGridView2.Columns[3].HeaderText = "GÖREVİ";
            Yetki.dataGridView2.Columns[4].HeaderText = "KULLANICI ADI";
            Yetki.dataGridView2.Columns[5].HeaderText = "ŞİFRE";
            Yetki.dataGridView2.Columns[6].HeaderText = "TARİH";
            Yetki.dataGridView2.Columns[7].HeaderText = "YETKİ TİPİ";
            Yetki.dataGridView2.Columns[8].HeaderText = "TELEFON";
        }


    }
}
