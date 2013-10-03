using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Worker.Model;

namespace Web.Controllers
{
    public class CollectTaskController : Controller
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
				sendCollectInQueue(collectForm);

				return RedirectToAction("CollectStarted");
			}
			else
				return View(collectForm);
		}

		
		public ActionResult CollectStarted()
		{
			return View();
		}


		void sendCollectInQueue(CollectForm collectForm)
		{
			CollectTask ct = new CollectTask(collectForm.Network, collectForm.Method);
			var inputFile = moveInputFile(collectForm);




		}

		string moveInputFile(CollectForm collectForm)
		{
			var directory = @"C:\temp\collect";
			var fileName = collectForm.InputFile.FileName;
			var fullName = Path.Combine(directory, fileName);

			using (var file = System.IO.File.OpenWrite(fullName))
			{
				collectForm.InputFile.InputStream.CopyTo(file);
			}

			return fullName;
		}

	}
}