using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC5FullCalandarPlugin.Models;

namespace MVC5FullCalandarPlugin.Services.Interfaces
{
    public interface IUserDbSet
    {
        void Add(User user);
        void Delete(string email);
        User Get(string email);
        void Update(User user);

    }
}
