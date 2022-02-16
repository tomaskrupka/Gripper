using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gripper.WebClient;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace Gripper.Test.IWebClientTests
{
    public class ReloadAsyncTests : UnitTestBase
    {
        [Test]
        public async Task DeletedElementReappearsOnReload(
            [ValueSource(typeof(Facts), nameof(Facts.MainTestSiteValidSelectors))]
            string validSelector)
        {
            var gripper = CreateGripper();

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await gripper.MainContext.ExecuteScriptAsync($"document.querySelector('{validSelector}').remove()", cts.Token);
            await gripper.ReloadAsync(PollSettings.FrameDetectionDefault, cts.Token);

            var contentWrapper = await gripper.MainContext.FindElementByCssSelectorAsync(validSelector);

            Assert.IsNotNull(contentWrapper);
        }

        [Test]
        public void DoesNotBlockOnPassthroughPollSettings()
        {
            var gripper = CreateGripper();

            Assert.DoesNotThrowAsync(async () =>
            {
                Stopwatch s = Stopwatch.StartNew();
                var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(100)); // cca 5 ms in dev env.
                await gripper.ReloadAsync(PollSettings.PassThrough, cts.Token);
                _logger.LogDebug(
                    "{name} reloaded with passthrough poll settings after {elapsed}.",
                    nameof(DoesNotBlockOnPassthroughPollSettings),
                    s.Elapsed);
            });
        }

        [Test]
        public void AbortsWhenCancelled()
        {
            var gripper = CreateGripper();

            Assert.ThrowsAsync<TaskCanceledException>(async () =>
            {
                var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(1));
                await gripper.ReloadAsync((5, 500), cts.Token);
            });

        }
    }
}
