namespace Gripper.WebClient
{
    /// <summary>
    /// Provides access to information about an iFrame on the page,
    /// as defined by the browser backend.
    /// </summary>
    public interface IFrameInfo
    {
        /// <summary>
        /// Id of the Frame as defined by the browser backend.
        /// </summary>
        string BackendFrameId { get; }

        /// <summary>
        /// Value of the iFrame 'id' tag, or null.
        /// </summary>
        string? Id { get; }

        /// <summary>
        /// Value of the iFrame 'name' tag, or null.
        /// </summary>
        string? Name { get; }

        /// <summary>
        /// Frame document's URL without fragment.
        /// </summary>
        string Url { get; }
    }

}
