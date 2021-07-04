using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Restaurant
{
    public class yazdır
    {
        public PaketServis Nesne { get; set; }

        public string Adi { get; set; }

        public string Soyadi { get; set; }

        public string Telefon { get; set; }

        public DateTime Tarih { get; set; }

        public string Adres { get; set; }

        public int id { get; set; }

        public void MüsteriBilgileri()
        {
            
            using (SqlCommand m = new SqlCommand(@"SELECT MusteriAdi,MusteriSoyadi,Telefonu,Tarih,Adresi FROM
            MusteriBilgileri WHERE MusteriID=@id AND Durum=1", Baglanti.bag()))
            {
                m.Parameters.AddWithValue("@id", Müsteri.MusteriID);

                SqlDataReader oku = m.ExecuteReader();
                while (oku.Read())
                {
                    Adi = oku["MusteriAdi"].ToString();

                    Soyadi = oku["MusteriSoyadi"].ToString();

                    Telefon = oku["Telefonu"].ToString();

                    Tarih = Convert.ToDateTime(oku["Tarih"]);

                    Adres = oku["Adresi"].ToString();
                }

            }
        }
    }
}

