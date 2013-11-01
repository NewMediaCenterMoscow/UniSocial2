using Collector.Common;
using Collector.Interface;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Worker.Common;
using Worker.Model;
using Worker.Repository;

namespace Worker.Blocks
{
	class BlockFactory
	{
		[Inject]
		public TraceSource Trace { get; set; }

		CollectTask collectTask;

		public BlockFactory(CollectTask CollectTask)
		{
			collectTask = CollectTask;
		}

		static BlockFactory()
		{
			createNinjectModules();
		}


		#region Ninject

		static Dictionary<string, IKernel> ninjectKernels;
		static void createNinjectModules()
		{
			ninjectKernels = new Dictionary<string, IKernel>();
			ninjectKernels.Add(
				"vkontakte", 
				new StandardKernel(
					new NinjectModules.Vk(),
					new NinjectModules.Worker()
				)
			);
		}

		static IKernel getKernelFor(string socialNetwork)
		{
			if (!ninjectKernels.ContainsKey(socialNetwork))
				throw new NotImplementedException("Social network " + socialNetwork + " is not supported!");

			return ninjectKernels[socialNetwork];
		}

		#endregion


		#region Common blocks
		//public TransformManyBlock<Stream, string> StremToIds()
		//{
		//	return 
		//		new TransformManyBlock<Stream, string>(input => {
		//			StreamReader reader = new StreamReader(input);
		//			string result = reader.ReadToEnd();

		//			input.Close();

		//			var ret = result.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

		//			collectTask.AllItems = ret.Length;
		//			collectTask.CounterItems = 0;

		//			return ret;
		//		});
		//}

		public TransformBlock<string, Stream> FileToStream()
		{
			return 
				new TransformBlock<string, Stream>(
					filename => File.OpenRead(filename)
				);
		}

		public ActionBlock<Stream> StreamToFile(string Filename)
		{
			return
				new ActionBlock<Stream>(stream => {
					if(stream != null)
						using (var file = File.Open(Filename, FileMode.Append, FileAccess.Write,FileShare.None))
						{
							stream.Seek(0, SeekOrigin.Begin);
							stream.CopyTo(file);
							file.Flush();
						}
				});
		}

		#endregion

		public BufferBlock<string> Buffer()
		{
			return new BufferBlock<string>();
		}

		public BatchBlock<T> Batch<T>()
		{
			var ninjectKernel = getKernelFor(collectTask.SocialNetwork);
			var apiRequest = ninjectKernel.Get<IApiRequest>();

			var bacthSize = apiRequest.GetRequestBatchSize(collectTask.Method);

			return
				new BatchBlock<T>(bacthSize);
		}

		public TransformBlock<string[], object> Process()
		{
			var ninjectKernel = getKernelFor(collectTask.SocialNetwork);
			var apiRequest = ninjectKernel.Get<IApiRequest>();

			var method = collectTask.Method;
			return new TransformBlock<string[], object>(async ids =>
			{
				collectTask.CounterItems += ids.Length;
				if (collectTask.CounterItems % 64 == 0)
					Trace.TraceEvent(TraceEventType.Information, method.GetHashCode(), "Start process " + collectTask.CounterItems + "/" + collectTask.AllItems);

				if (collectTask.CounterItems % 10000 == 0)
				{
					System.Threading.Thread.Sleep(TimeSpan.FromSeconds(180));
				}
				else if (collectTask.CounterItems % 1000 == 0)
				{
					System.Threading.Thread.Sleep(TimeSpan.FromSeconds(60));
				}

				var requestType = apiRequest.GetRequestType(method);

				object result = null;

				try
				{
					if (requestType == ApiRequestType.ObjectInfo)
					{
						result = await apiRequest.ExecuteRequest(method, ids[0]);
					}
					if (requestType == ApiRequestType.ListObjectsInfo)
					{
						result = await apiRequest.ExecuteRequest(method, ids.ToList());
					}
					if (requestType == ApiRequestType.ListForObject)
					{
						result = await apiRequest.ExecuteRequest(method, ids[0], 0, apiRequest.GetRequestItemsMaxCount(collectTask.Method));
					}
				}
				catch (ApiException ex)
				{
					Trace.TraceEvent(TraceEventType.Error, method.GetHashCode(), ex.Message);
				}
				catch (Exception ex)
				{
					Trace.TraceEvent(TraceEventType.Error, method.GetHashCode(), ex.Message);
				}

				return result;
			//});
			}, new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 10 });
		}

		public BufferBlock<object> OutputBuffer()
		{
			return
				new BufferBlock<object>();
		}

		public ActionBlock<object> WriteResults(IRepository Repo)
		{
			return 
				new ActionBlock<object>(o =>
				{
					if(o != null)
						Repo.WriteResult(o);
				});
		}
	}
}
