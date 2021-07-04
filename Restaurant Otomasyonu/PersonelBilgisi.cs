using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Restaurant
{
    class PersonelBilgisi
    {
        private int personel;

        public string Ad { get; set; }
        public string Soyad{ get; set; }
        public string Görevi { get; set; }


        public void islemOku()
        {
            using (SqlCommand i=new SqlCommand("SELECT PersonelID FROM Islemler ",Baglanti.bag()))
            {
                SqlDataReader oku = i.ExecuteReader();

                while(oku.Read())
                {
                    personel = (int)oku["PersonelID"];
                }
                i.Dispose();
                oku.Close();
            }
        }

        public void Personel()
        {
            using (SqlCommand p = new SqlCommand("SELECT Adi,Soyadi,Gorevi FROM Personeller WHERE Personelid=@p ", Baglanti.bag()))
            {
                p.Parameters.AddWithValue("@p",personel);

                SqlDataReader oku = p.ExecuteReader();

                while (oku.Read())
                {
                    Ad = oku["Adi"].ToString();
                    Soyad = oku["Soyadi"].ToString();
                    Görevi = oku["Gorevi"].ToString();
                }
                p.Dispose();
                oku.Close();

            }
        }

    }
}
