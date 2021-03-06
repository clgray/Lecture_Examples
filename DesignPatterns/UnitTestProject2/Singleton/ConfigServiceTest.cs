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
			
		}

		[TestMethod]
		public void TestSave()
		{
			_target = ConfigService.Instance();
			var key = "key";
			var value = "value";
			_target.SetValue(key, value);
			var actualValue = _target.GetValue(key);
			Assert.AreEqual(value, actualValue);
		}
		[TestMethod]
		public void GetDefaultValue()
		{
			_target = ConfigService.Instance();
			var key = "key";
			var actualValue = _target.GetValue(key);
			Assert.AreEqual("0", actualValue);
		}
	}
}
