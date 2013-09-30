using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Interface
{
	public interface IApiRequest
	{
		Task<object> ExecuteRequest(string Method, string Id);
		Task<T> ExecuteRequest<T>(string Method, string Id) where T : class;

		Task<List<object>> ExecuteRequest(string Method, List<string> Ids);
		Task<List<T>> ExecuteRequest<T>(string Method, List<string> Ids) where T : class;

		Task<List<object>> ExecuteRequest(string Method, string Id, int Offset, int Count);
		Task<List<T>> ExecuteRequest<T>(string Method, string Id, int Offset, int Count) where T : class;

	}
}
