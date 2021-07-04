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
    public partial class RaporYazdir : Form
    {
        Kasaİslemleri sinif = new Kasaİslemleri();
        public RaporYazdir()
        {
            InitializeComponent();
        }

        private void RaporYazdir_Load(object sender, EventArgs e)
        {
           
            Kasaİslemleri k = (Kasaİslemleri)Application.OpenForms["Kasaİslemleri"];
          
            
            try
            {

                if (k.cboKasa.SelectedIndex == 0)
                {
                    // TODO: This line of code loads data into the 'DataSet1.Kasa' table. You can move, or remove it, as needed.
                    this.KasaTableAdapter.Fill(this.DataSet1.Kasa, k.sinif.dtp1, k.sinif.dtp2, Convert.ToBoolean(0));
                    this.reportViewer1.RefreshReport();
                }

                else if (k.cboKasa.SelectedIndex == 1)
                {
                    // TODO: This line of code loads data into the 'DataSet1.Kasa' table. You can move, or remove it, as needed.
                    this.KasaTableAdapter.Fill(this.DataSet1.Kasa, k.sinif.dtp1, k.sinif.dtp2, Convert.ToBoolean(1));
                    this.reportViewer1.RefreshReport();
                }

            }
            catch(Exception) { MessageBox.Show("Lütfen Tarih Aralığı Seçiniz", "Tarih Hatası",MessageBoxButtons.OK,MessageBoxIcon.Information); }  
        }
    }
}
