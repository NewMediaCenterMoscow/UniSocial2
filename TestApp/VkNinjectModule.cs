using Collector.Api;
using Collector.Interface;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
	public class VkNinjectModule : NinjectModule
	{
		public override void Load()
		{
			Bind<IApi>().To<VkApi>();
			Bind<IApiRequest>().To<VkApiRequest>();
			Bind<IDataExtractor>().To<VkDataExtractor>();
		}

	}
}
