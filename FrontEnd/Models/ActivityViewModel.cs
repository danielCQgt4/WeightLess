using Backend.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace FrontEnd.Models {
    public class ActivityViewModel {

        public int idActivity { get; set; }
        public string name { get; set; }
        public decimal met { get; set; }
        public string link { get; set; }
        public byte[] qrCode { get; set; }

        public string description { get; set; } //For Publications

        public static ActivityViewModel Converter(Activity activity) {
            return new ActivityViewModel() {
                idActivity = activity.idActivity,
                link = activity.link,
                met = activity.met,
                name = activity.name
            };
        }

        public static Activity Converter(ActivityViewModel activity) {
            return new Activity() {
                idActivity = activity.idActivity,
                link = activity.link,
                met = activity.met,
                name = activity.name
            };
        }

        public static List<ActivityViewModel> Converter(List<Activity> actvs) {
            List<ActivityViewModel> activities = new List<ActivityViewModel>();
            foreach (var item in actvs) {
                activities.Add(Converter(item));
            }
            return activities;
        }
    }
}