using Gripper.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gripper.Test.Models
{
    public class TestSite
    {
        public string Path { get; set; }
        public DomContext MainContext { get; set; }
        public List<DomContext> ChildContexts { get; set; }
        public List<string> ValidSelectors { get; set; }

        public TestSite()
        {
            ChildContexts = new List<DomContext>();
            ValidSelectors = new List<string>();
        }
    }
}
