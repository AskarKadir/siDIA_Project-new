using CrystalDecisions.CrystalReports.Engine;
using System;

namespace siDIA_Project
{
    class QReport
    {
        public String strData(string idRmh)
        {
            string str = "select Nama as NamaAnggota, jenis_kelamin as JK, " +
               "tempat_lahir as TmptL, FORMAT (tanggal_lahir , 'dd/MM/yyyy ') as TgL," +
               "agama as Agama, pendidikan as Didik,pekerjaan as Kerja, status_perkawinan as SKawin," +
               "status_bpjs as SBPJS,"+
               "(select count(No_Reg) from warga where id_rumah = '" + idRmh + "'" +
               " and jenis_kelamin like 'L')as JmlhL," +
               " (select count(No_Reg) from warga where id_rumah = '" + idRmh + "' and jenis_kelamin like 'P') as JmlhP " +
               " from warga where id_rumah = '" + idRmh + "' ";
            return str;
        }
    }
}
