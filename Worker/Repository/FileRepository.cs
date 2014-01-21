using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Worker.Common;
using Worker.Common.Formatters;
using Worker.Repository;

namespace Worker.Repository
{
	class FileRepository : IRepository
	{
		CSVStreamFormatter formatter;
		string filename;

		public FileRepository(string Filename)
		{
			filename = Filename;
			formatter = new CSVStreamFormatter();
		}

		public void WriteResult(object Obj)
		{
			var stream = formatter.FormatObject(Obj) as Stream;

			if (stream == null)
				return;

			using (var file = File.Open(filename, FileMode.Append, FileAccess.Write, FileShare.None))
			{
				stream.Seek(0, SeekOrigin.Begin);
				stream.CopyTo(file);
				file.Flush();
			}

			formatter.Dispose();
		}

		public IEnumerable<string> GetInputData()
		{
			return File.ReadLines(filename);
		}

		public void Dispose()
		{
			
		}
	}
}
