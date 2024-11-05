using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siDIA_Project
{
    class Koneksi
    {
        public String strKoneksi()
        {
            string strKoneksi = "Data Source = localhost\\SQLEXPRESS; Initial Catalog = siDIA1;" +
            "Integrated Security = True;";
            //string strKoneksi = "Data Source = DESKTOP-9I14KGV\\SQLEXPRESS; Initial Catalog = siDIA1;" +
            //"User ID = sidia; Password = 12345678";
            return strKoneksi;
        }
    }
}
