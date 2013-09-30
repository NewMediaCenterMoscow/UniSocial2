using Collector.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Common
{
	class ObjectFormatter
	{
		Dictionary<Type, Action<object, StreamWriter>> formatters;

		public ObjectFormatter()
		{
			formatters = new Dictionary<Type,Action<object, StreamWriter>>();

			formatters.Add(typeof(VkUser), (o,s) =>{
				var obj = o as VkUser;
				s.WriteLine(obj.FirstName);
			});
			formatters.Add(typeof(VkGroup), (o, s) =>
			{
				var obj = o as VkGroup;

				s.Write("\""); s.Write(obj.Id);							s.Write("\",");
				s.Write("\""); s.Write(obj.Name.Replace("\"", "\"\""));	s.Write("\",");
				s.Write("\""); s.Write(obj.ScreenName != null ? obj.ScreenName.Replace("\"", "\"\"") : ""); s.Write("\",");
				s.Write("\""); s.Write(obj.IsClosed);					s.Write("\",");
				s.Write("\""); s.Write((int)obj.Type);					s.Write("\",");
				s.Write("\""); s.Write(obj.MembersCount);				s.Write("\"\n");

			});





		}

		public Stream ToCSV(object Object)
		{
			var resultStream = new MemoryStream();
			var writer = new StreamWriter(resultStream);

			if(Object is List<object>)
			{
				var listObjs = Object as List<object>;
				var t = listObjs.First().GetType();

				foreach (var item in listObjs)
				{
					formatters[t](item, writer);
				}
			}
			else
			{
				var t = Object.GetType();
				formatters[t](Object, writer);
			}

			//resultStream.Seek(0, SeekOrigin.Begin);
			writer.Flush();

			return resultStream;
		}

	}
}
