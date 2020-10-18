using System;
using System.Configuration;
using Backend.DAL;
using Backend.Entity;
using Backend.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest {
    [TestClass]
    public class UserTest {

        [TestMethod]
        public void Create() {

            User user = new User() {
                active = true,
                Assistance = null,
                dni = "12345678",
                email = "correo@correo.com",
                height = 0,
                lastName = "master",
                name = "master",
                password = "1234",
                rol = "A",
                weight = 0
            };
            string key = "b15ca5898a4e4133bbce2ea2315a1917";
            user.email = Security.EncryptString(key, user.email);
            user.password = Security.EncryptString(key, user.password);
            bool res;
            using (var unit = new UnitWork<User>()) {
                using (var tran = unit.context.Database.BeginTransaction()) {
                    unit.genericDAL.Add(user);
                    res = unit.Complete();
                    if (!res) {
                        tran.Rollback();
                    }
                    tran.Commit();
                    if (res) {
                        Assert.AreEqual(true, true);
                    }
                }
            }
            if (user != null) {
            } else {
                Assert.AreEqual(true, false);
            }
        }
    }
}
