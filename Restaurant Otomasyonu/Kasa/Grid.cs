using System;
using System.Windows.Forms;

namespace Restaurant.Kasa
{
    
    public class Grid
    {
       
        public Kasaİslemleri DataGrid { get; set; }
        public Kasaİslemleri DataGrid2 { get; set; }

        //public void Gizle(ref DataGridView d)
        //{
        //    d.Columns[0].Visible = false;
        //}
        public void grid()
        {
            try
            {
                DataGrid.dataGridView1.Columns[0].Visible = false;
                DataGrid.dataGridView1.Columns[1].Width = 150;
                DataGrid.dataGridView1.Columns[2].Width = 75;
                DataGrid.dataGridView1.Columns[3].Width = 90;
                DataGrid.dataGridView1.Columns[4].Width = 90;
                DataGrid.dataGridView1.Columns[1].HeaderText = "Açıklama";
                DataGrid.dataGridView1.Columns[2].HeaderText = "Ödeme Türü";
                DataGrid.dataGridView1.Columns[3].HeaderText = "Tarih";
                DataGrid.dataGridView1.Columns[4].HeaderText = "Tutar";
            }
            catch(Exception hata) { MessageBox.Show(hata.ToString()); }
       }

        public void grid2()
        {
            try
            {
                DataGrid2.dataGridView2.Columns[0].Visible = false;
                DataGrid2.dataGridView2.Columns[1].Width = 150;
                DataGrid2.dataGridView2.Columns[2].Width = 75;
                DataGrid2.dataGridView2.Columns[3].Width = 90;
                DataGrid2.dataGridView2.Columns[4].Width = 90;
                DataGrid2.dataGridView2.Columns[1].HeaderText = "Açıklama";
                DataGrid2.dataGridView2.Columns[2].HeaderText = "Ödeme Türü";
                DataGrid2.dataGridView2.Columns[3].HeaderText = "Tarih";
                DataGrid2.dataGridView2.Columns[4].HeaderText = "Tutar";
            }
            catch(Exception hata) { MessageBox.Show(hata.ToString()); }
        }
    }
}
