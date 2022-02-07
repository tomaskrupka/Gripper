using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.Test.IContextTests
{
    public class ExecuteScriptTests : UnitTestBase
    {
        [Test]
        public async Task DeletedElementShouldDisappear()
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            await _webClient.MainContext.ExecuteScriptAsync("document.querySelector('#wrapper').remove()", cts.Token);
            var contentWrapper = await _webClient.MainContext.FindElementByCssSelectorAsync("#wrapper");

            Assert.IsNull(contentWrapper);
        }
    }
}
