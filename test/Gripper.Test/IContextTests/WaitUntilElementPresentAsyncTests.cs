using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.Test.IContextTests
{
    public class WaitUntilElementPresentAsyncTests : UnitTestBase
    {
        [Test]
        public async Task DoesNotReturnBeforeTimeoutIfElementDoesNotExist()
        {
            var timeoutMs = 10_000;
            Stopwatch s = Stopwatch.StartNew();

            await _commonWebClient.MainContext.WaitUntilElementPresentAsync(
                Fakers.GetInvalidCssSelector(),
                (1000, timeoutMs),
                CancellationToken.None);

            Assert.That(() => s.ElapsedMilliseconds >= timeoutMs);
        }

        [Test]
        public async Task ReturnsNullIfElementDoesNotExist()
        {
            var element = await _commonWebClient.MainContext.WaitUntilElementPresentAsync(
                Fakers.GetInvalidCssSelector(),
                (10, 100),
                CancellationToken.None);

            Assert.IsNull(element);
        }

        [Test]
        public async Task ReturnsIfElementExists(
            [ValueSource(typeof(Facts), nameof(Facts.MainTestSiteValidSelectors))] string validSelector)
        {
            _logger.LogDebug("Testing {name} with valid selector {validSelector}", nameof(ReturnsIfElementExists), validSelector);
            var element = await _commonWebClient.MainContext.WaitUntilElementPresentAsync(
                validSelector,
                (10, 100),
                CancellationToken.None);

            Assert.IsNotNull(element);
        }
    }
}
