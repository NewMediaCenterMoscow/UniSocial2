using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Threading;


namespace Worker.Model
{
	[Serializable()]
	[DataContract]
	public class CollectTask
	{
		[DataMember]
		public string SocialNetwork { get; set; }
		[DataMember]
		public string Method { get; set; }

		#region Progress & Error message
		[DataMember]
		public bool IsCompleted { get; set; }

		[DataMember]
		public long AllItems { get; set; }
		[DataMember]
		public long CounterItems { get; set; }

		[DataMember]
		public string ErrorMessage { get; set; }
		#endregion

		#region IO
		[DataMember]
		public CollectTaskIO Input { get; set; }
		[DataMember]
		public CollectTaskIO Output { get; set; }
		#endregion

		[DataMember]
		public int CollectTaskId { get; set; }


		public CancellationTokenSource CancellationSource;

		public override string ToString()
		{
			var res = "#" + CollectTaskId + " " + SocialNetwork + ": " + Method;

			return res;
		}
	}
}
