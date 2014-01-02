using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Diagnostics;
using Service;

namespace ConsoleService
{
	class Program
	{
		public static TraceSource Trace = new TraceSource("primes");

		static void Main(string[] args)
		{
			var selfHost = createService();

			if (selfHost != null)
			{
				Trace.TraceEvent(TraceEventType.Information, 0, "The service is ready");

				Console.ReadLine();
				selfHost.Close();
			}
		}

		private static ServiceHost createService()
		{
			var uniSocialServiceClass = new UniSocialService();

			var selfHost = new ServiceHost(uniSocialServiceClass);

			try
			{
				selfHost.Open();

				return selfHost;
			}
			catch (CommunicationException ce)
			{
				Trace.TraceEvent(TraceEventType.Error, 0, ce.Message);

				selfHost.Abort();

				return null;
			}
		}
	}
}
