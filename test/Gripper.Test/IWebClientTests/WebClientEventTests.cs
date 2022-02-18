using System;
using System.Threading;
using System.Threading.Tasks;
using Gripper.ChromeDevTools;
using Gripper.ChromeDevTools.Network;
using NUnit.Framework;

namespace Gripper.Test.IWebClientTests
{
    public class WebClientEventTests : UnitTestBase
    {
        private readonly ManualResetEventSlim _webClientEventRaised = new(false);
        private readonly string _url = Fakers.GetUrl();

        private void HandleWebClientEvent(object sender, IEvent e)
        {
            var isCorrectEvent =
                e is RequestWillBeSentEvent reqEvent &&
                reqEvent.Request.Url.Contains(_url);

            if (isCorrectEvent)
            {
                _webClientEventRaised.Set();
            }
        }

        [Test]
        public async Task RequestWillBeSentEventIsRaised()
        {
            _commonWebClient.WebClientEvent += HandleWebClientEvent;

            var fetchCommand = string.Format(
                "fetch('{0}', {1})", 
                _url, 
                "{ mode: 'no-cors' }");

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await _commonWebClient.MainContext.ExecuteScriptAsync(fetchCommand, cts.Token);

            _webClientEventRaised.Wait(cts.Token);

            Assert.IsTrue(_webClientEventRaised.IsSet);
            await Task.Delay(12345);
        }


    }
}
