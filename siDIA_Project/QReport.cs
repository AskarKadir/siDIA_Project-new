using System;

namespace siDIA_Project
{
    class QReport
    {
        public String strData(string idRmh)
        {
            string str = "select Nama as NamaAnggota, jenis_kelamin as JK, "+
               "tempat_lahir as TmptL, FORMAT (tanggal_lahir , 'dd/MM/yyyy ') as TgL," +
               "agama as Agama, pendidikan as Didik,pekerjaan as Kerja, status_perkawinan as SKawin," +
               "status_bpjs as SBPJS, (select count(No_Reg) from warga where id_rumah = '" + idRmh + "' " +
               "and jenis_kelamin like 'L') as JmlhL," +
               "(select count(No_Reg) from warga where id_rumah = '" + idRmh + "' and jenis_kelamin like 'P') " +
               "as JmlhP from warga where id_rumah = '" + idRmh + "' ";
            str += "select jmlh_KK as JmlhKK, Balita, PUS, WUS, Ibu_Hamil as Hamil, Ibu_Menyusui as BuSui, Lansia, " +
                "Mempunyai_Jamban as Jamban, Mempunyai_Sumber_Air as SumAir, Mempunyai_Tmpt_Sampah as Sampah, " +
                "Mempunyai_Saluran_Limbah as Limbah, Stiker_P4K as P4K, KriteriaRumah as KRumah, " +
                "Jmlh_Total_Anggota_RT as JmlhAnggota from Kesling where id_rumah = '" + idRmh + "'";
            return str;
        }
    }
}
