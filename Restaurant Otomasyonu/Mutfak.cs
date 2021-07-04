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
    public partial class Mutfak : Form
    {
        public Mutfak()
        {
            InitializeComponent();
        }

        private void grid()
        {
            dataGridView2.Columns[0].Visible = false;
            //dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[6].Visible = false;
            dataGridView2.Columns[1].HeaderText = "Masa No";
            dataGridView2.Columns[2].HeaderText = "Ürün Adı";
            dataGridView2.Columns[3].HeaderText = "Fiyatı";
            dataGridView2.Columns[4].HeaderText = "Adeti";
            dataGridView2.Columns[5].HeaderText = "Tutarı";
            dataGridView2.Columns[7].HeaderText = "Durumu";
            dataGridView2.Columns[2].Width = 200;
            dataGridView2.Columns[1].Width = 105;
            

        }

        
        private void MasaNo()
        {
            using (SqlCommand m=new SqlCommand("SELECT MasaID FROM Masalar WHERE Durumu=1 OR Durumu=3 ",Baglanti.bag()))
            {
                cboMasaOku.Items.Clear();
                SqlDataReader oku = m.ExecuteReader();
                
                while (oku.Read())
                {
                    cboMasaOku.Items.Add(oku["MasaID"]).ToString();
                }
            }
        }

        DataTable db = new DataTable();
        private void Getir()
        {
            int cbo = Convert.ToInt32(cboMasaOku.SelectedItem);
            using (SqlDataAdapter g=new SqlDataAdapter("SELECT SiparisID,MasaID,UrunAdi,Fiyati,Adeti,Tutari,Aciklama,Aciklama2 FROM MasaSiparisleri WHERE MasaID=@id AND (Durum=1 OR Durum=3) ",Baglanti.bag()))
            {

                g.SelectCommand.Parameters.AddWithValue("@id",cbo);
                g.Fill(db);
                dataGridView2.DataSource = db;
            }
        }

     
        private void SiparisHazır()
        {
            using (SqlCommand s=new SqlCommand("UPDATE MasaSiparisleri SET Aciklama2=@h WHERE MasaID=@id AND (Durum=1 OR Durum=3)",Baglanti.bag()))
            {
                int cbo = Convert.ToInt32(cboMasaOku.SelectedItem);
                s.Parameters.AddWithValue("@id", cbo);
                s.Parameters.AddWithValue("@h", "Hazır");
                s.ExecuteNonQuery();
            }
        }


        private void Mutfak_Load(object sender, EventArgs e)
        {
            MasaNo();
            cboMasaOku.SelectedIndex = 0;
           
           
        }

        private void btnMüsteri_Click(object sender, EventArgs e)
        {
            
            db.Clear();
            Getir();
            MasaNo();
            grid();
            cboMasaOku.SelectedIndex = 0;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtAciklama.Text = dataGridView2.CurrentRow.Cells[6].Value.ToString();
        }

        private void btnHazır_Click(object sender, EventArgs e)
        {
           
            SiparisHazır();
            db.Clear();
            Getir();
            grid();
        }

        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView2.ClearSelection();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Menu m = new Menu();
            m.Show();
            this.Dispose();
        }

        private void Mutfak_FormClosed(object sender, FormClosedEventArgs e)
        {
            Menu m = new Menu();
            m.Show();
            this.Dispose();
        }
    }
}
