using Backend.DAL;
using Backend.Entity;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.IMPL {

    public class AssistanceDALImp : IAssistanceDAL {

        private DBContext context;

        private Assistance CreateNewAssistance(User usu) {
            Assistance asis = new Assistance() {
                datetime = DateTime.Now,
                idUser = usu.idUser
            };
            bool res;
            using (var uas = new UnitWork<Assistance>()) {
                uas.genericDAL.Add(asis);
                res = uas.Complete();
            }
            if (res) {
                return asis;
            }
            return null;
        }

        public AssistanceControl CalcAssistante(User usu) {
            AssistanceControl Ac = new AssistanceControl();
            int caseAction = -1;
            string actualDt = DateTime.Now.ToString().Split(' ')[0];
            using (var u = new UnitWork<Assistance>()) {
                int idAsis = -1;
                try {
                    idAsis = u.genericDAL.Find(a => a.idUser == usu.idUser).Max(a => a.idAssistance);
                } catch (Exception e) {

                }
                if (idAsis != -1) {
                    Assistance assistance = u.genericDAL.Get(idAsis);
                    if (assistance != null) {
                        string calcDt = assistance.datetime.ToString().Split(' ')[0];
                        if (calcDt.Equals(actualDt)) {
                            Ac.Assistance = assistance;
                            caseAction = -2;
                        } else {
                            Assistance asis = CreateNewAssistance(usu);
                            if (asis != null) {
                                Ac.Assistance = asis;
                                caseAction = -1;
                            } else {
                                caseAction = -3;
                            }
                        }
                    } else {
                        Assistance asis = CreateNewAssistance(usu);
                        if (asis != null) {
                            Ac.Assistance = asis;
                            caseAction = -1;
                        } else {
                            caseAction = -3;
                        }
                    }
                } else {
                    Assistance asis = CreateNewAssistance(usu);
                    if (asis != null) {
                        Ac.Assistance = asis;
                        caseAction = -1;
                    } else {
                        caseAction = -3;
                    }
                }
            }
            Ac.CaseAction = caseAction;
            return Ac;
        }

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
