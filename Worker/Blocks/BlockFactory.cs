using Collector.Interface;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Worker.Common;
using Worker.Model;

namespace Worker.Blocks
{
	class BlockFactory
	{
		public BlockFactory()
		{
			createNinjectModules();
		}


		#region Ninject

		Dictionary<string, IKernel> ninjectKernels;
		void createNinjectModules()
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

		IKernel getKernelFor(string socialNetwork)
		{
			if (!ninjectKernels.ContainsKey(socialNetwork))
				throw new NotImplementedException("Social network " + socialNetwork + " is not supported!");

			return ninjectKernels[socialNetwork];
		}

		#endregion


		#region Common blocks
		public TransformManyBlock<Stream, string> StremToIds()
		{
			return 
				new TransformManyBlock<Stream, string>(input => {
					StreamReader reader = new StreamReader(input);
					string result = reader.ReadToEnd();

					return result.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
				});
		}

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

		public BatchBlock<T> Batch<T>(CollectTask CollectTask)
		{
			var ninjectKernel = getKernelFor(CollectTask.SocialNetwork);
			var apiRequest = ninjectKernel.Get<IApiRequest>();

			var bacthSize = apiRequest.GetRequestBatchSize(CollectTask.Method);

			return
				new BatchBlock<T>(bacthSize);
		}

		public TransformBlock<string[], object> Process(CollectTask CollectTask)
		{
			var ninjectKernel = getKernelFor(CollectTask.SocialNetwork);
			var apiRequest = ninjectKernel.Get<IApiRequest>();

			var method = CollectTask.Method;
			return new TransformBlock<string[], object>(async ids =>
			{
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
						result = await apiRequest.ExecuteRequest(method, ids[0], 0, apiRequest.GetRequestItemsMaxCount(CollectTask.Method));
					}
				}
				catch (Exception)
				{
				}

				return result;
			});
			//}, new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 5 });
		}

		public TransformBlock<object, Stream> ToCSVStream()
		{
			return new TransformBlock<object, Stream>(o => {

				if (o == null)
					return null;

				var objectFormatter = new ObjectFormatter();
				var stream = objectFormatter.ToCSV(o);

				return stream;
			});
		}

	}
}
