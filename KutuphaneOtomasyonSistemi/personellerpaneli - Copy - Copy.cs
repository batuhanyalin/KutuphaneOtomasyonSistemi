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
using System.IO;
using System.Net.Configuration;

namespace KutuphaneOtomasyonSistemi
{
    public partial class personelpaneli : Form
    {
        public personelpaneli()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-A405D24\\SQLEXPRESS;Initial Catalog=KutuphaneOtomasyon;Integrated Security=True");

        void listeleme()
        {
            SqlCommand listeleme = new SqlCommand("select * from tblpersonel",baglanti);
            SqlDataAdapter da = new SqlDataAdapter(listeleme);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource=dt;
        }
        void guncelleme()
        {
            if (CONTROLcinsiyet.Text == "Erkek")
            {
                CONTROLcinsiyetguncellebuton.Text = "True";
            }
            if (CONTROLcinsiyet.Text == "Kadın")
            {
                CONTROLcinsiyetguncellebuton.Text = "False";
            }
            baglanti.Open();
            SqlCommand komutguncelleme = new SqlCommand("update tblpersonel set TC=@pr1,Ad=@pr2,Soyad=@pr3,Cinsiyet=@pr4,Sehir=@pr5,PerDogumTarih=@pr6,Tel=@pr7,PerEposta=@pr8,PerHakkinda=@pr9 where PerID=@pr0",baglanti);
            komutguncelleme.Parameters.AddWithValue("@pr0", textid.Text);
            komutguncelleme.Parameters.AddWithValue("@pr1", maskedtc.Text);
            komutguncelleme.Parameters.AddWithValue("@pr2", textad.Text);
            komutguncelleme.Parameters.AddWithValue("@pr3", textsoyad.Text);
            komutguncelleme.Parameters.AddWithValue("@pr4", CONTROLcinsiyetguncellebuton.Text);
            komutguncelleme.Parameters.AddWithValue("@pr5", Convert.ToInt16(CONTROLplaka.Text));
            komutguncelleme.Parameters.AddWithValue("@pr6",Convert.ToDateTime(maskeddogumtarihi.Text));
            komutguncelleme.Parameters.AddWithValue("@pr7", maskedtel.Text);
            komutguncelleme.Parameters.AddWithValue("@pr8", texteposta.Text);
            komutguncelleme.Parameters.AddWithValue("@pr9", richTextBox1.Text);
            komutguncelleme.ExecuteNonQuery();
            baglanti.Close();
            listeleme();


        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            maskedtc.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textsoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            CONTROLcinsiyet.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            combosehir.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            maskeddogumtarihi.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            maskedtel.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            texteposta.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            richTextBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();

        }

        private void personellerpaneli_Load(object sender, EventArgs e)
        {
            listeleme();
        }

        private void combosehir_SelectedIndexChanged(object sender, EventArgs e)
        {
            int deger;
            SqlCommand komut = new SqlCommand("execute secilisehir @deger=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", combosehir.Text);
            baglanti.Open();
            deger = (int)komut.ExecuteScalar();
            baglanti.Close();
            CONTROLplaka.Text = deger.ToString();
        }

        private void CONTROLcinsiyet_TextChanged(object sender, EventArgs e)
        {
            if (CONTROLcinsiyet.Text == "Erkek")
            {
                radioberkek.Checked = true;
                CONTROLkaydetcinsiyet.Text = "True";
            }
            if (CONTROLcinsiyet.Text == "Kadın")
            {
                radiobkadin.Checked = false;
                CONTROLkaydetcinsiyet.Text = "False";
            }
        }

        private void radibkadin_CheckedChanged(object sender, EventArgs e)
        {
            if(radiobkadin.Checked==true)
            {
                CONTROLcinsiyet.Text = "Kadın";
            }
        }

        private void radioberkek_CheckedChanged(object sender, EventArgs e)
        {
            if (radioberkek.Checked == true)
            {
                CONTROLcinsiyet.Text = "Erkek";
            }
        }

        private void butonguncelleme_Click(object sender, EventArgs e)
        {
            guncelleme();
        }

        private void butonlisteleme_Click(object sender, EventArgs e)
        {
            listeleme();
        }


        // BUTON METOTLARI --------------------------------------


        //-------------------------------------------
    }
}
