﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siDIA_Project
{
    class QReportJmlhL
    {
        public String strDataLK(string idRmh)
        {
            string str = "select count(No_Reg) as JmlhL from warga where id_rumah = '" + idRmh + "'" +
               " and jenis_kelamin like 'L'";
            return str;
        }
    }
}
