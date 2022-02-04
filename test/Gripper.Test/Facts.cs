using Gripper.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gripper.Test
{
    internal static class Facts
    {
        internal static readonly Frame MainFrame = new() { Id = "", UrlSubstring = "Welcome_to_GOV.UK.htm" };
        internal static readonly List<Frame> ChildFrames = new() { new Frame { Id = "inlineFrameExample", UrlSubstring = "https://www.openstreetmap.org/export/embed.html" } };
    }
}
