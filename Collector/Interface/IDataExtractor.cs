using Collector.Api;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Interface
{
	public interface IDataExtractor
	{
		//int GetListCount(JObject Data);

		List<object> GetItems(JObject Data, Type T);

		object GetItem(JObject Data, Type T);
	}
}
