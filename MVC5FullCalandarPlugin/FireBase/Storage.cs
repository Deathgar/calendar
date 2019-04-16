using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC5FullCalandarPlugin.FireBase.Users;
using MVC5FullCalandarPlugin.Models;


namespace MVC5FullCalandarPlugin.FireBase
{
    public class Storage
    {
        private static Storage instance;
        private List<User> users;

        private Storage()
        {
            users = new List<User>();
        }

        public static Storage getInstance()
        {
            if (instance == null)
                instance = new Storage();
            return instance;
        }

        public User GetUser(string email)
        {
            if (!users.Any(x => x.Email == email))
            {
                var uS = new UserService();
                var user = uS.GetUser(email);
                users.Add(user);

                return user;
            }
            return users.First(user => user.Email == email);
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public void RemoveUser(string email)
        {
            users.Remove(GetUser(email));
        }

    }
}