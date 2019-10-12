using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AracKiralamaOtomasyon.Facade
{
    class Baglanti
    {
        public static readonly SqlConnection baglan = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=KubilaySimsek;Integrated Security=True");
    }
}
