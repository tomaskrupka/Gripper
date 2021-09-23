using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebScrapingServices.Authenticated.Browser
{
    public interface IBrowserWindow : IDisposable
    {
        public string Url { get; }
        public Task GoToUrlAsync(string address);
        public Task ReloadAsync();
        public Task EnterFullScreenAsync();
        public Task<string> ExecuteScriptAsync(string script);
        public Task<IElement?> FindElementByCssSelectorAsync(string cssSelector);
        public Task<IElement?> WaitUntilElementPresentAsync(string cssSelector, CancellationToken cancellationToken, PollSettings settings);
    }
}