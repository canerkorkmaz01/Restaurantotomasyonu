using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Restaurant
{
    class Islem:IDisposable
    {
        public Islem()
        {
        }


        public int IslemId { get; set; }
        public static int PersonelID { get; set; }
        public DateTime Tarih { get; set; }
        public string IslemAdi { get; set; }


        public Islem(int IslemId)
        {
            this.IslemAdi = IslemAdi;
        }


      
        public void Kaydet()
        {
            try
            {
                using(SqlCommand c=new SqlCommand("INSERT INTO Islemler(PersonelID, Tarih, IslemAdi) VALUES(@1,@2,@3)",Baglanti.bag()))
                {
                    c.Parameters.AddWithValue("@1", PersonelID);
                     c.Parameters.AddWithValue("@2",Tarih);
                     c.Parameters.AddWithValue("@3",IslemAdi);
                     c.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                ;
            }
        }

        public void Guncelle()
        {
            try
            {
                using (SqlCommand c = new SqlCommand("UPDATE Islemler SET PersonelId=@1, Tarih=@2, IslemAdi=@3 WHERE IslemId=@0",Baglanti.bag()))
                {
                    c.Parameters.AddWithValue("@0", IslemAdi);
                    c.Parameters.AddWithValue("@1", PersonelID);
                    c.Parameters.AddWithValue("@2", Tarih);
                    c.Parameters.AddWithValue("@3", IslemAdi);
                    c.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                ;
            }
        }

        public void Sil()
        {
             try
            {

                using (SqlCommand c = new SqlCommand("DELETE FROM Islemler WHERE IslemId=@0", Baglanti.bag()))
                {
                    c.Parameters.AddWithValue("@0", IslemAdi);
                    c.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                ;
            }
        }

        public void Dispose()
        {
            Tarih = new DateTime();
            IslemAdi = string.Empty;
            GC.SuppressFinalize(this);
        }

    }
}
