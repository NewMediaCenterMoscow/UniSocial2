using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
			ViewBag.SourceFiles = service.GetPossibleSourceFiles();

            return View(new CollectForm());
        }

		[HttpPost]
		public ActionResult Index(CollectForm collectForm)
		{
			if (ModelState.IsValid)
			{
				var inputFilename = service.GetSourceFilename(collectForm.InputFile);
				var outpuFilename = service.GetResultFilename(collectForm.OutputFilename);

				CollectTask ct = new CollectTask(collectForm.Network, collectForm.Method);
				ct.Input = new CollectTaskIOFile(inputFilename);
				ct.Output = new CollectTaskIOFile(outpuFilename);

				wkComm.SendTaskToQueue(ct);

				return RedirectToAction("CollectStarted");
			}
			else
			{
				ViewBag.SourceFiles = service.GetPossibleSourceFiles();

				return View(collectForm);
			}
		}

		
		public ActionResult CollectStarted()
		{
			return View();
		}


	}
}