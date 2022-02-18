using System;
using Gripper.WebClient;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.Test.IWebClientTests
{
    public class MainContextTests : UnitTestBase
    {
        private IContext _mainContext;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            _mainContext = _commonWebClient.MainContext;
        }

        [Test]
        public void MainContextIsNotNull()
        {
            Assert.IsNotNull(_mainContext);
        }

        [Test]
        public void MainContextContainsExpectedUrlSubstring()
        {
            var expected = Facts.MainTestSite.MainContext.UrlSubstring;
            var actual = _mainContext.Frame.Url;

            StringAssert.Contains(expected, actual);
        }
    }
}
