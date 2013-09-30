using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.NinjectModules
{
	class Worker : NinjectModule
	{
		public override void Load()
		{
			Bind<TraceSource>().ToConstant(Program.Trace);
		}
	}
}
