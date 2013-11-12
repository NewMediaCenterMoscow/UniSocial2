using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Model
{
	[Serializable()]
	[DataContract]
	public class CollectTaskIODatabase : CollectTaskIO
	{
		protected string _connStr;
		[DataMember]
		public string ConnectionString { get; set; }
	}
}
