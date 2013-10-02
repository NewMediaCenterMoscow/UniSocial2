using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Worker.Blocks;
using Worker.Common;
using Worker.Model;

namespace Worker
{
	class Program
	{
		public static TraceSource Trace = new TraceSource("primes");

		static void Main(string[] args)
		{
			//var appSettings = ConfigurationManager.AppSettings;
			//string queueName = appSettings["inputQueue"];

			//Trace.TraceInformation("Input queue name: {0}", queueName);

			//var task = Task.Factory.StartNew(() => mainLoop(queueName));
			//task.Wait();


			
			var task = TestWork();
			task.Wait();
		}


		static void mainLoop(string queueName)
		{
				MessageQueue queue;

				if (MessageQueue.Exists(queueName))
					queue = new MessageQueue(queueName);
				else
					queue = MessageQueue.Create(queueName);
				queue.Formatter = new BinaryMessageFormatter();

				var ninject = new StandardKernel(new NinjectModules.Worker());
				var dataCollector = ninject.Get<DataCollector>();

				while (true)
				{
					var receiveMessage = queue.Receive();
					var receiveMessageBody = receiveMessage.Body;

					Trace.TraceInformation("Receive message: {0}", receiveMessageBody);

					if (receiveMessageBody is string)
					{
						string cmd = (string)receiveMessageBody;
						if (cmd == "exit")
							return;
					}
					else if (receiveMessageBody is CollectTask)
					{
						var collectTask = receiveMessageBody as CollectTask;

						Trace.TraceEvent(TraceEventType.Start, collectTask.GetHashCode(), collectTask.ToString());

						var task = dataCollector.Collect(collectTask);

						task.ContinueWith(
							t =>
							{
								if (t.IsFaulted)
								{
									Trace.TraceEvent(TraceEventType.Error, collectTask.GetHashCode(), "Inner expection");
								}

								Trace.TraceEvent(TraceEventType.Stop, collectTask.GetHashCode(), collectTask.ToString());
							}
						);
					}
				}

		}

		static async Task TestWork()
		{
			var ninject = new StandardKernel(new NinjectModules.Worker());
			var dataCollector = ninject.Get<DataCollector>();

			var collectTask = new CollectTask("vkontakte", "wall.get");
			collectTask.Input = new CollectTaskIOFile("input.txt");
			collectTask.Output = new CollectTaskIOFile("result.txt");

			await dataCollector.Collect(collectTask);
		}


	}
}
