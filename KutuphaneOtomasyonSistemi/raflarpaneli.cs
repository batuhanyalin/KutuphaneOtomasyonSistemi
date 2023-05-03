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
    public partial class raflarpaneli : Form
    {
        public raflarpaneli()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(BaglanClass.sqlconnection);
        //METOTLAR------------------------------------------------------------------------
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
            SqlCommand komutlistele = new SqlCommand("execute raflarpaneli", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komutlistele);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            verisayisi();
            temizle();
        }

        void guncelle()
        {

            DialogResult secenek = MessageBox.Show("Raf bilgileri güncellensin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komutguncelle = new SqlCommand("update tblraf set RafAd=@p2,Kategori=@p3,Kat=@p4,RafBilgi=@p5 where RafID=@p1", baglanti);
                komutguncelle.Parameters.AddWithValue("@p1", txtid.Text);
                komutguncelle.Parameters.AddWithValue("@p2", txtad.Text);
                komutguncelle.Parameters.AddWithValue("@p3", Convert.ToInt16(CONTROLkategori.Text));
                komutguncelle.Parameters.AddWithValue("@p4", txtkat.Text);
                komutguncelle.Parameters.AddWithValue("@p5", richTextBox1.Text);
                komutguncelle.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Raf bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else { }
        }

        void kaydet()
        {
            DialogResult secenek = MessageBox.Show("Yeni raf kaydedilsin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secenek == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into tblraf (RafAd,Kategori,Kat,RafBilgi) values (@p1,@p2,@p3,@p4)", baglanti);
                komutkaydet.Parameters.AddWithValue("@p1", txtad.Text);
                komutkaydet.Parameters.AddWithValue("@p2", Convert.ToInt16(CONTROLkategori.Text));
                komutkaydet.Parameters.AddWithValue("@p3", txtkat.Text);
                komutkaydet.Parameters.AddWithValue("@p4", richTextBox1.Text);
                komutkaydet.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Yeni raf başarıyla başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else { }


        }

        void temizle()
        {
            txtid.Text = "";
            txtad.Text = "";
            txtkat.Text = "";
            cmbkategori.Text = "";
            cmbkategoriarama.Text = "";
            txtadarama.Text = "";
            richTextBox1.Text = "";
            txtkatarama.Text = "";

        }
        void sil()
        {
            //messageboxdan gelen cevaba göre işlem yaptırma.
            DialogResult secenek = MessageBox.Show("Seçilen raf silinsin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secenek == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komutsil = new SqlCommand("delete from tblraf where RafID=@p1 ", baglanti);
                komutsil.Parameters.AddWithValue("@p1", txtid.Text);
                komutsil.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Raf başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else { }
        }

        void cmbkategorilisteleme() //veritabanından comboboxa raf listesi çekme metotu
        {
            baglanti.Open();
            SqlCommand komutsehirlistele = new SqlCommand("select * from tblkategori", baglanti);
            SqlDataReader dr = komutsehirlistele.ExecuteReader();
            while (dr.Read())
            {
                cmbkategori.Items.Add(dr["KategoriAd"]);
                cmbkategoriarama.Items.Add(dr["KategoriAd"]);
            }
              baglanti.Close();
        }
      
        //BUTONLAR------------------------------------------------------------------------
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
            //Boş bilgi girmeyi önleyen şart yazdım.

            if ((string.IsNullOrEmpty(txtad.Text) || (cmbkategori.SelectedIndex == -1) || (txtkat.Text.Length) > 1))
            {
                MessageBox.Show("Tüm Bilgileri Eksiksiz ve Doğru Girmeden Kayıt Yapamazsınız.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                kaydet();
            }
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            sil();
        }

        private void btnsifirla_Click(object sender, EventArgs e)
        {
            temizle();
        }
// VERİ ARAMA VE FİLTRELEME ---------------------------------------------------------------

        //MENU STRIP BAR ----------------------------------------------------------------
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

        private void raflarpaneli_Load(object sender, EventArgs e)
        {
            gunlukzaman();
            cmbkategorilisteleme();
            listele();
            kullaniciadi();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cmbkategori.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtkat.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            richTextBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void cmbkategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            int deger;
            SqlCommand komut = new SqlCommand("execute secilikategori @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", cmbkategori.Text);
            baglanti.Open();
            deger = (int)komut.ExecuteScalar();
            baglanti.Close();
            CONTROLkategori.Text = deger.ToString();
        }

        private void txtadarama_TextChanged(object sender, EventArgs e)
        {
            SqlCommand komutara = new SqlCommand("select RafID,RafAd as 'Raf Adı',KategoriAd as 'Kategori Adı',Kat,RafBilgi as 'Raf Bilgisi' from tblraf\r\ninner join tblkategori\r\non tblraf.Kategori=tblkategori.KategoriID where RafAd like '%" + txtadarama.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komutara);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            verisayisi();
        }
        private void cmbkategoriarama_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand komutara = new SqlCommand("select RafID,RafAd as 'Raf Adı',KategoriAd as 'Kategori Adı',Kat,RafBilgi as 'Raf Bilgisi' from tblraf\r\ninner join tblkategori\r\non tblraf.Kategori=tblkategori.KategoriID where KategoriAd like '%" + cmbkategoriarama.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komutara);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            verisayisi();
        }

        private void cmbkategoriarama_TextChanged(object sender, EventArgs e)
        {
            SqlCommand komutara = new SqlCommand("select RafID,RafAd as 'Raf Adı',KategoriAd as 'Kategori Adı',Kat,RafBilgi as 'Raf Bilgisi' from tblraf\r\ninner join tblkategori\r\non tblraf.Kategori=tblkategori.KategoriID where KategoriAd like '%" + cmbkategoriarama.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komutara);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            verisayisi();
        }

        private void txtkatarama_TextChanged(object sender, EventArgs e)
        {
            SqlCommand komutara = new SqlCommand("select RafID,RafAd as 'Raf Adı',KategoriAd as 'Kategori Adı',Kat,RafBilgi as 'Raf Bilgisi' from tblraf\r\ninner join tblkategori\r\non tblraf.Kategori=tblkategori.KategoriID where Kat like '%" + txtkatarama.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komutara);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            verisayisi();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel=true;
        }
    }
}
