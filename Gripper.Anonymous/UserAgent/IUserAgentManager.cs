using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gripper.Anonymous.UserAgent
{
    public interface IUserAgentManager
    {
        public Task<IReadOnlyCollection<string>> GetUserAgentsAsync();
    }
}
