using System;

namespace Gripper.WebClient
{
    /// <summary>
    /// Provides options for pre-startup cleanup of the user profile directory.
    /// </summary>
    [Flags]
    public enum BrowserCleanupSettings
    {
        /// <summary>
        /// Do not perform any cleanup.
        /// </summary>
        None = 0,

        /// <summary>
        /// Clean browser cache.
        /// </summary>
        Cache = 1,

        /// <summary>
        /// Clean browser cookies.
        /// </summary>
        Cookies = 2,

        /// <summary>
        /// Attempt to clean active logins.
        /// </summary>
        Logins = 4,

        /// <summary>
        /// Purge whole profile directory.
        /// </summary>
        Profile = 8,
        /* Favicons,
         * History,*/
        //Sessions = 32, 
        //Storage = 64
    }
}
