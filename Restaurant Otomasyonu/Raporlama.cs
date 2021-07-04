using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Restaurant
{
    public partial class SatisRapor : Form
    {
        public SatisRapor()
        {
            InitializeComponent();
        }
       
        private void Raporlama_Load(object sender, EventArgs e)
        {
            var frapor= (RaporHazirla)Application.OpenForms["RaporHazirla"];

            try
            {
                if (frapor.rapor.data == "btnZraporu")
                {
                    this.SatislarTableAdapter.FillBy(this.DataSet2.Satislar, Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                    this.reportViewer1.RefreshReport();
                    frapor.rapor.data = null;
                }
                else
                {
                    // // TODO: This line of code loads data into the 'DataSet2.Satislar' table. You can move, or remove it, as needed.
                    this.SatislarTableAdapter.Fill(this.DataSet2.Satislar, frapor.rapor.dtp1, frapor.rapor.dtp2);
                    this.reportViewer1.RefreshReport();
                }
            }
            catch (Exception) { MessageBox.Show("İki Tarih Arasında Bir Tarih Giriniz..", "Tarih Hatası", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }
    }
}
