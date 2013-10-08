using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Worker.Model;

namespace Worker.Service
{
	[ServiceContract]
	public interface IUniSocial
	{
		[OperationContract]
		void StartNewTask(CollectTask CollectTask);

		[OperationContract]
		List<CollectTask> GetCurrentTasks();
	}
}
