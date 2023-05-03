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

namespace KutuphaneOtomasyonSistemi
{
    public partial class raporlamapanelics : Form
    {
        public raporlamapanelics()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(BaglanClass.sqlconnection);

        private void raporlamapanelics_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'kutuphaneOtomasyonDataSet.tblkitaptakip' table. You can move, or remove it, as needed.
            this.tblkitaptakipTableAdapter.Fill(this.kutuphaneOtomasyonDataSet.tblkitaptakip);
            // TODO: This line of code loads data into the 'kutuphaneOtomasyonDataSet.tblhareket' table. You can move, or remove it, as needed.
            this.tblhareketTableAdapter.Fill(this.kutuphaneOtomasyonDataSet.tblhareket);
            // TODO: This line of code loads data into the 'kutuphaneOtomasyonDataSet.tblpersonel' table. You can move, or remove it, as needed.
            this.tblpersonelTableAdapter.Fill(this.kutuphaneOtomasyonDataSet.tblpersonel);
            // TODO: This line of code loads data into the 'kutuphaneOtomasyonDataSet.tblmusteri' table. You can move, or remove it, as needed.
            this.tblmusteriTableAdapter.Fill(this.kutuphaneOtomasyonDataSet.tblmusteri);
            this.reportViewer1.RefreshReport();
            this.reportViewer2.RefreshReport();
            this.reportViewer3.RefreshReport();
            this.reportViewer4.RefreshReport();
            reportViewer1.Visible = false;
            reportViewer2.Visible = false;
            reportViewer3.Visible = false;
            reportViewer4.Visible = false;
        }

        private void btnmusteri_Click(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport(); 
            reportViewer1.Visible = true;
            reportViewer2.Visible = false;
            reportViewer3.Visible = false;
            reportViewer4.Visible = false;
        }

        private void btnpersonel_Click(object sender, EventArgs e)
        {
            this.reportViewer2.RefreshReport();
            reportViewer1.Visible = false;
            reportViewer2.Visible = true;
            reportViewer3.Visible = false;
            reportViewer4.Visible = false;

        }

        private void btnhareket_Click(object sender, EventArgs e)
        {
            this.reportViewer3.RefreshReport();
            reportViewer1.Visible = false;
            reportViewer2.Visible = false;
            reportViewer3.Visible = true;
            reportViewer4.Visible = false;
        }

        private void btnkitaptakip_Click(object sender, EventArgs e)
        {
            this.reportViewer4.RefreshReport();
            reportViewer1.Visible = false;
            reportViewer2.Visible = false;
            reportViewer3.Visible = false;
            reportViewer4.Visible = true;
        }

    }
}
