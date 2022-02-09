using NUnit.Framework;
using System.Threading.Tasks;

namespace Gripper.Test.IWebClientTests
{
    public class GetCurrentUrlAsyncTests : UnitTestBase
    {
        [Test]
        public async Task UrlIsNotNullAsync()
        {
            var url = await _commonWebClient.GetCurrentUrlAsync();

            Assert.IsNotNull(url);
        }

        [Test]
        public async Task UrlIsNotEmptyAsync()
        {
            var url = await _commonWebClient.GetCurrentUrlAsync();

            Assert.IsNotEmpty(url);
        }

        [Test]
        public async Task UrlIsCorrectAsync()
        {
            var url = await _commonWebClient.GetCurrentUrlAsync();
            var escapedUrl = System.Uri.EscapeDataString(url);
            var expectedSubstring = Facts.GovUkTestSite.MainContext.UrlSubstring;

            StringAssert.Contains(expectedSubstring, escapedUrl);
        }
    }
}