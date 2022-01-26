using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gripper.WebClient.Events
{
    public class Page_FrameLoadedEventArgs : RdpEventArgs
    {
        public Page_FrameLoadedEventArgs(object rawEventArgs) : base("Page", "frameLoaded")
        {

        }
    }
}
