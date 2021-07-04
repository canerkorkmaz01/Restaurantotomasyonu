using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Restaurant
{
    class SiparisBilgisi
    {

        public static int Siparis { get; private set; }
        public static int Siparisno { get; private set; }

        public void SiparisNo()// Siparis No Okuma 
        {
            
            try
            {
                using (SqlCommand s = new SqlCommand("SELECT RezervasyonID FROM MasaSiparisleri WHERE MasaID=@id AND  Durum=3", Baglanti.bag()))
                {
                    s.Parameters.AddWithValue("@id", Araclar.MasaId);

                    SqlDataReader oku = s.ExecuteReader();

                    while (oku.Read())
                    {
                        Siparis = (int)oku["RezervasyonID"];
                    }
                    s.Dispose();
                    oku.Close();
                }
            }
            catch (Exception)
            {; }
        }
    }
}

