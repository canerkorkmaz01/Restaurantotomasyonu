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
     public class HesapKapat
    {
        public Kasaİslemleri Kapat { get; set; }

        

        public int id { get; set; }


        public void HKapat()
        {
            try
            {

                using (SqlCommand h = new SqlCommand("UPDATE Kasa SET Onay=0 WHERE KasaID=@id", Baglanti.bag()))
                {

                    h.Parameters.AddWithValue("@id", id);
                    h.ExecuteNonQuery();
                    h.Dispose();

                }
            }
            catch (Exception hata) { MessageBox.Show(hata.ToString()); }
       }
    }
}
