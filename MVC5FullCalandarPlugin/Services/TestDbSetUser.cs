using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC5FullCalandarPlugin.Models;
using MVC5FullCalandarPlugin.Services;
using MVC5FullCalandarPlugin.Services.Interfaces;

namespace MVC5FullCalandarPlugin.Services
{
    class TestDbSetUser : IUserDbSet
    {
        public static List<User> Users { get; set; }

        public void Add(User user)
        {
            Users.Add(user);
        }

        public void Delete(string email)
        {
            var user = Users.First(x => x.Email == email);
            Users.Remove(user);
        }

        public User Get(string email)
        {
            return Users.First(x => x.Email == email);
        }

        public void Update(User user)
        {
            var userL = Users.First(x => x.Email == user.Email);
            userL.Days = user.Days;
            user.FirstName = user.FirstName;
        }

        public List<User> GetAll()
        {
            return Users;
        }
    }
}
