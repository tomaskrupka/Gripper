namespace Gripper.WebClient
{
    /// <summary>
    /// Configures response to the Chromium bug 924937 which affects how targets (iFrames) are attached.
    /// See https://bugs.chromium.org/p/chromium/issues/detail?id=924937#c13
    /// </summary>
    public enum TargetAttachmentMode
    {
        Default,

        // set --disable-features flags on the features that cause this bug
        Auto,

        // try to actively discover and attach to targets (iFrames)
        SeekAndAttach
    }
}
