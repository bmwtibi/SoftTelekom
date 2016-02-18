using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Newtonsoft.Json;
using SoftTelekom.Core.Helpers;
using SoftTelekom.Core.Models;
using SoftTelekom.Core.Models.ServiceModel;

namespace SoftTelekom.Core.Services
{
    public static class DataService
    {
        public static void GetNews(string requestUrl, Action<List<NewsItem>> successAction, Action<Exception> errorAction)
        {
            var request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Method = "GET";
            request.Accept = "text/html";
            Task.Factory.StartNew(() =>
            {

            });
            request.BeginGetResponse(token =>
            {
                try
                {
                    using (var response = request.EndGetResponse(token))
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            var reader = new StreamReader(stream);
                            string strContent = reader.ReadToEnd();
                            List<NewsItem> tmpNewsList = JsonConvert.DeserializeObject<List<NewsItem>>(strContent);
                            successAction(tmpNewsList);
                        }
                    }
                }
                catch (WebException ex)
                {
                    Mvx.Error("Error: '{0}' when making {1} request to {2}", ex.Message, request.Method,
                        request.RequestUri);
                    errorAction(ex);
                }
            }, null);
        }

        //public static void PostOrder(string requestUrl, string postData,Action<bool> succesAction, Action<Exception> errorAction)
        //{
        //    var request = (HttpWebRequest)WebRequest.Create(requestUrl);
        //    request.Method = "POST";
        //    request.ContentType = "application/x-www-form-urlencoded";
        //    request.BeginGetRequestStream(token =>
        //    {

        //        try
        //        {
        //            using (var stream = request.EndGetRequestStream(token))
        //            {
        //                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        //                stream.Write(byteArray,0,postData.Length);
        //            }
        //        }
        //        catch (Exception)
        //        {
                    
        //            throw;
        //        }
        //    }, null);

        //}

        public static bool LogIn(string userName, string userPwd)
        {
            if (userName == "teszt@teszt.hu" && userPwd == "teszt")
            {
                Settings.SavedUser = userName;
                return true;
            }
            return false;
        }

        public static bool GetLocationServices(double lon, double lat)
        {
            return true;
        }

        public static void GetUserData(Action<UserData,bool> resultAction)
        {
            resultAction(new UserData()
            {
                Name = "Góré Tibor Bence",
                BirthDate = new DateTime(1991,7,19),
                Address = "4031 Debrecen Szitakötő köz 26.",
                PhoneNumber = "+36209238734",
                Email = "goretibor@yahoo.com"
            }, true);
        }

        public static void GetBillItem(Action<List<BillItem>, bool> resultAction)
        {
            resultAction(new List<BillItem>()
            {
                new BillItem()
                {
                    Date = new DateTime(2015, 7, 1),
                    IsPaid = false
                },
                new BillItem()
                {
                    Date = new DateTime(2015, 6, 1),
                    IsPaid = true
                },
                new BillItem()
                {
                    Date = new DateTime(2015, 5, 1),
                    IsPaid = true
                },
                new BillItem()
                {
                    Date = new DateTime(2015, 4, 1),
                    IsPaid = true
                },
            }, true);
        }

        public static void GetCurrentDataUsage(Action<string, bool> resultAction)
        {
            resultAction("3.5 GB", true);
        }

        public static void GetListDataUsage(Action<List<InternetUsageItem>, bool> resultAction)
        {
            resultAction(new List<InternetUsageItem>()
            {
                new InternetUsageItem()
                {
                    Date = new DateTime(2015, 6, 1),
                    DataUsage = "12.4 GB"
                },
                new InternetUsageItem()
                {
                    Date = new DateTime(2015, 5, 1),
                    DataUsage = "122.4 GB"
                },
                new InternetUsageItem()
                {
                    Date = new DateTime(2015, 4, 1),
                    DataUsage = "42.4 GB"
                },
            }, true);
        }

    }
}