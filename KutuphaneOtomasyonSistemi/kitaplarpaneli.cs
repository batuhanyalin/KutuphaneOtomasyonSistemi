using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace KutuphaneOtomasyonSistemi
{
    public partial class kitaplarpaneli : Form
    {
        public kitaplarpaneli()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(BaglanClass.sqlconnection);

        private void kitaplarpaneli_Load(object sender, EventArgs e)
        {
            gunlukzaman();
            kullaniciadi();
            listele();
            stoktakibi();
            cmbyazarlisteleme();
            cmbyayinevilisteleme();
            cmbkategorilisteleme();
            cmbraflisteleme();
        }
        void verisayisi()
        {
            lblverisayisi.Text = (dataGridView1.RowCount - 1).ToString();
        }

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

        void cmbyazarlisteleme() //veritabanından comboboxa şehir listesi çekme metotu
        {
            baglanti.Open();
            SqlCommand komutsehirlistele = new SqlCommand("select * from tblyazar", baglanti);
            SqlDataReader dr = komutsehirlistele.ExecuteReader();
            while (dr.Read())
            {
                cmbyazar.Items.Add(dr[1] + " " + dr[2]).ToString();
            }
            baglanti.Close();

        }


        void cmbyayinevilisteleme() //veritabanından comboboxa şehir listesi çekme metotu
        {
            baglanti.Open();
            SqlCommand komutsehirlistele = new SqlCommand("select * from tblyayinevi", baglanti);
            SqlDataReader dr = komutsehirlistele.ExecuteReader();
            while (dr.Read())
            {
                cmbyayinevi.Items.Add(dr[1]);
            }
            baglanti.Close();

        }
        void cmbkategorilisteleme() //veritabanından comboboxa şehir listesi çekme metotu
        {
            baglanti.Open();
            SqlCommand komutsehirlistele = new SqlCommand("select * from tblkategori", baglanti);
            SqlDataReader dr = komutsehirlistele.ExecuteReader();
            while (dr.Read())
            {
                cmbkategori.Items.Add(dr[1]);
            }
            baglanti.Close();

        }
        void cmbraflisteleme() //veritabanından comboboxa şehir listesi çekme metotu
        {
            baglanti.Open();
            SqlCommand komutsehirlistele = new SqlCommand("select * from tblraf", baglanti);
            SqlDataReader dr = komutsehirlistele.ExecuteReader();
            while (dr.Read())
            {
                cmbraf.Items.Add(dr[1]);
            }
            baglanti.Close();

        }







        void listele()
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("execute kitaplarpaneli", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }

        void guncelle()
        {
            DialogResult secenek = MessageBox.Show("Kitap bilgileri güncellensin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secenek == DialogResult.No)
            {

            }
            else if (txttoplamstok.Text == "")
            {
                MessageBox.Show("Toplam stok değeri boş bırakılamaz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            else if (Convert.ToInt32(txtaktifstok.Text) + Convert.ToInt32(txtverilenstok.Text) > Convert.ToInt32(txttoplamstok.Text))
            {
                MessageBox.Show("Azalış işlemi yaparken toplam stok miktarında artış yapılamaz. Lütfen yapmak istediğiniz işlem türünü doğru seçiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Convert.ToInt32(CONTROLyenitoplamstok.Text) < Convert.ToInt32(txtverilenstok.Text))
            {
                MessageBox.Show("Yeni girilen toplam stok değeri verilen stok değerinden az olamaz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else if (Convert.ToInt32(txtaktifstok.Text) < 0 || (Convert.ToInt32(txtaktifstok.Text) + Convert.ToInt32(txtverilenstok.Text)) < Convert.ToInt32(txttoplamstok.Text))
            {
                MessageBox.Show("Azalış işlemi yaparken toplam stok miktarında artış yapılamaz. Lütfen yapmak istediğiniz işlem türünü doğru seçiniz.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (secenek == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komutguncelle = new SqlCommand("update tblkitap set ISBN=@p2,KitapAdi=@p3,Yazar=@p4,Yayınevi=@p5,Kategori=@p6,Raf=@p7,ToplamStok=@p8,AktifStok=@p9 where KitapID=@p1", baglanti);
                komutguncelle.Parameters.AddWithValue("@p1", txtid.Text);
                komutguncelle.Parameters.AddWithValue("@p2", mskisbn.Text);
                komutguncelle.Parameters.AddWithValue("@p3", txtad.Text);
                komutguncelle.Parameters.AddWithValue("@p4", Convert.ToInt16(CONTROLyazar.Text));
                komutguncelle.Parameters.AddWithValue("@p5", Convert.ToInt16(CONTROLyayinevi.Text));
                komutguncelle.Parameters.AddWithValue("@p6", Convert.ToInt16(CONTROLkategori.Text));
                komutguncelle.Parameters.AddWithValue("@p7", Convert.ToInt16(CONTROLraf.Text));
                komutguncelle.Parameters.AddWithValue("@p8", Convert.ToInt32(txttoplamstok.Text));
                komutguncelle.Parameters.AddWithValue("@p9", Convert.ToInt32(txtaktifstok.Text));
                komutguncelle.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kitap bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else
            {
                MessageBox.Show("asdasdasd", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        void kaydet()
        {
            DialogResult secenek = MessageBox.Show("Yeni kitap kaydedilsin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (secenek == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into tblkitap (ISBN,KitapAdi,Yazar,Yayınevi,Kategori,Raf,ToplamStok,AktifStok,VerilenStok,ToplamOkunmaSayisi) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p7,0,0)", baglanti);
                komutkaydet.Parameters.AddWithValue("@p1", mskisbn.Text);
                komutkaydet.Parameters.AddWithValue("@p2", txtad.Text);
                komutkaydet.Parameters.AddWithValue("@p3", Convert.ToInt16(CONTROLyazar.Text));
                komutkaydet.Parameters.AddWithValue("@p4", Convert.ToInt16(CONTROLyayinevi.Text));
                komutkaydet.Parameters.AddWithValue("@p5", Convert.ToInt16(CONTROLkategori.Text));
                komutkaydet.Parameters.AddWithValue("@p6", Convert.ToInt16(CONTROLraf.Text));
                komutkaydet.Parameters.AddWithValue("@p7", Convert.ToInt32(txttoplamstok.Text));
                komutkaydet.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Yeni kitap başarıyla başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else { }

        }
        void sil()
        {
            DialogResult secenek = MessageBox.Show("Seçilen kitap silinsin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secenek == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komutsil = new SqlCommand("delete from tblkitap where KitapID=@p1 ", baglanti);
                komutsil.Parameters.AddWithValue("@p1", txtid.Text);
                komutsil.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kitap başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            else { }
        }
        void temizle()
        {
            txtid.Text = "";
            mskisbn.Text = "";
            txtad.Text = "";
            cmbyazar.Text = "";
            cmbyayinevi.Text = "";
            cmbkategori.Text = "";
            cmbraf.Text = "";

            txttoplamstok.Text = "";
            txtaktifstok.Text = "0";
            txtverilenstok.Text = "0";
            lbltoplamokunmasayisi.Text = "";

            mskisbnara.Text = "";
            txtkitapadara.Text = "";
            txtkitapyazarara.Text = "";
                  
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



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            mskisbn.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtad.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cmbyazar.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            cmbyayinevi.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            cmbkategori.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            cmbraf.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txttoplamstok.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            txtaktifstok.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            txtverilenstok.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            lbltoplamokunmasayisi.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            CONTROLeskitoplamstok.Text = txttoplamstok.Text;
            CONTROLeskiaktifstok.Text = txtaktifstok.Text;

        }

        private void mskisbnara_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select KitapID,ISBN,KitapAdi as 'Kitap',YazarAdi+' '+YazarSoyadi as 'Yazar',YayineviAd as 'Yayınevi', KategoriAd as 'Kategori', RafAd as 'Raf',ToplamStok,AktifStok,VerilenStok,ToplamOkunmaSayisi\r\nfrom tblkitap\r\ninner join tblyazar\r\non tblkitap.Yazar=tblyazar.YazarID\r\ninner join tblyayinevi\r\non tblkitap.Yayınevi=tblyayinevi.YayineviID\r\ninner join tblkategori\r\non tblkitap.Kategori=tblkategori.KategoriID\r\ninner join tblraf\r\non tblkitap.Raf=tblraf.RafID where ISBN like '%"+mskisbnara.Text+"%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }

        private void txtkitapadara_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select KitapID,ISBN,KitapAdi as 'Kitap',YazarAdi+' '+YazarSoyadi as 'Yazar',YayineviAd as 'Yayınevi', KategoriAd as 'Kategori', RafAd as 'Raf',ToplamStok,AktifStok,VerilenStok,ToplamOkunmaSayisi\r\nfrom tblkitap\r\ninner join tblyazar\r\non tblkitap.Yazar=tblyazar.YazarID\r\ninner join tblyayinevi\r\non tblkitap.Yayınevi=tblyayinevi.YayineviID\r\ninner join tblkategori\r\non tblkitap.Kategori=tblkategori.KategoriID\r\ninner join tblraf\r\non tblkitap.Raf=tblraf.RafID where KitapAdi like '%" + txtkitapadara.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }

        private void txtkitapyazarara_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select KitapID,ISBN,KitapAdi as 'Kitap',YazarAdi+' '+YazarSoyadi as 'Yazar',YayineviAd as 'Yayınevi', KategoriAd as 'Kategori', RafAd as 'Raf',ToplamStok,AktifStok,VerilenStok,ToplamOkunmaSayisi\r\nfrom tblkitap\r\ninner join tblyazar\r\non tblkitap.Yazar=tblyazar.YazarID\r\ninner join tblyayinevi\r\non tblkitap.Yayınevi=tblyayinevi.YayineviID\r\ninner join tblkategori\r\non tblkitap.Kategori=tblkategori.KategoriID\r\ninner join tblraf\r\non tblkitap.Raf=tblraf.RafID where YazarAdi+' '+YazarSoyadi like '%" + txtkitapyazarara.Text + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            verisayisi();
        }

        private void txttoplamstok_TextChanged(object sender, EventArgs e)

        {
            if (lblislem.Text == "")
            {
                MessageBox.Show("Öncelikle yapmak istediğiniz işlem türünü seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                stoktakibi();
            }

        }

        public string toplamstokkaydet()
        {
            string komut = txttoplamstok.Text;
            var bosdeger = string.Empty;
            if (komut.ToString() != null)
            {
                bosdeger = Convert.ToString(komut);
            }
            return CONTROLyenitoplamstok.Text = bosdeger;
        }
        void controlstokdegersifirlama()
        {
            if (CONTROLyenitoplamstok.Text == "")
            {
                CONTROLyenitoplamstok.Text = "0";
            }
            if (CONTROLeskitoplamstok.Text == "")
            {
                CONTROLeskitoplamstok.Text = "0";
            }
            if (CONTROLeskiaktifstok.Text == "")
            {
                CONTROLeskiaktifstok.Text = "0";
            }
        }
        void stoktakibi()
        {
            toplamstokkaydet();
            controlstokdegersifirlama();

            if (lblislem.Text == "ARTIŞ")
            {
                int eskitoplamstok, yenitoplamstok, toplamstokfark;
                eskitoplamstok = Convert.ToInt32(CONTROLeskitoplamstok.Text);
                yenitoplamstok = Convert.ToInt32(CONTROLyenitoplamstok.Text);
                toplamstokfark = Convert.ToInt32(yenitoplamstok - eskitoplamstok);
                lbltoplamstokfark.Text = Math.Abs(toplamstokfark).ToString();
                int eskiaktifstok = Convert.ToInt32(CONTROLeskiaktifstok.Text);
                int toplamstokfark2 = Convert.ToInt32(lbltoplamstokfark.Text);
                int islem = eskiaktifstok + toplamstokfark2;
                txtaktifstok.Text = islem.ToString();
            }
            else if (lblislem.Text == "AZALIŞ")
            {
                int eskitoplamstok, yenitoplamstok, toplamstokfark;
                eskitoplamstok = Convert.ToInt32(CONTROLeskitoplamstok.Text);
                yenitoplamstok = Convert.ToInt32(CONTROLyenitoplamstok.Text);

                toplamstokfark = Convert.ToInt32(yenitoplamstok - eskitoplamstok);
                lbltoplamstokfark.Text = Math.Abs(toplamstokfark).ToString();
                int eskiaktifstok = Convert.ToInt32(CONTROLeskiaktifstok.Text);
                int toplamstokfark2 = Convert.ToInt32(lbltoplamstokfark.Text);
                int islem = eskiaktifstok - toplamstokfark2;
                txtaktifstok.Text = islem.ToString();
            }

        }

        private void txttoplamstok_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void mskisbn_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtid.Text = "";
            mskisbn.Text = "";
            txtad.Text = "";
            cmbyazar.Text = "";
            cmbyayinevi.Text = "";
            cmbkategori.Text = "";
            cmbraf.Text = "";
            txttoplamstok.Text = "";
            txtaktifstok.Text = "0";
            txtverilenstok.Text = "0";
            lbltoplamokunmasayisi.Text = "";
            mskisbnara.Text = "";
            txtkitapadara.Text = "";
            txtkitapyazarara.Text = "";
            
        }

        //Geriye değer döndüren combobox kontrol metotları
        //******************************************************************
        public string cmbyazarID()
        {
            SqlCommand komut = new SqlCommand("execute kitaplarpaneli_seciliyazar @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", cmbyazar.Text);
            baglanti.Open();
            var sonuc = komut.ExecuteScalar();
            var bosdeger = string.Empty;
            if (sonuc != DBNull.Value)
            {
                bosdeger = Convert.ToString(sonuc);
            }
            baglanti.Close();
            return CONTROLyazar.Text = bosdeger;
        }

        public string cmbyayineviID()
        {
            SqlCommand komut = new SqlCommand("execute kitaplarpaneli_seciliyayinevi @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", cmbyayinevi.Text);
            baglanti.Open();
            var sonuc = komut.ExecuteScalar();
            var bosdeger = string.Empty;
            if (sonuc != DBNull.Value)
            {
                bosdeger = Convert.ToString(sonuc);
            }
            baglanti.Close();
            return CONTROLyayinevi.Text = bosdeger;
        }
        public string cmbkategoriID()
        {
            SqlCommand komut = new SqlCommand("execute kitaplarpaneli_secilikategori @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", cmbkategori.Text);
            baglanti.Open();
            var sonuc = komut.ExecuteScalar();
            var bosdeger = string.Empty;
            if (sonuc != DBNull.Value)
            {
                bosdeger = Convert.ToString(sonuc);
            }
            baglanti.Close();
            return CONTROLkategori.Text = bosdeger;
        }
        public string cmbrafID()
        {
            SqlCommand komut = new SqlCommand("execute kitaplarpaneli_seciliraf @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", cmbraf.Text);
            baglanti.Open();
            var sonuc = komut.ExecuteScalar();
            var bosdeger = string.Empty;
            if (sonuc != DBNull.Value)
            {
                bosdeger = Convert.ToString(sonuc);
            }
            baglanti.Close();
            return CONTROLraf.Text = bosdeger;
        }
        //******************************************************************


        private void cmbyazar_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbyazarID();
        }

        private void cmbyayinevi_SelectedIndexChanged(object sender, EventArgs e)
        {

            cmbyayineviID();
        }

        private void cmbkategori_SelectedIndexChanged(object sender, EventArgs e)

        {
            cmbkategoriID();
        }

        private void cmbraf_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbrafID();
        }

        private void cmbraf_TextChanged(object sender, EventArgs e)
        {
            cmbrafID();
        }

        private void cmbkategori_TextChanged(object sender, EventArgs e)
        {
            cmbkategoriID();
        }

        private void cmbyayinevi_TextChanged(object sender, EventArgs e)
        {
            cmbyayineviID();
        }

        private void cmbyazar_TextChanged(object sender, EventArgs e)
        {
            cmbyazarID();
        }

        private void btnlistele_Click(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            if ((mskisbn.Text.Length < 13) || (string.IsNullOrEmpty(txtad.Text) || (cmbyazar.SelectedIndex == -1) || (CONTROLyazar.Text == "") || (CONTROLyayinevi.Text == "") || (CONTROLkategori.Text == "") || (CONTROLraf.Text == "")))
            {
                MessageBox.Show("Tüm bilgileri eksiksiz ve doğru girmeden kayıt yapamazsınız.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                kaydet();
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            if (txtid.Text == "")
            {
                MessageBox.Show("Güncellemek istediğiniz kitabı tablodan seçiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if ((mskisbn.Text.Length < 13) || (string.IsNullOrEmpty(txtad.Text) || (CONTROLyazar.Text == "") || (CONTROLyayinevi.Text == "") || (CONTROLkategori.Text == "") || (CONTROLraf.Text == "")))
            {
                MessageBox.Show("Tüm bilgileri eksiksiz ve doğru girmeden güncelleme yapamazsınız.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                guncelle();
            }
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            if (txtid.Text == "")
            {
                MessageBox.Show("Silmek istediğiniz kitabı tablodan seçiniz.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                sil();
            }
        }

        private void btntoplamstokartis_Click(object sender, EventArgs e)
        {
            lblislem.Text = "ARTIŞ";
            temizle();
        }

        private void btntoplamstokazalis_Click(object sender, EventArgs e)
        {
            lblislem.Text = "AZALIŞ";
            temizle();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
