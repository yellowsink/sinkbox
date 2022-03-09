using System;
using System.Collections.Generic;

namespace Sinkbox
{
	public static class Memoization
	{
		private static readonly Dictionary<Delegate, Dictionary<object[], object?>> Cache = new();

		public static object? MemoizedCall(this Delegate func, params object[] parameters)
		{
			if (Cache.ContainsKey(func))
			{
				if (Cache[func].ContainsKey(parameters))
					return Cache[func][parameters];
			}
			else
				Cache[func] = new();

			var result = func.DynamicInvoke(parameters);
			Cache[func][parameters] = result;
			return result;
		}

		public static Action<object[]> Memoize(this Delegate func)
			=> args => MemoizedCall(func, args);
		
		public static Func<object[], T> Memoize<T>(this Delegate func)
			=> args => (T) MemoizedCall(func, args)!;
	}
}