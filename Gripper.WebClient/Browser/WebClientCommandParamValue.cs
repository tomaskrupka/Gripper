using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Gripper.WebClient.Browser
{
    public class WebClientCommandParam
    {
        private bool _isArray;
        private string? _key;
        private bool _isLeaf;
        private WebClientCommandParam[]? _values;
        private string[]? _leafValues;

        public string Key
        {
            get
            {
                if (_key == null)
                {
                    throw new NotSupportedException($"This is an array param, access {nameof(Values)} without keys.");
                }
                return _key;
            }
            set => _key = value;
        }
        public bool IsLeaf { get => _isLeaf; set => _isLeaf = value; }
        public WebClientCommandParam[] Values
        {
            get
            {
                if (_values == null)
                {
                    throw new NotSupportedException($"This is a leaf of this command param, access {nameof(LeafValues)} instead of {nameof(Values)}");
                }
                return _values;
            }
            set => _values = value;
        }
        public string[] LeafValues
        {
            get
            {
                if (_leafValues == null)
                {
                    throw new NotSupportedException($"This is not a leaf of this command param, access {nameof(Values)} instead of {nameof(LeafValues)}");
                }
                return _leafValues;
            }
            set => _leafValues = value;
        }

        public WebClientCommandParam(string key, string value)
        {
            _isArray = false;
            _key = key;
            _leafValues = new[] { value };
            _isLeaf = true;
            _values = null;
        }

        public WebClientCommandParam(string key, IEnumerable<string> values)
        {
            _isArray = true;
            _key = key;
            _leafValues = values.ToArray();
            _isLeaf = true;
            _values = null;
        }

        public WebClientCommandParam(string key, WebClientCommandParam value)
        {
            _isArray = false;
            _key = key;
            _leafValues = null;
            _isLeaf = false;
            _values = new[] { value };
        }

        public WebClientCommandParam(string key, IEnumerable<WebClientCommandParam> values)
        {
            _isArray = true;
            _key = key;
            _values = values.ToArray();
            _isLeaf = false;
            _leafValues = null;
        }

        public WebClientCommandParam(IEnumerable<WebClientCommandParam> keyValuePairs)
        {
            _isArray = true;
            _key = null;
            _values = keyValuePairs.ToArray();
            _isLeaf = false;
            _leafValues = null;
        }

        public static implicit operator WebClientCommandParam((string key, string value) pair)
        {
            return new WebClientCommandParam(pair.key, pair.value);
        }

        public static implicit operator WebClientCommandParam((string key, IEnumerable<string> values) pair)
        {
            return new WebClientCommandParam(pair.key, pair.values);
        }

        public static implicit operator WebClientCommandParam((string key, WebClientCommandParam value) pair)
        {
            return new WebClientCommandParam(pair.key, pair.value);
        }

        public static implicit operator WebClientCommandParam((string key, IEnumerable<WebClientCommandParam> values) pair)
        {
            return new WebClientCommandParam(pair.key, pair.values);
        }

    }
}

