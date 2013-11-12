using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Messaging;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Worker.Blocks;
using Worker.Common;
using Worker.Model;
using Worker.Service;

namespace Worker
{
	class Program
	{
		public static TraceSource Trace = new TraceSource("primes");

		static void Main(string[] args)
		{

			var selfHost = createService();

			if (selfHost != null)
			{
				Trace.TraceEvent(TraceEventType.Information, 0, "Service is ready");

				Console.ReadLine();
				selfHost.Close();
			}

		}

		private static ServiceHost createService()
		{
			var ninject = new StandardKernel(new NinjectModules.Worker());
			var dataCollector = ninject.Get<DataCollector>();

			var uniSocialServiceClass = new UniSocialService(dataCollector);
			uniSocialServiceClass.Trace = Program.Trace;

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
