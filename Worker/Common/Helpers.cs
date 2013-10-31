using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Common
{
	static class Helpers
	{
		public static Type GetObjectType(object Obj)
		{
			Type t = null;

			if (IsList(Obj))
			{
				var listObjs = Obj as List<object>;

				if (listObjs.Count == 0)
					return null;

				t = listObjs.First().GetType();
			}
			else
			{
				t = Obj.GetType();
			}

			return t;
		}

		public static bool IsList(object Obj)
		{
			return Obj is List<object>;
		}

	}
}
