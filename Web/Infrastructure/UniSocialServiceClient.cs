using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using Worker.Model;

namespace Web.Infrastructure
{
	public class UniSocialServiceClient : ClientBase<IUniSocial>, IUniSocial
	{

		#region Constructors
		
		public UniSocialServiceClient()
		{
		}

		public UniSocialServiceClient(string endpointConfigurationName) :
			base(endpointConfigurationName)
		{
		}

		public UniSocialServiceClient(string endpointConfigurationName, string remoteAddress) :
			base(endpointConfigurationName, remoteAddress)
		{
		}

		public UniSocialServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
			base(endpointConfigurationName, remoteAddress)
		{
		}

		public UniSocialServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
			base(binding, remoteAddress)
		{
		}

		#endregion


		#region UniSocialService Members

		public void StartNewTask(CollectTask CollectTask)
		{
			base.Channel.StartNewTask(CollectTask);
		}

		public List<CollectTask> GetTasks()
		{
			return base.Channel.GetTasks();
		}

		public void RemoveTaskFromList(int CollectTaskId)
		{
			base.Channel.RemoveTaskFromList(CollectTaskId);
		}

		public void CancelTask(int CollectTaskId)
		{
			base.Channel.CancelTask(CollectTaskId);
		}

		#endregion

	}
}