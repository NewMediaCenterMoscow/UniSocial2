using Collector.Interface;
using Collector.Model;
using System;
using System.Collections;
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
			objectTypeForMethods.Add("groups.getMembers", typeof(long));
			objectTypeForMethods.Add("groups.get", typeof(long));
			objectTypeForMethods.Add("users.getSubscriptions", typeof(VkUserSubscriptions));
			objectTypeForMethods.Add("users.get", typeof(VkUser));
			objectTypeForMethods.Add("wall.get", typeof(VkPost));
			objectTypeForMethods.Add("wall.getReposts", typeof(VkPost));
			objectTypeForMethods.Add("friends.get", typeof(long));

			requestTypes.Add("groups.getById", ApiRequestType.ListObjectsInfo);
			requestTypes.Add("groups.getMembers", ApiRequestType.ListForObject);
			requestTypes.Add("groups.get", ApiRequestType.ListForObject);
			requestTypes.Add("users.getSubscriptions", ApiRequestType.ObjectInfo);
			requestTypes.Add("users.get", ApiRequestType.ListObjectsInfo);
			requestTypes.Add("wall.get", ApiRequestType.ListForObject);
			requestTypes.Add("wall.getReposts", ApiRequestType.ListForObject);
			requestTypes.Add("friends.get", ApiRequestType.ListForObject);

			requestParams.Add("groups.getById", new Dictionary<string, string>() {
				{ "fields", "members_count" } 
			});
			//requestParams.Add("groups.get",
			//	new ApiRequestParam(new Dictionary<string, string>() {
			//		{ "filter", "moder" } 
			//	})
			//);
			requestParams.Add("users.get", new Dictionary<string, string>() {
				{ "fields", "education,contacts,nickname, screen_name, sex, bdate, city, country, timezone, photo_50, photo_100, photo_200, photo_max, has_mobile, online" } 
			});
			requestParams.Add("wall.get", new Dictionary<string, string>() {
				{ "filter", "all" } 
			});

			itemsMaxCounts.Add("groups.getMembers", 1000);
			itemsMaxCounts.Add("groups.get", 1000);
			itemsMaxCounts.Add("wall.get", 100);
			itemsMaxCounts.Add("wall.getReposts", 1000);
			itemsMaxCounts.Add("users.getSubscriptions", 200);
			itemsMaxCounts.Add("friends.get", Int32.MaxValue);

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
				case "groups.get":
					requestParam.Params["user_id"] = id;
					break;
				case "groups.getMembers":
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
				case "wall.getReposts":
					var ids = id.Split('_'); // 0 - owner_id, 1 - post_id
					requestParam.Params["owner_id"] = ids[0];
					requestParam.Params["post_id"] = ids[1];
					break;
				case "friends.get":
					requestParam.Params["user_id"] = id;
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

		protected override object modifyResult(object Data, ApiRequestParam requestParam)
		{
			if (requestParam.Method == "users.getSubscriptions")
			{
				var data = Data as VkUserSubscriptions;

				data.Id = Int32.Parse(requestParam.Params["user_id"]);

				return data;
			}

			if (requestParam.Method == "groups.getMembers")
			{
				var res = new VkGroupMembers();

				res.GroupId = Int32.Parse(requestParam.Params["group_id"]);
				res.GroupMembers = ((IEnumerable)Data).Cast<long>().ToList();

				return res;
			}
			if (requestParam.Method == "friends.get")
			{
				var res = new VkFriends();

				res.UserId = Int32.Parse(requestParam.Params["user_id"]);
				res.Friends = ((IEnumerable)Data).Cast<long>().ToList();

				return res;
			}
			if (requestParam.Method == "groups.get")
			{
				var res = new VkUserGroups();

				res.UserId = Int32.Parse(requestParam.Params["user_id"]);
				res.Groups = ((IEnumerable)Data).Cast<long>().ToList();

				return res;
			}
			
			return Data;
		}
	}
}
