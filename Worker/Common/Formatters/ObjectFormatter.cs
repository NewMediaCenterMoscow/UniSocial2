using Collector.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collector.Common;
using System.Data;

namespace Worker.Common.Formatters
{
	public abstract class ObjectFormatter : IFormatter
	{
		public object FormatObject(object Obj)
		{
			var type = Helpers.GetObjectType(Obj);
			var isList = Helpers.IsList(Obj);

			if (type == null)
				return null;

			SetObjectType(type);

			if (isList)
			{
				var listObjs = Obj as List<object>;

				foreach (var item in listObjs)
				{
					this.HandleObject(item);
				}
			}
			else
			{
				HandleObject(Obj);
			}

			return GetResult();
		}

		protected abstract void SetObjectType(Type t);
		protected abstract void HandleObject(object Obj);
		protected abstract object GetResult();
		public abstract void Dispose();
	}
}
