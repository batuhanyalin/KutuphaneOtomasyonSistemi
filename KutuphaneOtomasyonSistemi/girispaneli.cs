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
using System.Net;
using System.Diagnostics;

namespace KutuphaneOtomasyonSistemi
{
    public partial class girispaneli : Form
    {
        public girispaneli()
        {

            versiyon v = new versiyon(); // ürettiğim versiyon classındaki programversiyonuna erişiyorum.
            
            InitializeComponent();

            WebClient webClient = new WebClient();
            if (!webClient.DownloadString("https://www.batuhanyalin.com/kosversiyon/").Contains(v.programversiyonu)) //v.programversiyonuyla versiyon classındaki versiyon kodunu çağırıyorum.
            {
                if ((MessageBox.Show("Yeni bir versiyon mevcut.\nProgramın stabil çalışabilmesi için en güncel sürümü indirmeniz gerekiyor.", "Kütüphane Otomasyon Sistemi Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK))

                {
                    System.Diagnostics.Process.Start("https://www.batuhanyalin.com/kutuphane-otomasyon-sistemi/");
                    Environment.Exit(0);
                }
                else { }
            }
            else
            {
                MessageBox.Show("Versiyon taraması yapıldı.\nProgramın en güncel sürümüne sahipsiniz.", "Kütüphane Otomasyon Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        SqlConnection baglanti = new SqlConnection(BaglanClass.sqlconnection);

        private void girisyap()
        {
            baglanti.Open();
            SqlCommand komutgiris = new SqlCommand("select * from tblyonetim where KullaniciAdi=@p1 and Sifre=@p2", baglanti);
            komutgiris.Parameters.AddWithValue("@p1", txtkullaniciadi.Text.ToLower());
            komutgiris.Parameters.AddWithValue("@p2", txtsifre.Text);
            SqlDataReader dr = komutgiris.ExecuteReader();

            if (dr.Read())
            {
                baglanti.Close();
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("insert into tblgirislog (KullaniciAd) values (@p3)", baglanti);
                cmd.Parameters.AddWithValue("@p3", txtkullaniciadi.Text.ToLower());
                cmd.ExecuteNonQuery();
                yonetimpaneli yonetimpaneli = new yonetimpaneli();
                yonetimpaneli.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı, lütfen tekrar deneyin.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            baglanti.Close();
        }

        private void btngirisyap_Click(object sender, EventArgs e)
        {
            girisyap();
        }

        private void girispaneli_Load(object sender, EventArgs e)
        {
            versiyon v= new versiyon();
            lblversiyon.Text = v.programversiyonu;
        }

        private void linklblsifreniunuttunmu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Lütfen sistem yöneticisiyle iletişime geçin.\nİletişim Adresi: info@batuhanyalin.com\n", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void linklblbatuhanyalin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("www.batuhanyalin.com");
        }

        private void girispaneli_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult cikis;
            cikis = MessageBox.Show("Programı kapatmak istediğinize emin misiniz?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (cikis == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
            else if (cikis == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto: info@batuhanyalin.com");
        }
    }
}
