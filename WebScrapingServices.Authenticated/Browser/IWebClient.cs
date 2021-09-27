using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WebScrapingServices.Authenticated.Browser
{
    public interface IWebClient : IDisposable
    {
        public IRdpSession RdpClient { get; }
        public IBrowserWindow BrowserWindow { get; }
        public CookieContainer Cookies { get; }
        public event EventHandler WebClientEvent;


        public Task<string> ExecuteScriptAsync(string script);
        public Task<IElement?> FindElementByCssSelectorAsync(string cssSelector);
        public Task<IElement?> WaitUntilElementPresentAsync(string cssSelector, CancellationToken cancellationToken, PollSettings pollSettings);
    }
}
