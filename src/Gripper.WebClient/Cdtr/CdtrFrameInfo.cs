using BaristaLabs.ChromeDevTools.Runtime.Page;

namespace Gripper.WebClient.Cdtr
{
    internal class CdtrFrameInfo : IFrameInfo
    {
        internal CdtrFrameInfo(Frame frame)
        {
            FrameId = frame.Id;
            Name = frame.Name;
            Url = frame.Url;
        }
        public string FrameId { get; private set; }
        public string Name { get; private set; }
        public string Url { get; private set; }
    }
}
