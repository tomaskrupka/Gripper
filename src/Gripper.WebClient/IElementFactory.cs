namespace Gripper.WebClient
{
    /// <summary>
    /// Dependency inversion vehicle for <see cref="Element"/> implementations.
    /// </summary>
    internal interface IElementFactory
    {
        /// <summary>
        /// Create element that mirrors the backend node with given id.
        /// </summary>
        /// <param name="nodeId">The id of the backend node</param>
        /// <returns></returns>
        internal IElement CreateElement(long nodeId);
    }
}
