﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gripper.WebClient
{
    public class JsBuilder : IJsBuilder
    {

        public string ClickFirstByCssSelector(string selector)
        {
            StringBuilder sb = new StringBuilder()
                .Append("document.querySelector(\"")
                .Append(selector)
                .Append("\").click()");

            return sb.ToString();
        }

        public string DocumentQuerySelectorAll(string selector)
        {
            StringBuilder sb = new StringBuilder()
                .Append("document.querySelectorAll(\"")
                .Append(selector)
                .Append("\")");

            return sb.ToString();
        }
        public string DocumentQuerySelector(string selector)
        {
            StringBuilder sb = new StringBuilder()
                .Append("document.querySelector(\"")
                .Append(selector)
                .Append("\")");

            return sb.ToString();
        }
    }
}