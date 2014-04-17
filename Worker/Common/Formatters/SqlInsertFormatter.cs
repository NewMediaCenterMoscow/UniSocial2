using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Common.Formatters
{
	public class SqlInsertFormatter : DataReaderFormatter
	{
		string tableName;
		public SqlInsertFormatter(string TableName)
		{
			tableName = TableName;
		}

		protected override object GetResult()
		{
			//MemoryStream stream = (MemoryStream)base.GetResult();
			//string csvString = Encoding.UTF8.GetString(stream.ToArray());
			//var rows = csvString.Split('\n');

			//var beforeSql = "INSERT INTO " + tableName + " VALUES (";
			//var afterSql = ")";
			//var result = 
			//	from row in rows
			//	select beforeSql + row + afterSql;


			//return result.ToList();



			var reader = (UniSocialObjectsDataReader)base.GetResult();
			
			var beforeSql = "INSERT INTO " + tableName + " VALUES (";
			var afterSql = ")";
			var result = new List<string>();

			int fieldCount = reader.FieldCount;
			int i;
			var sql = new StringBuilder();

			while (reader.Read())
			{
				sql.Clear().Append(beforeSql);
				for (i = 0; i < fieldCount; i++)
				{
					var value = reader.GetValue(i);

					if (value == null)
					{
						sql.Append("NULL,");
					}
					else
					{
						Type t = value.GetType();

						if (value is string)
						{
							value = ((string)value).Replace("'", @"\'");
							sql.Append("'").Append(value).Append("',");
						}
						else if (t.IsEnum)
						{
							sql.Append((int)value).Append(",");
						}
						else if (value is bool)
						{
							if ((bool)value)
							{
								sql.Append(1).Append(",");
							}
							else
							{
								sql.Append(0).Append(",");
							}
						}
						else 
						{
							sql.Append(value).Append(",");
						}
					}
				}
				sql.Remove(sql.Length-1,1).Append(afterSql);

				result.Add(sql.ToString());
			}

			return result;

		}

	}
}
