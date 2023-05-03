using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System.Data.SqlClient;
using System.Windows.Forms.VisualStyles;

namespace KutuphaneOtomasyonSistemi
{
    public partial class musterilerpaneli : Form
    {
        public musterilerpaneli()
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

        //METOTLAR --------------------------------------

        void kullaniciadi()
        {
            string deger;
            SqlCommand cmd = new SqlCommand("select * from tblgirislog", baglanti);
            baglanti.Open();
            deger = (string)cmd.ExecuteScalar();
            baglanti.Close();
            txtpersonelkullaniciadi.Text = deger;
        }
        //Buton Metotları
        void listele()
        {
            SqlCommand komutlistele = new SqlCommand("execute musterilerpaneli", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komutlistele);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            verisayisi();
            temizle();
        }

        void musterikontrol()
        {
            string deger;
            SqlCommand cmd = new SqlCommand("select MusTC from tblmusteri where MusTC=@p1", baglanti);
            cmd.Parameters.AddWithValue("@p1", msktc.Text);
            baglanti.Open();
            deger = (string)cmd.ExecuteScalar();
            baglanti.Close();
            lblmusteritckontrol.Text = deger;
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

            DialogResult secenek = MessageBox.Show("Müşteri bilgileri güncellensin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komutguncelle = new SqlCommand("update tblmusteri set MusTC=@p2,MusAd=(UPPER(substring(@p3,1,1))+LOWER(right(@p3,(len(@p3)-1)))),MusSoyad=UPPER(@p4),MusCinsiyet=@p1,MusSehir=@psehir,MusDogumTarihi=@p6,MusTel=@p7,MusEposta=@p8,MusHakkinda=@p9,MusCezaPuani=@p10 where MusteriID=@p0", baglanti);
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
                komutguncelle.Parameters.AddWithValue("@p10", txtceza.Text);
                komutguncelle.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Müşteri bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else { }
        }

        void kaydet()
        {
            DialogResult secenek = MessageBox.Show("Yeni müşteri kaydedilsin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secenek == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into tblmusteri (MusTC,MusAd,MusSoyad,MusCinsiyet,MusSehir,MusDogumTarihi,MusTel,MusEposta,MusHakkinda,MusCezaPuani) values (@p1,(UPPER(substring(@p2,1,1))+LOWER(right(@p2,(len(@p2)-1)))),UPPER(@p3),@p4,@p5,@p6,@p7,@p8,@p9,@p10)", baglanti);
                komutkaydet.Parameters.AddWithValue("@p1", msktc.Text);
                komutkaydet.Parameters.AddWithValue("@p2", txtad.Text);
                komutkaydet.Parameters.AddWithValue("@p3", txtsoyad.Text);
                komutkaydet.Parameters.AddWithValue("@p4", CONTROLkaydetcinsiyet.Text);
                komutkaydet.Parameters.AddWithValue("@p5", Convert.ToInt16(CONTROLplaka.Text));
                komutkaydet.Parameters.AddWithValue("@p6", Convert.ToDateTime(mskdogumtarihi.Text));
                komutkaydet.Parameters.AddWithValue("@p7", msktel.Text);
                komutkaydet.Parameters.AddWithValue("@p8", txteposta.Text);
                komutkaydet.Parameters.AddWithValue("@p9", richTextBox1.Text);
                komutkaydet.Parameters.AddWithValue("@p10", 0);
                komutkaydet.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Yeni müşteri başarıyla başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            listBox1.Items.Clear();
            richTextBox1.Text = "";
            txtceza.Text = "";
            rdberkek.Checked = false;
            rdbkadin.Checked = false;
            msktcarama.Text = "";
            txtadarama.Text = "";
            //Kitap Bilgileri Alanı
            lblid.Text = "";
            lblisbn.Text = "";
            lblad.Text = "";
            lblyazar.Text = "";
            lblyayinevi.Text = "";
            lblkategori.Text = "";
            lblraf.Text = "";
            lbltoplamstok.Text = "";
            lblaktifstok.Text = "";
            lblverilenstok.Text = "";
        }
        void sil()
        {
            //messageboxdan gelen cevaba göre işlem yaptırma.
            DialogResult secenek = MessageBox.Show("Seçilen müşteri silinsin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secenek == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komutsil = new SqlCommand("delete from tblmusteri where MusteriID=@p1 ", baglanti);
                komutsil.Parameters.AddWithValue("@p1", txtid.Text);
                komutsil.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Müşteri başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else { }
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
        void musterielindekikitaplar()
        {
            listBox1.Items.Clear();
            baglanti.Open();
            SqlCommand komutkitaplistele = new SqlCommand("execute musterielindekikitaplar @deger=@p1", baglanti);
            komutkitaplistele.Parameters.AddWithValue("@p1", txtid.Text);
            SqlDataReader dr = komutkitaplistele.ExecuteReader();
            while (dr.Read())
            {
                listBox1.Items.Add(dr["KitapAdi"]);
            }
            baglanti.Close();
        }
      
        private void musterilerpaneli_Load(object sender, EventArgs e)
        {
            listele();
            cmbsehirlisteleme();
            gunlukzaman();
            kullaniciadi();
            musterikontrol();
        }

        //Doğru formda e-posta kontrolü
        private void txteposta_Leave(object sender, EventArgs e)
        {
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(txteposta.Text, pattern))
            {
                errorProvider1.Clear();
            }
            else
            {
                errorProvider1.SetError(this.txteposta, "Lütfen geçerli bir e-posta adresi girin.");
                return;
            }
        }

        //TIKLAMA HAREKETİYLE VERİ AKTARIMI****************************************************************
        //veri tablosuna tıklanınca verinin ilgili alanlara aktarılması
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
            txtceza.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
        }
        private void txtid_TextChanged(object sender, EventArgs e)
        {
            musterielindekikitaplar();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string secili = listBox1.SelectedItems[0].ToString();


            int id;
            string ad;
            string isbn;
            string yazar;
            string yayinevi;
            string kategori;
            string raf;
            int toplamstok;
            int aktifstok;
            int verilenstok;

            SqlCommand kitapid = new SqlCommand("execute kitapbilgileriid @deger=@p1", baglanti);
            SqlCommand kitapisbn = new SqlCommand("execute kitapbilgileriisbn @deger=@p1", baglanti);
            SqlCommand kitapadi = new SqlCommand("execute kitapbilgilerikitapadi @deger=@p1", baglanti);
            SqlCommand kitapyazar = new SqlCommand("execute kitapbilgileriyazar @deger=@p1", baglanti);
            SqlCommand kitapyayinevi = new SqlCommand("execute kitapbilgileriyayinevi @deger=@p1", baglanti);
            SqlCommand kitapkategori = new SqlCommand("execute kitapbilgilerikategori @deger=@p1", baglanti);
            SqlCommand kitapraf = new SqlCommand("execute kitapbilgileriraf @deger=@p1", baglanti);
            SqlCommand kts = new SqlCommand("execute kitapbilgileritoplamstok @deger=@p1", baglanti);
            SqlCommand kas = new SqlCommand("execute kitapbilgileriaktifstok @deger=@p1", baglanti);
            SqlCommand kvs = new SqlCommand("execute kitapbilgileriverilenstok @deger=@p1", baglanti);
            kitapid.Parameters.AddWithValue("@p1", secili);
            kitapadi.Parameters.AddWithValue("@p1", secili);
            kitapisbn.Parameters.AddWithValue("@p1", secili);
            kitapyazar.Parameters.AddWithValue("@p1", secili);
            kitapyayinevi.Parameters.AddWithValue("@p1", secili);
            kitapkategori.Parameters.AddWithValue("@p1", secili);
            kitapraf.Parameters.AddWithValue("@p1", secili);
            kts.Parameters.AddWithValue("@p1", secili);
            kas.Parameters.AddWithValue("@p1", secili);
            kvs.Parameters.AddWithValue("@p1", secili);

            baglanti.Open();
            id = (int)kitapid.ExecuteScalar();
            isbn = (string)kitapisbn.ExecuteScalar();
            ad = (string)kitapadi.ExecuteScalar();
            yazar = (string)kitapyazar.ExecuteScalar();
            yayinevi = (string)kitapyayinevi.ExecuteScalar();
            kategori = (string)kitapkategori.ExecuteScalar();
            raf = (string)kitapraf.ExecuteScalar();
            toplamstok = Convert.ToInt32(kts.ExecuteScalar());
            aktifstok = Convert.ToInt32(kas.ExecuteScalar());
            verilenstok = Convert.ToInt32(kvs.ExecuteScalar());
            baglanti.Close();

            lblid.Text = id.ToString();
            lblisbn.Text = isbn;
            lblad.Text = ad;
            lblyazar.Text = yazar;
            lblyayinevi.Text = yayinevi;
            lblkategori.Text = kategori;
            lblraf.Text = raf;
            lbltoplamstok.Text = toplamstok.ToString();
            lblaktifstok.Text = aktifstok.ToString();
            lblverilenstok.Text = verilenstok.ToString();

        }

        // (EN ZORLANDIĞIM KISIM OLDU)
        // Veritabanındaki tblsehirlerden veri alan cmbsehirler comboboxından gelen
        //Sehir adıyla veritabanındaki procedure bağlı olarak filtreleme yapılıp ilgili sehirin Plaka (int) degiskeni sorgulanıyor
        //sorgudan alınan deger lblplaka labelinin Textine string ifade olarak aktarılıyor.

        //ve bu islem cmbsehir comboboxında herhangi bir değişiklik olduğu her an tekrarlanıyor.
        //sonrasında bu lblplaka.Text den gelen degeri guncelleme butonu içinde kullanarak veritabanına 
        //yeni deger olarak atadım.

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

        //*************************** CİNSİYET BUTONU AYARLAMASI ***********************************
        private void lblcinsiyet_TextChanged(object sender, EventArgs e)
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
        private void rdberkek_CheckedChanged(object sender, EventArgs e)
        {
            if (rdberkek.Checked == true)
            {
                CONTROLcinsiyet.Text = "Erkek";
            }
        }
        private void rdbkadin_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbkadin.Checked == true)
            {
                CONTROLcinsiyet.Text = "Kadın";
            }
        }

        // BUTONLAR******************************************************************************************
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
            else if (lblmusteritckontrol.Text == "")
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
            else if (lblmusteritckontrol.Text != "")
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
                MessageBox.Show("Silmek istediğiniz müşteriyi tablodan seçiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
        private void ÇIKIŞToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cikis x = new cikis();
            x.cikisbaglantisi();
            girispaneli gp = new girispaneli();
            gp.Show();
            this.Hide();

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

        private void msktcarama_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select MusteriID as 'ID',MusTC as'TC',MusAd as'Ad',MusSoyad as'Soyad',MusCinsiyet = CASE MusCinsiyet\r\nwhen '1' then 'Erkek'\r\nwhen '0' then 'Kadın'\r\nend,SehirAd as 'Şehir',MusDogumTarihi as 'Doğum Tarihi',MusTel as 'Telefon',MusEposta as 'E-posta',MusHakkinda as 'Hakkında',MusCezaPuani as 'Ceza Puanı'\r\nfrom tblmusteri\r\ninner join tblsehirler\r\non tblmusteri.MusSehir=tblsehirler.Plaka where MusTC like '%" + msktcarama.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }

        private void txtadarama_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select MusteriID as 'ID',MusTC as'TC',MusAd as'Ad',MusSoyad as'Soyad',MusCinsiyet = CASE MusCinsiyet\r\nwhen '1' then 'Erkek'\r\nwhen '0' then 'Kadın'\r\nend,SehirAd as 'Şehir',MusDogumTarihi as 'Doğum Tarihi',MusTel as 'Telefon',MusEposta as 'E-posta',MusHakkinda as 'Hakkında',MusCezaPuani as 'Ceza Puanı'\r\nfrom tblmusteri\r\ninner join tblsehirler\r\non tblmusteri.MusSehir=tblsehirler.Plaka where MusAd+' '+MusSoyad like '%" + txtadarama.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }

        private void btnkitaplar_Click(object sender, EventArgs e)
        {
            kitaplarpaneli kp = new kitaplarpaneli();
            kp.Show();
            this.Hide();
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
            musterikontrol();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}

