namespace KutuphaneOtomasyonSistemi
{
    partial class raporlamapanelics
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(raporlamapanelics));
            this.tblpersonelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.kutuphaneOtomasyonDataSet = new KutuphaneOtomasyonSistemi.KutuphaneOtomasyonDataSet();
            this.tblhareketBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblkitaptakipBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tblmusteriBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnmusteri = new System.Windows.Forms.Button();
            this.btnpersonel = new System.Windows.Forms.Button();
            this.btnhareket = new System.Windows.Forms.Button();
            this.btnkitaptakip = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer3 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer4 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tblmusteriTableAdapter = new KutuphaneOtomasyonSistemi.KutuphaneOtomasyonDataSetTableAdapters.tblmusteriTableAdapter();
            this.tblpersonelTableAdapter = new KutuphaneOtomasyonSistemi.KutuphaneOtomasyonDataSetTableAdapters.tblpersonelTableAdapter();
            this.tblhareketTableAdapter = new KutuphaneOtomasyonSistemi.KutuphaneOtomasyonDataSetTableAdapters.tblhareketTableAdapter();
            this.tblkitaptakipTableAdapter = new KutuphaneOtomasyonSistemi.KutuphaneOtomasyonDataSetTableAdapters.tblkitaptakipTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.tblpersonelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kutuphaneOtomasyonDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblhareketBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblkitaptakipBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblmusteriBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // tblpersonelBindingSource
            // 
            this.tblpersonelBindingSource.DataMember = "tblpersonel";
            this.tblpersonelBindingSource.DataSource = this.kutuphaneOtomasyonDataSet;
            // 
            // kutuphaneOtomasyonDataSet
            // 
            this.kutuphaneOtomasyonDataSet.DataSetName = "KutuphaneOtomasyonDataSet";
            this.kutuphaneOtomasyonDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tblhareketBindingSource
            // 
            this.tblhareketBindingSource.DataMember = "tblhareket";
            this.tblhareketBindingSource.DataSource = this.kutuphaneOtomasyonDataSet;
            // 
            // tblkitaptakipBindingSource
            // 
            this.tblkitaptakipBindingSource.DataMember = "tblkitaptakip";
            this.tblkitaptakipBindingSource.DataSource = this.kutuphaneOtomasyonDataSet;
            // 
            // tblmusteriBindingSource
            // 
            this.tblmusteriBindingSource.DataMember = "tblmusteri";
            this.tblmusteriBindingSource.DataSource = this.kutuphaneOtomasyonDataSet;
            // 
            // btnmusteri
            // 
            this.btnmusteri.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnmusteri.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnmusteri.Location = new System.Drawing.Point(24, 13);
            this.btnmusteri.Name = "btnmusteri";
            this.btnmusteri.Size = new System.Drawing.Size(161, 29);
            this.btnmusteri.TabIndex = 58;
            this.btnmusteri.Text = "Müşteriler";
            this.btnmusteri.UseVisualStyleBackColor = true;
            this.btnmusteri.Click += new System.EventHandler(this.btnmusteri_Click);
            // 
            // btnpersonel
            // 
            this.btnpersonel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnpersonel.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnpersonel.Location = new System.Drawing.Point(191, 13);
            this.btnpersonel.Name = "btnpersonel";
            this.btnpersonel.Size = new System.Drawing.Size(161, 29);
            this.btnpersonel.TabIndex = 59;
            this.btnpersonel.Text = "Personeller";
            this.btnpersonel.UseVisualStyleBackColor = true;
            this.btnpersonel.Click += new System.EventHandler(this.btnpersonel_Click);
            // 
            // btnhareket
            // 
            this.btnhareket.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnhareket.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnhareket.Location = new System.Drawing.Point(358, 13);
            this.btnhareket.Name = "btnhareket";
            this.btnhareket.Size = new System.Drawing.Size(161, 29);
            this.btnhareket.TabIndex = 60;
            this.btnhareket.Text = "Hareketler";
            this.btnhareket.UseVisualStyleBackColor = true;
            this.btnhareket.Click += new System.EventHandler(this.btnhareket_Click);
            // 
            // btnkitaptakip
            // 
            this.btnkitaptakip.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnkitaptakip.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnkitaptakip.Location = new System.Drawing.Point(525, 13);
            this.btnkitaptakip.Name = "btnkitaptakip";
            this.btnkitaptakip.Size = new System.Drawing.Size(161, 29);
            this.btnkitaptakip.TabIndex = 61;
            this.btnkitaptakip.Text = "Kitap Takip";
            this.btnkitaptakip.UseVisualStyleBackColor = true;
            this.btnkitaptakip.Click += new System.EventHandler(this.btnkitaptakip_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(667, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "_________________________________________________________________________________" +
    "_____________________________";
            // 
            // reportViewer2
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.tblpersonelBindingSource;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "KutuphaneOtomasyonSistemi.Report2.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(24, 73);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.ServerReport.BearerToken = null;
            this.reportViewer2.Size = new System.Drawing.Size(662, 347);
            this.reportViewer2.TabIndex = 64;
            // 
            // reportViewer3
            // 
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.tblhareketBindingSource;
            this.reportViewer3.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer3.LocalReport.ReportEmbeddedResource = "KutuphaneOtomasyonSistemi.Report3.rdlc";
            this.reportViewer3.Location = new System.Drawing.Point(24, 73);
            this.reportViewer3.Name = "reportViewer3";
            this.reportViewer3.ServerReport.BearerToken = null;
            this.reportViewer3.Size = new System.Drawing.Size(662, 347);
            this.reportViewer3.TabIndex = 65;
            // 
            // reportViewer4
            // 
            reportDataSource3.Name = "DataSet1";
            reportDataSource3.Value = this.tblkitaptakipBindingSource;
            this.reportViewer4.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer4.LocalReport.ReportEmbeddedResource = "KutuphaneOtomasyonSistemi.Report4.rdlc";
            this.reportViewer4.Location = new System.Drawing.Point(24, 73);
            this.reportViewer4.Name = "reportViewer4";
            this.reportViewer4.ServerReport.BearerToken = null;
            this.reportViewer4.Size = new System.Drawing.Size(662, 347);
            this.reportViewer4.TabIndex = 66;
            // 
            // reportViewer1
            // 
            reportDataSource4.Name = "DataSet1";
            reportDataSource4.Value = this.tblmusteriBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "KutuphaneOtomasyonSistemi.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(24, 73);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(662, 347);
            this.reportViewer1.TabIndex = 67;
            // 
            // tblmusteriTableAdapter
            // 
            this.tblmusteriTableAdapter.ClearBeforeFill = true;
            // 
            // tblpersonelTableAdapter
            // 
            this.tblpersonelTableAdapter.ClearBeforeFill = true;
            // 
            // tblhareketTableAdapter
            // 
            this.tblhareketTableAdapter.ClearBeforeFill = true;
            // 
            // tblkitaptakipTableAdapter
            // 
            this.tblkitaptakipTableAdapter.ClearBeforeFill = true;
            // 
            // raporlamapanelics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 435);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.reportViewer4);
            this.Controls.Add(this.reportViewer3);
            this.Controls.Add(this.reportViewer2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnkitaptakip);
            this.Controls.Add(this.btnhareket);
            this.Controls.Add(this.btnpersonel);
            this.Controls.Add(this.btnmusteri);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "raporlamapanelics";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Raporlama Paneli";
            this.Load += new System.EventHandler(this.raporlamapanelics_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tblpersonelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kutuphaneOtomasyonDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblhareketBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblkitaptakipBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tblmusteriBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnmusteri;
        private System.Windows.Forms.Button btnpersonel;
        private System.Windows.Forms.Button btnhareket;
        private System.Windows.Forms.Button btnkitaptakip;
        private System.Windows.Forms.Label label1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer3;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer4;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private KutuphaneOtomasyonDataSet kutuphaneOtomasyonDataSet;
        private System.Windows.Forms.BindingSource tblmusteriBindingSource;
        private KutuphaneOtomasyonDataSetTableAdapters.tblmusteriTableAdapter tblmusteriTableAdapter;
        private System.Windows.Forms.BindingSource tblpersonelBindingSource;
        private KutuphaneOtomasyonDataSetTableAdapters.tblpersonelTableAdapter tblpersonelTableAdapter;
        private System.Windows.Forms.BindingSource tblhareketBindingSource;
        private KutuphaneOtomasyonDataSetTableAdapters.tblhareketTableAdapter tblhareketTableAdapter;
        private System.Windows.Forms.BindingSource tblkitaptakipBindingSource;
        private KutuphaneOtomasyonDataSetTableAdapters.tblkitaptakipTableAdapter tblkitaptakipTableAdapter;
    }
}