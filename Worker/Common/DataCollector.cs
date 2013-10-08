using Collector.Interface;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Worker.Blocks;
using Worker.Model;

namespace Worker.Common
{
	class DataCollector
	{
		[Inject]
		public TraceSource Trace { get; set; }

		BlockFactory blockFactory;
		BlockController blockController;

		public DataCollector(BlockFactory Factory, BlockController Controller)
		{
			//blockFactory = new BlockFactory();
			//blockController = new BlockController();
			blockFactory = Factory;
			blockController = Controller;
		}

		public async Task Collect(CollectTask collectTask)
		{
			var inputBlock = blockFactory.FileToStream();
			var idsBlock = blockFactory.StremToIds(collectTask);
			var bacthBlock = blockFactory.Batch<string>(collectTask);
			var processBlock = blockFactory.Process(collectTask);

			var formatBlock = blockFactory.ToCSVStream();
			var outputBlock = blockFactory.StreamToFile((collectTask.Output as CollectTaskIOFile).Filename);

			blockController.LinkWithCompletion(inputBlock, idsBlock);
			blockController.LinkWithCompletion(idsBlock, bacthBlock);
			blockController.LinkWithCompletion(bacthBlock, processBlock);
			blockController.LinkWithCompletion(processBlock, formatBlock);
			blockController.LinkWithCompletion(formatBlock, outputBlock);

			inputBlock.Post( (collectTask.Input as CollectTaskIOFile).Filename );
			inputBlock.Complete();

			await outputBlock.Completion;
		}

	}
}
