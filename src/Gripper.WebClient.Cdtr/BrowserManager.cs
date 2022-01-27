using BaristaLabs.ChromeDevTools.Runtime;
using Gripper.WebClient.ProcessManagement;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gripper.WebClient.Cdtr
{
    public class BrowserManager : IBrowserManager
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly WebClientSettings _settings;

        private readonly ILogger _logger;

        public string DebuggerUrl { get; private set; }

        public Process BrowserProcess { get; private set; }

        public BrowserManager(IOptions<WebClientSettings> settings, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _settings = settings.Value;

            _logger = _loggerFactory.CreateLogger<BrowserManager>();
        }

        public async Task LaunchAsync(CancellationToken cancellationToken)
        {
            var userDataDir = new DirectoryInfo(
                _settings.UserDataDir ??
                throw new ApplicationException(string.Format("{0} needs a non-null {1}", nameof(LaunchAsync), nameof(_settings.UserDataDir))));

            var startupArgsSb = new StringBuilder()
                .Append(" --remote-debugging-port=").Append(_settings.RemoteDebuggingPort)
                .Append(" --user-data-dir=").Append(userDataDir.FullName);

            switch (_settings.TargetAttachment)
            {
                case TargetAttachmentMode.Default:
                case TargetAttachmentMode.Auto:
                    startupArgsSb.Append(" --disable-features=IsolateOrigins,site-per-process");
                    break;

                case TargetAttachmentMode.SeekAndAttach:
                default:
                    throw new NotImplementedException();
            }

            if (_settings.UseProxy == true)
            {
                var proxy = _settings.Proxy?.Address ?? throw new ApplicationException("Null proxy when UseProxy flag was up.");

                _logger.LogDebug("{this} launching browser with proxy: {proxy}.", nameof(BrowserManager), proxy);

                startupArgsSb.Append(" --proxy-server=").Append(proxy);

                if (startupArgsSb[^1] == '/')
                {
                    startupArgsSb.Remove(startupArgsSb.Length - 1, 1);
                }
            }
            else
            {
                _logger.LogDebug("{this} launching browser without proxy.", nameof(BrowserManager));
            }

            if (_settings.BrowserStartupArgs?.Any() == true)
            {
                foreach (var arg in _settings.BrowserStartupArgs)
                {
                    startupArgsSb.Append(' ').Append(arg);
                }
            }

            var browserLocation =
                _settings.BrowserLocation ??
                throw new ApplicationException(string.Format("{0} needs a non-null {1}", nameof(LaunchAsync), nameof(_settings.BrowserLocation)));

            DoPreStartupCleanup(userDataDir, _settings.StartupCleanup);

            var startupArgs = startupArgsSb.ToString();

            _logger.LogDebug("{this} launching browser with args: {args}", nameof(BrowserManager), startupArgs);

            BrowserProcess = Process.Start(browserLocation, startupArgs);

            _logger.LogDebug("Browser process started: {processId}:{processName}", BrowserProcess.Id, BrowserProcess.ProcessName);

            // TODO: ADD FLAG TO CONFIG FOR THIS. MAKE IT A SERVICE.
            ChildProcessTracker.AddProcess(BrowserProcess);

            using var httpClient = new HttpClient();

            // TODO: ENABLE REMOTE CONNECTION, ADD CONFIG TOKEN FOR THIS
            var remoteSessions = await httpClient.GetAsync($"http://localhost:{_settings.RemoteDebuggingPort}/json", cancellationToken);

            _logger.LogDebug("sessions response: {status}", remoteSessions.StatusCode);

            var remoteSessionsContent = await remoteSessions.Content.ReadAsStringAsync();

            var sessionInfos = JsonConvert.DeserializeObject<List<ChromeSessionInfo>>(remoteSessionsContent);

            _logger.LogDebug("Remote session infos: {remoteSessions}", remoteSessionsContent);

            DebuggerUrl = sessionInfos.First(x => x.Type == "page").WebSocketDebuggerUrl ??
                throw new ApplicationException("No debugger WS endpoint found at launched session.");
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        //// TODO: replace this with a separated launcher that will return references to services on demand.
        //private async Task<(Process, ChromeSession, IContextFactory)> LaunchAsync(WebClientSettings settings)
        //{
        //    try
        //    {
        //        var userDataDir = GetUserDataDir(settings.UserDataDir);

        //        DoPreStartupCleanup(userDataDir, settings.StartupCleanup);

        //        var browserArgs = new StringBuilder()
        //            .Append(" --remote-debugging-port=").Append(settings.RemoteDebuggingPort)
        //            .Append(" --user-data-dir=").Append(userDataDir?.FullName ?? throw new NotImplementedException("TODO: extract this and handle null user data dir."));

        //        switch (settings.TargetAttachment)
        //        {
        //            case TargetAttachmentMode.Default:
        //            case TargetAttachmentMode.Auto:
        //                browserArgs.Append(" --disable-features=IsolateOrigins,site-per-process");
        //                break;

        //            case TargetAttachmentMode.SeekAndAttach:
        //                break;

        //            default:
        //                throw new NotImplementedException();
        //        }

        //        if (settings.UseProxy == true)
        //        {
        //            _logger.LogDebug("{this} launching chrome with proxy: {proxy}.", nameof(CdtrChromeClient), settings.Proxy?.Address?.ToString() ?? "null");
        //            browserArgs.Append(" --proxy-server=").Append(settings.Proxy?.Address ?? throw new ApplicationException("Null proxy when UseProxy flag was up."));

        //            if (browserArgs[^1] == '/')
        //            {
        //                browserArgs.Remove(browserArgs.Length - 1, 1);
        //            }
        //        }
        //        else
        //        {
        //            _logger.LogDebug("{this} launching chrome without proxy.", nameof(CdtrChromeClient));
        //        }

        //        if (settings.BrowserStartupArgs?.Any() == true)
        //        {
        //            foreach (var flag in settings.BrowserStartupArgs)
        //            {
        //                browserArgs.Append(' ').Append(flag);
        //            }
        //        }

        //        _logger.LogDebug("{this} launching chrome with args: {args}", nameof(CdtrChromeClient), browserArgs.ToString());

        //        var chromeProcess = Process.Start(settings.BrowserLocation, browserArgs.ToString());

        //        _logger.LogDebug("Browser process started: {processId}:{processName}", chromeProcess.Id, chromeProcess.ProcessName);

        //        // TODO: ADD FLAG TO CONFIG FOR THIS. MAKE IT A SERVICE.
        //        ChildProcessTracker.AddProcess(chromeProcess);

        //        using var httpClient = new HttpClient();

        //        // TODO: ENABLE REMOTE CONNECTION, ADD CONFIG TOKEN FOR THIS
        //        var remoteSessions = await httpClient.GetAsync($"http://localhost:{settings.RemoteDebuggingPort}/json");

        //        _logger.LogDebug("sessions response: {status}", remoteSessions.StatusCode);

        //        var remoteSessionsContent = await remoteSessions.Content.ReadAsStringAsync();

        //        var sessionInfos = JsonConvert.DeserializeObject<List<ChromeSessionInfo>>(remoteSessionsContent);

        //        _logger.LogDebug("Remote session infos: {remoteSessions}", remoteSessionsContent);

        //        var chromeSession = new ChromeSession(_loggerFactory.CreateLogger<ChromeSession>(), sessionInfos.First(x => x.Type == "page").WebSocketDebuggerUrl);

        //        _logger.LogDebug("ChromeSession launched and connected to Chrome RDP server");

        //        //DoPostStartupCleanupAsync(chromeSession, settings.StartupCleanup).Wait();

        //        var contextFactory = new CdtrContextFactory(_loggerFactory, _cdtrElementFactory, _jsBuilder, chromeSession);

        //        return (chromeProcess, chromeSession, contextFactory);
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError("{name} error: {e}", nameof(LaunchAsync), e);
        //        throw;
        //    }
        //}


        private void DoPreStartupCleanup(DirectoryInfo userDataDir, BrowserCleanupSettings? startupCleanupOptional)
        {
            try
            {
                var startupCleanup = (startupCleanupOptional ?? BrowserCleanupSettings.None);

                if (startupCleanup == BrowserCleanupSettings.None)
                {
                    _logger.LogDebug("{name} set to {value}. Exiting {this}.", nameof(BrowserCleanupSettings), BrowserCleanupSettings.None, nameof(DoPreStartupCleanup));
                    return;
                }

                if (!userDataDir.Exists)
                {
                    return;
                }

                var profileDirectory = userDataDir.GetDirectories().FirstOrDefault(x => x.Name.ToLower() == "default");

                if (profileDirectory == null)
                {
                    return;
                }

                if (startupCleanup.HasFlag(BrowserCleanupSettings.Profile))
                {
                    profileDirectory.Delete(true);
                    return;
                }

                if (startupCleanup.HasFlag(BrowserCleanupSettings.Cache))
                {
                    _logger.LogWarning("Clearing browser cache.");
                    profileDirectory.GetDirectories().FirstOrDefault(x => x.Name.ToLower() == "cache")?.Delete(true);
                    profileDirectory.GetDirectories().FirstOrDefault(x => x.Name.ToLower() == "storage")?.Delete(true);
                    profileDirectory.GetDirectories().FirstOrDefault(x => x.Name.ToLower() == "session storage")?.Delete(true);
                    profileDirectory.GetDirectories().FirstOrDefault(x => x.Name.ToLower() == "sessions")?.Delete(true);
                    profileDirectory.GetDirectories().FirstOrDefault(x => x.Name.ToLower() == "local storage")?.Delete(true);
                }

                if (startupCleanup.HasFlag(BrowserCleanupSettings.Cookies))
                {
                    _logger.LogWarning("Clearing browser cookies.");
                    profileDirectory.GetFiles().FirstOrDefault(x => x.Name.ToLower() == "cookies")?.Delete();
                    profileDirectory.GetFiles().FirstOrDefault(x => x.Name.ToLower() == "cookies-journal")?.Delete();
                }

                if (startupCleanup.HasFlag(BrowserCleanupSettings.Logins))
                {
                    _logger.LogWarning("Clearing browser logins.");
                    profileDirectory.GetFiles().FirstOrDefault(x => x.Name.ToLower() == "login data")?.Delete();
                    profileDirectory.GetFiles().FirstOrDefault(x => x.Name.ToLower() == "login data for account")?.Delete();
                    profileDirectory.GetFiles().FirstOrDefault(x => x.Name.ToLower() == "login data for account-journal")?.Delete();
                    profileDirectory.GetFiles().FirstOrDefault(x => x.Name.ToLower() == "login data-journal")?.Delete();
                }

            }
            catch (Exception e)
            {
                _logger.LogCritical("Failed to {name}: {e}.", nameof(DoPreStartupCleanup), e);
                throw;
            }
        }
    }
}
