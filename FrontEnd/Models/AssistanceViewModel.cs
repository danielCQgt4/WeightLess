using Backend.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models {
    public class AssistanceViewModel {


        public int idAssistance { get; set; }
        public DateTime datetime { get; set; }
        public int idUser { get; set; }
        public string totalTime { get; set; }
        public string totalKcal { get; set; }
        public List<ActivitieAssistanceViewModel> activitieAssistanceViewModel { get; set; }

        public static AssistanceViewModel Converter(Assistance assistance) {
            return new AssistanceViewModel() {
                idUser = assistance.idUser,
                datetime = assistance.datetime,
                idAssistance = assistance.idAssistance
            };
        }

        public static List<AssistanceViewModel> Converter(List<Assistance> assistance) {
            List<AssistanceViewModel> userViewModels = new List<AssistanceViewModel>();
            foreach (var u in assistance) {
                userViewModels.Add(Converter(u));
            }
            return userViewModels;
        }

        public static Assistance Converter(AssistanceViewModel assistance) {
            return new Assistance() {
                idUser = assistance.idUser,
                datetime = assistance.datetime,
                idAssistance = assistance.idAssistance
            };
        }

        public void calculateMetaInformation() {
            int hr = 0, mm = 0, ss = 0;
            decimal kcal = 0;
            foreach (var aav in activitieAssistanceViewModel) {
                string[] parts = aav.timeOcurred.Split(':');
                int ihr = 0, imm = 0, iss = 0;
                ihr = Convert.ToInt32(parts[0]);
                imm = Convert.ToInt32(parts[1]);
                iss = Convert.ToInt32(parts[2]);
                hr += ihr;
                mm += imm;
                ss += iss;
                aav.kcal = aav.kcal * ((ihr * 60) + imm + (iss / 60));
                kcal += aav.kcal;
                aav.timeOcurred = ihr + " horas " + imm + " minutos " + iss + " segundos";
            }
            totalKcal = kcal + " calorias";
            totalTime = calculateTotalTime(hr, mm, ss);
        }

        public string calculateTotalTime(int hr, int mm, int ss) {
            if (ss > 59) {
                ss -= 60;
                mm++;
                return calculateTotalTime(hr, mm, ss);
            }
            if (mm > 59) {
                mm -= 60;
                hr++;
                return calculateTotalTime(hr, mm, ss);
            }
            return hr + " horas " + mm + " minutos " + ss + " segundos";
        }
    }
}