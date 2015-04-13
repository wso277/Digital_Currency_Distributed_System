using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IUserList
    {
        bool addUser(string username, string name, int password);
        bool checkLogin(string username, int password);
        bool changePassword(string username, int oldPassword, int newPassword);
    }
}
