using Gripper.WebClient;
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
            var element = await _commonWebClient.MainContext.FindElementByCssSelectorAsync(invalidSelector);
        }

        [Test]
        public async Task IsNullIfElementDoesNotExist()
        {
            var invalidSelector = Fakers.GetInvalidCssSelector();
            var element = await _commonWebClient.MainContext.FindElementByCssSelectorAsync(invalidSelector);
            Assert.IsNull(element);
        }

        [Test]
        public async Task IsNotNullIfElementExists()
        {
            var gripper = GetService<IWebClient>();
            var mainContext = gripper.MainContext;
            var element = await mainContext.FindElementByCssSelectorAsync("#wrapper");
            Assert.IsNotNull(element);
        }
    }
}
