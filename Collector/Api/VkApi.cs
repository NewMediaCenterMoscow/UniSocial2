using Collector.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collector.Api
{
	public class VkApi : BaseApi
	{
		//string authUrl = "https://oauth.vk.com/authorize?client_id=3813980&scope=offline,wall&redirect_uri=https://oauth.vk.com/blank.html&display=page&v=5&response_type=token";
//		string appId = "3813980";
//		string clientSecret = "wWFYljF9nBfMl6f7rdUm";

//		string authUrl = @"https://oauth.vk.com/authorize? 
// client_id=APP_ID& 
// scope=PERMISSIONS& 
// redirect_uri=REDIRECT_URI& 
// display=DISPLAY& 
// v=API_VERSION& 
// response_type=token";

		string apiVersion = "5";
		string authToken;

		List<string> methodNeedAuth = new List<string>() {
			"getReposts",
			"groups.get"
		};

		public VkApi()
		{
			this.baseUri = "https://api.vk.com/method/";

			var appSettings = ConfigurationManager.AppSettings;

			authToken = appSettings["vkAuthToken"];

			//authUrl = authUrl
			//	.Replace("APP_ID", appId)
			//	.Replace("PERMISSIONS", "offline")
			//	.Replace("REDIRECT_URI", "https://oauth.vk.com/blank.html")
			//	.Replace("DISPLAY", "page")
			//	.Replace("API_VERSION", apiVersion)
			//	;
		}

		bool isMethodNeedAuth(string method)
		{
			foreach (var m in methodNeedAuth)
			{
				if (method.EndsWith(m))
				{
					return true;
				}
			}

 			return false;
		}

		protected override StringBuilder formatUri(string Method, Dictionary<string, string> Params)
		{
			var sb = base.formatUri(Method, Params);

			sb.Append("&v=").Append(apiVersion);

			if (isMethodNeedAuth(Method))
				 sb.Append("&access_token=").Append(authToken);

			return sb;
		}

		public override async Task<JObject> ExecuteRequest(string Method, Dictionary<string, string> Params)
		{
			JObject result =  await base.ExecuteRequest(Method, Params);

            if (result != null && result["error"] != null)
            {
                var errorCode = (int)result["error"]["error_code"];
                var errorMessage = (string)result["error"]["error_msg"];

                throw new ApiException(errorMessage, errorCode);
            }
            else if(result == null)
            {
                throw new ApiException("Cannot get data from server",0);
            }

			if (isMethodNeedAuth(Method))
				Thread.Sleep(300);

			return result;
		}
	}
}
