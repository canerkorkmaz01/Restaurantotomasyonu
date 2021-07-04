using System;
using System.Windows.Forms;

namespace Restaurant.Kasa
{
    public class BilgiOku
    {
        public Kasaİslemleri oku { get; set; }


        private void Nesne()
        {
            try
            {
                oku.txtAciklama.Text = "";
                oku.txtOdeme.Text = "";
                oku.txtOdemeSekli.Text = "";
                oku.dtpTarih.Text = "";
            }
            catch(Exception hata) { MessageBox.Show(hata.ToString()); }
       }
        public void Bilgi()
        {
            Nesne();
            try
            {
                oku.txtAciklama.Text = oku.dataGridView1.CurrentRow.Cells[1].Value.ToString();
                oku.txtOdemeSekli.Text = oku.dataGridView1.CurrentRow.Cells[2].Value.ToString();
                oku.dtpTarih.Text = oku.dataGridView1.CurrentRow.Cells[3].Value.ToString();
                oku.txtOdeme.Text = oku.dataGridView1.CurrentRow.Cells[4].Value.ToString();
            }
            catch (Exception) { MessageBox.Show("Lütfen Veri Seçiniz","Seçili Nesne Bulunamadı",MessageBoxButtons.OK,MessageBoxIcon.Information); }
        }

        public void Bilgi1()
        {
            Nesne();

            try
            {
                oku.txtAciklama.Text = oku.dataGridView2.CurrentRow.Cells[1].Value.ToString();
                oku.txtOdemeSekli.Text = oku.dataGridView2.CurrentRow.Cells[2].Value.ToString();
                oku.dtpTarih.Text = oku.dataGridView2.CurrentRow.Cells[3].Value.ToString();
                oku.txtOdeme.Text = oku.dataGridView2.CurrentRow.Cells[4].Value.ToString();
            }

            catch (Exception) { MessageBox.Show("Lütfen Veri Seçiniz", "Seçili Nesne Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }
    }
}
