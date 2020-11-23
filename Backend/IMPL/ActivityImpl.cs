using Backend.DAL;
using Backend.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.IMPL {

    public class ActivityImpl : IActivityDAL {

        public List<Activity> GetActivities() {
            List<Activity> actvs;
            using (var u = new UnitWork<Activity>()) {
                actvs = u.genericDAL.GetAll().ToList();
            }
            return actvs;
        }

        public byte[] PlaceQRInActivity(string url, int id) {
            QRImpl QRimpl = new QRImpl();
            byte[] QRimage = QRimpl.genQR(url + "Activity/ActiveActivity/" + id);
            return QRimage;
        }

        public Activity_Assitance GetCurrentActivity(int idAissistance) {
            Activity_Assitance aa = null;
            using (var u = new UnitWork<Activity_Assitance>()) {
                List<Activity_Assitance> acts = u.genericDAL.Find(o => o.end == null && o.kcal == -5 && o.idAssistance == idAissistance).ToList();
                if (acts != null && acts.Count() > 0) {
                    aa = acts.First();
                }
            }
            return aa;
        }

        public Activity_Assitance StartActivity(Activity_Assitance aa) {
            bool res = false;
            using (var u = new UnitWork<Activity_Assitance>()) {
                u.genericDAL.Add(aa);
                res = u.Complete();
            }
            return res ? aa : null;
        }

        public object UpdateTime(string timeOcurred, int idActivityAssistance) {
            bool res = false;
            bool upd = false;
            bool finished = false;
            using (var u = new UnitWork<Activity_Assitance>()) {
                Activity_Assitance aa = u.genericDAL.Get(idActivityAssistance);
                if (aa != null) {
                    finished = aa.end == null;
                    if (finished) {
                        aa.timeOcurred = timeOcurred;
                        u.genericDAL.Update(aa);
                        res = u.Complete();
                    }
                }
            }
            return new { res, timeOcurred, upd, finished };
        }

        public bool StopTime(int idActivityAssistance, int idUser) {
            bool res = false;
            using (var u = new UnitWork<Activity_Assitance>()) {
                Activity_Assitance aa = u.genericDAL.Get(idActivityAssistance);
                using (var un = new UnitWork<Activity>()) {
                    string[] parts = aa.timeOcurred.Split(':');
                    User usu;
                    using (var unUsu = new UnitWork<User>()) {
                        usu = unUsu.genericDAL.Get(idUser);
                    }
                    Activity act = un.genericDAL.Get(aa.idActivity);
                    if (act != null) {
                        aa.kcal = act.met * 0.0175m * usu.weight;
                    }
                }
                aa.status = false;
                aa.end = DateTime.Now;
                u.genericDAL.Update(aa);
                res = u.Complete();
            }
            return res;
        }

        public string GetCurTime(int idActivityAssistance) {
            string cur = "00:00:00";
            using (var u = new UnitWork<Activity_Assitance>()) {
                Activity_Assitance aa = u.genericDAL.Get(idActivityAssistance);
                if (aa != null) {
                    cur = aa.timeOcurred;
                }
            }
            return cur;
        }

    }
}
