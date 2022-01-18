using NUnit.Framework;
using System.Threading.Tasks;

namespace Gripper.Test.IWebClientTests
{
    public class GetCurrentUrlAsyncTests : UnitTestBase
    {
        [Test]
        public async Task UrlIsNotNullAsync()
        {
            var url = await _webClient.GetCurrentUrlAsync();

            Assert.IsNotNull(url);
        }

        [Test]
        public async Task UrlIsNotEmptyAsync()
        {
            var url = await _webClient.GetCurrentUrlAsync();

            Assert.IsNotEmpty(url);
        }

        [Test]
        public async Task UrlIsCorrect()
        {
            var url = await _webClient.GetCurrentUrlAsync();
            var escapedUrl = System.Uri.EscapeDataString(url);
            var expectedSubstring = "Welcome_to_GOV.UK.htm";

            StringAssert.Contains(expectedSubstring, escapedUrl);
        }
    }
}