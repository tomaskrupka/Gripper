using System.Collections.Generic;
using System.Linq;
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
            _contexts = await _commonWebClient.GetContextsAsync();
        }

        [Test]
        public void ContextsAreNotNull()
        {
            Assert.IsNotNull(_contexts);
        }

        [Test]
        public void ContextsAreNotEmpty()
        {
            CollectionAssert.IsNotEmpty(_contexts);
        }

        [Test]
        public void ContainsContextWithMainContextUrlSubstring()
        {
            var containsContextWithMainContextUrlSubstring = _contexts.Any(x => x.FrameInfo.Url.Contains(Facts.GovUkTestSite.MainContext.UrlSubstring));
            Assert.IsTrue(containsContextWithMainContextUrlSubstring);
        }

        [Test]
        public void ContainsContextWithEmbeddedContextUrlSubstring()
        {
            var containsContextWithEmbeddedContextUrlSubstring = true;

            foreach (var embeddedContext in Facts.GovUkTestSite.ChildContexts)
            {
                containsContextWithEmbeddedContextUrlSubstring &= _contexts.Any(x => x.FrameInfo.Url.Contains(embeddedContext.UrlSubstring));
            }

            Assert.IsTrue(containsContextWithEmbeddedContextUrlSubstring);
        }

        [Test]
        public void ContainsContextWithEmbeddedContextName()
        {
            var containsContextWithEmbeddedContextName = true;

            foreach (var embeddedContext in Facts.GovUkTestSite.ChildContexts)
            {
                containsContextWithEmbeddedContextName &= _contexts.Any(x => x.FrameInfo.Name == embeddedContext.FrameId);
            }

            Assert.IsTrue(containsContextWithEmbeddedContextName);
        }
    }
}
