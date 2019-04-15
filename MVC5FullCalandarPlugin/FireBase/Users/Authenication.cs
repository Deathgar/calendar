using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Firebase.Auth;
using MVC5FullCalandarPlugin.Models;
using User = MVC5FullCalandarPlugin.Models.User;

namespace MVC5FullCalandarPlugin.FireBase.Users
{
    public class Authenication
    {
        private const string firebaseString = "AIzaSyD88164AWPWtE69-U8PQ7LW6USh-fNClv8";

        public static string Registration(string email, string password, string firstName)
        {
            var auth = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(firebaseString));
            var regist = auth.CreateUserWithEmailAndPasswordAsync(email, password);

            var user = new User
            {
                Email = email,
                FirstName = firstName,
                PublicHolidays = new List<PublicHoliday>()
            };

            var crud = new UserService();
            crud.AddUser(user);

            return Login(email, password);
        }

        private static string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }

        public static string Login(string email, string password)
        {
            var auth = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(firebaseString));
            try
            {
                var resultAuthorithation = auth.SignInWithEmailAndPasswordAsync(email, password).Result;
                
                var token = resultAuthorithation.FirebaseToken;
                var userCrud = new UserService();

                var user = userCrud.GetUser(resultAuthorithation.User.Email);
                //user.Token = token;
                Storage.getInstance().AddUser(user);

                return token;
            }
            catch (AggregateException ex)
            {
            }

            return null;
        }

        //mb not need
        public static void LogOut(string token)
        {
            Storage.getInstance().RemoveUser(TokenService.getEmailWithToken(token));
            //mb redirect
        }

    }
}