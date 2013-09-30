using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Worker.Blocks
{
	class BlockController
	{


		public void Link<TOutput>(ISourceBlock<TOutput> Source, ITargetBlock<TOutput> Target)
		{
			Source.LinkTo(Target);
		}

		public void LinkWithCompletion<TOutput>(ISourceBlock<TOutput> Source, ITargetBlock<TOutput> Target)
		{
			Link(Source, Target);
			Source.Completion.ContinueWith(t => {
				if (t.IsFaulted)
					Target.Fault(t.Exception);
				else
					Target.Complete();
			});
		}


	}
}
