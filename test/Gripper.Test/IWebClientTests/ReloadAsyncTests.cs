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
        //[Test]
        //[NonParallelizable]
        //public async Task DeletedElementShouldReappearOnReload()
        //{
        //    await _webClient.MainContext.FindElementByCssSelectorAsync("#wrapper");
        //    var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

        //    await _webClient.MainContext.ExecuteScriptAsync("document.querySelector('#wrapper').remove()", cts.Token);
        //    await _webClient.MainContext.FindElementByCssSelectorAsync("#wrapper");

        //    await _webClient.ReloadAsync(PollSettings.ElementDetectionDefault, cts.Token);

        //    var contentWrapper = await _webClient.MainContext.FindElementByCssSelectorAsync("#wrapper");

        //    Assert.IsNotNull(contentWrapper);
        //}
    }
}
