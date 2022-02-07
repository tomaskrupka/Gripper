using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gripper.WebClient;
using NUnit.Framework;

namespace Gripper.Test.IWebClientTests
{
    internal class ReloadAsyncTests : UnitTestBase
    {
        [Test]
        public async Task DeletedElementShouldReappearOnReload()
        {
            var contentWrapper = await _webClient.MainContext.FindElementByCssSelectorAsync("#wrapper");

            Assert.IsNotNull(contentWrapper);

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await _webClient.MainContext.ExecuteScriptAsync("document.querySelector('#wrapper').remove()", cts.Token);
            contentWrapper = await _webClient.MainContext.FindElementByCssSelectorAsync("#wrapper");

            Assert.IsNull(contentWrapper);

            await _webClient.ReloadAsync(PollSettings.ElementDetectionDefault, cts.Token);
            contentWrapper = await _webClient.MainContext.FindElementByCssSelectorAsync("#wrapper");

            Assert.IsNotNull(contentWrapper);
        }
    }
}
