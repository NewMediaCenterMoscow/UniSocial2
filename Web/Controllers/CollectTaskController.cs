using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Infrastructure;
using Web.Models;
using Web.UniSocialService;

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
				var inputFilename = collectForm.InputFile;

				CollectTask ct = new CollectTask() { SocialNetwork = collectForm.Network, Method = collectForm.Method };
				ct.Input = new CollectTaskIOFile() { Filename = inputFilename };

				if (collectForm.OutputInDb)
				{
					string connStr = ConfigurationManager.ConnectionStrings["postgresql"].ConnectionString;
					ct.Output = new CollectTaskIODatabase() { ConnectionString = connStr };
				}
				else
				{
					var outputFilename = collectForm.OutputFilename;
					ct.Output = new CollectTaskIOFile() { Filename = outputFilename };
				}


				wkComm.SendTaskToQueue(ct);

				return RedirectToAction("CollectStarted");
			}
			else
			{
				return View(collectForm);
			}
		}

		
		public ActionResult CollectStarted()
		{
			return View();
		}


	}
}