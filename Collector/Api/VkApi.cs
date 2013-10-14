using Collector.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
		string authToken = "031a1692f3b80a180429bca8d58533f6b35716f44bd4df6d86ca53855f7f40f3e755a576a6a722f80a89b";

		List<string> methodNeedAuth = new List<string>() {
			"getReposts"
		};

		public VkApi()
		{
			this.baseUri = "https://api.vk.com/method/";

			//authUrl = authUrl
			//	.Replace("APP_ID", appId)
			//	.Replace("PERMISSIONS", "offline")
			//	.Replace("REDIRECT_URI", "https://oauth.vk.com/blank.html")
			//	.Replace("DISPLAY", "page")
			//	.Replace("API_VERSION", apiVersion)
			//	;
		}

		protected override StringBuilder formatUri(string Method, Dictionary<string, string> Params)
		{
			var sb = base.formatUri(Method, Params);

			sb.Append("&v=").Append(apiVersion);

			foreach (var m in methodNeedAuth)
			{
				if (Method.EndsWith(m))
				{
					sb.Append("&access_token=").Append(authToken);
					break;
				}
			}

			return sb;
		}

		public override async Task<JObject> ExecuteRequest(string Method, Dictionary<string, string> Params)
		{
			JObject result = null;

			try
			{
				result = await base.ExecuteRequest(Method, Params);

			}
			catch (TaskCanceledException)
			{
				Trace.TraceEvent(TraceEventType.Warning, this.GetHashCode(), "Task cancelation");
				result = null;
				Thread.Sleep(60000);
			}

			if (result["error"] != null)
			{
				var errorCode = (int)result["error"]["error_code"];
				if (errorCode == 6)
				{
					Trace.TraceEvent(TraceEventType.Warning, this.GetHashCode(), "Too many request");
					result = null;
					Thread.Sleep(2000);
				}
				else
					throw new ApiException((string)result["error"]["error_msg"], errorCode);
			}

			if (result == null)
			{
				return await ExecuteRequest(Method, Params);
			}

			return result;
		}
	}
}
