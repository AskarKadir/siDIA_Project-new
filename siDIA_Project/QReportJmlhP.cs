using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siDIA_Project
{
    class QReportJmlhP
    {
        public String strDataPR(string idRmh)
        {
            string str = "select count(No_Reg) as JmlhP from warga where id_rumah = '" + idRmh + "'" +
               " and jenis_kelamin like 'P'";
            return str;
        }
    }
}
