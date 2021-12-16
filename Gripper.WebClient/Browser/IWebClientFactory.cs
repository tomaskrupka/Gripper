using System.Threading.Tasks;

namespace Gripper.WebClient.Browser
{
    public interface IWebClientFactory
    {
        public Task<IWebClient> LaunchAsync(WebClientSettings settings);
    }
}
