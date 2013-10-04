using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Model
{
	[Serializable()]
	public class CollectTaskIOFile : CollectTaskIO
	{
		protected string _filename;
		public string Filename { get { return _filename; } }

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
