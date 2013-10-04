using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Model
{
	[Serializable()]
	public class CollectTask
	{
		protected string _method;
		protected string _socialNetworks;

		public string SocialNetwork { get { return _socialNetworks; } }
		public string Method { get { return _method; } }

		public bool IsCompleted { get; set; }
		public string ErrorMessage { get; set; }

		public CollectTaskIO Input { get; set; }
		public CollectTaskIO Output { get; set; }

		public CollectTask(string Method)
		{
			_method = Method;

			IsCompleted = false;
		}
		public CollectTask(string SocialNetwork, string Method)
		{
			_socialNetworks = SocialNetwork;
			_method = Method;

			IsCompleted = false;
		}

		public override string ToString()
		{
			return _socialNetworks + ":" + _method;
		}
	}
}
