using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace Web.Infrastructure
{
	public class Service
	{
		string fileDir;
		string sourceDir;
		string resultDir;

		public Service()
		{
			var appSettings = ConfigurationManager.AppSettings;

			fileDir = appSettings["fileDirectory"];
			sourceDir = appSettings["sourceDirectory"];
			resultDir = appSettings["resultDirectory"];
		}


		public string MoveSourceFile(HttpPostedFileBase inputFile)
		{
			var fileName = inputFile.FileName;
			var fullName = Path.Combine(fileDir, sourceDir, fileName);

			inputFile.SaveAs(fullName);

			return fullName;
		}

		public string GetResultFilename(string Filename)
		{
			return Path.Combine(fileDir, resultDir, Filename);
		}
		public string GetSourceFilename(string Filename)
		{
			return Path.Combine(fileDir, sourceDir, Filename);
		}

		public List<string> GetPossibleSourceFiles()
		{
			var res =
				from fullName in Directory.GetFiles(Path.Combine(fileDir, sourceDir))
				select Path.GetFileName(fullName);

			return res.ToList();
		}


	}
}