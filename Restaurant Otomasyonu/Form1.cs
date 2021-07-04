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
    public partial class Arama : Form
    {
        public Arama()
        {
            InitializeComponent();
        }

        private void Arama_Load(object sender, EventArgs e)
        {
            axCIDv51.Hide();
            axCIDv51.Start();
          
        }
      
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Cihaz : " + axCIDv51.Command("Devicemodel") + "     " + axCIDv51.Command("Serial");
           
        }


        DataTable dt = new DataTable();
        private void axCIDv51_OnCallerID(object sender, Axcidv5callerid.ICIDv5Events_OnCallerIDEvent e)
        {
            using (SqlDataAdapter ıd = new SqlDataAdapter("SELECT * FROM MusteriBilgileri WHERE Telefonu=@id ", Baglanti.bag()))
            {
                ıd.SelectCommand.Parameters.AddWithValue("@id", e.phoneNumber);

               
                SqlDataReader oku = ıd.SelectCommand.ExecuteReader();

               

                PaketServis ps = new PaketServis();

                if (oku.Read())
                {
                    if (oku.GetString(3) == e.phoneNumber)
                    {
                        oku.Close();
                        ıd.Fill(dt);


                        this.Show();
                        label3.Text = "Kayıtlı Çagrı";
                        label2.Text = e.phoneNumber.ToString();
                        
                    }
                }
                else
                {
                    MusteriSiparisleri ms = new MusteriSiparisleri();

                    ms.mkdTel.Text = e.phoneNumber;
                    this.Show();

                    label3.Text = "Bilinmeyen Numara";
                    label2.Text = e.phoneNumber.ToString();
                    
                }
                oku.Close();
                ıd.Dispose();
            }
        }

   
        private void Arama_Click(object sender, EventArgs e)
        {
            
            PaketServis ps = new PaketServis();
            MusteriSiparisleri ms = new MusteriSiparisleri();
            if (label3.Text == "Kayıtlı Çagrı")
            {
                
                ps.dataGridView2.DataSource = dt;
                ps.gridview();

                ps.Show();
               
                this.Dispose();
            }

            else if(label3.Text == "Bilinmeyen Numara")
            {
                ps.Show();
                ms.Show();
                ms.btnGüncelle.Enabled = false;
                ms.btnSil.Enabled = false;
                this.Dispose();
            }
        }
    }
}
