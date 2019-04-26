using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Storage;
using MVC5FullCalandarPlugin.Models;
using MVC5FullCalandarPlugin.Services.Interfaces;
using Ninject;
using User = MVC5FullCalandarPlugin.Models.User;

namespace MVC5FullCalandarPlugin.Services.Users
{
    public class Authenication : IAuthenication
    {
        private const string firebaseString = "AIzaSyD88164AWPWtE69-U8PQ7LW6USh-fNClv8";
        private IUserDbSet storageUsers;
        private IUserDbSet userDbSet;

        public Authenication()
        {
            storageUsers = new Storage();
            userDbSet = new UserDbSet();
        }

        public string Registration(string email, string password, string firstName)
        {
            var auth = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(firebaseString));
            var regist = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            var user = new User
            {
                Email = email,
                FirstName = firstName,
                Days = new List<DayModel>()
            };

            userDbSet.Add(user);
            storageUsers.Add(user);

            return Login(email, password);
        }

        public string Login(string email, string password)
        {
            var auth = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(firebaseString));
            try
            {
                var resultAuthorithation = auth.SignInWithEmailAndPasswordAsync(email, password).Result;
                
                var token = resultAuthorithation.FirebaseToken;

                var user = userDbSet.Get(resultAuthorithation.User.Email);
                //user.Token = token;
                storageUsers.Add(user);

                return token;
            }
            catch (AggregateException ex)
            {
            }

            return null;
        }

        //mb not need
        public void LogOut(string token)
        {
            storageUsers.Delete(TokenService.getEmailWithToken(token));
        }

    }
}