using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Restaurant
{
    public partial class Yonetim : Form
    {
        public Yonetim()
        {
            InitializeComponent();
            //oku();
        }

       //string sifre;
       // public void oku()
       // {
       //     using (SqlCommand cmd = new SqlCommand("select Sifre from personeller", Baglanti.bag()))
       //     {
              
       //         SqlDataReader oku = cmd.ExecuteReader();
                 
       //         while (oku.Read())
       //         {
       //            sifre=(string)(oku["Sifre"]);
       //         }
       //         //if (txtPass.Text != dt.)
       //         //{ MessageBox.Show("Hatalı Şifre ", "Şifre Hatası", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
       //     }
           
       // }

        public void read()
        {
            using (SqlCommand cmd = new SqlCommand("SELECT KullaniciAdi FROM personeller WHERE TipId >=1 AND TipId <=5 ", Baglanti.bag()))
            {
                SqlDataReader oku = cmd.ExecuteReader();

                while (oku.Read())
                {
                    cbUser.Items.Add(oku["KullaniciAdi"]);
                }
                oku.Close();

            }
            cbUser.SelectedIndex = 0;
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {

                CPersoneller cp = new CPersoneller();
                cp.pas = txtPass.Text;
                cp.per = cbUser.Text;
                cp.Personeler();

         


        }

        private void Yonetim_Load(object sender, EventArgs e)
        {
           
            //oku();
            read();

        }

        private void Yonetim_FormClosing(object sender, FormClosingEventArgs e)
        {
               if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Programı Kapatmak İstediginizden Emin misiniz?",
                               "Kullanıc Girişi",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information) == DialogResult.Yes)
                              
                {
                    Environment.Exit(0);
                    using(Islem i =new Islem())
                    {
                        i.IslemAdi = "Program kapatıldı.";
                        i.Tarih = DateTime.Now;
                        i.Kaydet();
                    }
                }
               

                else
                    e.Cancel = true;
            }
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
             this.Close();
        }

        } 
    }