using System;
using MVC5FullCalandarPlugin.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Collections.Generic;
using MVC5FullCalandarPlugin.Services.Interfaces;
using Moq;
using MVC5FullCalandarPlugin.Services;
using MVC5FullCalandarPlugin.Controllers;
using Assert = NUnit.Framework.Assert;
using MVC5FullCalandarPlugin.Services.Users;

namespace Tests.MVC5FullCalandarPlugin
{
    [TestFixture]
    public class Test
    {
        private string token =
            "eyJhbGciOiJSUzI1NiIsImtpZCI6IjY1NmMzZGQyMWQwZmVmODgyZTA5ZTBkODY5MWNhNWM3ZjJiMGQ2MjEiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20vdGVzdGNhbGVuZGFyLTI3Mjg3IiwiYXVkIjoidGVzdGNhbGVuZGFyLTI3Mjg3IiwiYXV0aF90aW1lIjoxNTU2MDE0OTU1LCJ1c2VyX2lkIjoiYkkyR0RuRmJ6UGdGeEZISXpyVERZV3RhaWp1MSIsInN1YiI6ImJJMkdEbkZielBnRnhGSEl6clREWVd0YWlqdTEiLCJpYXQiOjE1NTYwMTQ5NTUsImV4cCI6MTU1NjAxODU1NSwiZW1haWwiOiJrb2tvQGtvLmtvIiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJmaXJlYmFzZSI6eyJpZGVudGl0aWVzIjp7ImVtYWlsIjpbImtva29Aa28ua28iXX0sInNpZ25faW5fcHJvdmlkZXIiOiJwYXNzd29yZCJ9fQ.XmdfbdBzorwVCd060lq99_TjqBQkPkV8jHWVER4zKCOtKwTJ5SrFN7kPPOKpV_sWDg1FMp2rE1q9nYT7wDcrBtf3IpsZfN3P1f8bxS625wVoYDmyf6Rbi8Mn7U_rzEFQgBCQm5zrLf8i-G3j76vwDHWqqufXmX8QSVScQGCKdRRo6ak2X2muSJSo4_PQ2SFt44IkcyOeK2P8eCU88KTs-Y1TmQK1piRZqtYfRC03CN7WuhfCzg7V6tBkZBF9kkFOuOrtSf086L1bFM9II_7VWSrgP1meUDvK4XrHL82hrOWcoJQqbIATwrXcFT84T9x-Y9vk98qm6W2UlqX-IwIYOA";

        private PublicHoliday pH;
        private List<DayModel> days;
        private User user;
        private string date;
        private string id;
        private string email;
        private string title;
        private string time;
        private string description;

        [SetUp]
        public void Inicialize()
        {
            date = "22-10-2016";
            id = "42";
            email = "koko@ko.ko";
            time = "4";
            title = "Title";
            description = "Description";

            pH = new PublicHoliday
            {
                Description = description,
                End_Date = date,
                Id = id,
                Image = null,
                Start_Date = date,
                Time = time,
                Title = title
            };

            days = new List<DayModel>()
            {
                new DayModel
                {
                    Date = date,
                    PublicHolidays = new List<PublicHoliday>
                    {
                        pH
                    }
                }
            };

            user = new User
            {
                Days = days,
                Email = email,
                FirstName = "Luntik"
            };

            TestDbSetUser.Users = new List<User>() {user};
        }

        [Test]
        public void TestHomeControllerGetEvent()
        {
            IUserDbSet userDbSet = new TestDbSetUser();
    
            var mock1 = new Mock<IUserDbSet>();

            mock1.Setup(x => x.Get(email)).Returns(user);

            var d = new DayEvents(mock1.Object);

            HomeController controller = new HomeController(userDbSet) { dayEvents = d };

            var result = controller.GetEvent(token, date, id);
            
            Assert.IsNotNull(result);
        }

        //var mock = new Mock<IDayEvent>();
        //mock.Setup(x => x.GetEvent(token, date, id)).Returns(pH);

        [Test]
        public void TestHomeControllerChangeTimeAndEvent()
        {
            var mock1 = new Mock<IUserDbSet>();

            mock1.Setup(x => x.Get(email)).Returns(user);

            var d = new DayEvents(mock1.Object);

            HomeController controller = new HomeController(null) { dayEvents = d };
            
            // Act
            var result = controller.TestChangeTimeAndEvent(title, description, time, date, token, id);

            // Assert
            Assert.AreEqual(result, "42");
        }

        [Test]
        public void Testzzz()
        {
            UserDbSet w = new UserDbSet();

            var l = w.GetAll();
        }

        [Test]
        public void TestHomeControllerAddTimeAndEvent()
        {
            var mock1 = new Mock<IUserDbSet>();

            mock1.Setup(x => x.Get(email)).Returns(user);

            var d = new DayEvents(mock1.Object);

            var mock = new Mock<IDayEvent>();

            mock.Setup(x => x.GetEvent(token, date, id)).Returns(pH);
            HomeController controller = new HomeController(null) { dayEvents = d };


            // Act
            var result = controller.TestAddTimeAndEvent(title, description, time, date, token);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestHomeControllerGetCalendarData()
        {
            var mock = new Mock<IUserDbSet>();

            mock.Setup(x => x.Get(email)).Returns(user);

            HomeController controller = new HomeController(mock.Object);

            var result = controller.GetCalendarData(token);

            Assert.IsNotNull(result);
        }

        [Test]
        public void TestHomeControllerGetAllTimeInDay()
        {
            var mock1 = new Mock<IUserDbSet>();

            mock1.Setup(x => x.Get(email)).Returns(user);

            var d = new DayEvents(mock1.Object);

            var mock = new Mock<IDayEvent>();

            mock.Setup(x => x.GetEvent(token, email, id)).Returns(pH);
            HomeController controller = new HomeController(null) { dayEvents = d };
            // Act

            var result = controller.GetAllTimeInDay(date, token);

            // Assert
            Assert.AreEqual(result, "4");
        }

        [Test]
        public void TestHomeControllerDelete()
        {
            var mock1 = new Mock<IUserDbSet>();

            mock1.Setup(x => x.Get(email)).Returns(user);

            var d = new DayEvents(mock1.Object);

            var mock = new Mock<IDayEvent>();

            mock.Setup(x => x.GetEvent(token, date, id)).Returns(pH);
            HomeController controller = new HomeController(null) { dayEvents = d };
            // Act

            var result = controller.Delete(id, date, token);

            // Assert
            Assert.AreEqual(result, "42");
        }

        [Test]
        public void TestDiagramControllerGetN()
        {
            var mock1 = new Mock<IUserDbSet>();

            mock1.Setup(x => x.Get(email)).Returns(user);

            var d = new DayEvents(mock1.Object);

            var mock = new Mock<IDayEvent>();

            mock.Setup(x => x.GetEvent(token, date, id)).Returns(pH);
            DiagramsController controller = new DiagramsController(mock1.Object);
            // Act

            var result = controller.GetN(token);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void TestTokenService()
        {
            var email = TokenService.getEmailWithToken(token);
            Assert.AreEqual(email, "koko@ko.ko");
        }
    }
}
