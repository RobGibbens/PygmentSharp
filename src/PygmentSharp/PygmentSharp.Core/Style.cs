﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PygmentSharp.Core
{
    public class Style : IEnumerable<KeyValuePair<TokenType, StyleData>>
    {
        private readonly Dictionary<TokenType, StyleData> _styles;

        public Style()
        {
            _styles = new Dictionary<TokenType, StyleData>();
        }

        public Style(IDictionary<TokenType, string> styles)
        {
            _styles = ParseStyles(styles);
        }

        private static Dictionary<TokenType, StyleData> ParseStyles(IDictionary<TokenType, string> styles)
        {
            foreach (var ttype in TokenTypeMap.Instance.Keys)
            {
                if (!styles.ContainsKey(ttype))
                    styles[ttype] = "";
            }

            var output = new Dictionary<TokenType, StyleData>();
            foreach (var style in styles)
            {
                foreach (var ttype in style.Key.Split())
                {
                    output[ttype] = StyleData.Parse(style.Value);
                }
            }

            return output;
        }

        public string BackgroundColor { get; set; } = "#ffffff";

        public string HighlightColor { get; set; } = "#ffffcc";

        public StyleData StyleForToken(TokenType ttype)
        {
            return this[ttype];
        }

        public StyleData this[TokenType ttype] => _styles.ContainsKey(ttype) ? _styles[ttype] : null;

        public IEnumerator<KeyValuePair<TokenType, StyleData>> GetEnumerator()
        {
            return _styles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _styles).GetEnumerator();
        }
    }
}
