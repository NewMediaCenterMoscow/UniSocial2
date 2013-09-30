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

		public CollectTask(string Method)
		{
			_method = Method;
		}
		public CollectTask(string SocialNetwork, string Method)
		{
			_socialNetworks = SocialNetwork;
			_method = Method;
		}

		public string SocialNetwork { get { return _socialNetworks; } }
		public string Method { get { return _method; } }

		public CollectTaskIO Input { get; set; }

		public CollectTaskIO Output { get; set; }

	}
}
