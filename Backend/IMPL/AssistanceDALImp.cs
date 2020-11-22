using Backend.DAL;
using Backend.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.IMPL {

    public class AssistanceDALImp : IAssistanceDAL {

        private DBContext context;

        List<sp_Report_Assistance_Result> IAssistanceDAL.sp_Report_Assistance(DateTime date) {
            List<sp_Report_Assistance_Result> resultado;
            try {

                using (context = new DBContext()) {
                    resultado = context.sp_Report_Assistance(date).ToList();
                }
                return resultado;
            } catch (Exception e) {
            }
            return null;
        }

    }


}
