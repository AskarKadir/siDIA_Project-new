using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siDIA_Project
{
    class QReportKesling
    {
        public String strDataKesling(string idRmh,string nmKRT)
        {
            string str = 
            "select jmlh_KK as JmlhKK, Balita, PUS, WUS, Ibu_Hamil as Hamil, Ibu_Menyusui as BuSui, Lansia, year(getdate()) as Tahun, " +
                "'"+nmKRT+"'"+" as NamaKRT, "+
                "Mempunyai_Jamban as Jamban, Mempunyai_Sumber_Air as SumAir, Mempunyai_Tmpt_Sampah as Sampah, " +
                "Mempunyai_Saluran_Limbah as Limbah, Stiker_P4K as P4K, KriteriaRumah as KRumah, " +
                "Jmlh_Total_Anggota_RT as JmlhAnggota from Kesling where id_rumah = '" + idRmh + "'";
            return str;
        }
    }
}
