using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gripper.Test.IWebClientTests
{
    public class GetAllCookiesAsyncTests : UnitTestBase
    {
        private readonly List<Models.Cookie> _injectedCookies = new();

        [OneTimeSetUp]
        public void Init()
        {
            for (int i = 0; i < 10; i++)
            {
                var cookieObject = Fakers.GetCookie();
                _injectedCookies.Add(cookieObject);
                _webClient.ExecuteRdpCommandAsync("Network.setCookie", JToken.FromObject(cookieObject));
            }
        }

        [Test]
        public async Task AllCookiesCaptured()
        {
            var cookieContainer = await _webClient.GetAllCookiesAsync();

            Assert.Multiple(() =>
            {
                foreach (var injectedCookie in _injectedCookies)
                {
                    var cookies = cookieContainer.GetCookies(new System.Uri(injectedCookie.Domain));
                    CollectionAssert.IsNotEmpty(cookies);
                    Assert.IsTrue(cookies.Any(x => x.Name == injectedCookie.Name && x.Value == injectedCookie.Value));
                };
            });
        }

    }
}