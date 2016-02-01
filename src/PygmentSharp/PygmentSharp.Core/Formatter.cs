﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PygmentSharp.Core
{
    public abstract class Formatter
    {
        public abstract string Name { get; }

        public void Format(IEnumerable<Token> tokenSource, TextWriter writer)
        {
            FormatUnencoded(tokenSource, writer);
        }

        protected abstract void FormatUnencoded(IEnumerable<Token> tokenSource, TextWriter writer);
    }
}