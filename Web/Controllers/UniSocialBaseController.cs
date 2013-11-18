using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.UniSocialService;

namespace Web.Controllers
{
	public class UniSocialBaseController : Controller
	{
		internal static UniSocialClient uniSocialClient;

		static UniSocialBaseController()
		{
			uniSocialClient = new UniSocialClient();
		}
	}
}