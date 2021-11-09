using System.Threading;
using System.Threading.Tasks;

namespace Gripper.Authenticated.Browser
{
    public interface IContext
    {
        public Task<string?> ExecuteScriptAsync(string script, CancellationToken cancellationToken);
        public Task<IElement?> FindElementByCssSelectorAsync(string cssSelector, CancellationToken cancellationToken);
        public Task<IElement?> WaitUntilElementPresentAsync(string cssSelector, PollSettings pollSettings, CancellationToken cancellationToken);
    }

}
