using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sinkbox
{
	public static class Threading
	{
		public static T Wait<T>(this Task<T> t) => t.GetAwaiter().GetResult();
		
		public static TOut[] BatchTask<TIn, TOut>(TIn[] items, Func<TIn, TOut> processFunc, int threads)
		{
			if (items.Length < threads) threads = items.Length;

			var threadBatches = new List<(int, TIn)>?[threads];
			for (var i = 0; i < items.Length; i++)
			{
				threadBatches[i % threads] ??= new List<(int, TIn)>();

				threadBatches[i % threads]!.Add((i, items[i]));
			}

			var threadTasks = new Task<(int, TOut)[]>[threads];
			for (var i = 0; i < threadBatches.Length; i++)
			{
				// c# scoping rules fun
				var i1 = i;
				threadTasks[i] = Task.Run(() => QueueProcess(threadBatches[i1]!, processFunc));
			}

			Task.WaitAll(threadTasks.Cast<Task>().ToArray());

			return threadTasks.SelectMany(thread => thread.Result)
							  .OrderBy(item => item.Item1)
							  .Select(item => item.Item2)
							  .ToArray();

			static (int, TOut)[] QueueProcess(IEnumerable<(int, TIn)> items, Func<TIn, TOut> func) 
				=> items.Select(item => (item.Item1, func(item.Item2))).ToArray();
		}

		public static TOut[] BatchTaskUnordered<TIn, TOut>(TIn[] items, Func<TIn, TOut> processFunc, int threads)
		{
			if (items.Length < threads) threads = items.Length;

			var threadBatches = new List<TIn>?[threads];
			for (var i = 0; i < items.Length; i++)
			{
				threadBatches[i % threads] ??= new List<TIn>();

				threadBatches[i % threads]!.Add(items[i]);
			}

			var threadTasks = new Task<TOut[]>[threads];
			for (var i = 0; i < threadBatches.Length; i++)
			{
				// c# scoping rules fun
				var i1 = i;
				threadTasks[i] = Task.Run(() => QueueProcess(threadBatches[i1]!, processFunc));
			}

			Task.WaitAll(threadTasks.Cast<Task>().ToArray());

			return threadTasks.SelectMany(thread => thread.Result).ToArray();

			static TOut[] QueueProcess(IEnumerable<TIn> items, Func<TIn, TOut> func)
				=> items.Select(item => func(item)).ToArray();
		}
	}
}