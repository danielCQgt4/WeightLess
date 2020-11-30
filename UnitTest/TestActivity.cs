using System;
using System.Collections.Generic;
using System.Linq;
using Backend.DAL;
using Backend.Entity;
using Backend.IMPL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest {
    [TestClass]
    public class TestActivity {

        [TestMethod]
        public void GetAll() {
            IActivityDAL dalAct = new ActivityImpl();
            List<Activity> actvs = dalAct.GetActivities();
            Assert.AreEqual(true, actvs != null);
        }

        [TestMethod]
        public void MakeActivity() {
            IActivityDAL dalAct = new ActivityImpl();
            Activity_Assitance aa = dalAct.StartActivity(new Activity_Assitance() {
                idActivity = 1,
                idAssistance = 19,
                end = null,
                kcal = -5,
                start = DateTime.Now,
                status = false,
                timeOcurred = "00:00:00"
            });
            Assert.AreEqual(true, aa != null);
        }
    }
}
