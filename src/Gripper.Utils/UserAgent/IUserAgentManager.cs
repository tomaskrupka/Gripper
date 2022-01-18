using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gripper.Utils.UserAgent
{
    public interface IUserAgentManager
    {
        public Task<IReadOnlyCollection<string>> GetUserAgentsAsync();
    }
}
