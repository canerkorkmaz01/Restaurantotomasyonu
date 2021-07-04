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
    public partial class Menu : Form
    {


        public Menu()
        {
            InitializeComponent();
           
        }
        
        //private void Yetkilendirme()
        //{
        //    using (SqlCommand s = new SqlCommand("SELECT * FROM PaketServis Durum WHERE Durum=1 ", Baglanti.bag()))
        //    {
        //        SqlDataReader oku = s.ExecuteReader();

        //        if (oku.Read())
        //        {
        //            if (oku.GetByte(7) == 1)
        //            {
        //                Bitmap b = new Bitmap("dosya/paket.png");
        //                button3.BackgroundImage = b;
        //                button3.BackgroundImageLayout = ImageLayout.Stretch;
        //            }

        //            else if (oku.GetByte(7) == 0)
        //            {
        //                Bitmap b = new Bitmap(Properties.Resources.paket_servis);
        //                button3.BackgroundImage = b;
        //                button3.BackgroundImageLayout = ImageLayout.Stretch;
        //            }
        //            s.Dispose();
        //            oku.Close();
        //        }
        //    }
        //}

        private void Siparis()
        {
            using (SqlCommand s=new SqlCommand("SELECT * FROM PaketServis Durum WHERE Durum=1 ",Baglanti.bag()))
            {
                SqlDataReader oku = s.ExecuteReader();

                if (oku.Read())
                {
                    if (oku.GetByte(7)==1)
                    {
                        Bitmap b = new Bitmap("dosya/paket.png");
                        button3.BackgroundImage = b;
                        button3.BackgroundImageLayout = ImageLayout.Stretch;
                    }

                 else if (oku.GetByte(7) == 0)
                    {
                        Bitmap b = new Bitmap(Properties.Resources.paket_servis);
                        button3.BackgroundImage = b;
                        button3.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    s.Dispose();
                    oku.Close();
                }
            }
        }


        private void Menu_Load(object sender, EventArgs e)
        {
            Siparis();

            //if (sa==3)
            //{
            //    MessageBox.Show(sa.ToString());

            //    btnAyar.Enabled = false;
            //}
           
        }

        private void btnAyar_Click(object sender, EventArgs e)
        {
            YonetimPaneli yp = new YonetimPaneli();
            yp.Show();
            yp.tabPage2.Hide();
            this.Dispose();
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {

            Yonetim y = new Yonetim();
            y.Show();
            this.Dispose();

        }


        private void btnMasalar_Click(object sender, EventArgs e)
        {

            Masalar masa = new Masalar();
            masa.Show();
            this.Dispose();


        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Yonetim y = new Yonetim();
            this.Hide();
            y.Show();
            this.Dispose();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void btnRezervasyon_Click(object sender, EventArgs e)
        {
            Rezervasyon r = new Rezervasyon();
            r.Show();
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {


            PaketServis p = new PaketServis();
            p.Show();
            this.Dispose();
        }

        private void btnMutfak_Click(object sender, EventArgs e)
        {
            Mutfak m = new Mutfak();
            m.Show();
            this.Dispose();
        }

        private void btnSatis_Click(object sender, EventArgs e)
        {
            Satis s = new Satis();
            s.Show();
            this.Dispose();
        }

        private void btnKasa_Click(object sender, EventArgs e)
        {
            Kasaİslemleri k = new Kasaİslemleri();
            k.Show();
            this.Dispose();
        }

        private void btnRapor_Click(object sender, EventArgs e)
        {
            RaporHazirla r = new RaporHazirla();
            r.Show();
            this.Dispose();

        }
    }
}