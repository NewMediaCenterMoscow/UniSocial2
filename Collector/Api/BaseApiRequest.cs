using Collector.Interface;
using Newtonsoft.Json.Linq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Api
{
	public class ApiRequestParam
	{
		public ApiRequestParam(string Method, Dictionary<string, string> Params)
		{
			this.Method = Method;
			this.Params = Params;
		}

		public ApiRequestParam(Dictionary<string, string> Params)
		{
			this.Method = "";
			this.Params = Params;
		}
		public ApiRequestParam(string Method)
		{
			this.Method = Method;
			this.Params = new Dictionary<string, string>();
		}

		public ApiRequestParam()
		{
			this.Method = "";
			this.Params = new Dictionary<string, string>();
		}

		public string Method;
		public Dictionary<string,string> Params;
	}

	public abstract class BaseApiRequest : IApiRequest
	{
		protected IApi api;
		protected IDataExtractor dataExtractor;

		protected Dictionary<string, ApiRequestParam> requestParams;
		protected Dictionary<string, Type> typeForMethods;

		[Inject]
		public TraceSource Trace { get; set; }

		public BaseApiRequest(IApi Api, IDataExtractor DataExtractor)
		{
			api = Api;
			dataExtractor = DataExtractor;

			requestParams = new Dictionary<string, ApiRequestParam>();
			typeForMethods = new Dictionary<string, Type>();
		}


		protected virtual Type getTypeForMethod(string Method)
		{
			if (!typeForMethods.ContainsKey(Method))
				throw new NotSupportedException("Method " + Method + " not supported!");

			return typeForMethods[Method];
		}

		protected virtual ApiRequestParam getParams(string method)
		{
			if (!requestParams.ContainsKey(method))
				return new ApiRequestParam(method);

			var result = requestParams[method];
			if (String.IsNullOrEmpty(result.Method))
				result.Method = method;

			return result;
		}

		protected abstract void setListParams(ApiRequestParam requestParam, int Offset, int Count);
		protected abstract void setIdParams(ApiRequestParam requestParam, string id);
		protected abstract void setIdParams(ApiRequestParam requestParam, List<string> ids);


		public async Task<object> ExecuteRequest(string Method, string Id)
		{
			var needObjectType = getTypeForMethod(Method);
			var param = getParams(Method);
			setIdParams(param, Id);

			var rawData = await api.ExecuteRequest(param.Method, param.Params);
			var result = dataExtractor.GetItem(rawData, needObjectType);

			return result;
		}
		public async Task<List<object>> ExecuteRequest(string Method, List<string> Ids)
		{
			var needObjectType = getTypeForMethod(Method);
			var param = getParams(Method);
			setIdParams(param, Ids);

			var rawData = await api.ExecuteRequest(param.Method, param.Params);
			var result = dataExtractor.GetItems(rawData, needObjectType);

			return result;
		}
		public async Task<List<object>> ExecuteRequest(string Method, string Id, int Offset, int Count)
		{
			var needObjectType = getTypeForMethod(Method);
			var param = getParams(Method);
			setIdParams(param, Id);
			setListParams(param, Offset, Count);

			var rawData = await api.ExecuteRequest(param.Method, param.Params);
			var result = dataExtractor.GetItems(rawData, needObjectType);

			return result;
		}

		public async Task<T> ExecuteRequest<T>(string Method, string Id) where T : class
		{
			return await this.ExecuteRequest(Method, Id) as T;
		}
		public async Task<List<T>> ExecuteRequest<T>(string Method, List<string> Ids) where T : class
		{
			var result = await this.ExecuteRequest(Method, Ids);
			return result.Cast<T>().ToList();
		}
		public async Task<List<T>> ExecuteRequest<T>(string Method, string Id, int Offset, int Count) where T : class
		{
			var result = await this.ExecuteRequest(Method, Id, Offset, Count);
			return result.Cast<T>().ToList();
		}
	}
}
