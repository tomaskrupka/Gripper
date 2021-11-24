using System;

namespace Gripper.WebClient.Browser
{
    [Flags]
    public enum BrowserCleanupSettings
    {
        None = 0,
        Cache = 1,
        Cookies = 2,
        Logins = 4,
        Profile = 8,
        /* Favicons,
         * History,*/
        //Sessions = 32, 
        //Storage = 64
    }
}
