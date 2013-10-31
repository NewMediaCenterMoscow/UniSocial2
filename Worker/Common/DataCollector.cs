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
using Worker.Blocks;
using Worker.Model;
using Worker.Repository;

namespace Worker.Common
{
	class DataCollector
	{
		[Inject]
		public TraceSource Trace { get; set; }

		public DataCollector()
		{

		}

		public async Task Collect(CollectTask collectTask)
		{
			var blockFactory = new BlockFactory(collectTask);
			var blockController = new BlockController();
			blockController.Trace = this.Trace;
			blockFactory.Trace = this.Trace;

			// Create repo for write results
			var inputRepo = getRepository(collectTask.Input);
			var outputRepo = getRepository(collectTask.Output);

			var bufferBlock = blockFactory.Buffer();
			var bacthBlock = blockFactory.Batch<string>();
			var processBlock = blockFactory.Process();
			var outputBufferBlock = blockFactory.OutputBuffer();
			var outputBlock = blockFactory.WriteResults(outputRepo);

			blockController.LinkWithCompletion(bufferBlock, bacthBlock);
			blockController.LinkWithCompletion(bacthBlock, processBlock);
			blockController.LinkWithCompletion(processBlock, outputBufferBlock);
			blockController.LinkWithCompletion(outputBufferBlock, outputBlock);
			//blockController.LinkWithCompletion(processBlock, outputBlock);

			// Read data and send to blocks
			foreach (var item in inputRepo.GetInputData())
			{
				bufferBlock.Post(item);
				collectTask.AllItems++;
			}
			bufferBlock.Complete();

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
				repo = new DbRepository((collectTaskIO as CollectTaskIODatabase).ConnectionString);
			}

			return repo;
		}



	}
}
