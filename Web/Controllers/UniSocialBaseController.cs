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
		protected UniSocialServiceClient uniSocialClient;

		public UniSocialBaseController()
		{
			uniSocialClient = new UniSocialServiceClient();
		}
	}
}