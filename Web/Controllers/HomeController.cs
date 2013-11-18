using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
	public class HomeController : UniSocialBaseController
	{
		public ActionResult Index()
		{
			var allTasks = uniSocialClient.GetTasks();

			return View(allTasks);
		}
	}
}