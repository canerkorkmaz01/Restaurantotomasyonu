using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant.Kasa
{
   public class Toplam
    {
        private Kasaİslemleri Form;
        public Toplam(Kasaİslemleri frm)
        {
            Form = frm;
        }
      //  public Kasaİslemleri Form { get; set; }

        //public  Kasaİslemleri Form { get; set; }

        //public Kasaİslemleri Top { get; set; }

        float giren = 0f;
        float cikan = 0f;
        float toplam = 0f;

        public void KasaForm()
        {
            for (int i = 0; i < Form.dataGridView1.Rows.Count; i++)
            {
                 giren+= Convert.ToSingle(Form.dataGridView1.Rows[i].Cells[4].Value);
            }
                 Form.label6.Text = giren.ToString("N2");
        }

        public void KasaCıkan()
        {
            for (int i = 0; i < Form.dataGridView2.Rows.Count; i++)
            {
                cikan += Convert.ToSingle(Form.dataGridView2.Rows[i].Cells[4].Value);
            }
              Form.label9.Text = cikan.ToString("N2");
        }

        public void ToplamKasa()
        {
            toplam = giren - cikan;
           Form.label11.Text = toplam.ToString();
        }

    }
}
