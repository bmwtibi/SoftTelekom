using System;
using SoftTelekom.Core.Services;

namespace SoftTelekom.Core
{
	public class DataServiceSingletone
	{
		
			 private static DataServiceSingletone _instance;

		public DataService Service { get; set; }
		private DataServiceSingletone()
		{
			Service = new DataService();
		}
		public static DataServiceSingletone Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new DataServiceSingletone();
				}

				return _instance;
			}
		}

	}
}
