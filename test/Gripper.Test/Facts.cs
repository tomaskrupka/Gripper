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
        internal static readonly TestSite GovUkTestSite = new()
        {
            Path = Path.GetFullPath("../../../Pages/gov_uk/Welcome_to_GOV.UK.htm"),
            MainContext = new() { FrameId = "", UrlSubstring = "Welcome_to_GOV.UK.htm" },
            ChildContexts = new() { new DomContext { FrameId = "inlineFrameExample", UrlSubstring = "https://www.openstreetmap.org/export/embed.html" } },
        };
        internal static readonly TestSite WikipediaTestSite = new()
        {
            Path = Path.GetFullPath("../../../Pages/wikipedia_race_condition/Race condition - Wikipedia.htm"),
            MainContext = new() { FrameId = "", UrlSubstring = "Race condition - Wikipedia.htm" },
            ChildContexts = new()
        };
    }
    internal class TestSite
    {
        internal string Path;
        internal DomContext MainContext;
        internal List<DomContext> ChildContexts;
    }
}
