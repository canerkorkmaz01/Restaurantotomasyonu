namespace Restaurant
{
    partial class MasaRapor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DataSet4 = new Restaurant.DataSet4();
            this.MasaSiparisleriBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MasaSiparisleriTableAdapter = new Restaurant.DataSet4TableAdapters.MasaSiparisleriTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MasaSiparisleriBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.DocumentMapWidth = 42;
            reportDataSource1.Name = "DataSet4";
            reportDataSource1.Value = this.MasaSiparisleriBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Restaurant.Report4.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(784, 432);
            this.reportViewer1.TabIndex = 0;
            // 
            // DataSet4
            // 
            this.DataSet4.DataSetName = "DataSet4";
            this.DataSet4.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // MasaSiparisleriBindingSource
            // 
            this.MasaSiparisleriBindingSource.DataMember = "MasaSiparisleri";
            this.MasaSiparisleriBindingSource.DataSource = this.DataSet4;
            // 
            // MasaSiparisleriTableAdapter
            // 
            this.MasaSiparisleriTableAdapter.ClearBeforeFill = true;
            // 
            // MasaRapor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.ClientSize = new System.Drawing.Size(784, 432);
            this.Controls.Add(this.reportViewer1);
            this.Name = "MasaRapor";
            this.Text = "MasaRapor";
            this.Load += new System.EventHandler(this.MasaRapor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataSet4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MasaSiparisleriBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource MasaSiparisleriBindingSource;
        private DataSet4 DataSet4;
        private DataSet4TableAdapters.MasaSiparisleriTableAdapter MasaSiparisleriTableAdapter;
    }
}