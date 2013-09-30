using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Model
{
	public class VkPost
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("to_id")]
		public long WallId { get; set; }

		[JsonProperty("from_id")]
		public long FromId { get; set; }

		[JsonProperty("date")]
		public DateTime Date { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }

	}
}
