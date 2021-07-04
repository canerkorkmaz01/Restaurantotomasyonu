using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Restaurant
{
    public partial class Kasaİslemleri : Form
    {
        public Kasaİslemleri()
        {
            InitializeComponent();
        }
        
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }


        Kasa.KasaIslem ki = new Kasa.KasaIslem();
        Kasa.BilgiOku b = new Kasa.BilgiOku();
        Kasa.HesapKapat h = new Kasa.HesapKapat();
        
        private void Kasaİslemleri_Load(object sender, EventArgs e)
        {
            ki.yukle = this;
            Kasa.Toplam t = new Kasa.Toplam(this);
            t.KasaForm();
            Kasa.KasaOku k = new Kasa.KasaOku();
            k.Form = this;
            Kasa.Grid g = new Kasa.Grid();
            dataGridView1.ClearSelection();
            cboKasa.SelectedIndex = 0;
            b.oku = this;
            h.Kapat = this;
            k.KasaRead();
            k.KOku();
            g.DataGrid = this;
            g.DataGrid2 = this;
            g.grid();
            g.grid2();
            t.KasaCıkan();
            t.ToplamKasa();
            t.KasaForm();

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            ki.yukle = this;

            if (txtAciklama.Text=="")
            {
                MessageBox.Show("Açıklamayı Boş Geçemzsin", "Boş Geçilmeaz",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else if(txtOdeme.Text=="")

            { MessageBox.Show("Tutarı Boş Geçemzsin", "Boş Geçilmeaz", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            else if (txtOdemeSekli.Text=="")

            { MessageBox.Show("Ödeme Şeklini Boş Geçemzsin", "Boş Geçilmeaz", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            else
            {
                ki.Ekle();
                Kasaİslemleri_Load(sender, e);
            }
            
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0 && dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silinecek Veri Seçiniz", "Silmede Hata",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

            else if(txtSilme.Text=="")
            {
                MessageBox.Show("Lütfen Bir Açıklama Girniz...!", "Silmede Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                ki.Sil();
                Kasaİslemleri_Load(sender, e);
            }
            
        }
        
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                h.id = (int)dataGridView2.CurrentRow.Cells[0].Value;
                ki.id = (int)dataGridView2.CurrentRow.Cells[0].Value;
            }
            catch(Exception) {; }
            b.Bilgi1();
        }
        private void dataGridView2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView2.ClearSelection();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                h.id = (int)dataGridView1.CurrentRow.Cells[0].Value;
                ki.id = (int)dataGridView1.CurrentRow.Cells[0].Value;
            }
            catch(Exception) {; }

                b.Bilgi();
        }

        private void Kasaİslemleri_Shown(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0 && dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Güncellenecek Veri Seçiniz", "Güncelleme Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                ki.Güncelle();
                Kasaİslemleri_Load(sender, e);
            }
           
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0 && dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Kapatılacak Veri Seçiniz", "Kapat Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                h.HKapat();
                Kasaİslemleri_Load(sender, e);
            }

        }

        private void btnVerecek_Click(object sender, EventArgs e)
        {
            //Kasa.Grid g = new Kasa.Grid();
            //Kasa.Silinmis s = new Kasa.Silinmis();
            //g.DataGrid = this;
            //g.DataGrid2 = this;
            //s.Sil = this;
            //s.Del = this;
            //s.Silinen();
            //g.grid();
            //g.grid2();
            
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtAciklama.Text = "";
            txtOdeme.Text = "";
            txtOdemeSekli.Text = "";
            txtSilme.Text = "";
        }

        private void btnAlacak_Click(object sender, EventArgs e)
        {
            Kasa.Grid g = new Kasa.Grid();
            Kasa.KapatılanHesaplar h = new Kasa.KapatılanHesaplar();
            g.DataGrid = this;
            g.DataGrid2 = this;
            h.Hesaplar = this;
            h.Hesapp = this;
            h.Hesap();
            g.grid();
            g.grid2();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnGetir_Click(object sender, EventArgs e)
        {
            Kasa.Grid g = new Kasa.Grid();
            Kasa.Silinmis s = new Kasa.Silinmis();
            Kasa.KapatılanHesaplar kh = new Kasa.KapatılanHesaplar();
            Kasa.KasaOku k = new Kasa.KasaOku();
            k.Form = this;
            g.DataGrid = this;
            g.DataGrid2 = this;
            kh.Hesaplar = this;
            kh.Hesapp = this;
            s.d.Clear();
            s.dv.Clear();
            kh.hp.Clear();
            kh.ht.Clear();
            k.dt.Clear();
            k.db.Clear();
            k.KasaRead();
            k.KOku();
            g.grid();
            g.grid2();
            Kasaİslemleri_Load(sender, e);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

       public Kasa.Yazdir sinif = new Kasa.Yazdir(); 
        private void dtpTarih1_ValueChanged(object sender, EventArgs e)
        {
            sinif.dtp1 = dtpTarih1.Value.Date;
         
        }

        private void dtpTarih2_ValueChanged(object sender, EventArgs e)
        {
            sinif.dtp2 = dtpTarih2.Value.Date.AddDays(1);
        
        }


        private void btnYazdır_Click(object sender, EventArgs e)
        {
            RaporYazdir r = new RaporYazdir();
            r.Show();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Menu m = new Menu();
            m.Show();
            this.Dispose();
        }

        private void Kasaİslemleri_FormClosed(object sender, FormClosedEventArgs e)
        {
            Menu m = new Menu();
            m.Show();
            this.Dispose();
        }
    }
 }
