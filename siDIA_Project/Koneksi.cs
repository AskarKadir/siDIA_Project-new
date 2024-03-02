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
            //string strKoneksi = "Data Source = localhost; Initial Catalog = siDIA;" +
            //"Integrated Security = True;";
            string strKoneksi = "Data Source = INAYAHMUFIDAH\\PIYOMARU842; Initial Catalog = siDIA;" +
            "User ID = sa; Password = SeokJin_82";
            return strKoneksi;
        }
    }
}
