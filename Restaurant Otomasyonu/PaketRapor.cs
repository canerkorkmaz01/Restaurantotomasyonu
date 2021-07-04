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
    public partial class PaketRapor : Form
    {
        public PaketRapor()
        {
            InitializeComponent();
        }

        private void PaketRapor_Load(object sender, EventArgs e)
        {
            var paket = (RaporHazirla)Application.OpenForms["RaporHazirla"];
            try
            {
                if (paket.rapor.data=="btnZraporu")
                {
                    this.PaketServisTableAdapter.FillBy(this.DataSet3.PaketServis, Convert.ToDateTime(DateTime.Now.ToShortDateString()));
                    this.reportViewer1.RefreshReport();
                    paket.rapor.data = null;
                }

                else
                {
                    // TODO: This line of code loads data into the 'DataSet3.PaketServis' table. You can move, or remove it, as needed.
                    this.PaketServisTableAdapter.Paket(this.DataSet3.PaketServis, paket.rapor.dtp1, paket.rapor.dtp2);
                    this.reportViewer1.RefreshReport();

                }

            }
            catch (Exception) { MessageBox.Show("İki Tarih Arasında Bir Tarih Giriniz..", "Tarih Hatası", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }
    }
}
