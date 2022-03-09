using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sinkbox
{
	public static class Threading
	{
		public static T Await<T>(this Task<T> t) => t.GetAwaiter().GetResult();
		
		public static async Task<TOut[]> MultiThread<TIn, TOut>(TIn[] items, Func<TIn, TOut> processFunc, int threads)
		{
			var threadBatches = items.Distribute(threads);

			var threadTasks = threadBatches.Select(b => Task.Run(() => b.Select(processFunc).ToArray()));

			return (await Task.WhenAll(threadTasks)).Interleave();
		}
	}
}