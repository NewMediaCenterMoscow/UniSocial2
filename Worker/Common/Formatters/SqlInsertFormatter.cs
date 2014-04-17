using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Common.Formatters
{
	public class SqlInsertFormatter : CSVStreamFormatter
	{
		string tableName;
		public SqlInsertFormatter(string TableName)
		{
			tableName = TableName;
		}

		protected override object GetResult()
		{
			MemoryStream stream = (MemoryStream)base.GetResult();
			string csvString = Encoding.UTF8.GetString(stream.ToArray());
			var rows = csvString.Split('\n');

			var beforeSql = "INSERT INTO " + tableName + " VALUES (";
			var afterSql = ")";
			var result = 
				from row in rows
				select beforeSql + row + afterSql;


			return result.ToList();
		}

	}
}
