using System.Collections.Generic;
using FireSharp.Interfaces;
using FireSharp.Response;
using MVC5FullCalandarPlugin;
using MVC5FullCalandarPlugin.Models;
using MVC5FullCalandarPlugin.Services.Interfaces;
using NUnit.Framework;
using NUnit.Framework.Internal;
using FirebaseConfig = FireSharp.Config.FirebaseConfig;


namespace MVC5FullCalandarPlugin.Services.Users
{
    public class UserDbSet : IUserDbSet
    {
        private const string userPathPart = "Users/";

        private IFirebaseConfig config = new FirebaseConfig
        {

            AuthSecret = "2zIQovx5SfYZ18tQ3kaPtm9H3JX6GE4ur1pSTVlK",
            BasePath = "https://testcalendar-27287.firebaseio.com/"
        };

        private IFirebaseClient client;

        private IFirebaseClient Client
        {
            get
            {
                if (client == null)
                {
                    client = new FireSharp.FirebaseClient(config);
                    return client;
                }
                return client;
            }
        }

        public void Add(User user)
        {
            SetResponse response = Client.Set(userPathPart + UpdateInGoodString(user.Email), user);
        }

        public void Update(User user)
        {
            SetResponse response = Client.Set(userPathPart + UpdateInGoodString(user.Email), user);
        }

        public List<User> GetAll()
        {
            var response = Client.Get(userPathPart);
            var j = System.Web.Helpers.Json.Decode(response.Body);

            foreach (var v in j)
            {
                var l = v;
            }
            
            return null;
        }

        public void Delete(string email)
        {
            Client.Delete(userPathPart + UpdateInGoodString(email));
        }

        public User Get(string email)
        {
            FirebaseResponse response = Client.Get(userPathPart + UpdateInGoodString(email));

            return response.ResultAs<User>();
        }

        private string UpdateInGoodString(string str)
        {
            string[] stringsSplitMail = str.Split('@');
            string[] stringsSplitPoint = stringsSplitMail[1].Split('.');

            return stringsSplitMail[0] + "@" + stringsSplitPoint[0] + " " + stringsSplitPoint[1];
        }

        
    }
}