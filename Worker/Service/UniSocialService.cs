using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Worker.Common;
using Worker.Model;

namespace Worker.Service
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	class UniSocialService : IUniSocial
	{
		public TraceSource Trace { get; set; }

		DataCollector dataCollector;

		List<CollectTask> tasks;

		public UniSocialService(DataCollector Collector)
		{
			tasks = new List<CollectTask>();

			dataCollector = Collector;
		}


		#region Service
		public void StartNewTask(CollectTask CollectTask)
		{
			tasks.Add(CollectTask);

			Trace.TraceEvent(TraceEventType.Start, CollectTask.GetHashCode(), CollectTask.ToString());
			var task = dataCollector.Collect(CollectTask);

			task.ContinueWith(
				t =>
				{
					if (t.IsFaulted)
					{
						Trace.TraceEvent(TraceEventType.Error, CollectTask.GetHashCode(), t.Exception.Message);
						CollectTask.ErrorMessage = t.Exception.Message;
					}

					CollectTask.IsCompleted = true;
					//tasks.Remove(CollectTask);
					Trace.TraceEvent(TraceEventType.Stop, CollectTask.GetHashCode(), CollectTask.ToString());
				}
			);

		}

		public List<CollectTask> GetTasks()
		{
			return tasks;
		}

		public void RemoveTaskFromList(CollectTask CollectTask)
		{
			tasks.Remove(CollectTask);
		}

	
		#endregion




	}
}
