using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Infrastructure;
using Web.Models;
using Worker.Model;

namespace Web.Controllers
{
	public class CollectTaskController : UniSocialBaseController
    {
        public ActionResult Index()
        {
            return View(new CollectForm());
        }

		[HttpPost]
		public ActionResult Index(CollectForm collectForm)
		{
			if (ModelState.IsValid)
			{
				var ct = wkComm.CreateCollectTask(collectForm);
				wkComm.SendTaskToQueue(ct);

				return RedirectToAction("CollectStarted");
			}
			else
				return View(collectForm);
		}

		
		public ActionResult CollectStarted()
		{
			var collectWork = wkComm.GetCollectTaskCount();
			return View((object)collectWork);
		}


	}
}