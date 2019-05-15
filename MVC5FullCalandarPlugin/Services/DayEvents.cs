using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Firebase.Storage;
using MVC5FullCalandarPlugin.Models;
using MVC5FullCalandarPlugin.Services.Interfaces;
using MVC5FullCalandarPlugin.Services.Users;

namespace MVC5FullCalandarPlugin.Services
{
    public class DayEvents : IDayEvent
    {
        IUserDbSet storageUsers = new Storage();

        public DayEvents(IUserDbSet s)
        {
            storageUsers = s;
        }

        public DayEvents()
        {

        }

        public string ChangeTimeAndEvent(string title, string description, string time, string date, string token, string id, string status, HttpPostedFileBase image)
        {
            var email = TokenService.getEmailWithToken(token);
            var user = storageUsers.Get(email);
            var dat = user.Days.First(x => x.Date == date);
            var holy = dat.PublicHolidays.First(x => x.Id == id);

            holy.Time = time;
            holy.Description = description;
            holy.Title = title;
            holy.Status = status;

            if (image != null)
            {
                if (holy.Image != null)
                {
                    var task = Task.Run(async () => { await FireBaseStorage.UploadImage(holy, image, holy.Image.Id); });
                    task.Wait();
                }
                else
                {
                    var task = Task.Run(async () => { await FireBaseStorage.UploadImage(holy, image); });
                    task.Wait();
                }
            }

            storageUsers.Update(user);

            return id;
        }

        public string ChangeTimeAndEventWithEmail(string title, string description, string time, string date, string email, string id, string status, HttpPostedFileBase image)
        {
            string url;
            var user = storageUsers.Get(email);
            var dat = user.Days.First(x => x.Date == date);
            var holy = dat.PublicHolidays.First(x => x.Id == id);

            holy.Time = time;
            holy.Description = description;
            holy.Title = title;
            holy.Status = status;

            if (image != null)
            {
                if (holy.Image != null)
                {
                    var task = Task.Run(async () => { await FireBaseStorage.UploadImage(holy, image, holy.Image.Id); });
                    task.Wait();
                }
                else
                {
                    var task = Task.Run(async () => { await FireBaseStorage.UploadImage(holy, image); });
                    task.Wait();
                }
            }

            storageUsers.Update(user);


            return holy.Image.Url;
        }


        public string AddTimeAndEvent(string title, string description, string time, string date, string token, string status , HttpPostedFileBase image)
        {
            var email = TokenService.getEmailWithToken(token);
            var user = storageUsers.Get(email);

            var startDate = date + " " + "00:00";
            var id = DateTime.Now.GetHashCode().ToString();

            var eventHoliday = new PublicHoliday
            {
                Id = id,
                Description = description,
                Start_Date = startDate,
                End_Date = date,
                Time = time,
                Title = title,
                Status = status
            };

            if (image != null)
            {

                var task = Task.Run(async () => { await FireBaseStorage.UploadImage(eventHoliday, image); });
                task.Wait();
            }

            if (user.Days == null)
            {
                user.Days = new List<DayModel>();
            }

            if (user.Days.Any(x => x.Date == date))
            {
                user.Days.First(x => x.Date == date).PublicHolidays.Add(eventHoliday);
            }
            else
            {
                user.Days.Add(new DayModel()
                {
                    Date = date,
                    PublicHolidays = new List<PublicHoliday>()
                    {
                        eventHoliday
                    }
                });
            }

            storageUsers.Update(user);

            return id;
        }

        public PublicHoliday AddTimeAndEventWithEmail(string title, string description, string time, string date, string email, string status,
            HttpPostedFileBase image)
        {
            var user = storageUsers.Get(email);

            var startDate = date + " " + "00:00";
            var id = DateTime.Now.GetHashCode().ToString();

            var eventHoliday = new PublicHoliday
            {
                Id = id,
                Description = description,
                Start_Date = startDate,
                End_Date = date,
                Time = time,
                Title = title,
                Status = status
            };

            if (image != null)
            {

                var task = Task.Run(async () => { await FireBaseStorage.UploadImage(eventHoliday, image); });
                task.Wait();
            }


            if (user.Days.Any(x => x.Date == date))
            {
                user.Days.First(x => x.Date == date).PublicHolidays.Add(eventHoliday);
            }
            else
            {
                user.Days.Add(new DayModel()
                {
                    Date = date,
                    PublicHolidays = new List<PublicHoliday>()
                    {
                        eventHoliday
                    }
                });
            }

            storageUsers.Update(user);

            return eventHoliday;
        }

        public string GetAllTimeInDay(string date, string token)
        {
            var user = storageUsers.Get(TokenService.getEmailWithToken(token));
            try
            {
                string str = user.Days.FirstOrDefault(x => x.Date == date).AllTime;
                return str;
            }
            catch (NullReferenceException e)
            {
                return "0";
            }
          
            
        }

        public PublicHoliday GetEvent(string token, string date, string id)
        {
            var email = TokenService.getEmailWithToken(token);
            List<DayModel> data = storageUsers.Get(email).Days;
            var day = data.First(x => x.Date == date);
            var holy = day.PublicHolidays.First(x => x.Id == id);

            return holy;

        }

        public async Task<string> Delete(string id, string date, string token)
        {
            var email = TokenService.getEmailWithToken(token);
            var user = storageUsers.Get(email);
            var day = user.Days.First(x => x.Date == date);
            var holy = day.PublicHolidays.First(x => x.Id == id);

            if (holy.Image != null)
            {
                FireBaseStorage.DeleteImage(holy.Image.Id);
            }
            if (day.PublicHolidays.Count == 1)
            {
                user.Days.Remove(day);
            }
            else
            {
                day.PublicHolidays.Remove(holy);
            }
            
            

            storageUsers.Update(user);

            return id;
        }

        public async void DeleteWithEmail(string id, string date, string email)
        {
            var user = storageUsers.Get(email);
            user.Days.RemoveAll(x => x == null);
            var day = user.Days.First(x => x.Date == date);
            var holy = day.PublicHolidays.First(x => x.Id == id);
            if (holy.Image != null)
            {
               FireBaseStorage.DeleteImage(holy.Image.Id);
            }

            if (day.PublicHolidays.Count == 1)
            {
                user.Days.Remove(day);
            }
            else
            {
                day.PublicHolidays.Remove(holy);
            }

            storageUsers.Update(user);
            
        }
    }
}