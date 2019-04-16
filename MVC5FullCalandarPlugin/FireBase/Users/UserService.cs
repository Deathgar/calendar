using FireSharp.Interfaces;
using FireSharp.Response;
using FirebaseConfig = FireSharp.Config.FirebaseConfig;
using User = MVC5FullCalandarPlugin.Models.User;


namespace MVC5FullCalandarPlugin.FireBase.Users
{
    public class UserService
    {
        public static string s;
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

        public void AddUser(User user)// User user
        {
            //var l = new List<PublicHoliday>();
            //l.Add(new PublicHoliday
            //{
            //    Description = "destk",
            //    Start_Date = DateTime.Now.ToString(),
            //    End_Date = DateTime.Today.ToString(),
            //    Time = 5,
            //    Title = "title"
            //});
            //l.Add(new PublicHoliday
            //{
            //    Description = "WWWD",
            //    Start_Date = DateTime.Now.ToString(),
            //    End_Date = DateTime.Today.ToString(),
            //    Time = 8,
            //    Title = "GFW"
            //});
            //var n = new User
            //{
            //    Email = "ru@mail.ru",
            //    FirstName = "pop",
            //    Days = l
            //};
            //SetResponse response = Client.Set("Users/" + UpdateInGoodString(n.Email), n);

            SetResponse response = Client.Set("Users/" + UpdateInGoodString(user.Email), user);

            //var w = auth.CreateUserWithEmailAndPasswordAsync("vlad@mail.ru", "vladik", "hz che tut")



            // var e = auth.SignInWithEmailAndPasswordAsync("123@123.com", "123456").Result.FirebaseToken;

            //  var p = w.Equals(e);
            //SetResponse response = Client.Set("Users/" + UpdateInGoodString(user.Email), user);

        }

        public void UpdateUser(User user) //or User user
        {
            SetResponse response = Client.Set("Users/" + UpdateInGoodString(user.Email), user);
        }

        public void DeleteUser(string email)
        {
            Client.Delete("Users/" + UpdateInGoodString(email));
        }

        public User GetUser(string email)
        {
            FirebaseResponse response = Client.Get("Users/" + UpdateInGoodString(email));

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