using BaristaLabs.ChromeDevTools.Runtime.Page;

namespace Gripper.WebClient.Cdtr
{
    internal class CdtrFrameInfo : IFrameInfo
    {
        internal CdtrFrameInfo(Frame frame)
        {
            BackendFrameId = frame.Id;

            // Intentionally assigning name to id. Cdtr makes no difference between these, assigns both to Name.
            Id = frame.Name;
            Name = frame.Name;
            Url = frame.Url;

        }
        /// <inheritdoc/>
        public string BackendFrameId { get; private set; }

        /// If the iFrame has the 'name' attribute, contains its value. If not and the 'id' is present, contains its value. If neither is present, contains null.
        public string? Id { get; private set; }

        /// If the iFrame has the 'name' attribute, contains its value. If not and the 'id' is present, contains its value. If neither is present, contains null.
        public string? Name { get; private set; }

        /// <inheritdoc/>
        public string Url { get; private set; }

    }
}
