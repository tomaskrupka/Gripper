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
        [NonParallelizable]
        public async Task DeletedElementShouldReappearOnReload()
        {
            var gripper = GetRequiredService<IWebClient>();

            await gripper.MainContext.FindElementByCssSelectorAsync("#wrapper");
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await gripper.MainContext.ExecuteScriptAsync("document.querySelector('#wrapper').remove()", cts.Token);
            await gripper.MainContext.FindElementByCssSelectorAsync("#wrapper");

            await gripper.ReloadAsync(PollSettings.ElementDetectionDefault, cts.Token);

            var contentWrapper = await gripper.MainContext.FindElementByCssSelectorAsync("#wrapper");

            Assert.IsNotNull(contentWrapper);
        }
    }
}
