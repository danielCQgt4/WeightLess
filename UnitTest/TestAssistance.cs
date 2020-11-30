using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Backend.DAL;
using Backend.Entity;
using Backend.IMPL;
using Backend.Models;
using Backend.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest {
    [TestClass]
    public class TestAssistance {

        private User GetFirtsUser() {
            User usu;
            using (var u = new UnitWork<User>()) {
                usu = u.genericDAL.Find(o => o.rol.Equals("C")).First();
            }
            return usu;
        }

        [TestMethod]
        public void CreateAssistance() {
            /*
                -1: to view (well) 
                -2: already with assistance
                -3: Error
            */
            User usu = GetFirtsUser();
            if (usu != null) {
                IAssistanceDAL asis = new AssistanceDALImp();
                AssistanceControl ac = asis.CalcAssistante(usu);

                Assert.AreEqual(true, ac.CaseAction != -2);
            } else {
                Assert.AreEqual(false, true);
            }
        }

        [TestMethod]
        public void GetAssistance() {
            User user = GetFirtsUser();
            List<Assistance> assistances = new List<Assistance>();
            List<Activity> activities;
            using (var u = new UnitWork<Activity>()) {
                activities = u.genericDAL.GetAll().ToList();
            }
            using (var u = new UnitWork<Assistance>()) {
                List<Assistance> asis = u.genericDAL.Find(o => o.idUser == user.idUser).ToList();
                if (asis != null) {
                    assistances = asis;
                    foreach (var a in assistances) {
                        using (var un = new UnitWork<Activity_Assitance>()) {
                            a.Activity_Assitance = un.genericDAL.Find(o => o.idAssistance == a.idAssistance).ToList();
                        }
                        if (a.Activity_Assitance != null) {
                            foreach (var aav in a.Activity_Assitance) {
                                aav.Activity = activities.Find(o => o.idActivity == aav.idActivity);
                            }
                        }
                    }
                }
            }
            Assert.AreEqual(true, assistances != null);
        }

        [TestMethod]
        public void GetCurrentActivityOnUser() {
            IActivityDAL dalAct = new ActivityImpl();
            Activity_Assitance aa = dalAct.GetCurrentActivity(19);
            Assert.AreEqual(true, aa != null);
        }
    }
}
