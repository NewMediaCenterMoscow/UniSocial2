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

namespace Worker.Repository
{
	class DbRepository : IRepository
	{
		string connStr;
		string queryForInputData;

		NpgsqlConnection writeConn;

		ObjectFormatter formatter;
		Dictionary<Type, string> tableNames;

		public DbRepository(string ConnectionString, string QueryForInputData = "")
		{
			connStr = ConnectionString;
			queryForInputData = QueryForInputData;

			tableNames = new Dictionary<Type, string>();
			setTableNames();

			formatter = new ObjectFormatter();

			writeConn = new NpgsqlConnection(connStr);
			//factory = DbProviderFactories.GetFactory("Npgsql");
		}

		public IEnumerable<string> GetInputData()
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

		public void WriteResult(object Obj)
		{
			var dataStream = formatter.ToCSVStream(Obj);

			if (dataStream == null)
				return;

			var tableName = getTableNameFromObject(Obj);
			var query = "COPY " + tableName + " FROM STDIN WITH CSV";


			if (writeConn.State != System.Data.ConnectionState.Open)
				writeConn.Open();

			var cmd = new NpgsqlCommand(query, writeConn);
			var cin = new NpgsqlCopyIn(cmd, writeConn);

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
		}

		public void Dispose()
		{
			if (writeConn != null)
			{
				if (writeConn.State != System.Data.ConnectionState.Closed)
					writeConn.Close();

				writeConn.Dispose();
			}
		}

		void setTableNames()
		{
			tableNames.Add(typeof(VkPost), "new_post");
			tableNames.Add(typeof(VkFriends), "new_friends");
		}

		string getTableNameFromObject(object obj)
		{
			var type = Helpers.GetObjectType(obj);

			return tableNames[type];
		}
	}
}
