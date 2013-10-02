using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Interface
{
	public enum ApiRequestType
	{
		ObjectInfo,
		ListForObject,
		ListObjectsInfo
	}

	public interface IApiRequest
	{
		int GetRequestBatchSize(string Method);

		ApiRequestType GetRequestType(string Method);

		int GetRequestItemsMaxCount(string Method);

		Task<object> ExecuteRequest(string Method, string Id);
		Task<object> ExecuteRequest(string Method, List<string> Ids);
		Task<object> ExecuteRequest(string Method, string Id, int Offset, int Count);
	}
}
