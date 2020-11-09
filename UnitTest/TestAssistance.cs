using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Backend.DAL;
using Backend.Entity;
using Backend.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest {
    [TestClass]
    public class TestAssistance {

        [TestMethod]
        public void CreateAssistance() {
            User usu;
            bool res = false;
            using (var u = new UnitWork<User>()) {
                usu = u.genericDAL.Find(o => o.rol.Equals("C")).First();
            }
            if (usu != null) {
                Assistance asis = new Assistance() {
                    datetime = DateTime.Now,
                    idUser = usu.idUser
                };
                using (var uas = new UnitWork<Assistance>()) {
                    uas.genericDAL.Add(asis);
                    res = uas.Complete();
                }
            }
            Assert.AreEqual(res, true);
        }

    }
}
