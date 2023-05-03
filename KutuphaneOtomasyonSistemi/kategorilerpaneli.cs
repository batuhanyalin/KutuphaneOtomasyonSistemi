using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace KutuphaneOtomasyonSistemi
{
    public partial class kategorilerpaneli : Form
    {
        public kategorilerpaneli()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(BaglanClass.sqlconnection);
        void verisayisi()
        {
            lblverisayisi.Text = (dataGridView1.RowCount - 1).ToString();
        }
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
            SqlCommand komutlistele = new SqlCommand("execute kategorilerpaneli", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komutlistele);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            temizle();
            verisayisi();
        }
        void guncelle()
        {

            DialogResult secenek = MessageBox.Show("Yeni kategori güncellensin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secenek == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komutguncelle = new SqlCommand("update tblkategori set KategoriAd=@p2 where KategoriID=@p1", baglanti);
                komutguncelle.Parameters.AddWithValue("@p1", txtid.Text);
                komutguncelle.Parameters.AddWithValue("@p2", txtad.Text);
                komutguncelle.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kategori Adı başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();

            }
            else { }
        }
        void kaydet()
        {
            DialogResult secenek = MessageBox.Show("Yeni kategori kaydedilsin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secenek == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into tblkategori (KategoriAd) values (@p1)", baglanti);
                komutkaydet.Parameters.AddWithValue("@p1", txtad.Text);
                baglanti.Close();
                MessageBox.Show("Yeni kategori başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            else { }
        }
        void sil()
        {
            DialogResult secenek = MessageBox.Show("Seçilen kategori silinsin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secenek == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komutsil = new SqlCommand("delete from tblkategori where KategoriID=@p1", baglanti);
                komutsil.Parameters.AddWithValue("@p1", txtid.Text);
                baglanti.Close();
                MessageBox.Show("Kategori başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            else { }
        }

        void temizle()
        {
            txtid.Text = "";
            txtad.Text = "";
            txtadarama.Text = "";
        }

        //MENU STRIP BAR-----------------------------------------------------------
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

        private void btnlistele_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            guncelle();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            kaydet();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            sil();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnsifirla_Click(object sender, EventArgs e)
        {
            temizle();
        }
        private void btnadarama_Click(object sender, EventArgs e)
        {
            SqlCommand komutlistele = new SqlCommand("execute kategoriadarama @deger=@p1", baglanti);
            komutlistele.Parameters.AddWithValue("@p1", txtadarama.Text);
            SqlDataAdapter da = new SqlDataAdapter(komutlistele);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            verisayisi();
        }

        private void kategorilerpaneli_Load(object sender, EventArgs e)
        {
            listele();
            gunlukzaman();
            kullaniciadi();
        }

        private void txtadarama_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select KategoriID as 'ID',KategoriAd as 'Kategori' from tblkategori where KategoriAd like '%" + txtadarama.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
