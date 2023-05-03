using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyonSistemi
{
    public partial class sifreislemleripaneli : Form
    {
        public sifreislemleripaneli()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(BaglanClass.sqlconnection);

        void gunlukzaman()
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

        private void sifreislemleripaneli_Load(object sender, EventArgs e)
        {
            gunlukzaman();
            kullaniciadi();
            hesapdetaylari();         
        }

        void hesapdetaylari()
        {
            string addeger, soyaddeger, sifredeger;
            baglanti.Open();
            SqlCommand ad = new SqlCommand("execute sifreislemleripaneli_ad @deger=@p1", baglanti);
            SqlCommand soyad = new SqlCommand("execute sifreislemleripaneli_soyad @deger=@p2", baglanti);
            SqlCommand sifre = new SqlCommand("execute sifreislemleripaneli_sifre @deger=@p3", baglanti);
            ad.Parameters.AddWithValue("@p1",txtpersonelkullaniciadi.Text);
            soyad.Parameters.AddWithValue("@p2", txtpersonelkullaniciadi.Text);
            sifre.Parameters.AddWithValue("@p3", txtpersonelkullaniciadi.Text);
            addeger = (string)ad.ExecuteScalar();
            soyaddeger = (string)soyad.ExecuteScalar();
            sifredeger = (string)sifre.ExecuteScalar();
            baglanti.Close();
            txtad.Text = addeger;
            txtsoyad.Text = soyaddeger;
            txtsifre.Text = sifredeger;
            txtkullaniciadi.Text = txtpersonelkullaniciadi.Text;
        }

        void sifredegistirme()
        {
            DialogResult secenek = MessageBox.Show("Şifre güncellensin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand sifre = new SqlCommand("execute sifreislemleripaneli_sifredegistirme @deger=@p1,@deger2=@p2", baglanti);
                sifre.Parameters.AddWithValue("@p1", txtsifredegistir1.Text);
                sifre.Parameters.AddWithValue("@p2", txtpersonelkullaniciadi.Text);
                sifre.ExecuteNonQuery();
                MessageBox.Show("Şifre başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                baglanti.Close();
            }
            else
            {

            }
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            if (txtsifredegistir1.Text!=txtsifredegistir2.Text)
            {
                MessageBox.Show("Girilen iki şifre birbiriyle uyumsuz. Lütfen tekrar deneyin.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else if (txtsifredegistir1.Text.Length<8)
            {
                MessageBox.Show("Yeni girilecek şifre 8 hane olmak zorundadır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                sifredegistirme();
                hesapdetaylari();
            }
        }

        private void txtsifredegistir1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtsifredegistir2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
      
    }
}
