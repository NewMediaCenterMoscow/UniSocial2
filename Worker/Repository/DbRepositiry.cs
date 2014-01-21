using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Worker.Common;
using Worker.Common.Formatters;

namespace Worker.Repository
{
	public abstract class DbRepositiry : IRepository
	{
		protected string connStr;
		protected string queryForInputData;

		protected DbConnection writeConn;

		protected IFormatter formatter;
		protected Dictionary<Type, string> tableNames;

		public DbRepositiry(string ConnectionString, string QueryForInputData = "")
		{
			connStr = ConnectionString;
			queryForInputData = QueryForInputData;

			tableNames = new Dictionary<Type, string>();
			setTableNames();

			// Set DbConnection in derived classes
			// Set formatter in derived classes
		}

		protected abstract void setTableNames();
		public abstract IEnumerable<string> GetInputData();
		public abstract void WriteResult(object Obj);

		protected string getTableNameFromObject(object obj)
		{
			var type = Helpers.GetObjectType(obj);

			return tableNames[type];
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
	}
}
