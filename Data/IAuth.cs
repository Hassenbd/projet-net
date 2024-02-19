using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projet_net.Data
{
    public interface IAuth
    {
        Task<int>Register (User user, string password);
        Task<string>Login(string username,string password);
        Task<bool>UserExists(string username);
    }
}