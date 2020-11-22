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
    public class TestPublication {

        [TestMethod]
        public void CreatePublication() {
            Publication publication = new Publication() {
                datetime = DateTime.Now,
                title = "Publication add TestCase",
                description = "Publication add TestCase",
                type = "N",
                likes = 0,
                disLikes = 0,
                idUser = 64
            };
            using (var unitP = new UnitWork<Publication>()) {
                unitP.genericDAL.Add(publication);
                Assert.AreEqual(true, unitP.Complete());
            }
        }

        [TestMethod]
        public void DeletePublication() {
            Publication publication;
            using (var unitP = new UnitWork<Publication>()) {
                publication = unitP.genericDAL.Get(1088);
            }
            using (var unitP = new UnitWork<Publication>()) {
                unitP.genericDAL.Remove(publication);
                Assert.AreEqual(true, unitP.Complete());
            }
        }

        [TestMethod]
        public void GetPublications() {
            IEnumerable<Publication> pb;
            using (var unitP = new UnitWork<Publication>()) {
                pb = unitP.genericDAL.GetAll();
                Assert.AreEqual(true, (pb != null));
            }
        }

    }
}
