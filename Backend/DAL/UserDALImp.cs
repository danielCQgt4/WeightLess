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
                user.password = null;
                user.email = Security.Security.DecryptString(key, user.email);
                return user;
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
                user.password = null;
                user.email = Security.Security.DecryptString(key, user.email);
                return user;
            } catch (Exception e) {
            }
            return null;
        }

        public User Create(User user) {
            try {
                string key = ConfigurationManager.AppSettings["SecretKey"];
                user.email = Security.Security.EncryptString(key, user.email);
                user.password = Security.Security.EncryptString(key, user.password);
                using (var unit = new UnitWork<User>()) {
                    unit.genericDAL.Add(user);
                    bool res = unit.Complete();
                    if (res) {
                        return user;
                    }
                }
            } catch (Exception e) {
            }
            return null;
        }

    }
}
