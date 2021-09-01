using System;
using System.Diagnostics;

namespace Sinkbox
{
	public static class Timing
	{
		public static long MeasureTime(Action @delegate)
		{
			var sw = Stopwatch.StartNew();
			@delegate();
			sw.Stop();
			return sw.ElapsedTicks;
		}


		public static (long, TReturn) MeasureTime<TReturn>(Func<TReturn> @delegate)
		{
			var sw = Stopwatch.StartNew();
			var result = @delegate();
			sw.Stop();
			return (sw.ElapsedTicks, result);
		}
	}
}