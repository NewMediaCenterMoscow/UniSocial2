using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Worker.Common;
using Worker.Model;

namespace Service
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	class UniSocialService : IUniSocial
	{
		public TraceSource Trace { get; set; }

		DataCollector dataCollector;

		List<CollectTask> tasks;
		int currentId;

		public UniSocialService()
		{
			tasks = new List<CollectTask>();
			currentId = 0;

			this.Trace = new TraceSource("primes");

			dataCollector = new DataCollector();
			DataCollector.Trace = this.Trace;
		}

		#region Service
		public void StartNewTask(CollectTask collectTask)
		{
			collectTask.CollectTaskId = currentId;
			currentId++;

			tasks.Add(collectTask);

			Trace.TraceEvent(TraceEventType.Start, collectTask.GetHashCode(), collectTask.ToString());

			Task.Factory.StartNew(startTask, collectTask);
		}

		public List<CollectTask> GetTasks()
		{
			return tasks;
		}

		public void RemoveTaskFromList(int CollectTaskId)
		{
			var t = tasks.Where(c => c.CollectTaskId == CollectTaskId).FirstOrDefault();

			if (t != null)
				tasks.Remove(t);
		}

		public void CancelTask(int CollectTaskId)
		{
			var t = tasks.Where(c => c.CollectTaskId == CollectTaskId).FirstOrDefault();

			if (t != null)
			{
				if (!t.CancellationSource.IsCancellationRequested)
					t.CancellationSource.Cancel();
			}
		}

		#endregion

		void startTask(object collectTask)
		{
			var ct = collectTask as CollectTask;
			ct.CancellationSource = new CancellationTokenSource();

			var task = dataCollector.Collect(ct);

			task.ContinueWith(t =>
			{
				if (t.IsFaulted)
				{
					Trace.TraceEvent(TraceEventType.Error, collectTask.GetHashCode(), t.Exception.Message);
					ct.ErrorMessage = t.Exception.Message;
				}

				ct.IsCompleted = true;
				Trace.TraceEvent(TraceEventType.Stop, collectTask.GetHashCode(), collectTask.ToString());
			});
		}

	}
}
