using Collector.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Common;
using Worker.Common.Formatters;

namespace Worker.Repository
{
	class PgsqlRepository : DbRepositiry
	{
		public PgsqlRepository(string ConnectionString, string QueryForInputData = "")
			: base(ConnectionString, QueryForInputData)
		{
			writeConn = new NpgsqlConnection(ConnectionString);
			formatter = new CSVStreamFormatter();
		}

		public override IEnumerable<string> GetInputData()
		{
			if (queryForInputData == "")
				yield break;

			var conn = new NpgsqlConnection(connStr);
			conn.Open();

			NpgsqlCommand cmd = new NpgsqlCommand(queryForInputData, conn);
			var reader = cmd.ExecuteReader();

			while (reader.Read())
				yield return reader.GetString(0);
		}

		public override void WriteResult(object Obj)
		{
			var dataStream = formatter.FormatObject(Obj) as Stream;

			if (dataStream == null)
				return;

			var tableName = getTableNameFromObject(Obj);
			var query = "COPY " + tableName + " FROM STDIN WITH CSV";


			if (writeConn.State != System.Data.ConnectionState.Open)
				writeConn.Open();

			var cmd = new NpgsqlCommand(query, writeConn as NpgsqlConnection);
			var cin = new NpgsqlCopyIn(cmd, writeConn as NpgsqlConnection);

			try
			{
				cin.Start();

				dataStream.Seek(0, SeekOrigin.Begin);
				dataStream.CopyTo(cin.CopyStream);

				cin.End();
			}
			catch
			{
				cin.Cancel("Undo copy");
				throw;
			}
			finally 
			{
				dataStream.Dispose();
			}
		}

		protected override void setTableNames()
		{
			tableNames.Add(typeof(VkPost), "new_post");
			tableNames.Add(typeof(VkFriends), "new_friends");
		}
	}
}
