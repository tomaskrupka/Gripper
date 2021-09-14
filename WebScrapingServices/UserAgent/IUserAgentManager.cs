using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebScrapingServices.Anonymous.UserAgent
{
    public interface IUserAgentManager
    {
        public Task<IReadOnlyCollection<string>> GetUserAgentsAsync();
    }
}
