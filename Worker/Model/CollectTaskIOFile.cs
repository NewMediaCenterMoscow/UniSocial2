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
		protected string _filename;
		[DataMember]
		public string Filename { get { return _filename; } protected set { _filename = value; } }

		public CollectTaskIOFile(string Filename)
		{
			_filename = Filename;
		}

		public override string ToString()
		{
			return "File: " + _filename;
		}

	}
}
