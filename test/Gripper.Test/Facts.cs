using Gripper.Test.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gripper.Test
{
    internal static class Facts
    {
        public static IEnumerable<string> MainTestSiteValidSelectors => MainTestSite.ValidSelectors;

        internal static readonly TestSite MainTestSite = new TestSite
        {
            Path = Path.GetFullPath("../../../Pages/gov_uk/Welcome_to_GOV.UK.htm"),
            MainContext = new DomContext() { FrameId = "", UrlSubstring = "Welcome_to_GOV.UK.htm" },
            ChildContexts = new List<DomContext> { new DomContext { FrameId = "inlineFrameExample", UrlSubstring = "https://www.openstreetmap.org/export/embed.html" } },
            ValidSelectors = new List<string> { "#wrapper" }
        };

        internal static readonly TestSite[] AltTestSites = new[] 
        {
            new TestSite
            {
                Path = Path.GetFullPath("../../../Pages/wikipedia_race_condition/Race condition - Wikipedia.htm"),
                MainContext = new DomContext() { FrameId = "", UrlSubstring = "Race condition - Wikipedia.htm" },
            }
        };

    }
}
