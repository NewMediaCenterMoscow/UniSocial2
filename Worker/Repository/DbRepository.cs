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

		public DbRepository(string ConnectionString, string QueryForInputData = "")
		{
			connStr = ConnectionString;
			queryForInputData = QueryForInputData;
		}


		public IEnumerable<string> GetInputData()
		{
			throw new NotImplementedException();
		}

		public void WriteResult(object Object)
		{
			throw new NotImplementedException();
		}
	}
}
