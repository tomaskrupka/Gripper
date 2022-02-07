using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gripper.Test.IContextTests
{
    public class FindElementByCssSelectorAsyncTests : UnitTestBase
    {
        [Test]
        public async Task DoesNotThrowExceptionIfInvalidSelector()
        {
            var invalidSelector = Fakers.GetInvalidCssSelector();
            var element = await _webClient.MainContext.FindElementByCssSelectorAsync(invalidSelector);
        }

        [Test]
        public async Task IsNullIfElementDoesNotExist()
        {
            var invalidSelector = Fakers.GetInvalidCssSelector();
            var element = await _webClient.MainContext.FindElementByCssSelectorAsync(invalidSelector);
            Assert.IsNull(element);
        }

        [Test]
        public async Task IsNotNullIfElementExists()
        {
            var mainContext = _webClient.MainContext;
            var element = await mainContext.FindElementByCssSelectorAsync("#wrapper");
            Assert.IsNotNull(element);
        }
    }
}
