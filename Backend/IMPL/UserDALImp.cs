using Backend.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Backend.DAL {
    public class UserDALImp : IUserDAL {

        private DBContext context;

        public User Get_User(int id) {
            User user;
            try {
                string key = ConfigurationManager.AppSettings["SecretKey"];
                using (var u = new UnitWork<User>()) {
                    user = u.genericDAL.Get(id);
                }
                if (user != null) {
                    user.password = null;
                    user.email = Security.Security.DecryptString(key, user.email);
                    return user;
                }
            } catch (Exception e) {
            }
            return null;
        }

        public User Validate_LogIn(string email, string password) {
            User user;
            try {
                string key = ConfigurationManager.AppSettings["SecretKey"];
                email = Security.Security.EncryptString(key, email);
                password = Security.Security.EncryptString(key, password);
                using (context = new DBContext()) {
                    user = context.validate_login(email, password).FirstOrDefault();
                }
                if (user != null) {
                    user.password = null;
                    user.email = Security.Security.DecryptString(key, user.email);
                    return user;
                }
            } catch (Exception e) {
            }
            return null;
        }

        public User Create(User user) {
            try {
                string key = ConfigurationManager.AppSettings["SecretKey"];
                user.email = Security.Security.EncryptString(key, user.email);
                user.password = Security.Security.EncryptString(key, user.password);
                bool res;
                using (var unit = new UnitWork<User>()) {
                    unit.genericDAL.Add(user);
                    res = unit.Complete();
                }
                if (res) {
                    return user;
                }
                //using (var unit = new UnitWork<User>()) {
                //    using (var tran = unit.context.Database.BeginTransaction()) {
                //        unit.genericDAL.Add(user);
                //        res = unit.Complete();
                //        if (!res) {
                //            tran.Rollback();
                //        }
                //        tran.Commit();
                //        if (res) {
                //            return user;
                //        }
                //    }
                //}
            } catch (Exception e) {
            }
            return null;
        }

        public string ValidationUserCreation(User user) {
            string res = "";
            try {
                using (var u = new UnitWork<User>()) {
                    List<User> users = u.genericDAL.Find(o => o.email == user.email || o.dni == user.dni).ToList();
                    foreach (var i in users) {
                        if (i.email.Equals(user.email)) {
                            res = "Este correo ya esta en uso por uno de los usuarios";
                            break;
                        }
                        if (i.dni.Equals(user.dni)) {
                            res = "Este cédula ya esta en uso";
                            break;
                        }
                    }
                }
            } catch (Exception e) {
                res = "Error al crear el usuario";
            }
            return res;
        }
    }
}
