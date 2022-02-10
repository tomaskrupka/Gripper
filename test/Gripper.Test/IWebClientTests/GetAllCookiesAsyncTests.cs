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
                var setCookieResponse = _commonWebClient.ExecuteCdpCommandAsync("Network.setCookie", JToken.FromObject(cookieObject)).Result;
            }
        }

        [Test]
        public async Task CapturesAllCookies()
        {
            var cookies = await _commonWebClient.GetAllCookiesAsync();

            Assert.Multiple(() =>
            {
                foreach (var injectedCookie in _injectedCookies)
                {
                    CollectionAssert.IsNotEmpty(cookies);
                    Assert.IsTrue(cookies.Any(x => x.Name == injectedCookie.name && x.Value == injectedCookie.value));
                };
            });
        }

    }
}