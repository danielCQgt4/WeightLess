using Backend.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models {

    public class ActivitieAssistanceViewModel {

        public int idActivityAssistance { get; set; }
        public int idActivity { get; set; }
        public int idAssistance { get; set; }
        public System.DateTime start { get; set; }
        public Nullable<System.DateTime> end { get; set; }
        public decimal kcal { get; set; }
        public string timeOcurred { get; set; }
        public bool status { get; set; }
        public ActivityViewModel activity;

        public ActivitieAssistanceViewModel() { }

        public ActivitieAssistanceViewModel(UserViewModel user, int idActivity) {
            end = null;
            this.idActivity = idActivity;
            kcal = -5;
            start = DateTime.Now;
            status = false;
            timeOcurred = "00:00:00";
            idAssistance = user.assistance.idAssistance;
        }

        public static Activity_Assitance Converter(ActivitieAssistanceViewModel sup) {
            if (sup == null) {
                return null;
            }
            return new Activity_Assitance() {
                idActivityAssistance = sup.idActivityAssistance,
                end = sup.end,
                idActivity = sup.idActivity,
                idAssistance = sup.idAssistance,
                kcal = sup.kcal,
                start = sup.start,
                status = sup.status,
                timeOcurred = sup.timeOcurred
            };
        }

        public static ActivitieAssistanceViewModel Converter(Activity_Assitance sup) {
            if (sup == null) {
                return null;
            }
            return new ActivitieAssistanceViewModel() {
                idActivityAssistance = sup.idActivityAssistance,
                end = sup.end,
                idActivity = sup.idActivity,
                idAssistance = sup.idAssistance,
                kcal = sup.kcal,
                start = sup.start,
                status = sup.status,
                timeOcurred = sup.timeOcurred
            };
        }

        public static List<ActivitieAssistanceViewModel> Converter(List<Activity_Assitance> sup) {
            if (sup == null) {
                return null;
            }
            List<ActivitieAssistanceViewModel> l = new List<ActivitieAssistanceViewModel>();
            foreach (var item in sup) {
                l.Add(Converter(item));
            }
            return l;
        }
    }

}