using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gripper.WebClient;
using NUnit.Framework;

namespace Gripper.Test.IWebClientTests
{
    public class GetContextsAsyncTests : UnitTestBase
    {
        private IReadOnlyCollection<IContext> _contexts;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
            _contexts = await _webClient.GetContextsAsync();
        }

        [Test]
        public async Task ContextsAreNotNull()
        {
            Assert.IsNotNull(_contexts);
        }

        [Test]
        public async Task ContextsAreNotEmpty()
        {
            CollectionAssert.IsNotEmpty(_contexts);
        }

        [Test]
        public async Task ContainsContextWithMainContextUrlSubstring()
        {
            var containsContextWithMainContextUrlSubstring = _contexts.Any(x => x.FrameInfo.Url.Contains(Facts.MainFrame.UrlSubstring));
            Assert.IsTrue(containsContextWithMainContextUrlSubstring);
        }

        [Test]
        public async Task ContainsContextWithEmbeddedContextUrlSubstring()
        {
            var containsContextWithEmbeddedContextUrlSubstring = true;

            foreach (var embeddedContext in Facts.ChildFrames)
            {
                containsContextWithEmbeddedContextUrlSubstring &= _contexts.Any(x => x.FrameInfo.Url.Contains(embeddedContext.UrlSubstring));
            }

            Assert.IsTrue(containsContextWithEmbeddedContextUrlSubstring);
        }

        [Test]
        public async Task ContainsContextWithEmbeddedContextName()
        {
            var containsContextWithEmbeddedContextName = true;

            foreach (var embeddedContext in Facts.ChildFrames)
            {
                containsContextWithEmbeddedContextName &= _contexts.Any(x => x.FrameInfo.Name == embeddedContext.Id);
            }

            Assert.IsTrue(containsContextWithEmbeddedContextName);
        }
    }
}
