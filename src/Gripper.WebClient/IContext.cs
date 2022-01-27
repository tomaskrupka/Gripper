using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.WebClient
{
    public interface IContext
    {
        public long Id { get; }
        public IFrameInfo FrameInfo { get; }
        public Task<JToken> ExecuteScriptAsync(string script, CancellationToken cancellationToken);
        public Task<IElement?> FindElementByCssSelectorAsync(string cssSelector, CancellationToken cancellationToken);
        public Task<IElement?> WaitUntilElementPresentAsync(string cssSelector, PollSettings pollSettings, CancellationToken cancellationToken);
    }

}
