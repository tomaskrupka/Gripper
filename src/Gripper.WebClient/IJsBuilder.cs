namespace Gripper.WebClient
{
    /// <summary>
    /// Provides builders for repetitive JS expressions.
    /// </summary>
    public interface IJsBuilder
    {
        /// <summary>
        /// Creates a query similar to 'document.querySelector(...).click().
        /// </summary>
        /// <param name="selector">CSS selector to inject into the query.</param>
        /// <returns>Valid JS query.</returns>
        public string ClickFirstByCssSelector(string selector);

        /// <summary>
        /// Creates a query similar to 'document.querySelectorAll(...).
        /// </summary>
        /// <param name="selector">CSS selector to inject into the query.</param>
        /// <returns>Valid JS query.</returns>
        public string DocumentQuerySelectorAll(string selector);

        /// <summary>
        /// Creates a query similar to 'document.querySelector(...).
        /// </summary>
        /// <param name="selector">CSS selector to inject into the query.</param>
        /// <returns>Valid JS query.</returns>
        public string DocumentQuerySelector(string selector);
    }
}