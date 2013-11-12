using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Model
{
	[Serializable()]
	[DataContract]
	public class CollectTaskIOFile : CollectTaskIO
	{
		[DataMember]
		public string Filename { get; set; }

		public override string ToString()
		{
			return "File: " + Filename;
		}

	}
}
