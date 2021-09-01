using System;
using NUnit.Framework;

namespace Sinkbox.Tests
{
	[TestFixture]
	public class MemoizationTests
	{
		[Test]
		public void IdenticalOutputsTest()
		{
			var rand = new Random();
			for (var i = 0; i < 10000; i++)
			{
				var inValue        = rand.Next(10000);
				var actualResult   = func(inValue);
				var memoized       = ((Func<int, double>) func).Memoize<double>();
				var memoizedFirst  = memoized(new object[] { inValue });
				var memoizedSecond = memoized(new object[] { inValue });
				
				Assert.AreEqual(actualResult, memoizedFirst);
				Assert.AreEqual(actualResult, memoizedSecond);
			}

			double func(int @in) => Math.Pow(@in, 5);
		}
	}
}