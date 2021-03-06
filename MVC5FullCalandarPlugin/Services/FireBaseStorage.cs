﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Firebase.Storage;
using MVC5FullCalandarPlugin.Models;

namespace MVC5FullCalandarPlugin.Services
{
    public class FireBaseStorage
    {
        private const string storageName = "testcalendar-27287.appspot.com";
        private const string partPathSaveFiles = @"D:/Files/";

        public static async Task<string> UploadImage(PublicHoliday holy, HttpPostedFileBase imagePath, string id = null)
        {
            
            if (id != null)
            {
               DeleteImage(id);
            }

            id = DateTime.Now.GetHashCode() + "";

            var path = partPathSaveFiles + imagePath.FileName;

            imagePath.SaveAs(path);

            var stream = System.IO.File.Open(path, FileMode.Open);

            var task = new FirebaseStorage(storageName)
                .Child(id)
                .PutAsync(stream);


            var f = await task;

            holy.Image = new ImageHTML
            {
                Id = id,
                Url = f
            };

            stream.Close();
            File.Delete(path);

            return f;
        }

        public static async void DeleteImage(string id)
        {
            var taskDel = new FirebaseStorage(storageName)
                .Child(id)
                .DeleteAsync();

            taskDel.Wait();
        }
    }
}