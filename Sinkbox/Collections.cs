using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Sinkbox
{
	[SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
	[SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
	public static class Collections
	{
		public static T[][] Distribute<T>(this T[] arr, int amount)
		{
			var actualLength = Math.Min(amount, arr.Length);
			var lengths      = new int[actualLength];

			for (var i = 0; i < arr.Length; i++) lengths[i % actualLength]++;

			var res = new T[]?[actualLength];
			for (var i = 0; i < arr.Length; i++)
			{
				if (res[i % actualLength] != null) 
					res[i % actualLength]![i / actualLength] = arr[i];
				else
				{
					res[i % actualLength] = new T[lengths[i % actualLength]];

					res[i % actualLength]![0] = arr[i];
				}
			}

			return res!;
		}

		public static IEnumerable<IEnumerable<T>> Distribute<T>(this IEnumerable<T> seq, int amount)
			=> Distribute(seq.ToArray(), amount);

		public static T[] Interleave<T>(this T[][] arr)
		{
			var length = 0;
			for (var i = 0; i < arr.Length; i++)
				length += arr[i].Length;

			var interleaved = new T[length];
			for (var i = 0; i < interleaved.Length; i++) 
				interleaved[i] = arr[i % arr.Length][i / arr.Length];

			return interleaved;
		}

		public static IEnumerable<T> Interleave<T>(this IEnumerable<IEnumerable<T>> seq)
		{
			foreach (var enumerable in seq)
			foreach (var elem in enumerable)
				yield return elem;
		}

		public static IEnumerable<TOut> Choose<TIn, TOut>(this IEnumerable<TIn> seq, Func<TIn, TOut?> func)
		{
			foreach (var elem in seq)
			{
				var res = func(elem);
				if (res != null)
					yield return res;
			}
		}

		public static IEnumerable<TOut> Choose<TIn, TOut>(this IEnumerable<TIn> seq, Func<TIn, (TOut?, bool)> func)
			=> seq.Select(func).Where(p => p.Item2).Select(p => p.Item1)!;
	}
}