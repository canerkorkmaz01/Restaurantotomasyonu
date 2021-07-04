using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Restaurant
{

  public class CPersoneller
    {
        public string per { get; set; }
        public string pas { get; set; }
        public static  byte tab { get; set; }
       
     //   public Menu yetki { get; set; }
        public static byte Numara { get; set; }

        public CPersoneller()
        {
         //   yetki = new Menu(3);
        }
        //SESİN GELMİYOR

            //seninde sesin gelmiyor
            //gelmiyor senin de sesin gelmiyor 
            //şidi, kurucu metoda parametre verecez. 
            //istediğin değeri parametre verebilirsin.
            //bunun statik olması ya da formu bu class içinde show edilmesi lazım ikinsinden biri

        public void Personeler()
        {
            using (SqlCommand cmd = new SqlCommand("select * From personeller where KullaniciAdi=@k and Sifre=@p", Baglanti.bag()))
            {
                cmd.Parameters.AddWithValue("@k", per);
                cmd.Parameters.AddWithValue("@p", pas);
                bool yonetici = false;
                bool yardımcı = false;
                bool personel = true;
                SqlDataReader oku = cmd.ExecuteReader();

                if (oku.Read())
                {

                    if (oku.GetInt32(8) == 1)
                    {
                        yonetici = true;

                        MessageBox.Show("Yönetici Girişi Başarılı", "Yönetici Girişi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       

                    }

                    else if (oku.GetInt32(8) == 2)
                    {
                        yardımcı = true;
                        MessageBox.Show("Yönetici Yardımcısı Girişi Başarılı", "Perosnel Girişi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
                        
                    }

                    else if (oku.GetInt32(8) == 3)
                    {
                        personel = false;
                        MessageBox.Show("Personel Girişi Başarılı", "Perosnel Girişi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
                    }

                    Islem.PersonelID = oku.GetInt32(0);
                    using (Islem i = new Islem())
                    {
                        i.IslemAdi = "Kullanıcı Girişi Yapıldı!";
                        i.Tarih = DateTime.Now;
                        i.Kaydet();
                    }

                    Yonetim.ActiveForm.Hide();

                    Menu m = new Menu();
                    m.Show();
                    Arama a = new Arama();
                    a.Show();
                    a.Hide();

                    try
                    {
                        if (yonetici)
                        {

                            Menu m2 = (Menu)Application.OpenForms["Menu"];
                            Menu.ActiveForm.Controls["btnAyar"].Enabled = true;
                        }

                       else if (yardımcı)
                        {
                            Menu m2 = (Menu)Application.OpenForms["Menu"];
                            m2.btnAyar.Enabled= true;
                            tab = 1;
                        }
                    
                        else if(personel)
                        {
                            Menu frmMenu = new Menu();
                            frmMenu.Show();
                            // yetki.sa = 3;
                            Numara = 3;
                          
                        }
                            
                    }
                    catch (Exception) {; }

                }

                else if (pas == "") 
                { 
                    MessageBox.Show("Şifreyi Boş Geçemezsin", "Şifre Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Hatalı Şifre Girişi", "Şifre Hatası", MessageBoxButtons.OK, MessageBoxIcon.Stop); 

                }

            }
        }
    }
}
