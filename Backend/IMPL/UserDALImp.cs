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
                    if (user.rol.Equals("C")) {
                        UserDataHistory udh = new UserDataHistory() {
                            date = DateTime.Now,
                            heigth = user.height,
                            weight = user.weight,
                            idUser = user.idUser
                        };
                        using (var u = new UnitWork<UserDataHistory>()) {
                            u.genericDAL.Add(udh);
                            if (u.Complete()) {
                                return user;
                            } else {
                                using (var u2 = new UnitWork<User>()) {
                                    u2.genericDAL.Remove(user);
                                    u2.Complete();
                                }
                            }
                        }
                    } else {
                        return user;
                    }
                }
            } catch (Exception e) {
                Console.Write(e);
            }
            return null;
        }

        public string ValidationUserCreation(User user) {
            string res = "";
            try {
                string key = ConfigurationManager.AppSettings["SecretKey"];
                user.email = Security.Security.EncryptString(key, user.email);
                using (var u = new UnitWork<User>()) {
                    List<User> users = u.genericDAL.Find(o => o.email == user.email || o.dni == user.dni).ToList();
                    foreach (var i in users) {
                        if (user.idUser != i.idUser) {
                            if (i.email.Equals(user.email)) {
                                res = "Este correo ya esta en uso por uno de los usuarios";
                                break;
                            }
                            if (i.dni.Equals(user.dni)) {
                                res = "Esta cédula ya esta en uso";
                                break;
                            }
                        }
                    }
                }
            } catch (Exception e) {
                res = "Error al crear el usuario";
            }
            return res;
        }

        public bool Delete(int idUser)
        {
            try
            {
                User user = this.Get_User(idUser);
                using (context = new DBContext())
                {
                    context.User.Attach(user);
                    context.User.Remove(user);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Update(User user)
        {
            try
            {
                using (context = new DBContext())
                {
                    context.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
