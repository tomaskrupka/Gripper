using BaristaLabs.ChromeDevTools.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapingServices.Authenticated.Browser.BaristaLabsCdtr
{
    public abstract class ElementBase : IElement
    {
        protected ChromeSession ChromeSession;
        public ElementBase(ChromeSession chromeSession)
        {
            this.ChromeSession = chromeSession;
        }
        public abstract Task ClickAsync();

        public abstract Task SendKeysAsync(string keys);

        public abstract Task SendKeysAsync(string keys, TimeSpan delayAfterStroke);

        public async Task SendSpecialKeyAsync(SpecialKey key)
        {

            var commands = key.ToDispatchKeyEventCommands();
            foreach (var command in commands)
            {
                await ChromeSession.Input.DispatchKeyEvent(command);
            }
        }
    }
}
