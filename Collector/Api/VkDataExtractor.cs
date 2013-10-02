using Collector.Interface;
using Collector.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Api
{
	public class VkDataExtractor : IDataExtractor
	{
		public int GetListCount(JObject Data)
		{
			return (int)Data["response"]["count"];
		}

		public List<object> GetItems(JObject Data, Type T)
		{
			var listType = typeof(List<>).MakeGenericType(T);

			var data = Data["response"];

			if (data is JArray)
			{
				var res = data.ToObject(listType);
				var res2 = ((IEnumerable)res).Cast<object>().ToList();
				return res2;
			}
			else
			{
				var tmp = data["items"];
				var res = tmp.ToObject(listType);
				return ((IEnumerable)res).Cast<object>().ToList();
			}
		}

		public object GetItem(JObject Data, Type T)
		{
			var data = Data["response"];

			if (data is JArray)
				data = data.First;

			var result = data.ToObject(T);
			return result;
		}
	}
}
