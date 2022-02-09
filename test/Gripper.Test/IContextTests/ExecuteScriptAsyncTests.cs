using Gripper.WebClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.Test.IContextTests
{
    public class ExecuteScriptAsyncTests : UnitTestBase
    {
        [Test]
        public async Task DeletedElementShouldDisappear()
        {
            var gripper = GetService<IWebClient>();
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            await gripper.MainContext.ExecuteScriptAsync("document.querySelector('#wrapper').remove()", cts.Token);
            var contentWrapper = await gripper.MainContext.FindElementByCssSelectorAsync("#wrapper");

            Assert.IsNull(contentWrapper);
        }
    }
}
