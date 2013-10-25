﻿using Collector.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Model
{
	public enum VkPostType
	{
		Post,
		Copy,
		Reply,
		Postpone,
		Suggest,
		Photo,
		Video
	}

	public class VkPost
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("from_id")]
		public long FromId { get; set; }

		[JsonProperty("to_id")]
		public long ToId { get; set; }

		[JsonProperty("date")]
		[JsonConverter(typeof(UnixTimestampToDateTimeConverter))]
		public DateTime Date { get; set; }

		[JsonProperty("post_type")]
		public VkPostType PostType { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("comments")]
		public VkNestedCount Comments { get; set; }

		[JsonProperty("likes")]
		public VkNestedCount Likes { get; set; }

		[JsonProperty("reposts")]
		public VkNestedCount Reposts { get; set; }

		//[JsonProperty("signer_id")]
		//public long SignerId { get; set; }

		////[JsonProperty("reply_owner_id")]
		//public long ReplyOwnerId { get; set; }

		////[JsonProperty("reply_post_id")]
		//public long ReplyPostId { get; set; }

		[JsonProperty("copy_history")]
		public List<VkPost> CopyHistory { get; set; }


		[OnError]
		internal void OnError(StreamingContext context, ErrorContext errorContext)
		{
			errorContext.Handled = true;
		}

	}
}
