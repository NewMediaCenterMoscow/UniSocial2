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
	[KnownType(typeof(CollectTaskIOFile))]
	[KnownType(typeof(CollectTaskIODatabase))]
	public abstract class CollectTaskIO
	{

	}
}
