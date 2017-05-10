using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Newtonsoft.Json;
using SoftTelekom.Core.Helpers;
using SoftTelekom.Core.Models;
using SoftTelekom.Core.Models.ServiceModel;


namespace SoftTelekom.Core.Services
{
    public class DataService
    {
		private const string URL = "http://172.20.10.2/REST";
		//private const string URL = "http://softtelekom.hu/backend"; //éles szerver
		//private const string URL = "http://softtelekom.hu/backend/test"; //test szerver

		public async void GetNews(Action<List<NewsItem>> successAction, Action<String> errorAction)
        {
			var jsonString = await GetResponseString(new BaseRequestModel(), URL + ServiceConstants.GETNEWS);
			var responseModel = JsonConvert.DeserializeObject<BaseResponseModel<List<NewsModel>>>(jsonString);
			var newsItem = new List<NewsItem>();
			if (responseModel != null && !responseModel.IsError)
			{
				foreach (var item in responseModel.Response)
				{
					newsItem.Add(new NewsItem()
					{
						news_title = item.Title,
						news_descr = item.Message,
						news_time = item.DateTime
					});
				}
				successAction(newsItem);
			}
			else 
			{
				errorAction("Connection Error");
			}


        }

		public async void PostOrder(OrderModel orderModel,Action<bool,string,bool> responseAction)
		{
			var jsonString = await GetResponseString(orderModel, URL + ServiceConstants.POSTORDER);
			var responseModel = JsonConvert.DeserializeObject<BaseResponseModel<OrderResponseModel>>(jsonString);
			if (responseModel != null)
			{
				if (!responseModel.IsError)
				{
					responseAction(responseModel.Response.IsSucces, string.Empty, false);
				}
				else 
				{
					responseAction(false, responseModel.ErrorMessage, true);
				}

			}
			else
			{
				responseAction(false, "Connection Error", true);
			}


		}

		public async void Login(LoginModel loginModel, Action<string, string, bool> responseAction)
		{
			var jsonString = await GetResponseString(loginModel, URL + ServiceConstants.LOGIN);
			var responseModel = JsonConvert.DeserializeObject<BaseResponseModel<LoginResponseModel>>(jsonString);
			if (responseModel != null)
			{
				if (!responseModel.IsError)
				{
					Settings.SavedUserEmail = loginModel.Email;
					Settings.SavedUserToken = responseModel.Response.Token;
					responseAction(responseModel.Response.Token, string.Empty, false);
				}
				else 
				{
					responseAction(string.Empty, responseModel.ErrorMessage, true);
				}

			}
			else
			{
				
				responseAction(string.Empty, "Connection Error", true);
			}


		}

		public async void GetMyInfo(Action<UserData, string, bool> responseAction)
		{
			var myInfoModel = new GetInfoModel() { Token = Settings.SavedUserToken };
			var jsonString = await GetResponseString(myInfoModel, URL + ServiceConstants.GETMYINFO);
			var responseModel = JsonConvert.DeserializeObject<BaseResponseModel<GetInfoResponseModel>>(jsonString);
			if (responseModel != null)
			{
				if (!responseModel.IsError)
				{
					UserData responseData;
					if (responseModel.Response != null)
					{
						responseData = new UserData
						{
							Name = responseModel.Response.Name,
							Address = responseModel.Response.Address,
							PhoneNumber = responseModel.Response.Phone,
							Email = responseModel.Response.Email
						};
						responseAction(responseData, string.Empty, false);
					}


				}
				else
				{
					responseAction(null, responseModel.ErrorMessage, true);
				}

			}
			else
			{
				responseAction(null, "Connection Error", true);
			}


		}

		public async void SaveMyInfo(UserData myInfo,Action<bool, string, bool> responseAction)
		{
			var myInfoModel = new SaveInfoModel() 
			{ 
				Token = Settings.SavedUserToken,
				Name = myInfo.Name,
				Email = myInfo.Email,
				Phone = myInfo.PhoneNumber,
				Address = myInfo.Address
			};
			var jsonString = await GetResponseString(myInfoModel, URL + ServiceConstants.SAVEMYINFO);
			var responseModel = JsonConvert.DeserializeObject<BaseResponseModel<SaveInfoResponseModel>>(jsonString);
			if (responseModel != null)
			{
				if (!responseModel.IsError)
				{
					if (responseModel.Response != null)
					{
						responseAction(responseModel.Response.IsSucces, string.Empty, false);
					}


				}
				else
				{
					responseAction(false, responseModel.ErrorMessage, true);
				}

			}
			else
			{
				responseAction(false, "Connection Error", true);
			}


		}

		public async void GetDataUsage(Action<DataUsageResponseModel, string, bool> responseAction)
		{
			var myDataUsage = new DataUsageModel() { Token = Settings.SavedUserToken };
			var jsonString = await GetResponseString(myDataUsage, URL + ServiceConstants.GETDATAINFO);
			var responseModel = JsonConvert.DeserializeObject<BaseResponseModel<DataUsageResponseModel>>(jsonString);
			if (responseModel != null)
			{
				if (!responseModel.IsError)
				{
					
					if (responseModel.Response != null)
					{
						responseAction(responseModel.Response, string.Empty, false);
					}


				}
				else
				{
					responseAction(null, responseModel.ErrorMessage, true);
				}

			}
			else
			{
				responseAction(null, "Connection Error", true);
			}


		}

        //public static bool LogIn(string userName, string userPwd)
        //{
        //    if (userName == "teszt@teszt.hu" && userPwd == "teszt")
        //    {
        //        Settings.SavedUser = userName;
        //        return true;
        //    }
        //    return false;
        //}

        public static bool GetLocationServices(double lon, double lat)
        {
            return true;
        }

        public static void GetBillItem(Action<List<BillItem>, bool> resultAction)
        {
            resultAction(new List<BillItem>()
            {
                //new BillItem()
                //{
                //    Date = new DateTime(2015, 7, 1),
                //    IsPaid = false
                //},
                //new BillItem()
                //{
                //    Date = new DateTime(2015, 6, 1),
                //    IsPaid = true
                //},
                //new BillItem()
                //{
                //    Date = new DateTime(2015, 5, 1),
                //    IsPaid = true
                //},
                //new BillItem()
                //{
                //    Date = new DateTime(2015, 4, 1),
                //    IsPaid = true
                //},
            }, true);
        }

        //public static void GetCurrentDataUsage(Action<string, bool> resultAction)
        //{
        //    resultAction("0.0 GB", true);
        //}

        //public static void GetListDataUsage(Action<List<InternetUsageItem>, bool> resultAction)
        //{
        //    resultAction(new List<InternetUsageItem>()
        //    {
        //        //new InternetUsageItem()
        //        //{
        //        //    Date = new DateTime(2015, 6, 1),
        //        //    DataUsage = "12.4 GB"
        //        //},
        //        //new InternetUsageItem()
        //        //{
        //        //    Date = new DateTime(2015, 5, 1),
        //        //    DataUsage = "122.4 GB"
        //        //},
        //        //new InternetUsageItem()
        //        //{
        //        //    Date = new DateTime(2015, 4, 1),
        //        //    DataUsage = "42.4 GB"
        //        //},
        //    }, true);
        //}

		//private static async Task<string> GetResponseString(object objForJson)
		//{
		//	string saveUrl = "http://localhost/REST/status";
		//	if (string.IsNullOrEmpty(saveUrl))
		//	{
		//		return string.Empty;
		//	}

		//	string requestUriString = "http://" + saveUrl + "/rstws.php";
		//	var request = (HttpWebRequest)WebRequest.Create("http://localhost/REST/status");
		//	var httpClient = new HttpClient();
		//	var httpCont = httpClient.;


		//	request.ContentType = "application/json";
		//	request.Method = "POST";

		//	var json = JsonConvert.SerializeObject(objForJson);
		//	byte[] byteArr = new UTF8Encoding().GetBytes(json);
		//	HttpRequestMessage myrequest = new HttpRequestMessage(HttpMethod.Post, "http://localhost/REST/status");
		//	Stream srddf = null;
		//	using (var strme = new StreamWriter(srddf))
		//	{
		//		strme.Write
		//	}

		//	string responseString;

		//	try
		//	{

		//		using (var stream = await httpClient.SendAsync(myrequest))
		//		{
		//			await stream.WriteAsync(byteArr, 0, byteArr.Length);
		//		}
		//		var response = (HttpWebResponse)await request.GetResponseAsync();
		//		var reader = new StreamReader(response.GetResponseStream());
		//		responseString = await reader.ReadToEndAsync();
		//	}
		//	catch (Exception)
		//	{
		//		responseString = string.Empty;
		//	}
		//	return responseString;

		//}
		public static async void GetStatus()
		{
			//var stg = await GetResponseString(null);
			//var responseModel = JsonConvert.DeserializeObject<BaseResponseModel<StatusModel>>(stg);


		}


		private static async Task<string> GetResponseString(object objForJson,String Url)
		{
			var request = (HttpWebRequest)WebRequest.Create(Url);

			request.ContentType = "application/json";
			request.Method = "POST";

			var json = JsonConvert.SerializeObject(objForJson);
			byte[] byteArr = new UTF8Encoding().GetBytes(json);

			string responseString;

			try
			{
				using (var stream = await request.GetRequestStreamAsync())
				{
					await stream.WriteAsync(byteArr, 0, byteArr.Length);
				}
				var response = (HttpWebResponse)await request.GetResponseAsync();
				var reader = new StreamReader(response.GetResponseStream());
				responseString = await reader.ReadToEndAsync();
			}
			catch (Exception)
			{
				responseString = string.Empty;
			}
			return responseString;








			//var httpClient = new HttpClient();

			//string responseString;

			//try
			//{
			//	var json = JsonConvert.SerializeObject(objForJson);
			//	var content = new StringContent(json, Encoding.UTF8, "application/json");
			//	var result = await httpClient.PostAsync(Url, content);

			//	using (var stream = await result.Content.ReadAsStreamAsync())
			//	{
			//		var reader = new StreamReader(stream);
			//		responseString = await reader.ReadToEndAsync();
			//	}
			//}
			//catch (Exception ex)
			//{
			//	responseString = string.Empty;
			//	Debug.WriteLine(ex.Message);
			//}

			//return responseString;
		}

    }
}