using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collector.Common
{
	public class ApiException : Exception
	{
		public int ErrorCode;

		public ApiException()
		{

		}
		public ApiException(string Message, int ErrorCode)
			: base(Message)
		{
			this.ErrorCode = ErrorCode;
		}
	}
}
