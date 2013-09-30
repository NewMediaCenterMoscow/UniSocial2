using Collector.Interface;
using Collector.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Api
{
	public class VkApiRequest : BaseApiRequest
	{
		public VkApiRequest(IApi Api, IDataExtractor DataExtractor)
			: base(Api, DataExtractor)
		{
			typeForMethods.Add("groups.getById", typeof(VkGroup));
			typeForMethods.Add("users.getSubscriptions", typeof(VkUserSubscriptions));
			typeForMethods.Add("users.get", typeof(VkUser));

			requestParams.Add("groups.getById",
				new ApiRequestParam(new Dictionary<string, string>() {
					{ "fields", "members_count" } 
				})
			);

			requestParams.Add("users.get",
				new ApiRequestParam(new Dictionary<string, string>() {
					{ "fields", "sex,bdate,city,country" } 
				})
			);
		}

		protected override void setListParams(ApiRequestParam requestParam, int Offset, int Count)
		{
			if (Count != 0)
			{
				requestParam.Params["offset"] = Offset.ToString();
				requestParam.Params["count"] = Count.ToString();
			}
		}

		protected override void setIdParams(ApiRequestParam requestParam, string id)
		{
			switch (requestParam.Method)
			{
				case "groups.getById":
					requestParam.Params["group_id"] = id;
					break;
				case "users.getSubscriptions":
					requestParam.Params["user_id"] = id;
					break;
				case "users.get":
					requestParam.Params["user_ids"] = id;
					break;
				default:
					throw new NotSupportedException("Method `" + requestParam.Method + "` is not supported!");
			}
		}

		protected override void setIdParams(ApiRequestParam requestParam, List<string> ids)
		{
			switch (requestParam.Method)
			{
				case "groups.getById":
					requestParam.Params["group_ids"] = String.Join(",", ids);
					break;
				case "users.get":
					requestParam.Params["user_ids"] = String.Join(",", ids);
					break;
				default:
					throw new NotSupportedException("Method `" + requestParam.Method + "` is not supported!");
			}
		}
	}
}
