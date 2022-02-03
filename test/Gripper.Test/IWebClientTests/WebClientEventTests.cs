using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Gripper.Test.IWebClientTests
{
    public class WebClientEventTests : UnitTestBase
    {
        ManualResetEventSlim _webClientEventRaised;

        [Test]
        public async Task RequestWillBeSentEventIsRaised()
        {
            _webClientEventRaised = new ManualResetEventSlim(false);
            _webClient.WebClientEvent += HandleWebClientEvent;
            
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
            await _webClient.MainContext.ExecuteScriptAsync("fetch('https://www.example.com', { mode: 'no-cors'})", cts.Token);
            _webClientEventRaised.Wait(1000);

        }

        private void HandleWebClientEvent(object sender, WebClient.RdpEventArgs e)
        {
            var isCorrectEvent =
                e is WebClient.Events.Network_RequestWillBeSentEventArgs reqEvent &&
                reqEvent.DomainName.Contains("google.com");

            if (isCorrectEvent)
            {
                _webClientEventRaised.Set();
            }
        }
    }
}
