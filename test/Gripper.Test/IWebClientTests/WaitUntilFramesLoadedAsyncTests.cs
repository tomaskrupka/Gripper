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
    public class WaitUntilFramesLoadedAsyncTests : UnitTestBase
    {
        [Test]
        public async Task ChildElementsAreLoaded()
        {
            var gripper = CreateGripper();
            CancellationTokenSource cts = new(TimeSpan.FromSeconds(10));
            await gripper.ReloadAsync(PollSettings.FrameDetectionDefault, cts.Token);

            var childContexts = await gripper.GetContextsAsync();
            var firstChildContext = childContexts.First(x => x.Frame.Name == Facts.MainTestSite.ChildContexts[0].FrameName);

            Assert.Multiple(async () =>
            {
                foreach (var validChildSelector in Facts.MainTestSiteFirstChildValidSelectors)
                {
                    var element = await firstChildContext.FindElementByCssSelectorAsync(validChildSelector);
                    Assert.IsNotNull(element);
                }
            });
        }
    }
}
