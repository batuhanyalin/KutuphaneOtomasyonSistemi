using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace KutuphaneOtomasyonSistemi
{
    internal class cikis
    {

        SqlConnection baglanti = new SqlConnection(BaglanClass.sqlconnection);

        public SqlCommand cikisbaglantisi()
        {
            baglanti.Open();
            SqlCommand cikkomut = new SqlCommand("delete from tblgirislog",baglanti);
            cikkomut.ExecuteNonQuery();
            baglanti.Close();
            return cikkomut;
        }
    }
}
