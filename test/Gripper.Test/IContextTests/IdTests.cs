using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gripper.Test.IContextTests
{
    internal class IdTests : UnitTestBase
    {
        [Test]
        public void MainContextIdIsDefined()
        {
            Assert.IsTrue(_webClient.MainContext.Id > 0);
        }

        [Test]
        public async Task AllContextsIdsAreDefined()
        {
            var contexts = await _webClient.GetContextsAsync();

            Assert.Multiple(() =>
            {
                foreach (var context in contexts)
                {
                    Assert.IsTrue(context.Id > 0);
                }
            });
        }
    }
}
