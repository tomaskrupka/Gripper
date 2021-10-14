using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebScrapingServices.Authenticated.Browser
{
    public interface IWebClientLifeCycleManager
    {
        public IWebClient WebClient { get; }
        public ManualResetEventSlim WebClientActive { get; }

        public void CanKillBrowser();
        public void NeedNewBrowser();
    }
    public class WebClientLifeCycleManagerSettings
    {
        public TimeSpan MinBrowserLifeSpan;
    }
}
