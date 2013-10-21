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
		public string Method { get; set; }

		[Required]
		public string InputFile { get; set; }

		[Required]
		public string OutputFilename { get; set; }

		[Required]
		public string Network { get; set; }

	}
}