using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Api
{
	public class FbApi : BaseApi
	{
		string appId = "523485671046004";
		string appSecret = "d8e71f7f5548f570854caef528a75433";
		string appAuthToken;

		List<string> methodNeedAuth = new List<string>() {
			"/feed",
		};

		public FbApi()
		{
			this.baseUri = "https://graph.facebook.com/";

			appAuthToken = appId + "|" + appSecret;
		}

		protected override StringBuilder formatUri(string Method, Dictionary<string, string> Params)
		{
			var sb = base.formatUri(Method, Params);

			foreach (var m in methodNeedAuth)
			{
				if (Method.EndsWith(m))
				{
					sb.Append("&access_token=").Append(appAuthToken);
					break;
				}
			}

			return sb;
		}

		public override async Task<JObject> ExecuteRequest(string Method, Dictionary<string, string> Params)
		{
			var result = await base.ExecuteRequest(Method, Params);

			return result;
		}

	}
}
