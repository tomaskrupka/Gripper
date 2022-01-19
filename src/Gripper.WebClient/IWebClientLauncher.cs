using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gripper.WebClient
{
    public interface IWebClientLauncher
    {
        public IWebClient LaunchWebClient(WebClientSettings webClientSettings);
    }
}
