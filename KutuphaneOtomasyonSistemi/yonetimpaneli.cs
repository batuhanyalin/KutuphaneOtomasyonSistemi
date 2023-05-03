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
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.X509Certificates;

namespace KutuphaneOtomasyonSistemi
{
    public partial class yonetimpaneli : Form
    {
        public yonetimpaneli()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(BaglanClass.sqlconnection);

        //************************** METOTLAR *****************************************
        void gunlukzaman()
        {
            DateTime gunlukzaman = DateTime.Now;
            lblgunlukzaman.Text = gunlukzaman.ToString("g");
        }

        void cmbmusterilisteleme()
        {
            // ÖDÜNÇ VERME MÜŞTERİLER combobox listeleme
            baglanti.Open();
            SqlCommand musterilistele = new SqlCommand("select * from tblmusteri", baglanti);
            SqlDataReader dr = musterilistele.ExecuteReader();
            while (dr.Read())
            {
                cmbmusteri.Items.Add(dr[1]).ToString();
            }
            baglanti.Close();
        }
        void cmbmusterilisteleme2()
        {
            // ÖDÜNÇ VERME MÜŞTERİLER combobox listeleme
            baglanti.Open();
            SqlCommand musterilistele = new SqlCommand("execute yonetimpaneli_cmbmusteriler @deger=@p1", baglanti);
            musterilistele.Parameters.AddWithValue("@p1", mskkitapisbnkodu2.Text);
            SqlDataReader dr = musterilistele.ExecuteReader();
            while (dr.Read())
            {
                cmbmusteri2.Items.Add(dr["MusTC"]).ToString();
            }
            baglanti.Close();
        }

        void kullaniciadi()//personelIDsi için  tblgirislogdan gelen değeri txtpersonelkullaniciadi texti ne aktarıyorum.

        {
            string deger;
            SqlCommand cmd = new SqlCommand("select * from tblgirislog", baglanti);
            baglanti.Open();
            deger = (string)cmd.ExecuteScalar();
            baglanti.Close();
            txtpersonelkullaniciadi.Text = deger;
        }

        void yetkiduzeyi()
        {
            string deger;
            SqlCommand cmd = new SqlCommand("select Yetki from tblyonetim where KullaniciAdi=@p1", baglanti);
            cmd.Parameters.AddWithValue("@p1", txtpersonelkullaniciadi.Text);
            baglanti.Open();
            deger = (string)cmd.ExecuteScalar();
            baglanti.Close();
            lblyetki.Text = deger;
        }
        void personelID() //personelID textine personelin IDsini getiriyor
        {
            int deger;
            SqlCommand komut = new SqlCommand("execute yonetimpaneli_personelID @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", txtpersonelkullaniciadi.Text);
            baglanti.Open();
            deger = (int)komut.ExecuteScalar();
            baglanti.Close();
            CONTROLpersonelID.Text = deger.ToString();
        }
        void kitaptakipcontrol()
        {
            int deger;
            SqlCommand komut = new SqlCommand("execute yonetimpaneli_kitaptakipkontrol @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", mskkitapisbnkodu2.Text);
            baglanti.Open();
            deger = (int)komut.ExecuteScalar();
            baglanti.Close();
            CONTROLkitaptakipliste.Text = deger.ToString();
        }
        //************* TABLODAN ALINAN ISBN koduna GÖRE LABEL'A VERİ YAZDIRMA İŞLEMİ ***********************
        // (metot kullanarak ödünç alma ve verme işlemlerindeki detay ekranına yazdırma yapıyorum)
        void kitapadi()
        {
            string deger;
            SqlCommand komut = new SqlCommand("execute kitapadi @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", mskkitapisbnkodu1.Text);
            baglanti.Open();
            deger = (string)komut.ExecuteScalar();
            baglanti.Close();
            lblkitapadi.Text = deger;
        }

        void yazaradi()
        {
            string deger;
            SqlCommand komut = new SqlCommand("execute yazaradi @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", mskkitapisbnkodu1.Text);
            baglanti.Open();
            deger = (string)komut.ExecuteScalar();
            baglanti.Close();
            lblyazaradi.Text = deger;
        }

        void yayinevi()
        {
            string deger;
            SqlCommand komut = new SqlCommand("execute yayinevi @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", mskkitapisbnkodu1.Text);
            baglanti.Open();
            deger = (string)komut.ExecuteScalar();
            baglanti.Close();
            lblyayinevi.Text = deger;
        }
        void kategoriadi()
        {
            string deger;
            SqlCommand komut = new SqlCommand("execute kategoriadi @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", mskkitapisbnkodu1.Text);
            baglanti.Open();
            deger = (string)komut.ExecuteScalar();
            baglanti.Close();
            lblkategori.Text = deger;
        }
        void rafadi()
        {
            string deger;
            SqlCommand komut = new SqlCommand("execute rafadi @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", mskkitapisbnkodu1.Text);
            baglanti.Open();
            deger = (string)komut.ExecuteScalar();
            baglanti.Close();
            lblraf.Text = deger;
        }
        void rafkatno()
        {
            string deger;
            SqlCommand komut = new SqlCommand("execute rafkatno @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", mskkitapisbnkodu1.Text);
            baglanti.Open();
            deger = (string)komut.ExecuteScalar();
            baglanti.Close();
            lblkat.Text = deger;
        }
        //--------------------------
        void kitapadi2()
        {
            string deger;
            SqlCommand komut = new SqlCommand("execute kitapadi @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", mskkitapisbnkodu2.Text);
            baglanti.Open();
            deger = (string)komut.ExecuteScalar();
            baglanti.Close();
            lblkitapadi2.Text = deger;
        }

        void yazaradi2()
        {
            string deger;
            SqlCommand komut = new SqlCommand("execute yazaradi @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", mskkitapisbnkodu2.Text);
            baglanti.Open();
            deger = (string)komut.ExecuteScalar();
            baglanti.Close();
            lblyazaradi2.Text = deger;
        }

        void yayinevi2()
        {
            string deger;
            SqlCommand komut = new SqlCommand("execute yayinevi @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", mskkitapisbnkodu2.Text);
            baglanti.Open();
            deger = (string)komut.ExecuteScalar();
            baglanti.Close();
            lblyayinevi2.Text = deger;
        }
        void kategoriadi2()
        {
            string deger;
            SqlCommand komut = new SqlCommand("execute kategoriadi @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", mskkitapisbnkodu2.Text);
            baglanti.Open();
            deger = (string)komut.ExecuteScalar();
            baglanti.Close();
            lblkategori2.Text = deger;
        }
        void rafadi2()
        {
            string deger;
            SqlCommand komut = new SqlCommand("execute rafadi @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", mskkitapisbnkodu2.Text);
            baglanti.Open();
            deger = (string)komut.ExecuteScalar();
            baglanti.Close();
            lblraf2.Text = deger;
        }
        void rafkatno2()
        {
            string deger;
            SqlCommand komut = new SqlCommand("execute rafkatno @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", mskkitapisbnkodu2.Text);
            baglanti.Open();
            deger = (string)komut.ExecuteScalar();
            baglanti.Close();
            lblkat2.Text = deger;
        }
        //*************************************************************************
        void yetki() //admine özel menü görünümü  //admin kullanıcı adına göre yetkilendirme
        {
            if (txtpersonelkullaniciadi.Text=="admin")
            {
                musterilerToolStripMenuItem.Visible = true;
                cezalimusterilerToolStripMenuItem.Visible = true;
                //--
                personellerToolStripMenuItem.Visible = true;
                sifreislemleriToolStripMenuItem.Visible = true;
                raporlamatoolStripMenuItem1.Visible = true;
                veritabaniyonetimitoolStripMenuItem1.Visible = true;
                //--
                kategorilerToolStripMenuItem1.Visible = true;
                yazarlarToolStripMenuItem2.Visible = true;
                yayinevleriToolStripMenuItem.Visible = true;
                kitaplarToolStripMenuItem1.Visible = true;
                raflarToolStripMenuItem2.Visible = true;
            }

            else if (lblyetki.Text == "3")
            {
                musterilerToolStripMenuItem.Visible = true;
                cezalimusterilerToolStripMenuItem.Visible = true;
                //--
                personellerToolStripMenuItem.Visible = true;
                sifreislemleriToolStripMenuItem.Visible = true;
                raporlamatoolStripMenuItem1.Visible = true;
                veritabaniyonetimitoolStripMenuItem1.Visible = false;
                //--
                kategorilerToolStripMenuItem1.Visible = true;
                yazarlarToolStripMenuItem2.Visible = true;
                yayinevleriToolStripMenuItem.Visible = true;
                kitaplarToolStripMenuItem1.Visible = true;
                raflarToolStripMenuItem2.Visible = true;

            }
            else if (lblyetki.Text == "2")
            {
                musterilerToolStripMenuItem.Visible = true;
                cezalimusterilerToolStripMenuItem.Visible = true;
                //--
                personellerToolStripMenuItem.Visible = false;
                sifreislemleriToolStripMenuItem.Visible = true;
                raporlamatoolStripMenuItem1.Visible = false;
                veritabaniyonetimitoolStripMenuItem1.Visible = false;
                //--
                kategorilerToolStripMenuItem1.Visible = true;
                yazarlarToolStripMenuItem2.Visible = true;
                yayinevleriToolStripMenuItem.Visible = true;
                kitaplarToolStripMenuItem1.Visible = true;
                raflarToolStripMenuItem2.Visible = true;
            }
            else
            {
                musterilerToolStripMenuItem.Visible = true;
                cezalimusterilerToolStripMenuItem.Visible = true;
                //--
                personellerToolStripMenuItem.Visible = false;
                sifreislemleriToolStripMenuItem.Visible = true;
                raporlamatoolStripMenuItem1.Visible = false;
                veritabaniyonetimitoolStripMenuItem1.Visible = false;
                //--
                kategorilerToolStripMenuItem1.Visible = false;
                yazarlarToolStripMenuItem2.Visible = false;
                yayinevleriToolStripMenuItem.Visible = false;
                kitaplarToolStripMenuItem1.Visible = false;
                raflarToolStripMenuItem2.Visible = false;
            }
        }

        //*************************** ÖDÜNÇ VER - GERİ AL İŞLEMLERİ METOTLARI ****************************
        void oduncver()
        {
            DialogResult secenek = MessageBox.Show("Ödünç verme işlemi gerçekleştirilsin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secenek == DialogResult.No)
            {

            }
            else if (mskkitapisbnkodu1.Text.Length != 13)
            {
                MessageBox.Show("Ödünç istenen kitabın ISBN kodunu eksiksiz ve doğru giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (CONTROLkitapID.Text == "")
            {
                MessageBox.Show("Veritabanında girilen ISBN koduna ait kitap bulunmamaktadır.\nÖncelikle kitabı sisteme kayıt ediniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (CONTROLmusteriID.Text == "")
            {
                MessageBox.Show("Kitabın ödünç verileceği müşteriyi seçiniz. ", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (lblrezervasyongun.Text == "")
            {
                MessageBox.Show("Rezervasyon gününü seçiniz. ", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (CONTROLmusterikitapsorgu.Text == "Var")
            {
                MessageBox.Show("Seçilen kitap zaten seçilen müşterinin elindedir.\nBir kitap bir müşteriye 2 kere verilemez.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Convert.ToInt32(CONTROLcezapuani.Text) >= 3)
            {
                MessageBox.Show("Kullanıcının ceza puanı dolu olduğu için kitap verilemez.\nYöneticiyle iletişim kurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else if (secenek == DialogResult.Yes && Convert.ToInt32(CONTROLaktifstok.Text) >= 1 && CONTROLmusterikitapsorgu.Text == "")
            {
                baglanti.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into tblhareket (Personel,Kitap,Musteri,islemTarihi,Yapilanislem) VALUES (@p1,@p2,@p3,@p4,@p5)", baglanti);
                komutkaydet.Parameters.AddWithValue("@p1", Convert.ToInt32(CONTROLpersonelID.Text));
                komutkaydet.Parameters.AddWithValue("@p2", Convert.ToInt32(CONTROLkitapID.Text));
                komutkaydet.Parameters.AddWithValue("@p3", Convert.ToInt32(CONTROLmusteriID.Text));
                komutkaydet.Parameters.AddWithValue("@p4", Convert.ToDateTime(lblgunlukzaman.Text));
                komutkaydet.Parameters.AddWithValue("@p5", CONTROLyapilanislem.Text);
                komutkaydet.ExecuteNonQuery();
                baglanti.Close();

                DateTime rezervasyongunu = DateTime.Now.AddDays(Convert.ToInt32(lblrezervasyongun.Text));
                baglanti.Open();
                SqlCommand komutkitaptakip = new SqlCommand("insert into tblkitaptakip (MusID,KitID,PerID,AlinanTarih,GelecekTarih) values (@s1,@s2,@s3,@s4,@s5)", baglanti);
                komutkitaptakip.Parameters.AddWithValue("@s1", Convert.ToInt32(CONTROLmusteriID.Text));
                komutkitaptakip.Parameters.AddWithValue("@s2", Convert.ToInt32(CONTROLkitapID.Text));
                komutkitaptakip.Parameters.AddWithValue("@s3", Convert.ToInt32(CONTROLpersonelID.Text));
                komutkitaptakip.Parameters.AddWithValue("@s4", Convert.ToDateTime(lblgunlukzaman.Text));
                komutkitaptakip.Parameters.AddWithValue("@s5", rezervasyongunu);
                komutkitaptakip.ExecuteNonQuery();
                baglanti.Close();
                baglanti.Open();
                SqlCommand komutaktifstok = new SqlCommand("update tblkitap set AktifStok-=1 where KitapID=" + CONTROLkitapID.Text, baglanti);
                SqlCommand komutverilenstok = new SqlCommand("update tblkitap set VerilenStok+=1 where KitapID=" + CONTROLkitapID.Text, baglanti);
                SqlCommand komuttoplamokunmasayisi = new SqlCommand("update tblkitap set ToplamOkunmaSayisi+=1 where KitapID=" + CONTROLkitapID.Text, baglanti);
                komutverilenstok.ExecuteNonQuery();
                komutaktifstok.ExecuteNonQuery();
                komuttoplamokunmasayisi.ExecuteNonQuery();
                baglanti.Close();


                MessageBox.Show("Ödünç verme işlemi başarıyla gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Convert.ToInt32(CONTROLaktifstok.Text) == 0)
            {
                MessageBox.Show("Seçilen kitabın aktif stoğu bulunmamaktadır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else { }
        }

        void gerial()
        {
            DialogResult secenek = MessageBox.Show("İade alma işlemi gerçekleştirilsin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secenek == DialogResult.No)
            {

            }
            else if (mskkitapisbnkodu2.Text.Length != 13)
            {
                MessageBox.Show("İade edilecek kitabın ISBN kodunu eksiksiz ve doğru giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Convert.ToInt32(CONTROLkitaptakipliste.Text) <= 0)
            {
                MessageBox.Show("Girilen ISBN koduna ait kitap herhangi bir müşteride bulunmamaktadır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (CONTROLmusteriID2.Text == "")
            {
                MessageBox.Show("Seçilen kitabı iade eden müşteriyi seçiniz. ", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Convert.ToInt32(CONTROLverilenstok.Text) <= 0)
            {
                MessageBox.Show("Seçilen kitap verilen stok listesinde yer almamaktadır.\nVeritabanından düzenleme yapınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (secenek == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into tblhareket (Personel,Kitap,Musteri,islemTarihi,Yapilanislem) VALUES (@p1,@p2,@p3,@p4,@p5)", baglanti);
                komutkaydet.Parameters.AddWithValue("@p1", Convert.ToInt32(CONTROLpersonelID.Text));
                komutkaydet.Parameters.AddWithValue("@p2", Convert.ToInt32(CONTROLkitapID2.Text));
                komutkaydet.Parameters.AddWithValue("@p3", Convert.ToInt32(CONTROLmusteriID2.Text));
                komutkaydet.Parameters.AddWithValue("@p4", Convert.ToDateTime(lblgunlukzaman.Text));
                komutkaydet.Parameters.AddWithValue("@p5", CONTROLyapilanislem.Text);
                komutkaydet.ExecuteNonQuery();
                baglanti.Close();
                baglanti.Open();
                SqlCommand komutkitaptakipsil = new SqlCommand("yonetimpaneli_gerialmaislemi @deger=@p1,@deger2=@p2", baglanti);
                komutkitaptakipsil.Parameters.AddWithValue("@p1", CONTROLmusteriID2.Text);
                komutkitaptakipsil.Parameters.AddWithValue("@p2", CONTROLkitapID2.Text);
                SqlCommand komutaktifstok = new SqlCommand("update tblkitap set AktifStok+=1 where KitapID=" + CONTROLkitapID2.Text, baglanti);
                SqlCommand komutverilenstok = new SqlCommand("update tblkitap set VerilenStok-=1 where KitapID=" + CONTROLkitapID2.Text, baglanti);
                komutverilenstok.ExecuteNonQuery();
                komutaktifstok.ExecuteNonQuery();
                komutkitaptakipsil.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("İade alma işlemi başarıyla gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbmusteri2.Text = "";
                cmbmusteri2.Items.Clear();
            }
            else
            {

            }
        }

        //********************  CONTROL METOTLARI (geriye değer döndüren metotlar) ****************************

        public string kitapID()
        {
            SqlCommand komut = new SqlCommand("execute yonetimpaneli_kitapID @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", mskkitapisbnkodu1.Text);
            baglanti.Open();
            var sonuc = komut.ExecuteScalar();
            var bosdeger = string.Empty;

            if (sonuc != DBNull.Value)
            {
                bosdeger = Convert.ToString(sonuc);
            }
            baglanti.Close();
            return CONTROLkitapID.Text = bosdeger;
        }
        //-----------------------------------
        public string kitapID2()
        {
            SqlCommand komut = new SqlCommand("execute yonetimpaneli_kitapID2 @deger=@p1,@deger2=@p2", baglanti);

            komut.Parameters.AddWithValue("@p1", mskkitapisbnkodu2.Text);
            komut.Parameters.AddWithValue("@p2", cmbmusteri2.Text);
            baglanti.Open();
            var sonuc = komut.ExecuteScalar();
            var bosdeger = string.Empty;

            if (sonuc != DBNull.Value)
            {
                bosdeger = Convert.ToString(sonuc);
            }
            baglanti.Close();
            return CONTROLkitapID2.Text = bosdeger;
        }
        //----------------------
        public string cmbmusteriIDbosdeger()
        {
            SqlCommand komut = new SqlCommand("execute yonetimpaneli_musteriID @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", cmbmusteri.Text);
            baglanti.Open();
            var sonuc = komut.ExecuteScalar();
            var bosdeger = string.Empty;
            if (sonuc != DBNull.Value)
            {
                bosdeger = Convert.ToString(sonuc);
            }
            baglanti.Close();
            return CONTROLmusteriID.Text = bosdeger;
        }
        //----------------------
        public string cmbmusteriID2bosdeger()
        {
            SqlCommand komut = new SqlCommand("execute yonetimpaneli_musteriID2 @deger=@p1,@deger2=@p2", baglanti);
            komut.Parameters.AddWithValue("@p1", mskkitapisbnkodu2.Text);
            komut.Parameters.AddWithValue("@p2", cmbmusteri2.Text);
            baglanti.Open();
            var sonuc = komut.ExecuteScalar();
            var bosdeger = string.Empty;
            if (sonuc != DBNull.Value)
            {
                bosdeger = Convert.ToString(sonuc);
            }
            baglanti.Close();
            return CONTROLmusteriID2.Text = bosdeger;
        }
        //----------------------
        public string cmbmusteriadbosdeger()
        {
            SqlCommand komut = new SqlCommand("execute yonetimpaneli_musteriad @deger2=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", cmbmusteri.Text);
            baglanti.Open();
            var sonuc = komut.ExecuteScalar();
            var bosdeger = string.Empty;
            if (sonuc != DBNull.Value)
            {
                bosdeger = Convert.ToString(sonuc);
            }
            baglanti.Close();
            return lblmusteriadsoyad.Text = bosdeger;
        }
        public string cmbmusteriad2bosdeger()
        {
            SqlCommand komut = new SqlCommand("execute yonetimpaneli_musteriad2 @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", cmbmusteri2.Text);
            baglanti.Open();
            var sonuc = komut.ExecuteScalar();
            var bosdeger = string.Empty;
            if (sonuc != DBNull.Value)
            {
                bosdeger = Convert.ToString(sonuc);
            }
            baglanti.Close();
            return lblmusteriadsoyad2.Text = bosdeger;
        }
        //----------------------
        public string musterikitapcontrol()
        {
            SqlCommand komut = new SqlCommand("execute yonetimpaneli_musterikitapsorgu @deger=@p1,@deger2=@p2", baglanti);
            komut.Parameters.AddWithValue("@p1", CONTROLmusteriID.Text);
            komut.Parameters.AddWithValue("@p2", CONTROLkitapID.Text);
            baglanti.Open();
            var sonuc = komut.ExecuteScalar();
            var bosdeger = string.Empty;

            if (sonuc != DBNull.Value)
            {
                bosdeger = Convert.ToString(sonuc);
            }
            baglanti.Close();
            return CONTROLmusterikitapsorgu.Text = bosdeger;
        }
        //----------------------
        public string aktifstok()
        {
            SqlCommand komut = new SqlCommand("select AktifStok from tblkitap where KitapID=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", CONTROLkitapID.Text);
            baglanti.Open();
            var sonuc = komut.ExecuteScalar();
            var bosdeger = string.Empty;

            if (sonuc != DBNull.Value)
            {
                bosdeger = Convert.ToString(sonuc);
            }
            baglanti.Close();
            return CONTROLaktifstok.Text = bosdeger;
        }

        public string verilenstok()
        {
            SqlCommand komut = new SqlCommand("select VerilenStok from tblkitap where ISBN=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", mskkitapisbnkodu2.Text);
            baglanti.Open();
            var sonuc = komut.ExecuteScalar();
            var bosdeger = string.Empty;

            if (sonuc != DBNull.Value)
            {
                bosdeger = Convert.ToString(sonuc);
            }
            baglanti.Close();
            return CONTROLverilenstok.Text = bosdeger;
        }

        public string cezapuani()
        {

            SqlCommand komut = new SqlCommand("select MusCezaPuani from tblmusteri where MusteriID=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", CONTROLmusteriID.Text);
            baglanti.Open();
            var sonuc = komut.ExecuteScalar();
            var bosdeger = string.Empty;
            if (sonuc != DBNull.Value)
            {
                bosdeger = Convert.ToString(sonuc);
            }
            baglanti.Close();
            return CONTROLcezapuani.Text = bosdeger;

        }
        //****************************************************************************
        void pversiyon()
        {
            versiyon v = new versiyon();
            lblversiyon.Text = v.programversiyonu;
        }


        private void mskkitapisbnkodu1_TextChanged(object sender, EventArgs e)
        {
            kitapID();
            kitapadi();
            yazaradi();
            yayinevi();
            kategoriadi();
            rafadi();
            rafkatno();
            aktifstok();
            musterikitapcontrol();
            cmbmusteri.Text = "";
            cmbmusteri.Items.Clear();
            cmbmusterilisteleme();
            rdb10.Checked = false;
            rdb20.Checked = false;
            rdb30.Checked = false;
        }

        private void mskkitapisbnkodu2_TextChanged(object sender, EventArgs e)
        {
            kitapID2();
            kitapadi2();
            yazaradi2();
            yayinevi2();
            kategoriadi2();
            rafadi2();
            rafkatno2();
            verilenstok();
            cmbmusteri2.Text = "";
            cmbmusteri2.Items.Clear();
            cmbmusterilisteleme2();
            cmbmusteriad2bosdeger();
            cmbmusteriID2bosdeger();
            kitapID2();
            kitaptakipcontrol();
        }

        private void yonetimpaneli_Load(object sender, EventArgs e)
        {
            gunlukzaman();
            kullaniciadi();
            personelID();
            aktifstok();
            verilenstok();
            cezapuani();
            yetki();
            cmbmusterilisteleme();
            musterikitapcontrol();
            encokokunankitaplargrafik();
            bugunencokalinankitapgrafik();
            yetkiduzeyi();
            pversiyon();
        }

        private void cmbmusteri_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbmusteriIDbosdeger();
            cmbmusteriadbosdeger();
            musterikitapcontrol();
            cezapuani();
        }
        private void cmbmusteri_TextChanged(object sender, EventArgs e)
        {
            cmbmusteriIDbosdeger();
            cmbmusteriadbosdeger();
            musterikitapcontrol();
            cezapuani();
        }

        private void cmbmusteri2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbmusteriID2bosdeger();
            cmbmusteriad2bosdeger();
            kitapID2();
        }

        private void cmbmusteri2_TextChanged(object sender, EventArgs e)
        {
            cmbmusteriID2bosdeger();
            cmbmusteriad2bosdeger();
            kitapID2();
        }
        private void btngerial_Click(object sender, EventArgs e)
        {
            CONTROLyapilanislem.Text = "False";
            gerial();
            verilenstok();
            cmbmusteriad2bosdeger();
            cmbmusteriID2bosdeger();
            kitaptakipcontrol();
            kitapID2();

        }
        private void btnoduncver_Click(object sender, EventArgs e)
        {
            CONTROLyapilanislem.Text = "True";
            oduncver();
            aktifstok();
            cezapuani();
            musterikitapcontrol();

        }

        private void rdb10_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb10.Checked == true)
            {
                lblrezervasyongun.Text = "10";
            }
        }

        private void rdb20_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb20.Checked == true)
            {
                lblrezervasyongun.Text = "20";
            }
        }

        private void rdb30_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb30.Checked == true)
            {
                lblrezervasyongun.Text = "30";
            }
        }


        //***************** STRIP BAR BUTONLARI ******************************
        private void musterilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            musterilerpaneli mp = new musterilerpaneli();
            mp.Show();
            this.Hide();
        }

        private void cezalimusterilerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cezalimusterilerpaneli cmp = new cezalimusterilerpaneli();
            cmp.Show();
            this.Hide();
        }

        private void personellerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            personellerpaneli pp = new personellerpaneli();
            pp.Show();
            this.Hide();
        }

        private void sifreislemleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sifreislemleripaneli sip = new sifreislemleripaneli();
            sip.Show();
            this.Hide();
        }

        private void kitaplarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            kitaplarpaneli kp = new kitaplarpaneli();
            kp.Show();
            this.Hide();
        }

        private void kategorilerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            kategorilerpaneli kp = new kategorilerpaneli();
            kp.Show();
            this.Hide();

        }

        private void yazarlarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            yazarlarpaneli yp = new yazarlarpaneli();
            yp.Show();
            this.Hide();
        }

        private void yayinevleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yayinevipaneli yayp = new yayinevipaneli();
            yayp.Show();
            this.Hide();
        }

        private void raflarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            raflarpaneli rp = new raflarpaneli();
            rp.Show();
            this.Hide();
        }

        private void hareketlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hareketlerpaneli hp = new hareketlerpaneli();
            hp.Show();
            this.Hide();
        }

        private void kitaptakipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kitaptakippaneli ktp = new kitaptakippaneli();
            ktp.Show();
            this.Hide();
        }

        private void istatistiklerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            istatistikpaneli ip = new istatistikpaneli();
            ip.Show();
            this.Hide();
        }

        private void programhakkindaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hakkindapaneli hp = new hakkindapaneli();
            hp.Show();

        }
        private void cikisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // cikis adında bir class oluşturdum ve onu çağırıyorum.
            cikis x = new cikis(); // class içindeki cikisbaglantisi metotuyla
            x.cikisbaglantisi(); // sql sorgusuyla tblgirislogdaki değerleri sildiriyorum.
            girispaneli gp = new girispaneli();
            this.Hide();
            gp.Show();
        }

        private void btnoduncveryenile_Click(object sender, EventArgs e)
        {
            mskkitapisbnkodu1.Text = "";
            cmbmusteri.Text = "";
            rdb10.Checked = false;
            rdb20.Checked = false;
            rdb30.Checked = false;
            lblrezervasyongun.Text = "";
        }

        private void btngerialyenile_Click(object sender, EventArgs e)
        {
            mskkitapisbnkodu2.Text = "";
            CONTROLkitaptakipliste.Text = "";
        }

        private void mskkitapisbnkodu1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void mskkitapisbnkodu2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        void encokokunankitaplargrafik()
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("execute yonetimpaneli_top5okunankategori", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chart1.Series["Kategori"].Points.AddXY(dr[0], dr[1]);
            }
            baglanti.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto: info@batuhanyalin.com");
        }
        void bugunencokalinankitapgrafik()
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("execute yonetimpaneli_top5bugunalinankitaplar", baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                chart2.Series["Kitaplar"].Points.AddXY(dr[0], dr[2]);
            }
            baglanti.Close();
        }

        private void lblyetki_TextChanged(object sender, EventArgs e)
        {
            yetki();
        }

        private void yardımToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yardimpaneli yp = new yardimpaneli();
            yp.Show();
        }

        private void raporlamatoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            raporlamapanelics rp = new raporlamapanelics();
            rp.Show();
        }

        private void veritabaniyonetimitoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            veritabaniyonetimpaneli vtyp = new veritabaniyonetimpaneli();
            vtyp.Show();
            this.Hide();
        }
    }
}
