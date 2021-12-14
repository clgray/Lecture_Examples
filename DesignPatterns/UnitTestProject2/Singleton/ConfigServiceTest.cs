using System.Reflection;
using Implementation.Singleton;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject2
{
	[TestClass]
	public class ConfigServiceTest
	{
		private ConfigService _target;

		[TestInitialize]
		public void Init()
		{
			_target = ConfigService.Instance();
		}

		[TestMethod]
		public void TestSave()
		{
			var key = "key";
			var value = "value";
			_target.SetValue(key, value);
			var actualValue = _target.GetValue(key);
			Assert.AreEqual(value, actualValue);
		}
		[TestMethod]
		public void GetDefaultValue()
		{
			var key = "key";
			var actualValue = _target.GetValue(key);
			Assert.AreEqual("0", actualValue);
		}
	}
}
