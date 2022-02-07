using Gripper.WebClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.Test.IWebClientTests
{
    public class NavigateAsyncTests : UnitTestBase 
    {
        [Test]
        [NonParallelizable]
        public async Task ShouldNavigate()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await _webClient.NavigateAsync(Facts.WikipediaTestSite.Path, PollSettings.ElementDetectionDefault, cts.Token);

            var wikipediaUrl = await _webClient.GetCurrentUrlAsync();
            var escapedWikipediaUrlSubstring = Uri.EscapeDataString(Facts.WikipediaTestSite.MainContext.UrlSubstring);

            StringAssert.Contains(escapedWikipediaUrlSubstring, wikipediaUrl);

            await _webClient.NavigateAsync(Facts.GovUkTestSite.Path, PollSettings.ElementDetectionDefault, cts.Token);

            var govUkUrl = await _webClient.GetCurrentUrlAsync();
            var escapedGovUkUrlSubstring = Uri.EscapeDataString(Facts.GovUkTestSite.MainContext.UrlSubstring);

            StringAssert.Contains(escapedGovUkUrlSubstring, govUkUrl);
        }
    }
}
