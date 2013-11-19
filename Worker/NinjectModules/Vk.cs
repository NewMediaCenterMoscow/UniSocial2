using Collector.Api;
using Collector.Interface;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Common;

namespace Worker.NinjectModules
{
	class Vk : NinjectModule
	{
		public override void Load()
		{
			Bind<IApi>().To<VkApi>();
			Bind<IApiRequest>().To<VkApiRequest>();
			Bind<IDataExtractor>().To<VkDataExtractor>();

			Bind<TraceSource>().ToConstant(DataCollector.Trace);
		}
	}
}
