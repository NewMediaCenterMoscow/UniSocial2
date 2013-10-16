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

			//var inputBlock = blockFactory.FileToStream();
			//var idsBlock = blockFactory.StremToIds();
			var bufferBlock = blockFactory.Buffer();
			var bacthBlock = blockFactory.Batch<string>();
			var processBlock = blockFactory.Process();
			var outputBlock = blockFactory.WriteResults(outputRepo);

			//blockController.LinkWithCompletion(inputBlock, idsBlock);
			//blockController.LinkWithCompletion(idsBlock, bacthBlock);
			blockController.LinkWithCompletion(bufferBlock, bacthBlock);
			blockController.LinkWithCompletion(bacthBlock, processBlock);
			blockController.LinkWithCompletion(processBlock, outputBlock);

			//inputBlock.Post( (collectTask.Input as CollectTaskIOFile).Filename );
			//inputBlock.Complete();

			// Read data and send to blocks
			foreach (var item in inputRepo.GetInputData())
			{
				bufferBlock.Post(item);
				collectTask.AllItems++;
			}
			bufferBlock.Complete();

			await outputBlock.Completion;
		}

		IRepository getRepository(CollectTaskIO collectTaskIO)
		{
			IRepository repo = null;
			
			if (collectTaskIO is CollectTaskIOFile)
			{
				repo = new FileRepository((collectTaskIO as CollectTaskIOFile).Filename);
			}

			return repo;
		}



	}
}
