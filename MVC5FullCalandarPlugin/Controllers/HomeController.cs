using MVC5FullCalandarPlugin.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Firebase.Auth;
using MVC5FullCalandarPlugin.FireBase;

namespace MVC5FullCalandarPlugin.Controllers
{
    public class HomeController : Controller
    {
        #region Index method

        /// <summary>
        /// GET: Home/Index method.
        /// </summary>
        /// <returns>Returns - index view page</returns> 
        public ActionResult Index()
        {
            // Info.
            return View();
        }

        #endregion

        #region Get Calendar data method.


        /// <summary>
        /// GET: /Home/GetCalendarData
        /// </summary>
        /// <returns>Return data</returns>
        [HttpGet]
        public ActionResult GetCalendarData(string token)
        {

            if (token != "null" && token != null)
            {
                var email = TokenService.getEmailWithToken(token);

                // Initialization.
                JsonResult result = new JsonResult();

                try
                {

                    // Loading.
                    List<PublicHoliday> data = Storage.getInstance().GetUser(email).PublicHolidays;

                    // Processing.
                    result = this.Json(data, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    // Info
                    Console.Write(ex);
                }

                // Return info.
                return result;
            }

            return null;
        }

        #endregion

        #region Helpers

        #region Load Data

        /// <summary>
        /// Load data method.
        /// </summary>
        /// <returns>Returns - Data</returns>
        private List<PublicHoliday> LoadData()
        {
            // Initialization.
            List<PublicHoliday> lst = new List<PublicHoliday>();

            try
            {
                // Initialization.
                string line = string.Empty;
                string srcFilePath = "Content/files/PublicHoliday.txt";
                var rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                var fullPath = Path.Combine(rootPath, srcFilePath);
                string filePath = new Uri(fullPath).LocalPath;
                StreamReader sr = new StreamReader(new FileStream(filePath, FileMode.Open, FileAccess.Read));

                // Read file.
                while ((line = sr.ReadLine()) != null)
                {
                    // Initialization.
                    PublicHoliday infoObj = new PublicHoliday();
                    string[] info = line.Split(',');

                    // Setting.
                  //  infoObj.Time = Convert.ToInt32(info[0].ToString());
                    infoObj.Title = info[1].ToString();
                    infoObj.Description = info[2].ToString();
                    infoObj.Start_Date = info[3].ToString();
                    infoObj.End_Date = info[4].ToString();


                    // Adding.
                    lst.Add(infoObj);
                }

                // Closing.
                sr.Dispose();
                sr.Close();
            }
            catch (Exception ex)
            {
                // info.
                Console.Write(ex);
            }

            // info.
            return lst;
        }

        #endregion

        #endregion
    }
}