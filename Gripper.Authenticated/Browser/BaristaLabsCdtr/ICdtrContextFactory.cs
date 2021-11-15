using BaristaLabs.ChromeDevTools.Runtime;
using BaristaLabs.ChromeDevTools.Runtime.Page;
using BaristaLabs.ChromeDevTools.Runtime.Runtime;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.Authenticated.Browser.BaristaLabsCdtr
{
    public interface ICdtrContextFactory
    {
        public Task<IContext> CreateContextAsync(ExecutionContextDescription executionContextDescription, Frame frame);
    }
}
