using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant
{
    public partial class MasaRapor : Form
    {
        public MasaRapor()
        {
            InitializeComponent();
        }

        private void MasaRapor_Load(object sender, EventArgs e)
        {

                 
            var Masa = (RaporHazirla)Application.OpenForms["RaporHazirla"];

            try
            {
                if (Masa.rapor.data == "btnZraporu")
                {
                    this.MasaSiparisleriTableAdapter.FillBy(this.DataSet4.MasaSiparisleri,Convert.ToDateTime (DateTime.Now.ToShortDateString()));
                    this.reportViewer1.RefreshReport();
                    Masa.rapor.data = null;
                }
                
                else 
                {
                    
                    // TODO: This line of code loads data into the 'DataSet4.MasaSiparisleri' table. You can move, or remove it, as needed.
                    this.MasaSiparisleriTableAdapter.Fill(this.DataSet4.MasaSiparisleri, Masa.rapor.dtp1, Masa.rapor.dtp2);
                    this.reportViewer1.RefreshReport();
                    
                }
               
            }
            catch (Exception) { MessageBox.Show("İki Tarih Arasında Bir Tarih Giriniz..","Tarih Hatası",MessageBoxButtons.OK,MessageBoxIcon.Information); }
        }
    }
}
