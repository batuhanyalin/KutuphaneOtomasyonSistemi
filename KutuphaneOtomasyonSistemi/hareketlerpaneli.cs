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
using System.Net.Configuration;

namespace KutuphaneOtomasyonSistemi
{
    public partial class hareketlerpaneli : Form
    {
        public hareketlerpaneli()
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
        void temizle()
        {
            mskpertc.Text = "";
            txtperad.Text = "";
          
            mskmustc.Text = "";
            txtmusad.Text = "";
          
            mskkitapisbn.Text = "";
            txtkitapad.Text = "";
        }
        void listele()
        {
            SqlCommand cmd = new SqlCommand("execute hareketpaneli", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            verisayisi();
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

        private void hareketlerpaneli_Load(object sender, EventArgs e)
        {
            gunlukzaman();
            kullaniciadi();
            listele();
        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void mskpertc_TextChanged(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select HareketID as 'ID',PerAd+' '+PerSoyad as 'Personel',KitapAdi as 'Kitap',MusAd+' '+MusSoyad as 'Müşteri',islemTarihi as 'İşlem Tarihi',Yapilanislem= case Yapilanislem\r\nwhen '1' then 'Ödünç Verildi'\r\nwhen '0' then 'Geri Alındı'\r\nend\r\nfrom tblhareket\r\ninner join tblpersonel\r\non tblhareket.Personel=tblpersonel.PerID\r\ninner join tblkitap\r\non tblhareket.Kitap=tblkitap.KitapID\r\ninner join tblmusteri\r\non tblhareket.Musteri=tblmusteri.MusteriID\r\nwhere PerTC like '%" + mskpertc.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            verisayisi();
        }

        private void txtperad_TextChanged(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select HareketID as 'ID',PerAd+' '+PerSoyad as 'Personel',KitapAdi as 'Kitap',MusAd+' '+MusSoyad as 'Müşteri',islemTarihi as 'İşlem Tarihi',Yapilanislem= case Yapilanislem\r\nwhen '1' then 'Ödünç Verildi'\r\nwhen '0' then 'Geri Alındı'\r\nend\r\nfrom tblhareket\r\ninner join tblpersonel\r\non tblhareket.Personel=tblpersonel.PerID\r\ninner join tblkitap\r\non tblhareket.Kitap=tblkitap.KitapID\r\ninner join tblmusteri\r\non tblhareket.Musteri=tblmusteri.MusteriID\r\nwhere PerAd+' '+PerSoyad like '%" + txtperad.Text + "%'", baglanti);
            komut.Parameters.AddWithValue("@p1", txtperad.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            verisayisi();
        }

        private void mskmustc_TextChanged(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select HareketID as 'ID',PerAd+' '+PerSoyad as 'Personel',KitapAdi as 'Kitap',MusAd+' '+MusSoyad as 'Müşteri',islemTarihi as 'İşlem Tarihi',Yapilanislem= case Yapilanislem\r\nwhen '1' then 'Ödünç Verildi'\r\nwhen '0' then 'Geri Alındı'\r\nend\r\nfrom tblhareket\r\ninner join tblpersonel\r\non tblhareket.Personel=tblpersonel.PerID\r\ninner join tblkitap\r\non tblhareket.Kitap=tblkitap.KitapID\r\ninner join tblmusteri\r\non tblhareket.Musteri=tblmusteri.MusteriID\r\nwhere MusTC like '%" + mskmustc.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            verisayisi();
        }

        private void txtmusad_TextChanged(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select HareketID as 'ID',PerAd+' '+PerSoyad as 'Personel',KitapAdi as 'Kitap',MusAd+' '+MusSoyad as 'Müşteri',islemTarihi as 'İşlem Tarihi',Yapilanislem= case Yapilanislem\r\nwhen '1' then 'Ödünç Verildi'\r\nwhen '0' then 'Geri Alındı'\r\nend\r\nfrom tblhareket\r\ninner join tblpersonel\r\non tblhareket.Personel=tblpersonel.PerID\r\ninner join tblkitap\r\non tblhareket.Kitap=tblkitap.KitapID\r\ninner join tblmusteri\r\non tblhareket.Musteri=tblmusteri.MusteriID\r\nwhere MusAd+' '+MusSoyad like '%" + txtmusad.Text + "%'", baglanti);

            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            verisayisi();
        }

        private void mskkitapisbn_TextChanged(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select HareketID as 'ID',PerAd+' '+PerSoyad as 'Personel',KitapAdi as 'Kitap',MusAd+' '+MusSoyad as 'Müşteri',islemTarihi as 'İşlem Tarihi',Yapilanislem= case Yapilanislem\r\nwhen '1' then 'Ödünç Verildi'\r\nwhen '0' then 'Geri Alındı'\r\nend\r\nfrom tblhareket\r\ninner join tblpersonel\r\non tblhareket.Personel=tblpersonel.PerID\r\ninner join tblkitap\r\non tblhareket.Kitap=tblkitap.KitapID\r\ninner join tblmusteri\r\non tblhareket.Musteri=tblmusteri.MusteriID\r\nwhere ISBN like '%" + mskkitapisbn.Text + "%'", baglanti);

            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            verisayisi();
        }

        private void txtkitapad_TextChanged(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select HareketID as 'ID',PerAd+' '+PerSoyad as 'Personel',KitapAdi as 'Kitap',MusAd+' '+MusSoyad as 'Müşteri',islemTarihi as 'İşlem Tarihi',Yapilanislem= case Yapilanislem\r\nwhen '1' then 'Ödünç Verildi'\r\nwhen '0' then 'Geri Alındı'\r\nend\r\nfrom tblhareket\r\ninner join tblpersonel\r\non tblhareket.Personel=tblpersonel.PerID\r\ninner join tblkitap\r\non tblhareket.Kitap=tblkitap.KitapID\r\ninner join tblmusteri\r\non tblhareket.Musteri=tblmusteri.MusteriID\r\nwhere KitapAdi like '%" + txtkitapad.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            verisayisi();
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("execute hareketpaneli_tarihselsorgulama @deger=@p1,@deger2=@p2", baglanti);
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
            SqlCommand cmd = new SqlCommand("execute hareketpaneli_tarihselsorgulama @deger=@p1,@deger2=@p2", baglanti);
            cmd.Parameters.AddWithValue("@p1", Convert.ToDateTime(dateTimePicker1.Text));
            cmd.Parameters.AddWithValue("@p2", Convert.ToDateTime(dateTimePicker2.Text));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }

        private void btnbugunyapilanislemler_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("execute hareketpaneli_gununhareketleri", baglanti);
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
