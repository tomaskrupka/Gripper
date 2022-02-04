using Gripper.WebClient;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.Test.IWebClientTests
{
    public class ExecuteCdpCommandAsyncTests : UnitTestBase
    {

        [Test]
        public async Task NetworkSetCookieIsCommandIsSuccessful()
        {
            var cookieObject = Fakers.GetCookie();
            var commandResult = await _webClient.ExecuteCdpCommandAsync("Network.setCookie", JToken.FromObject(cookieObject));
            bool isCommandSuccessful = (bool)commandResult["success"];

            Assert.IsTrue(isCommandSuccessful);
        }
    }
}
