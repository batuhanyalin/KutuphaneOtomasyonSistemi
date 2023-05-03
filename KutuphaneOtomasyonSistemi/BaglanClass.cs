using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace KutuphaneOtomasyonSistemi
{
    internal class BaglanClass
    {
        public static string sqlconnection = ConfigurationManager.ConnectionStrings["KutuphaneOtomasyonSistemi.Properties.Settings.KutuphaneOtomasyonConnectionString"].ConnectionString;
    }
}
