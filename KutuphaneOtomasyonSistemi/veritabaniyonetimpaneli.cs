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
    public partial class veritabaniyonetimpaneli : Form
    {
        public veritabaniyonetimpaneli()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(BaglanClass.sqlconnection);


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

        private void veritabaniyonetimpaneli_Load(object sender, EventArgs e)
        {
            gunlukzaman();
            kullaniciadi();
            uyari();
        }

        void uyari()
        {
            lbluyari.Text = "DİKKAT! Bu paneldeki yapacağınız sıfırlama işlemi tüm veritabanı verilerini sıfırlayacaktır.\nTablolar kalacak, fakat tüm verileri kaybedeceksiniz. Sıfırlamak istediğiniz verileri işaretleyin.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ckbmusteri.Checked == false && ckbkitaplar.Checked == false && ckbkategori.Checked == false && ckbyazar.Checked == false && ckbyayinevi.Checked == false && ckbraflar.Checked == false)
            {
                MessageBox.Show("Sıfırlama yapmak için en az bir veritabanı seçiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult secenek = MessageBox.Show("Seçilen veritabanları sıfırlanacak.\nBu işlemi yapmak istediğinize emin misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (secenek == DialogResult.Yes)
                {
                    sifirla();
                    MessageBox.Show("Seçilen veritabanları başarıyla sıfırlandı.", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {

                }
            }
        }


        void sifirla()
        {
            if (ckbmusteri.Checked == true)
            {
                baglanti.Open();
                SqlCommand musteri = new SqlCommand("truncate table tblmusteri", baglanti);
                musteri.ExecuteNonQuery();
                baglanti.Close();
             
            }
 
         // ----
            if (ckbkitaplar.Checked == true)
            {
                baglanti.Open();
                SqlCommand musteri = new SqlCommand("truncate table tblkitaplar", baglanti);
                musteri.ExecuteNonQuery();
                baglanti.Close();
               
            }
         //   ----
            if (ckbkategori.Checked == true)
            {
                baglanti.Open();
                SqlCommand musteri = new SqlCommand("truncate table tblkategori", baglanti);
                musteri.ExecuteNonQuery();
                baglanti.Close();
            }
           // ----
            if (ckbyayinevi.Checked == true)
            {
                baglanti.Open();
                SqlCommand musteri = new SqlCommand("truncate table tblyayinevi", baglanti);
                musteri.ExecuteNonQuery();
                baglanti.Close();
            }
          //  ----
            if (ckbyazar.Checked == true)
            {
                baglanti.Open();
                SqlCommand musteri = new SqlCommand("truncate table tblyazar", baglanti);
                musteri.ExecuteNonQuery();
                baglanti.Close();
            }
           // ----
            if (ckbraflar.Checked == true)
            {
                baglanti.Open();
                SqlCommand musteri = new SqlCommand("truncate table tblra", baglanti);
                musteri.ExecuteNonQuery();
                baglanti.Close();

            }

        }
    }
}
