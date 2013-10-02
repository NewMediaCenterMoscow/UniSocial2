using Collector.Common;
using Collector.Interface;
using Newtonsoft.Json.Linq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Api
{
	public abstract class BaseApi : IApi
	{
		[Inject]
		public TraceSource Trace { get; set; }

		protected HttpClient client = new HttpClient();

		protected string baseUri;

		protected static int requestNumber = 0;

		public BaseApi()
		{
			var timeout = client.Timeout;
		}

		protected virtual StringBuilder formatUri(string Method, Dictionary<string, string> Params)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(baseUri).Append(Method);

			if (Params != null)
			{
				sb.Append("?");
				foreach (var param in Params)
				{
					sb.Append(param.Key).Append("=").Append(param.Value).Append("&");
				}
			}

			sb.Remove(sb.Length - 1, 1);

			return sb;
		}

		public virtual async Task<JObject> ExecuteRequest(string Method, Dictionary<string, string> Params)
		{
			var requestUri = formatUri(Method, Params);

			var data = await client.GetStringAsync(requestUri.ToString());
			var result = JObject.Parse(data);

			//Trace.TraceInformation("Request success: " + Method);
			Trace.TraceEvent(TraceEventType.Information, requestNumber++, "Request success: " + Method);

			return result;
		}


	}
}
