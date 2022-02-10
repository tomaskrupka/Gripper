namespace Gripper.WebClient
{
    /// <summary>
    /// Represents a special key as defined <see href="https://github.com/chromedp/chromedp/blob/fd310a9b849a/kb/keys.go">here.</see>
    /// </summary>
    public enum SpecialKey
    {
        /// <summary>
        /// Represents Enter key press. See <see href="https://github.com/chromedp/chromedp/blob/fd310a9b849a/kb/keys.go">here</see> for char codes.
        /// </summary>
        Enter,

        /// <summary>
        /// Represents Backspace key press. See <see href="https://github.com/chromedp/chromedp/blob/fd310a9b849a/kb/keys.go">here</see> for char codes.
        /// </summary>
        Backspace,

        /// <summary>
        /// Represents Tab key press. See <see href="https://github.com/chromedp/chromedp/blob/fd310a9b849a/kb/keys.go">here</see> for char codes.
        /// </summary>
        Tab,

        /// <summary>
        /// Represents Esc key press. See <see href="https://github.com/chromedp/chromedp/blob/fd310a9b849a/kb/keys.go">here</see> for char codes.
        /// </summary>
        Escape,

        /// <summary>
        /// Represents PgDn key press. See <see href="https://github.com/chromedp/chromedp/blob/fd310a9b849a/kb/keys.go">here</see> for char codes.
        /// </summary>
        PageDown,

        /// <summary>
        /// Represents End key press. See <see href="https://github.com/chromedp/chromedp/blob/fd310a9b849a/kb/keys.go">here</see> for char codes.
        /// </summary>
        End,

        /// <summary>
        /// Represents Home key press. See <see href="https://github.com/chromedp/chromedp/blob/fd310a9b849a/kb/keys.go">here</see> for char codes.
        /// </summary>
        Home
    }
}
