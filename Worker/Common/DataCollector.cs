using Collector.Interface;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Worker.Blocks;
using Worker.Model;
using Worker.Repository;

namespace Worker.Common
{
	public class DataCollector
	{
		public static TraceSource Trace { get; set; }

		public DataCollector()
		{

		}

		public async Task Collect(CollectTask collectTask)
		{
			var blockFactory = new BlockFactory(collectTask);
			var blockController = new BlockController();
			blockController.Trace = DataCollector.Trace;
			blockFactory.Trace = DataCollector.Trace;

			// Create repo for write results
			var inputRepo = getRepository(collectTask.Input);
			var outputRepo = getRepository(collectTask.Output);

			var inputBufferBlock = blockFactory.InputBuffer();
			var bacthBlock = blockFactory.Batch<string>();
			var processBlock = blockFactory.Process();
			var outputBufferBlock = blockFactory.OutputBuffer();
			var outputBlock = blockFactory.WriteResults(outputRepo);

			blockController.LinkWithCompletion(inputBufferBlock, bacthBlock);
			blockController.LinkWithCompletion(bacthBlock, processBlock);
			blockController.LinkWithCompletion(processBlock, outputBufferBlock);
			blockController.LinkWithCompletion(outputBufferBlock, outputBlock);

			// Read data and send to blocks
			foreach (var item in inputRepo.GetInputData())
			{
				await inputBufferBlock.SendAsync(item);
				collectTask.AllItems++;

                if (collectTask.AllItems % 524288 == 0)
                    Thread.Sleep(TimeSpan.FromMinutes(15));

				if (collectTask.CancellationSource.IsCancellationRequested)
					break;
			}
			inputBufferBlock.Complete();

			await outputBlock.Completion;

			inputRepo.Dispose();
			outputRepo.Dispose();
		}

		IRepository getRepository(CollectTaskIO collectTaskIO)
		{
			IRepository repo = null;
			
			if (collectTaskIO is CollectTaskIOFile)
			{
				repo = new FileRepository((collectTaskIO as CollectTaskIOFile).Filename);
			}
			if (collectTaskIO is CollectTaskIODatabase)
			{
				//repo = new PgsqlRepository((collectTaskIO as CollectTaskIODatabase).ConnectionString);
				repo = new MsSqlRepository((collectTaskIO as CollectTaskIODatabase).ConnectionString);
			}

			return repo;
		}



	}
}
