using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC5FullCalandarPlugin.Services.Interfaces
{
    interface IAuthenication
    {
        string Login(string email, string password);
        string Registration(string email, string password, string firstName);
        void LogOut(string token);
    }
}
