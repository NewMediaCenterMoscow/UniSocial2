using Collector.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Common.Formatters;

namespace Worker.Repository
{
	public class MsSqlRepository : DbRepositiry
	{
		public MsSqlRepository(string ConnectionString, string QueryForInputData = "")
			: base(ConnectionString, QueryForInputData)
		{
			writeConn = new SqlConnection(ConnectionString);
			formatter = new DataReaderFormatter();
		}

		public override IEnumerable<string> GetInputData()
		{
			throw new NotImplementedException();
		}

		public override void WriteResult(object Obj)
		{
			if (writeConn.State != System.Data.ConnectionState.Open)
				writeConn.Open();

			var dataReader = formatter.FormatObject(Obj) as IDataReader;

			if (dataReader == null)
				return;

			var tableName = getTableNameFromObject(Obj);

			using (SqlBulkCopy bulkCopy = new SqlBulkCopy(writeConn as SqlConnection))
			{
				try
				{
					bulkCopy.DestinationTableName = tableName;
					bulkCopy.WriteToServer(dataReader);
				}
				catch
				{
					throw;
				}
				finally
				{
					dataReader.Dispose();
				}
			}
		}

		protected override void setTableNames()
		{
			tableNames.Add(typeof(VkPost),		"posts");
			tableNames.Add(typeof(VkFriends),	"friends");
			tableNames.Add(typeof(VkUser),		"users");
		}
	}
}
