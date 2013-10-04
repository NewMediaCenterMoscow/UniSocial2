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

		static MessageQueue inputQueue;
		static MessageQueue outputQueue;

		static void Main(string[] args)
		{
			var appSettings = ConfigurationManager.AppSettings;

			string inputQueueName = appSettings["inputQueue"];
			string outputQueueName = appSettings["outputQueue"];
			createQueues(inputQueueName, outputQueueName);
			Trace.TraceInformation("Queue names: {0}, {1}", inputQueueName, outputQueueName);

			var task = Task.Factory.StartNew(mainLoop);
			Console.ReadLine();

			//var t = TestWork();
			//t.Wait();
		}


		static void mainLoop()
		{
			var ninject = new StandardKernel(new NinjectModules.Worker());
			var dataCollector = ninject.Get<DataCollector>();

			while (true)
			{
				var receiveMessage = inputQueue.Receive();
				var receiveMessageBody = receiveMessage.Body;

				Trace.TraceInformation("Receive message: {0}", receiveMessageBody);

				if (receiveMessageBody is string)
				{
					string cmd = (string)receiveMessageBody;
					if (cmd == "exit")
					{
						return;
					}
					if (cmd == "taskcount")
					{
						outputQueue.Send(5);
					}
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
								Trace.TraceEvent(TraceEventType.Error, collectTask.GetHashCode(), t.Exception.Message);
								collectTask.ErrorMessage = t.Exception.Message;
							}

							collectTask.IsCompleted = true;
							Trace.TraceEvent(TraceEventType.Stop, collectTask.GetHashCode(), collectTask.ToString());
						}
					);
				}
			}

		}

		private static void createQueues(string inputQueueName, string outputQueueName)
		{
			createQueue(ref inputQueue, inputQueueName);
			createQueue(ref outputQueue, outputQueueName);
		}
		private static void createQueue(ref MessageQueue queue, string inputQueueName)
		{
			if (MessageQueue.Exists(inputQueueName))
				queue = new MessageQueue(inputQueueName);
			else
				queue = MessageQueue.Create(inputQueueName);
			queue.Formatter = new BinaryMessageFormatter();
		}


		static async Task TestWork()
		{
			var ninject = new StandardKernel(new NinjectModules.Worker());
			var dataCollector = ninject.Get<DataCollector>();

			var collectTask = new CollectTask("vkontakte", "wall.get");
			collectTask.Input = new CollectTaskIOFile("users.txt");
			collectTask.Output = new CollectTaskIOFile("result.txt");

			await dataCollector.Collect(collectTask);
		}


	}
}
