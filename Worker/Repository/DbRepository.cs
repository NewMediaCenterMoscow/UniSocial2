using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Repository
{
	class DbRepository : IRepository
	{
		string connStr;
		string queryForInputData;

		NpgsqlConnection writeConn;

		public DbRepository(string ConnectionString, string QueryForInputData = "")
		{
			connStr = ConnectionString;
			queryForInputData = QueryForInputData;

			writeConn = new NpgsqlConnection(connStr);
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

		public void WriteResult(object Object)
		{
			if (writeConn.State != System.Data.ConnectionState.Open)
				writeConn.Open();


		}

		public void Dispose()
		{
			if (writeConn != null)
			{
				if(writeConn.State != System.Data.ConnectionState.Closed)
					writeConn.Close();

				writeConn.Dispose();
			}
		}
	}
}
