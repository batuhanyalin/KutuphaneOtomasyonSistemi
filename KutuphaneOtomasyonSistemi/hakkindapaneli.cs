using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyonSistemi
{
    public partial class hakkindapaneli : Form
    {
        public hakkindapaneli()
        {
            InitializeComponent();
        }

        private void hakkindapaneli_Load(object sender, EventArgs e)
        {
            versiyon v=new versiyon();
            lblversiyon.Text = v.programversiyonu;          
            lblaciklama.Text = "Bu program proje odaklı tüm her şeyiyle tamamen tarafımdan\r\nüretilmiştir. Program fikri mülkiyete tabidir, içeriği izinsiz\r\nkopyalanıp çoğaltılamaz. Herhangi bir talebiniz için iletişim\r\nkanallarını kullanın.";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("www.batuhanyalin.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto: info@batuhanyalin.com");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("www.linkedin.com/in/batuhanyalin/");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("www.github.com/batuhanyalin/");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("www.youtube.com/batuhanyalin/");
        }
    }
}
