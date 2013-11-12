using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Worker.Model;

namespace Worker.Service
{
	[ServiceContract]
	public interface IUniSocial
	{
		[OperationContract(IsOneWay=true)]
		void StartNewTask(CollectTask CollectTask);

		[OperationContract]
		List<CollectTask> GetTasks();

		[OperationContract(IsOneWay = true)]
		void RemoveTaskFromList(CollectTask CollectTask);
	}
}
