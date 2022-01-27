namespace Gripper.WebClient
{
    /// <summary>
    /// Configures response to the <see href="https://bugs.chromium.org/p/chromium/issues/detail?id=924937#c13"> Chromium bug 924937 </see> 
    /// which affects how targets (iFrames) are attached.
    /// </summary>
    public enum TargetAttachmentMode
    {
        /// <summary>
        /// Let the implementation decide.
        /// </summary>
        Default,

        /// <summary>
        /// The <see cref="IBrowserManager"/> will set --disable-features flags on the features that cause this bug when launching the browser.
        /// This will probably disable the IsolateOrigins and the site-per-process features.
        /// The runtime manager will then set up automatic target binding at the startup.
        /// </summary>
        Auto,

        /// <summary>
        /// Launch browser without disabling any features.
        /// Try to actively discover and bind targets.
        /// </summary>
        SeekAndAttach
    }
}
