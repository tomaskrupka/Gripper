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
        public async Task ShouldNavigateBackAndForth()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await _commonWebClient.NavigateAsync(Facts.AltTestSites[0].Path, PollSettings.ElementDetectionDefault, cts.Token);

            var wikipediaUrl = await _commonWebClient.GetCurrentUrlAsync();
            var escapedWikipediaUrlSubstring = Uri.EscapeDataString(Facts.AltTestSites[0].MainContext.UrlSubstring);

            StringAssert.Contains(escapedWikipediaUrlSubstring, wikipediaUrl);

            await _commonWebClient.NavigateAsync(Facts.MainTestSite.Path, PollSettings.ElementDetectionDefault, cts.Token);

            var govUkUrl = await _commonWebClient.GetCurrentUrlAsync();
            var escapedGovUkUrlSubstring = Uri.EscapeDataString(Facts.MainTestSite.MainContext.UrlSubstring);

            StringAssert.Contains(escapedGovUkUrlSubstring, govUkUrl);
        }
    }
}
