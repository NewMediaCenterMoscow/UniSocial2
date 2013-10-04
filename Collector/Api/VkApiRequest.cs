﻿using Collector.Interface;
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
			objectTypeForMethods.Add("groups.getById", typeof(VkGroup));
			objectTypeForMethods.Add("users.getSubscriptions", typeof(VkUserSubscriptions));
			objectTypeForMethods.Add("users.get", typeof(VkUser));
			objectTypeForMethods.Add("wall.get", typeof(VkPost));

			requestParams.Add("groups.getById",
				new ApiRequestParam(new Dictionary<string, string>() {
					{ "fields", "members_count" } 
				})
			);
			requestParams.Add("users.get",
				new ApiRequestParam(new Dictionary<string, string>() {
					{ "fields", "education,contacts,nickname, screen_name, sex, bdate, city, country, timezone, photo_50, photo_100, photo_200, photo_max, has_mobile, online" } 
				})
			);
			requestParams.Add("wall.get",
				new ApiRequestParam(new Dictionary<string, string>() {
					{ "filter", "all" } 
				})
			);

			requestTypes.Add("groups.getById", ApiRequestType.ListObjectsInfo);
			requestTypes.Add("users.getSubscriptions", ApiRequestType.ListForObject);
			requestTypes.Add("users.get", ApiRequestType.ListObjectsInfo);
			requestTypes.Add("wall.get", ApiRequestType.ListForObject);

			itemsMaxCounts.Add("wall.get", 100);
			itemsMaxCounts.Add("users.getSubscriptions", 200);

			batchSizes.Add("users.get", 300);
			batchSizes.Add("groups.getById", 300);
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
				case "wall.get":
					long lId;
					if (long.TryParse(id, out lId))
						requestParam.Params["owner_id"] = lId.ToString();
					else
						requestParam.Params["domain"] = id;
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
