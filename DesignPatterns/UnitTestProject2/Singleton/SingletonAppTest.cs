using System.Reflection;
using Implementation.Singleton;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject2
{
	[TestClass]
	public class SingletonAppTest
	{
		private SingletonApp _target;

		[TestInitialize]
		public void Init()
		{
			_target = new SingletonApp(new ConfigServiceStub());
		}

		[TestMethod]
		public void RunTest()
		{
			var result = _target.Run();
			Assert.AreEqual("10000000", result);
		}
		
	}
}
