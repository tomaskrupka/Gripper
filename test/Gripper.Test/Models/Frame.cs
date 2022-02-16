using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gripper.Test.Models
{
    public class DomContext
    {
        public string FrameId { get; set; }
        public string FrameName { get; set; }
        public string UrlSubstring { get; set; }
        public List<string> ValidSelectors { get; set; } = new List<string>();
    }
}
