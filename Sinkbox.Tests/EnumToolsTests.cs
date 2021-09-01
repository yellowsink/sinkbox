using NUnit.Framework;
using static Sinkbox.EnumTools;

namespace Sinkbox.Tests
{
	[TestFixture]
	public class EnumToolsTests
	{
		[Test]
		public void AttributeTest()
		{
			Assert.AreEqual("this is a test string",          TestEnum.test1.EnumStr());
			Assert.AreEqual("test2",                          TestEnum.test2.EnumStr());
			Assert.AreEqual("this is another string",         TestEnum.test3.EnumStr());
			Assert.AreEqual("test test test brbrbrbrbrbrbrb", TestEnum.test4.EnumStr());
		}
		
		
		public enum TestEnum
		{
			[EnumStr("this is a test string")]
			test1,
			// this one doesnt have one
			test2,
			[EnumStr("this is another string")]
			test3,
			[EnumStr("test test test brbrbrbrbrbrbrb")]
			test4
		}
	}
}