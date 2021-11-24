using System.Threading;
using System.Threading.Tasks;

namespace Gripper.WebClient.Browser
{
    public interface IContext
    {
        public int Id { get; }
        public IFrameInfo FrameInfo { get; }
        public Task<string?> ExecuteScriptAsync(string script, CancellationToken cancellationToken);
        public Task<IElement?> FindElementByCssSelectorAsync(string cssSelector, CancellationToken cancellationToken);
        public Task<IElement?> WaitUntilElementPresentAsync(string cssSelector, PollSettings pollSettings, CancellationToken cancellationToken);
    }

}
