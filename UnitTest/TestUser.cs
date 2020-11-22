using System;
using System.Collections.Generic;
using System.Configuration;
using Backend.DAL;
using Backend.Entity;
using Backend.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest {

    [TestClass]
    public class TestUser {

        [TestMethod]
        public void CreateAdmin()
        {

            User user = new User() {
                active = true,
                Assistance = null,
                dni = "12345678",
                email = "correo@correo.com",
                height = 0,
                lastName = "prueba",
                name = "prueba",
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

        //[TestMethod]
        //public void CreateClient() {

        //    Assistance ass = new Assistance();
        //    ass.idAssistance = 1;
        //    ass.date = DateTime.Now;
        //    ass.time = DateTime.Now;
        //    ass.idUser = 2;

        //    List<Assistance> listAss = new List<Assistance>();
        //    listAss.Add(ass);

        //    User user = new User() {
        //        active = true,
        //        Assistance = listAss,
        //        dni = "111111111",
        //        email = "cliente@cliente.com",
        //        height = 180,
        //        lastName = "cliente",
        //        name = "cliente",
        //        password = "1234",
        //        rol = "C",
        //        weight = 80
        //    };
        //    string key = "b15ca5898a4e4133bbce2ea2315a1917";
        //    user.email = Security.EncryptString(key, user.email);
        //    user.password = Security.EncryptString(key, user.password);
        //    bool res;
        //    using (var unit = new UnitWork<User>()) {
        //        using (var tran = unit.context.Database.BeginTransaction()) {
        //            unit.genericDAL.Add(user);
        //            res = unit.Complete();
        //            if (!res) {
        //                tran.Rollback();
        //            }
        //            tran.Commit();
        //            if (res) {
        //                Assert.AreEqual(true, true);
        //            }
        //        }
        //    }
        //    if (user != null) {
        //    } else {
        //        Assert.AreEqual(true, false);
        //    }
        //}

        [TestMethod]
        public void Get()
        {
            IEnumerable<User> user;
            using (var unitUser = new UnitWork<User>())
            {
                user = unitUser.genericDAL.GetAll();
                Assert.AreEqual(true, (user != null));
            }
        }
        [TestMethod]
        public void TestDelete()
        {
            User user;
            using (var unitU = new UnitWork<User>())
            {
                user = unitU.genericDAL.Get(93);
            }
            using (var unitU = new UnitWork<User>())
            {
                unitU.genericDAL.Remove(user);
                Assert.AreEqual(true, unitU.Complete());
            }
        }

        [TestMethod]
        public void TestEdit()
        {
            User user;
            using (var unitU = new UnitWork<User>())
            {
                user = unitU.genericDAL.Get(94);
               
            }
            using (var unitU = new UnitWork<User>())
            {
                user.name = "Prueba modificada";
                unitU.genericDAL.Update(user);
                Assert.AreEqual(true, unitU.Complete());
            }
        }


    }
  }

