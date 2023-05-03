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
    public partial class kitaptakippaneli : Form
    {
        public kitaptakippaneli()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(BaglanClass.sqlconnection);

        void verisayisi()
        {
            lblverisayisi.Text = (dataGridView1.RowCount-1).ToString();
        }

        void listele()
        {
            SqlCommand cmd = new SqlCommand("execute kitaptakippaneli", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            verisayisi();
        }
        void temizle()
        {
            txtperad.Text = "";
          
            txtmusad.Text = "";
          
            mskkitapisbn.Text = "";
            txtkitapad.Text = "";
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

        private void kitaptakippaneli_Load(object sender, EventArgs e)
        {
            gunlukzaman();
            kullaniciadi();
            listele();
            verisayisi();
        }
        private void mskpertc_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select MusAd+' '+MusSoyad as 'Müşteri',KitapAdi as 'Kitap',PerAd+' '+PerSoyad as 'Personel', AlinanTarih as 'Alınan Tarih', GelecekTarih as 'Gelecek Tarih',AlinanGunSayi as 'Rezervasyon',KalanGunSayi as 'Kalan Gün Sayısı', GecikenGunSayi as 'Geciken Gün Sayısı'\r\nfrom tblkitaptakip\r\ninner join tblmusteri\r\non tblkitaptakip.MusID=tblmusteri.MusteriID\r\ninner join tblkitap\r\non tblkitaptakip.KitID=tblkitap.KitapID\r\ninner join tblpersonel\r\non tblpersonel.PerID=tblkitaptakip.PerID where PerTC like '%" + mskpertc.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }

        private void txtperad_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select MusAd+' '+MusSoyad as 'Müşteri',KitapAdi as 'Kitap',PerAd+' '+PerSoyad as 'Personel', AlinanTarih as 'Alınan Tarih', GelecekTarih as 'Gelecek Tarih',AlinanGunSayi as 'Rezervasyon',KalanGunSayi as 'Kalan Gün Sayısı', GecikenGunSayi as 'Geciken Gün Sayısı'\r\nfrom tblkitaptakip\r\ninner join tblmusteri\r\non tblkitaptakip.MusID=tblmusteri.MusteriID\r\ninner join tblkitap\r\non tblkitaptakip.KitID=tblkitap.KitapID\r\ninner join tblpersonel\r\non tblpersonel.PerID=tblkitaptakip.PerID where PerAd+' '+PerSoyad like '%" + txtperad.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }

        private void mskmustc_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select MusAd+' '+MusSoyad as 'Müşteri',KitapAdi as 'Kitap',PerAd+' '+PerSoyad as 'Personel', AlinanTarih as 'Alınan Tarih', GelecekTarih as 'Gelecek Tarih',AlinanGunSayi as 'Rezervasyon',KalanGunSayi as 'Kalan Gün Sayısı', GecikenGunSayi as 'Geciken Gün Sayısı'\r\nfrom tblkitaptakip\r\ninner join tblmusteri\r\non tblkitaptakip.MusID=tblmusteri.MusteriID\r\ninner join tblkitap\r\non tblkitaptakip.KitID=tblkitap.KitapID\r\ninner join tblpersonel\r\non tblpersonel.PerID=tblkitaptakip.PerID where MusTC like '%" + mskpertc.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }
        private void txtmusad_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select MusAd+' '+MusSoyad as 'Müşteri',KitapAdi as 'Kitap',PerAd+' '+PerSoyad as 'Personel', AlinanTarih as 'Alınan Tarih', GelecekTarih as 'Gelecek Tarih',AlinanGunSayi as 'Rezervasyon',KalanGunSayi as 'Kalan Gün Sayısı', GecikenGunSayi as 'Geciken Gün Sayısı'\r\nfrom tblkitaptakip\r\ninner join tblmusteri\r\non tblkitaptakip.MusID=tblmusteri.MusteriID\r\ninner join tblkitap\r\non tblkitaptakip.KitID=tblkitap.KitapID\r\ninner join tblpersonel\r\non tblpersonel.PerID=tblkitaptakip.PerID where MusAd+' '+MusSoyad like '%" + txtmusad.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }

        private void mskkitapisbn_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select MusAd+' '+MusSoyad as 'Müşteri',KitapAdi as 'Kitap',PerAd+' '+PerSoyad as 'Personel', AlinanTarih as 'Alınan Tarih', GelecekTarih as 'Gelecek Tarih',AlinanGunSayi as 'Rezervasyon',KalanGunSayi as 'Kalan Gün Sayısı', GecikenGunSayi as 'Geciken Gün Sayısı'\r\nfrom tblkitaptakip\r\ninner join tblmusteri\r\non tblkitaptakip.MusID=tblmusteri.MusteriID\r\ninner join tblkitap\r\non tblkitaptakip.KitID=tblkitap.KitapID\r\ninner join tblpersonel\r\non tblpersonel.PerID=tblkitaptakip.PerID where ISBN like '%" + mskkitapisbn.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }
  
        private void txtkitapad_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select MusAd+' '+MusSoyad as 'Müşteri',KitapAdi as 'Kitap',PerAd+' '+PerSoyad as 'Personel', AlinanTarih as 'Alınan Tarih', GelecekTarih as 'Gelecek Tarih',AlinanGunSayi as 'Rezervasyon',KalanGunSayi as 'Kalan Gün Sayısı', GecikenGunSayi as 'Geciken Gün Sayısı'\r\nfrom tblkitaptakip\r\ninner join tblmusteri\r\non tblkitaptakip.MusID=tblmusteri.MusteriID\r\ninner join tblkitap\r\non tblkitaptakip.KitID=tblkitap.KitapID\r\ninner join tblpersonel\r\non tblpersonel.PerID=tblkitaptakip.PerID where KitapAdi like '%" + txtkitapad.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("execute kitaptakippaneli_alinantarih @tarih1=@p1,@tarih2=@p2", baglanti);
            cmd.Parameters.AddWithValue("@p1", Convert.ToDateTime(dateTimePicker1.Text));
            cmd.Parameters.AddWithValue("@p2", Convert.ToDateTime(dateTimePicker2.Text));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }

        private void dateTimePicker2_CloseUp(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("execute kitaptakippaneli_alinantarih @tarih1=@p1,@tarih2=@p2", baglanti);
            cmd.Parameters.AddWithValue("@p1", Convert.ToDateTime(dateTimePicker1.Text));
            cmd.Parameters.AddWithValue("@p2", Convert.ToDateTime(dateTimePicker2.Text));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }

        private void dateTimePicker3_CloseUp(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("execute kitaptakippaneli_gelecektarih @tarih1=@p1,@tarih2=@p2", baglanti);
            cmd.Parameters.AddWithValue("@p1", Convert.ToDateTime(dateTimePicker3.Text));
            cmd.Parameters.AddWithValue("@p2", dateTimePicker4.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }

        private void dateTimePicker4_CloseUp(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("execute kitaptakippaneli_gelecektarih @tarih1=@p1,@tarih2=@p2", baglanti);
            cmd.Parameters.AddWithValue("@p1", Convert.ToDateTime(dateTimePicker3.Text));
            cmd.Parameters.AddWithValue("@p2", Convert.ToDateTime(dateTimePicker4.Text));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }
        private void btnlistele_Click_1(object sender, EventArgs e)
        {
            temizle();
            listele();
        }

        private void btnbugunyapilanislemler_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("execute kitaptakippaneli_bugungelecekler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel=true;
        }
    }
}

