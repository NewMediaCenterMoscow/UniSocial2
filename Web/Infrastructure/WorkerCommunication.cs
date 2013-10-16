using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Threading.Tasks;
using System.Web;
using Web.Models;
using Web.UniSocialService;
using Worker.Model;

namespace Web.Infrastructure
{
	class WorkerCommunication
	{
		UniSocialClient uniSocialClient;

		public WorkerCommunication()
		{
			uniSocialClient = new UniSocialClient();

		}   

		public void SendTaskToQueue(CollectTask ct)
		{
			uniSocialClient.StartNewTask(ct);
		}

		public List<CollectTask> GetCurrentTasks()
		{
			return uniSocialClient.GetCurrentTasks();
		}

	}
}