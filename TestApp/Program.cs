using Collector.Interface;
using Collector.Model;
using Ninject;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestApp
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				var task = Work();
				task.Wait();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		static async Task Work()
		{
			var ninjectKernal = new StandardKernel(new VkNinjectModule());
			var apiRequest = ninjectKernal.Get<IApiRequest>();
			//var result = await apiRequest.GetObject<VkGroup>("club1");
			//var result2 = await apiRequest.GetMultipleObjects<VkGroup>(new List<string>() { "apiclub", "55293029" });

			string filename = "friends_new_cutted.txt";
			var lines = File.ReadAllLines(filename).ToList();

			var partData = splitData(lines);
			lines = null;

			int all = partData.Count;
			int counter = 1;
			var results = new List<VkUser>();
			foreach (var users in partData)
			{
				var usersData = await apiRequest.ExecuteRequest<VkUser>("users.get", users);
				results.AddRange(usersData);
				Thread.Sleep(10);

				if (counter % 50 == 0)
				{
					Console.WriteLine("Friends part " + counter + "/" + all + " finished!");
					Thread.Sleep(100);
				}

				counter++;
			}

			Console.WriteLine("Start insert data!");
			//writeData(results);
			Console.WriteLine("End insert data!");
		}

		static List<List<string>> splitData(List<string> Data)
		{
			var result = new List<List<string>>();

			int partitionSize = 200;
			int counterData, counter, partCounter;
			int dataNum = Data.Count;

			partCounter = 0;
			counter = 0;
			result.Add(new List<string>());
			for (counterData = 0; counterData < dataNum; counterData++)
			{
				result[partCounter].Add(Data[counterData]);

				counter++;
				if (counter == partitionSize)
				{
					counter = 0;
					partCounter++;
					result.Add(new List<string>());
				}
			}

			return result;
		}

		static void writeData(List<VkUser> Users)
		{
			//string connStr = "Server=vs2013-ipk.cloudapp.net;Port=5432;User Id=postgres;Password=123;Database=unisocial;SyncNotification=true;";
			//var conn = new NpgsqlConnection(connStr);
			//conn.Open();

			//// Insert new data
			//var query = "COPY unisocial.vk_user FROM STDIN WITH CSV";
			//var cmd = new NpgsqlCommand(query, conn);
			//var cin = new NpgsqlCopyIn(cmd, conn);
			//cmd.CommandTimeout = 999999999;


			try
			{
				//cin.Start();
				FileStream fsError = new FileStream("error.csv", FileMode.OpenOrCreate);
				FileStream fsBackup = new FileStream("result.csv", FileMode.OpenOrCreate);
				StreamWriter errorWriter = new StreamWriter(fsError);
				//StreamWriter writer = new StreamWriter(cin.CopyStream);
				StreamWriter writer = new StreamWriter(fsBackup);
				StringBuilder sb = new StringBuilder();
				foreach (var usr in Users)
				{
					sb.Clear();

					sb.Append("\"").Append(usr.Id).Append("\",");
					sb.Append("\"").Append(usr.FirstName.Replace("\"", "\"\"")).Append("\",");
					sb.Append("\"").Append(usr.LastName.Replace("\"", "\"\"")).Append("\",");
					sb.Append("\"").Append((int)usr.Sex).Append("\",");
					sb.Append("\"").Append(usr.BDate ?? "").Append("\",");
					sb.Append("\"").Append(usr.City).Append("\",");
					sb.Append("\"").Append(usr.Country).Append("\",");
					sb.Append("\"").Append(usr.Deactivated).Append("\",");
					sb.Append("\"").Append(2).Append("\"\n");

					if ((usr.BDate != null && usr.BDate.Length > 48) || usr.FirstName.Length > 98 || usr.LastName.Length > 98)
						errorWriter.Write(sb.ToString());
					else
						writer.Write(sb.ToString());
				}
				writer.Flush();
				errorWriter.Flush();
				//cin.End();
			}
			catch
			{
				//cin.Cancel("Undo copy");
				throw;
			}			
		}


	}
}
