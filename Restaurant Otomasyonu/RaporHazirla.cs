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
    public partial class RaporHazirla : Form
    {
        public RaporHazirla()
        {
            InitializeComponent();

            rapor = new RaporHazirlama();
        }


        public RaporHazirlama rapor { get; set; }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            rapor.dtp1 = dateTimePicker1.Value.Date;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

            rapor.dtp2 = dateTimePicker2.Value.AddDays(1); 
        }

       
       

        private void RaporHazirla_Load(object sender, EventArgs e)
        {
            cboSecim.SelectedIndex = 0;
          
        }

        private void btnZraporu_Click(object sender, EventArgs e)
        {

            if (cboSecim.SelectedIndex==0)
            {

                SatisRapor r = new SatisRapor();
                rapor.data = btnZraporu.Name;
                r.Show();
            }
          else if(cboSecim.SelectedIndex == 1)
            {
                MasaRapor mr = new MasaRapor();
                rapor.data = btnZraporu.Name;
                mr.Show();
            }
            else if (cboSecim.SelectedIndex == 2)
            {
                
                PaketRapor p = new PaketRapor();
                rapor.data = btnZraporu.Name;
                p.Show();
            }
        }

        private void RaporHazirla_FormClosed(object sender, FormClosedEventArgs e)
        {
            Menu m = new Menu();
            m.Show();
            this.Dispose();
        }

        private void btnAylik_Click(object sender, EventArgs e)
        {
            if (cboSecim.SelectedIndex==0)
            {
                SatisRapor s = new SatisRapor();
                s.Show();
                
            }
            
            else if(cboSecim.SelectedIndex == 1)
            {
                MasaRapor m = new MasaRapor();
                m.Show();

            }
            else if(cboSecim.SelectedIndex==2)
            {
                PaketRapor p = new PaketRapor();
                p.Show();
            }
        }
    }
}
