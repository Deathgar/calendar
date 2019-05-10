using System.Collections.Generic;
using System.Web.Helpers;
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

            var users = new List<User>();

            foreach (var v in j)
            {
                var tempUser = new User();
                var daysList = new List<DayModel>();
                
                var userJson = v.Value;

                var days = userJson.Days;

        //public string Id { get; set; }
        //public string Time { get; set; }
        //public string Title { get; set; }
        //public string Description { get; set; }
        //public string Start_Date { get; set; }
        //public string End_Date { get; set; }
        //public ImageHTML Image { get; set; }

                if (days != null)
                {
                    foreach (var day in days)
                    {
                        var tempNewDay = new DayModel() {PublicHolidays = new List<PublicHoliday>()};
                        var tempDay = day.PublicHolidays;

                        foreach (var holiday in tempDay)
                        {
                            ImageHTML tempImage;

                            if (holiday.Image == null)
                            {
                                tempImage = null;
                            }
                            else
                            {
                                tempImage = new ImageHTML() { Id = holiday.Image.Id, Url = holiday.Image.Url };
                            }

                            var tempHoliday = new PublicHoliday() { Id = holiday.Id, Time = holiday.Time, Title = holiday.Title, Description = holiday.Description,
                                                                    Start_Date = holiday.Start_Date, End_Date = holiday.End_Date, Image = tempImage};
                            tempNewDay.PublicHolidays.Add(tempHoliday);
                            tempNewDay.Date = holiday.End_Date;
                        }

                        daysList.Add(tempNewDay);
                    }

                    tempUser.Email = userJson.Email;
                    tempUser.FirstName = userJson.FirstName;
                    tempUser.Days = daysList;
                    users.Add(tempUser);
                }
            }

            return users;
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