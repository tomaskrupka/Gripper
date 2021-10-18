using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gripper.Anonymous.UserAgent
{
    public class UserAgentManager : IUserAgentManager
    {
        #region Hardcoded constants
        private readonly string[] _browsers = new[] { "chrome", "firefox", "edge", "opera" };
        private readonly string _agentsApiEndpoint = "https://www.whatismybrowser.com/guides/the-latest-user-agent/BROWSER_PLACEHOLDER";
        #endregion

        private ILogger _logger;
        private List<string> _userAgents;
        private readonly string _userAgentsFileName;
        private readonly string _probeUserAgent;

        private async Task<List<string>> SaveNewAndGetAllUserAgents(List<string> newUserAgents)
        {
            try
            {
                var fileUserAgents = await File.ReadAllLinesAsync(_userAgentsFileName);
                var distinctUserAgents = fileUserAgents.Concat(newUserAgents).Distinct();
                await File.AppendAllLinesAsync(_userAgentsFileName, distinctUserAgents);
                return distinctUserAgents.ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.ToString());
                return new List<string>();
            }
        }
        private async Task<List<string>> DownloadLatestUserAgentsAsync()
        {
            List<string> userAgents = new();

            using var client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Teh forest poussin O>");

            foreach (var browser in _browsers)
            {
                try
                {
                    var agentsApiEndpoint = _agentsApiEndpoint.Replace("BROWSER_PLACEHOLDER", browser);
                    var rawAgents = await client.GetStringAsync(agentsApiEndpoint);
                    var match = Regex.Match(rawAgents, "Mozilla/(.+)Windows(.+)(?=</span>)");
                    while (match.Success)
                    {
                        userAgents.Add(match.Value);
                        match = match.NextMatch();
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.ToString());
                }
            }

            if (userAgents.Any())
            {
                return userAgents;
            }
            else
            {
                _logger.LogWarning("No user agents downloaded. Fallback to hardcoded list.");

                return new List<string>
                {
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36 OPR/77.0.4054.90",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36"
                };
            }
        }

        public UserAgentManager(ILogger<UserAgentManager> logger)
        {
            _logger = logger;
            _userAgents = new List<string>();
            _userAgentsFileName = "UserAgents.txt";
        }

        public async Task<IReadOnlyCollection<string>> GetUserAgentsAsync()
        {
            bool refreshNeeded;

            if (!File.Exists(_userAgentsFileName))
            {
                File.Create(_userAgentsFileName);
                refreshNeeded = true;
            }
            else
            {
                var lastFileWrite = File.GetLastWriteTime(_userAgentsFileName);
                var lastFileWriteTime = DateTime.Now - lastFileWrite;

                _logger.LogInformation("Last file write was {lastFileWriteTime} ago.", lastFileWriteTime);
                refreshNeeded = lastFileWriteTime > TimeSpan.FromDays(1);
            }

            if (refreshNeeded)
            {
                var latestUserAgents = await DownloadLatestUserAgentsAsync();
                _userAgents = latestUserAgents;

                return await SaveNewAndGetAllUserAgents(latestUserAgents);
            }
            else
            {
                return _userAgents;
            }
            // TODO: isolate hardcoded constants.
        }
    }
}
