using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Worker.Model;

namespace Service
{
	[ServiceContract]
	public interface IUniSocial
	{
		[OperationContract(IsOneWay = true)]
		void StartNewTask(CollectTask CollectTask);

		[OperationContract]
		List<CollectTask> GetTasks();

		[OperationContract(IsOneWay = true)]
		void RemoveTaskFromList(int CollectTaskId);

		[OperationContract(IsOneWay = true)]
		void CancelTask(int CollectTaskId);
	}
}
