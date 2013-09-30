using Collector.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Model
{
	public enum VkUserSex
	{
		Undefined,
		Female,
		Male
	}
	public class VkUser
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("first_name")]
		public string FirstName { get; set; }

		[JsonProperty("last_name")]
		public string LastName { get; set; }

		[JsonProperty("sex")]
		public VkUserSex Sex { get; set; }

		[JsonProperty("bdate")]
		public string BDate { get; set; }

		[JsonProperty("country")]
		public long Country { get; set; }

		[JsonProperty("city")]
		public long City { get; set; }

		[JsonProperty("deactivated")]
		[JsonConverter(typeof(VkUserDeactivatedConverted))]
		public bool Deactivated { get; set; }
	}
}
