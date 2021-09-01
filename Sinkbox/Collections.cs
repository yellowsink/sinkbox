using System;
using System.Collections.Generic;

namespace Sinkbox
{
	public static class Collections
	{
		private static Random _rand = new();
		
		public static T Random<T>(this IReadOnlyList<T> collection) => collection[_rand.Next(collection.Count)];
	}
}