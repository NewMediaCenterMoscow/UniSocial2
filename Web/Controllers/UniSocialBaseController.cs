using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Infrastructure;

namespace Web.Controllers
{
	public class UniSocialBaseController : Controller
	{
		internal static WorkerCommunication wkComm = new WorkerCommunication();

		static UniSocialBaseController()
		{
 
		}
	}
}