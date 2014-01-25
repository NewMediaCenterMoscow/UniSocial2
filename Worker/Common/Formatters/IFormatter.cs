using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Common.Formatters
{
	public interface IFormatter : IDisposable
	{
		//void SetObjectType(Type t);
		//void HandleObject(object Obj);
		//object GetResult();

		//void Initialize();

		object FormatObject(object Obj);
	}
}
