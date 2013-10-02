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
		//string authUrl = "https://oauth.vk.com/authorize?client_id=3813980&scope=offline&redirect_uri=https://oauth.vk.com/blank.html&display=page&v=5&response_type=token";
		//string appId = "3813980";
		//string clientSecret = "wWFYljF9nBfMl6f7rdUm";

		string apiVersion = "5";
		string authToken = "8fbcaaf861ba2d5fadaafa4c73fdcc8a56f891b1d08fb72253cba7a90ccbeede8e9fc024f52c5ff49e8a8";

		List<string> methodNeedAuth = new List<string>() {
		};

		public VkApi()
		{
			this.baseUri = "https://api.vk.com/method/";
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
