using System;
using System.Collections.Generic;
using System.Linq;
using MVC5FullCalandarPlugin.Models;
using MVC5FullCalandarPlugin.Services.Interfaces;
using Ninject;


namespace MVC5FullCalandarPlugin.Services.Users
{
    public class Storage : IUserDbSet
    {
        private IUserDbSet userDbSet;
        private static List<User> users;

        public Storage()
        {
            users = new List<User>();
            userDbSet = new UserDbSet();
        }

        public User Get(string email)
        {
            if (!users.Any(x => x.Email == email))
            {
                var user = userDbSet.Get(email);
                users.Add(user);

                return user;
            }
            return users.First(user => user.Email == email);
        }

        public void Update(User user)
        {
            userDbSet.Update(user);
        }

        public void Add(User user)
        {
            users.Add(user);
        }

        public void Delete(string email)
        {
            users.Remove(Get(email));
        }
    }
}