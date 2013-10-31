using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
	public class CollectForm
	{
		[Required]
		[Display(Name="Need data")]
		public string Method { get; set; }

		[Required]
		[Display(Name = "Input file")]
		public string InputFile { get; set; }

		[Display(Name = "Output file")]
		public string OutputFilename { get; set; }

		[Required]
		[Display(Name = "Save result in DB")]
		public bool OutputInDb { get; set; }

		[Required]
		[Display(Name = "Social network")]
		public string Network { get; set; }

	}
}