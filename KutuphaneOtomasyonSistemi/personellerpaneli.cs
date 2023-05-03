using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyonSistemi
{
    public partial class personellerpaneli : Form
    {
        public personellerpaneli()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(BaglanClass.sqlconnection);
        void verisayisi()
        {
            lblverisayisi.Text = (dataGridView1.RowCount - 1).ToString();
        }




        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            msktc.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtsoyad.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            CONTROLcinsiyet.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            cmbsehir.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            mskdogumtarihi.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            msktel.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            txteposta.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            richTextBox1.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            tblyonetimkullaniciadi();
            tblyonetimyetki();
            if (lblyetki.Text == "")
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }

        }

        void tblyonetimkullaniciadi()
        {
            string degerkullaniciadi,degersifre;
            SqlCommand cmdkullaniciadi = new SqlCommand("select KullaniciAdi from tblyonetim where Personel=@p1", baglanti);
            SqlCommand cmdsifre = new SqlCommand("select Sifre from tblyonetim where Personel=@p1", baglanti);
            cmdkullaniciadi.Parameters.AddWithValue("@p1", txtid.Text);
            cmdsifre.Parameters.AddWithValue("@p1", txtid.Text);
            baglanti.Open();
            degerkullaniciadi = (string)cmdkullaniciadi.ExecuteScalar();
            degersifre = (string)cmdsifre.ExecuteScalar();
            baglanti.Close();
            txtkullaniciadi.Text = degerkullaniciadi;
            txtsifre.Text= degersifre;
            
        }
        void tblyonetimyetki()
        {
            string deger;
            SqlCommand cmd = new SqlCommand("select Yetki from tblyonetim where Personel=@p1", baglanti);
            cmd.Parameters.AddWithValue("@p1", txtid.Text);
            baglanti.Open();
            deger = (string)cmd.ExecuteScalar();
            baglanti.Close();
            lblyetki.Text = deger;
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
            SqlCommand komutlistele = new SqlCommand("execute personellerpaneli", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komutlistele);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            verisayisi();
            temizle();
        }

        void guncelle()
        {
            if (CONTROLcinsiyet.Text == "Erkek")
            {
                CONTROLcinsiyetguncellebuton.Text = "True";
            }
            if (CONTROLcinsiyet.Text == "Kadın")
            {
                CONTROLcinsiyetguncellebuton.Text = "False";
            }

            DialogResult secenek = MessageBox.Show("Personel bilgileri güncellensin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komutguncelle = new SqlCommand("update tblpersonel set PerTC=@p2,PerAd=(UPPER(substring(@p3,1,1))+LOWER(right(@p3,(len(@p3)-1)))),PerSoyad=UPPER(@p4),PerCinsiyet=@p1,PerSehir=@psehir,PerDogumTarihi=@p6,PerTel=@p7,PerEposta=@p8,PerHakkinda=@p9 where PerID=@p0", baglanti);
                komutguncelle.Parameters.AddWithValue("@p0", txtid.Text);
                komutguncelle.Parameters.AddWithValue("@p2", msktc.Text);
                komutguncelle.Parameters.AddWithValue("@p3", txtad.Text);
                komutguncelle.Parameters.AddWithValue("@p4", txtsoyad.Text);
                komutguncelle.Parameters.AddWithValue("@p1", CONTROLcinsiyetguncellebuton.Text);
                komutguncelle.Parameters.AddWithValue("@psehir", Convert.ToInt16(CONTROLplaka.Text));
                komutguncelle.Parameters.AddWithValue("@p6", Convert.ToDateTime(mskdogumtarihi.Text));
                komutguncelle.Parameters.AddWithValue("@p7", msktel.Text);
                komutguncelle.Parameters.AddWithValue("@p8", txteposta.Text);
                komutguncelle.Parameters.AddWithValue("@p9", richTextBox1.Text);
                komutguncelle.ExecuteNonQuery();
                baglanti.Close();


                baglanti.Open();
                SqlCommand komutyetki = new SqlCommand("update tblyonetim set Yetki=@p2 where Personel=@p1", baglanti);
                komutyetki.Parameters.AddWithValue("@p1", txtid.Text);
                komutyetki.Parameters.AddWithValue("@p2", lblyetki.Text);
                komutyetki.ExecuteNonQuery();
                baglanti.Close();

                if (txtpersonelkullaniciadi.Text=="admin")
                {
                    baglanti.Open();
                    SqlCommand komutkullaniciadi = new SqlCommand("update tblyonetim set KullaniciAdi=@p2,Sifre=@p3 where Personel=@p1", baglanti);
                    komutkullaniciadi.Parameters.AddWithValue("@p1", txtid.Text);
                    komutkullaniciadi.Parameters.AddWithValue("@p2", txtkullaniciadi.Text);
                    komutkullaniciadi.Parameters.AddWithValue("@p3", txtsifre.Text);
                    komutkullaniciadi.ExecuteNonQuery();
                    baglanti.Close();
                }

                MessageBox.Show("Personel bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else { }
        }
        void tamyetkikontrol()
        {
            string deger;
            SqlCommand cmd = new SqlCommand("select Yetki from tblyonetim where KullaniciAdi=@p1",baglanti);
            cmd.Parameters.AddWithValue("@p1", txtpersonelkullaniciadi.Text);
            baglanti.Open();
            deger = (string)cmd.ExecuteScalar();
            baglanti.Close();
            lblkullaniciadiyetkikontrol.Text = deger;

            if (lblkullaniciadiyetkikontrol.Text == "3")
            {
                txtkullaniciadi.ReadOnly = false;
                txtsifre.ReadOnly = false;
                txtsifre.UseSystemPasswordChar = false;
            }
            else
            {

            }
        }

        void personelkontrol()
        {
            string deger;
            SqlCommand cmd = new SqlCommand("select PerTC from tblpersonel where PerTC=@p1", baglanti);
            cmd.Parameters.AddWithValue("@p1", msktc.Text);
            baglanti.Open();
            deger = (string)cmd.ExecuteScalar();
            baglanti.Close();
            lblpersoneltckontrol.Text = deger;
        }

        void kaydet()
        {
            DialogResult secenek = MessageBox.Show("Yeni personel kaydedilsin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into tblpersonel (PerTC,PerAd,PerSoyad,PerCinsiyet,PerSehir,PerDogumTarihi,PerTel,PerEposta,PerHakkinda) values (@p1,(UPPER(substring(@p2,1,1))+LOWER(right(@p2,(len(@p2)-1)))),UPPER(@p3),@p4,@p5,@p6,@p7,@p8,@p9)", baglanti);
                komutkaydet.Parameters.AddWithValue("@p1", msktc.Text);
                komutkaydet.Parameters.AddWithValue("@p2", txtad.Text);
                komutkaydet.Parameters.AddWithValue("@p3", txtsoyad.Text);
                komutkaydet.Parameters.AddWithValue("@p4", CONTROLkaydetcinsiyet.Text);
                komutkaydet.Parameters.AddWithValue("@p5", Convert.ToInt16(CONTROLplaka.Text));
                komutkaydet.Parameters.AddWithValue("@p6", Convert.ToDateTime(mskdogumtarihi.Text));
                komutkaydet.Parameters.AddWithValue("@p7", msktel.Text);
                komutkaydet.Parameters.AddWithValue("@p8", txteposta.Text);
                komutkaydet.Parameters.AddWithValue("@p9", richTextBox1.Text);
                komutkaydet.ExecuteNonQuery();
                baglanti.Close();


                int deger;
                SqlCommand cmd = new SqlCommand("select PerID from tblpersonel where PerTC=@pTC", baglanti);
                cmd.Parameters.AddWithValue("@pTC", msktc.Text);
                baglanti.Open();
                deger = (int)cmd.ExecuteScalar();
                baglanti.Close();
                lblid.Text = deger.ToString();

                baglanti.Open();
                SqlCommand komutyetki = new SqlCommand("insert into tblyonetim (Personel,KullaniciAdi,Sifre,Yetki) values (@p1,@p2,@p4,@p5)", baglanti);
                komutyetki.Parameters.AddWithValue("@p1", txtid.Text);
                komutyetki.Parameters.AddWithValue("@p2", txtkullaniciadi.Text);
                komutyetki.Parameters.AddWithValue("@p4", txtsifre.Text);
                komutyetki.Parameters.AddWithValue("@p5", lblyetki.Text);
                komutyetki.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Yeni personel başarıyla başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else { }
        }

        void sil()
        {
            //messageboxdan gelen cevaba göre işlem yaptırma.
            DialogResult secenek = MessageBox.Show("Seçilen personel silinsin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secenek == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komutsiltblpersonel = new SqlCommand("delete from tblpersonel where PerID=@p1 ", baglanti);
                SqlCommand komutsiltblyonetim = new SqlCommand("delete from tblyonetim where Personel=@p1 ", baglanti);
                komutsiltblpersonel.Parameters.AddWithValue("@p1", txtid.Text);
                komutsiltblyonetim.Parameters.AddWithValue("@p1", txtid.Text);
                komutsiltblpersonel.ExecuteNonQuery();
                komutsiltblyonetim.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Personel başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else { }
        }
        void temizle()
        {
            txtid.Text = "";
            msktc.Text = "";
            txtad.Text = "";
            txtsoyad.Text = "";
            CONTROLcinsiyetguncellebuton.Text = "";
            cmbsehir.Text = "";
            CONTROLplaka.Text = "";
            mskdogumtarihi.Text = "";
            msktel.Text = "";
            txteposta.Text = "";
            richTextBox1.Text = "";
            rdberkek.Checked = false;
            rdbkadin.Checked = false;
            msktcarama.Text = "";
            txtadarama.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            txtsifre.Text = "87654321";
        }

        void cmbsehirlisteleme() //veritabanından comboboxa şehir listesi çekme metotu
        {
            baglanti.Open();
            SqlCommand komutsehirlistele = new SqlCommand("select * from tblsehirler", baglanti);
            SqlDataReader dr = komutsehirlistele.ExecuteReader();
            while (dr.Read())
            {
                cmbsehir.Items.Add(dr["SehirAd"]);
            }
            baglanti.Close();
        }
        // YÜKLEME VE DEĞİŞİMLER -------------------------------
        private void cmbsehir_SelectedIndexChanged(object sender, EventArgs e)
        {
            int deger;
            SqlCommand komut = new SqlCommand("execute secilisehir @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", cmbsehir.Text);
            baglanti.Open();
            deger = (int)komut.ExecuteScalar();
            baglanti.Close();
            CONTROLplaka.Text = deger.ToString();
        }
        //--------------------
        private void personellerpaneli_Load(object sender, EventArgs e)
        {
            gunlukzaman();
            cmbsehirlisteleme();
            listele();
            kullaniciadi();
            yetki();
            personelkontrol();
            tamyetkikontrol();
        }

        // CİNSİYET KONTROLÜ-------------------------------------------------------
        private void CONTROLcinsiyet_TextChanged(object sender, EventArgs e)
        {
            if (CONTROLcinsiyet.Text == "Erkek")
            {
                rdberkek.Checked = true;
                CONTROLkaydetcinsiyet.Text = "True";
            }
            if (CONTROLcinsiyet.Text == "Kadın")
            {
                rdbkadin.Checked = true;
                CONTROLkaydetcinsiyet.Text = "False";
            }
        }

        private void rdbkadin_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbkadin.Checked == true)
            {
                CONTROLcinsiyet.Text = "Kadın";
            }
        }

        private void rdberkek_CheckedChanged(object sender, EventArgs e)
        {
            if (rdberkek.Checked == true)
            {
                CONTROLcinsiyet.Text = "Erkek";
            }
        }

        private void lblyetki_TextChanged(object sender, EventArgs e)
        {
            if (lblyetki.Text == "3")
            {
                checkBox1.Checked = true;
            }
            if (lblyetki.Text == "2")
            {
                checkBox2.Checked = true;
            }
            if (lblyetki.Text == "1")
            {
                checkBox3.Checked = true;
            }
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                lblyetki.Text = "2";
                checkBox1.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                lblyetki.Text = "3";
                checkBox2.Checked = false;
                checkBox3.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                lblyetki.Text = "1";
                checkBox1.Checked = false;
                checkBox2.Checked = false;
            }
        }



        // BUTONLAR ---------------------------------------------------------

        private void btnlistele_Click(object sender, EventArgs e)
        {
            listele();
        }
        private void btnguncelle_Click(object sender, EventArgs e)
        {
            if ((txtid.Text == "") || (msktc.Text.Length < 11) || (string.IsNullOrEmpty(txtad.Text) && (string.IsNullOrEmpty(txtsoyad.Text))) || (cmbsehir.SelectedIndex == -1) || (rdberkek.Checked == false && rdbkadin.Checked == false) || (mskdogumtarihi.Text.Length < 10) || (msktel.Text.Length < 10))
            {
                MessageBox.Show("Tüm bilgileri eksiksiz ve doğru girmeden güncelleme yapamazsınız.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (lblyetki.Text == "")
            {
                MessageBox.Show("Seçili personele yetki atamadan güncelleme işlemi yapamazsınız.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (lblpersoneltckontrol.Text == "")
            {
                MessageBox.Show("Bu TC kimlik numarasına ait bir kayıt mevcut değildir.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                guncelle();
            }
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            //Boş bilgi girmeyi önleyen şart yazdım.
            if ((msktc.Text.Length < 11) || (string.IsNullOrEmpty(txtad.Text) && (string.IsNullOrEmpty(txtsoyad.Text))) || (cmbsehir.SelectedIndex == -1) || (rdberkek.Checked == false && rdbkadin.Checked == false) || (mskdogumtarihi.Text.Length < 10) || (msktel.Text.Length < 10))
            {
                MessageBox.Show("Tüm bilgileri eksiksiz ve doğru girmeden kayıt yapamazsınız.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (lblyetki.Text == "")
            {
                MessageBox.Show("Personel yetkisini tanımlamadan kayıt oluşturamazsınız.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (lblpersoneltckontrol.Text != "")
            {
                MessageBox.Show("Bu TC kimlik numarasıyla zaten bir kayıt mevcut.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                kaydet();
            }
        }
        private void btnsil_Click(object sender, EventArgs e)
        {
            if (txtid.Text == "")
            {
                MessageBox.Show("Silmek istediğiniz personeli tablodan seçiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                sil();
            }

        }

        private void btnsifirla_Click(object sender, EventArgs e)
        {
            temizle();
        }


        // MENUSTRIP BAR ---------------------------------------
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

        private void txtadarama_TextChanged(object sender, EventArgs e)
        {
            SqlCommand komuttcara = new SqlCommand("select PerID as 'ID',PerTC as 'TC',PerAd as 'Ad',PerSoyad as 'Soyad',PerCinsiyet=case PerCinsiyet \r\nwhen '1' then 'Erkek'\r\nwhen '0' then 'Kadın'\r\nend ,SehirAd as 'Şehir',PerDogumTarihi as 'Doğum Tarihi',PerTel as 'Telefon',PerEposta as 'E-posta',PerHakkinda as 'Hakkında'\r\nfrom tblpersonel\r\ninner join tblsehirler\r\non tblpersonel.PerSehir=tblsehirler.Plaka where PerAd+' '+PerSoyad like '%" + txtadarama.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komuttcara);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            verisayisi();
        }

        private void msktcarama_TextChanged(object sender, EventArgs e)
        {
            SqlCommand komuttcara = new SqlCommand("select PerID as 'ID',PerTC as 'TC',PerAd as 'Ad',PerSoyad as 'Soyad',PerCinsiyet=case PerCinsiyet \r\nwhen '1' then 'Erkek'\r\nwhen '0' then 'Kadın'\r\nend ,SehirAd as 'Şehir',PerDogumTarihi as 'Doğum Tarihi',PerTel as 'Telefon',PerEposta as 'E-posta',PerHakkinda as 'Hakkında'\r\nfrom tblpersonel\r\ninner join tblsehirler\r\non tblpersonel.PerSehir=tblsehirler.Plaka where PerTC like '%" + msktcarama.Text + "%'", baglanti); //
            SqlDataAdapter da = new SqlDataAdapter(komuttcara);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            verisayisi();
        }
        void yetki()
        {
            if (txtpersonelkullaniciadi.Text == "admin")
            {
                checkBox1.Enabled = true;
            }
            else
            {
                checkBox1.Enabled = false;
            }
        }



        private void txtad_TextChanged(object sender, EventArgs e)
        {
            txtkullaniciadi.Text = arndrma((txtad.Text + txtsoyad.Text).ToLower());
            CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtad.Text);

        }

        private void txtsoyad_TextChanged(object sender, EventArgs e)
        {

            txtkullaniciadi.Text = arndrma((txtad.Text + txtsoyad.Text).ToLower());
            CultureInfo.CurrentCulture.TextInfo.ToUpper(txtsoyad.Text);

        }
        public static string arndrma(string metin)
        {

            char[] türkcekarakterler = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö', 'ü', 'Ü', ' ' };
            char[] ingilizce = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U', '_' };//karakterler sırayla ingilizce karakter karşılıklarıyla yazıldı
            for (int i = 0; i < türkcekarakterler.Length; i++)
            {
                metin = metin.Replace(türkcekarakterler[i], ingilizce[i]);
            }
            return metin;
        }

        private void txtad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void txtsoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void msktc_TextChanged(object sender, EventArgs e)
        {
            personelkontrol();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel= true;
        }
    }
}
