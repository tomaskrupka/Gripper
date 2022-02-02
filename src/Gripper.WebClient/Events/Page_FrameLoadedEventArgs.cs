namespace Gripper.WebClient.Events
{
    public class Page_FrameStoppedLoadingEventArgs : RdpEventArgs
    {
        public string FrameId { get; set; }
        public Page_FrameStoppedLoadingEventArgs(string frameId) : base("Page", "frameLoaded")
        {
            FrameId = frameId;
        }
    }
}
