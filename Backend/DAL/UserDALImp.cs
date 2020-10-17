using Backend.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL {
    public class UserDALImp : IUserDAL {

        private DBContext context;

        public User Validate_LogIn(string email, string password) {
            User result;
            using (context = new DBContext()) {
                result = context.validate_login(email, password).FirstOrDefault();
            }
            return result;
        }

    }
}
