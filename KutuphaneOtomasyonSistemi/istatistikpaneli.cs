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
using System.Windows.Forms.DataVisualization.Charting;

namespace KutuphaneOtomasyonSistemi
{
    public partial class istatistikpaneli : Form
    {
        public istatistikpaneli()
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

        private void istatistikpaneli_Load(object sender, EventArgs e)
        {
            gunlukzaman();
            kullaniciadi();
            sayisalistatistikler();
            mustericinsiyetdagilimi();
            musterisehirdagilimi();
            personelcinsiyetdagilimi();
            buayencokokunan10kategorigrafik();
            buayencokokunan10kitapgrafik();
            aylikverilenkitapgrafik();
        }

        void sayisalistatistikler()
        {
            int personel;
            baglanti.Open();
            SqlCommand cmdpersonel = new SqlCommand("select COUNT(PerID) as 'Personel' from tblpersonel", baglanti);         
            personel=(int)cmdpersonel.ExecuteScalar();
            baglanti.Close();
            lblpersonel.Text = personel.ToString();
            //----------------
            int musteri;
            baglanti.Open();
            SqlCommand cmdmusteri = new SqlCommand("select COUNT(MusteriID) as 'Müşteri' from tblmusteri", baglanti);
            musteri = (int)cmdmusteri.ExecuteScalar();
            baglanti.Close();
            lblmusteri.Text = musteri.ToString();
            //----------------
            int kitap;
            baglanti.Open();
            SqlCommand cmdkitap = new SqlCommand("select COUNT(KitapID) as 'Kitap' from tblkitap", baglanti);
            kitap = (int)cmdkitap.ExecuteScalar();
            baglanti.Close();
            lblkitap.Text = kitap.ToString();
            //----------------
            int kategori;
            baglanti.Open();
            SqlCommand cmdkategori = new SqlCommand("select COUNT(KategoriID) as 'Kategori' from tblkategori", baglanti);
            kategori = (int)cmdkategori.ExecuteScalar();
            baglanti.Close();
            lblkategori.Text = kategori.ToString();
            //----------------
            int yazar;
            baglanti.Open();
            SqlCommand cmdyazar = new SqlCommand("select COUNT(YazarID) as 'Yazar' from tblyazar", baglanti);
            yazar = (int)cmdyazar.ExecuteScalar();
            baglanti.Close();
            lblyazar.Text = yazar.ToString();
            //----------------
            int yayinevi;
            baglanti.Open();
            SqlCommand cmdyayinevi = new SqlCommand("select COUNT(YayineviID) from tblyayinevi", baglanti);
            yayinevi = (int)cmdyayinevi.ExecuteScalar();
            baglanti.Close();
            lblyayinevi.Text = yayinevi.ToString();
            //----------------
            int raf;
            baglanti.Open();
            SqlCommand cmdraf = new SqlCommand("select COUNT(RafID) from tblraf", baglanti);
            raf = (int)cmdraf.ExecuteScalar();
            baglanti.Close();
            lblraf.Text = raf.ToString();
            //---------------
            int toplamverilenkitap;
            baglanti.Open();
            SqlCommand cmdkitapsayi = new SqlCommand("execute istatistikpaneli_toplamverilenkitap", baglanti);
            toplamverilenkitap = (int)cmdkitapsayi.ExecuteScalar();
            baglanti.Close();
            lbltoplamverilenkitap.Text= toplamverilenkitap.ToString();
            //---------------
            int buayverilenkitap;
            baglanti.Open();
            SqlCommand cmdaylikkitapsayi = new SqlCommand("execute istatistikpaneli_buayverilenkitap", baglanti);
            buayverilenkitap = (int)cmdaylikkitapsayi.ExecuteScalar();
            baglanti.Close();
            lblbuayverilenkitap.Text = buayverilenkitap.ToString();


        }
        void mustericinsiyetdagilimi()
        {
            //---- Erkek cinsiyet
            baglanti.Open();
            SqlCommand cmderkek = new SqlCommand("select COUNT(MusteriID) from tblmusteri where MusCinsiyet=1", baglanti);
            SqlDataReader dr = cmderkek.ExecuteReader();
            while (dr.Read())
            {       
                chartmustericinsiyet.Series["Cinsiyet"].Points.AddXY("Erkek",dr[0]);
            }
            baglanti.Close();

            //---- Kadın cinsiyet
            baglanti.Open();
            SqlCommand cmdkadin = new SqlCommand("select COUNT(MusteriID) from tblmusteri where MusCinsiyet=0", baglanti);
            SqlDataReader dr2 = cmdkadin.ExecuteReader();
            while (dr2.Read())
            {
                chartmustericinsiyet.Series["Cinsiyet"].Points.AddXY("Kadın",dr2[0]);
            }
            baglanti.Close();
        }
        void personelcinsiyetdagilimi()
        {
            //---- Erkek cinsiyet
            baglanti.Open();
            SqlCommand cmderkek = new SqlCommand("select COUNT(PerID) from tblpersonel where PerCinsiyet=1", baglanti);
            SqlDataReader dr = cmderkek.ExecuteReader();
            while (dr.Read())
            {
                chartpersonelcinsiyet.Series["Cinsiyet"].Points.AddXY("Erkek",dr[0]);
            }
            baglanti.Close();

            //---- Kadın cinsiyet
            baglanti.Open();
            SqlCommand cmdkadin = new SqlCommand("select COUNT(PerID) from tblpersonel where PerCinsiyet=0", baglanti);
            SqlDataReader dr2 = cmdkadin.ExecuteReader();
            while (dr2.Read())
            {
                chartpersonelcinsiyet.Series["Cinsiyet"].Points.AddXY("Kadın",dr2[0]);
            }
            baglanti.Close();
        }

        void musterisehirdagilimi()
        {
            baglanti.Open();
            SqlCommand cmdsehir = new SqlCommand("execute istatistikpaneli_musterisehir", baglanti);
            SqlDataReader dr = cmdsehir.ExecuteReader();
            while (dr.Read())
            {
                chartmusterisehir.Series["Şehirler"].Points.AddXY(dr[0], dr[1]);
            }
            baglanti.Close();
        }

            void buayencokokunan10kitapgrafik()
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("execute istatistikpaneli_buayencokokunantop10kitap", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartbuayencokokunan10kitap.Series["Kitaplar"].Points.AddXY(dr[0], dr[1]);
            }
            baglanti.Close();
        }


        void buayencokokunan10kategorigrafik()
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("execute istatistikpaneli_encokokunan10kategori", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chartbuayencokokunan10kategori.Series["Kategoriler"].Points.AddXY(dr[0], dr[1]);
            }
            baglanti.Close();
        }

        void aylikverilenkitapgrafik()
        {
            baglanti.Open();
            SqlCommand cmd12 = new SqlCommand("execute istatistikpaneli_aylikverilenkitap12", baglanti);
            SqlDataReader dr12 = cmd12.ExecuteReader();
            while (dr12.Read())
            {
                chartaylikverilenkitapmiktari.Series["Okuma Sayısı"].Points.AddXY(dr12[1], dr12[0]);
            }
            baglanti.Close();
            baglanti.Open();
            SqlCommand cmd11 = new SqlCommand("execute istatistikpaneli_aylikverilenkitap11", baglanti);
            SqlDataReader dr11 = cmd11.ExecuteReader();
            while (dr11.Read())
            {
                chartaylikverilenkitapmiktari.Series["Okuma Sayısı"].Points.AddXY(dr11[1], dr11[0]);
            }
            baglanti.Close();
            baglanti.Open();
            SqlCommand cmd10 = new SqlCommand("execute istatistikpaneli_aylikverilenkitap10", baglanti);
            SqlDataReader dr10 = cmd10.ExecuteReader();
            while (dr10.Read())
            {
                chartaylikverilenkitapmiktari.Series["Okuma Sayısı"].Points.AddXY(dr10[1], dr10[0]);
            }
            baglanti.Close();
            baglanti.Open();
            SqlCommand cmd9 = new SqlCommand("execute istatistikpaneli_aylikverilenkitap9", baglanti);
            SqlDataReader dr9 = cmd9.ExecuteReader();
            while (dr9.Read())
            {
                chartaylikverilenkitapmiktari.Series["Okuma Sayısı"].Points.AddXY(dr9[1], dr9[0]);
            }
            baglanti.Close();
            //--------------
            baglanti.Open();
            SqlCommand cmd8 = new SqlCommand("execute istatistikpaneli_aylikverilenkitap8", baglanti);
            SqlDataReader dr8 = cmd8.ExecuteReader();
            while (dr8.Read())
            {
                chartaylikverilenkitapmiktari.Series["Okuma Sayısı"].Points.AddXY(dr8[1], dr8[0]);
            }
            baglanti.Close();
            //--------------
            baglanti.Open();
            SqlCommand cmd7 = new SqlCommand("execute istatistikpaneli_aylikverilenkitap7", baglanti);
            SqlDataReader dr7 = cmd7.ExecuteReader();
            while (dr7.Read())
            {
                chartaylikverilenkitapmiktari.Series["Okuma Sayısı"].Points.AddXY(dr7[1], dr7[0]);
            }
            baglanti.Close();
            //---------------
            baglanti.Open();
            SqlCommand cmd6 = new SqlCommand("execute istatistikpaneli_aylikverilenkitap6", baglanti);
            SqlDataReader dr6 = cmd6.ExecuteReader();
            while (dr6.Read())
            {
                chartaylikverilenkitapmiktari.Series["Okuma Sayısı"].Points.AddXY(dr6[1], dr6[0]);
            }
            baglanti.Close();
            //---------------
            baglanti.Open();
            SqlCommand cmd5 = new SqlCommand("execute istatistikpaneli_aylikverilenkitap5", baglanti);
            SqlDataReader dr5 = cmd5.ExecuteReader();
            while (dr5.Read())
            {
                chartaylikverilenkitapmiktari.Series["Okuma Sayısı"].Points.AddXY(dr5[1], dr5[0]);
            }
            baglanti.Close();
            //---------------
            baglanti.Open();
            SqlCommand cmd4 = new SqlCommand("execute istatistikpaneli_aylikverilenkitap4", baglanti);
            SqlDataReader dr4 = cmd4.ExecuteReader();
            while (dr4.Read())
            {
                chartaylikverilenkitapmiktari.Series["Okuma Sayısı"].Points.AddXY(dr4[1], dr4[0]);
            }
            baglanti.Close();
            //---------------
            baglanti.Open();
            SqlCommand cmd3 = new SqlCommand("execute istatistikpaneli_aylikverilenkitap3", baglanti);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                chartaylikverilenkitapmiktari.Series["Okuma Sayısı"].Points.AddXY(dr3[1], dr3[0]);
            }
            baglanti.Close();
            //---------------
            baglanti.Open();
            SqlCommand cmd2 = new SqlCommand("execute istatistikpaneli_aylikverilenkitap2", baglanti);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                chartaylikverilenkitapmiktari.Series["Okuma Sayısı"].Points.AddXY(dr2[1], dr2[0]);
            }
            baglanti.Close();
            //---------------
            baglanti.Open();
            SqlCommand cmd1 = new SqlCommand("execute istatistikpaneli_aylikverilenkitap1", baglanti);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                chartaylikverilenkitapmiktari.Series["Okuma Sayısı"].Points.AddXY(dr1[1],dr1[0]);
            }
            baglanti.Close();    
 
        }
    }
}
