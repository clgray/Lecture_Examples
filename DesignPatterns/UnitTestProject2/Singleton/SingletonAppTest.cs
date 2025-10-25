using System;
using System.Diagnostics;
using System.Reflection;
using AutoFixture;
using Implementation.Singleton;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject2
{
	[TestClass]
	public class SingletonAppTest
	{
		private SingletonApp _target;

		[TestInitialize]
		public void Init()
		{
			_mock = new Mock<IConfigService>();
			_target = new SingletonApp(_mock.Object);
		}

		[TestMethod]
		public void RunTest()
		{
			var f = new Fixture();
			_mock.Setup(x => x.GetValue("count")).Returns(f.Create<int>().ToString);
			_mock.Setup(x => x.SetValue("count",It.IsAny<string>()));
			var sw = new Stopwatch();
			sw.Start();
			var result = _target.Run();
			sw.Stop();
			Console.WriteLine(sw.Elapsed);
			Assert.AreEqual("10000000", result);
			_mock.Verify(x => x.GetValue("count"), Times.Exactly(102));
		}

		[TestMethod]
		public void RunTest1()
		{
			var result = _target.Run();
			Assert.AreEqual("10000000", result);
		}

		Mock<IConfigService> _mock;

	}
}
