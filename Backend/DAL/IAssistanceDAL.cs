using Backend.Entity;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL {
    public interface IAssistanceDAL {
        List<sp_Report_Assistance_Result> sp_Report_Assistance(DateTime date);

        AssistanceControl CalcAssistante(User u);
    }
}
