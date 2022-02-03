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
        [Test]
        public void MainContextIsNotNull()
        {
            var context = _webClient.MainContext;
            Assert.IsNotNull(context);
        }

    }
}
