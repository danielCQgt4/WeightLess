using Backend.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.DAL {
    public interface IUserDAL {
        User Validate_LogIn(string email, string password);
        User Get_User(int id);
        User Create(User user);
    }
}
