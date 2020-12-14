using Backend.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL {
    public interface IActivityDAL {

        List<Activity> GetActivities();

        byte[] PlaceQRInActivity(string url, int id);

        Activity_Assitance GetCurrentActivity(int idAissistance);
        
        Activity_Assitance GetActivity_Assistance(int id);

        Activity_Assitance StartActivity(Activity_Assitance aa);

        object UpdateTime(string PtimeOcurred, int idActivityAssistance);

        bool StopTime(int idActivityAssistance, int idUser);

        string GetCurTime(int idActivityAssistance);
    }
}
