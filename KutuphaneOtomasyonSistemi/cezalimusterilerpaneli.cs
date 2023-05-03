using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyonSistemi
{
    public partial class cezalimusterilerpaneli : Form
    {
        public cezalimusterilerpaneli()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-A405D24\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyon;Integrated Security=True");

        public void gunlukzaman()
        {
            DateTime gunlukzaman = DateTime.Now;
            lblgunlukzaman.Text = gunlukzaman.ToString("g");
        }
        void kullaniciadi()
        {
            string deger;
            SqlCommand cmd = new SqlCommand("select * from tblgirislog", baglanti);
            baglanti.Open();
            deger = (string)cmd.ExecuteScalar();
            baglanti.Close();
            txtpersonelkullaniciadi.Text = deger;
        }
        void listele()
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("execute cezalimusterilerpaneli", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        void toplamcezalimusterisayi()
        {
            int deger;
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from tblmusteri where MusCezaPuani>=3 ",baglanti);
            deger=(int)cmd.ExecuteScalar();
            baglanti.Close();
            if (deger==0)
            {
                lbltoplamsayi.Text = "0";
            }
            lbltoplamsayi.Text = deger.ToString();
        }
        private void anasayfaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yonetimpaneli yp = new yonetimpaneli();
            yp.Show();
            this.Hide();
        }

        private void programHakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hakkindapaneli hp = new hakkindapaneli();
            hp.Show();
        }

        private void ÇIKIŞToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cikis x = new cikis();
            x.cikisbaglantisi();
            girispaneli gp = new girispaneli();
            gp.Show();
            this.Hide();
        }

        private void cezalimusterilerpaneli_Load(object sender, EventArgs e)
        {
            gunlukzaman();
            kullaniciadi();
            listele();
            toplamcezalimusterisayi();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            musterilerpaneli mp = new musterilerpaneli();
            mp.Show();
            this.Hide();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel=true;
        }
    }
}
