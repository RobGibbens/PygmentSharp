﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFluent;
using PygmentSharp.Core;
using PygmentSharp.Core.Lexers;
using Xunit;
using Xunit.Abstractions;

namespace PygmentSharp.UnitTests.Lexers
{
    public class CSharpExampleFileTests
    {
        private readonly ITestOutputHelper _output;
        private readonly Token[] _results;

        public CSharpExampleFileTests(ITestOutputHelper output)
        {
            _output = output;
            var subject = new CSharpLexer();
            _results = subject.GetTokens(SampleFile.Load("csharp-sample.txt"))
                .ToArray();
        }


        [Fact]
        public void DoesntContainErrorTokens()
        {

            foreach (var t in _results)
            {
                //_output.WriteLine(t.ToString());

                if (t.Type == TokenTypes.Error)
                {
                    throw new Exception($"Lexer reported an error at pos {t.Index} : '{t.Value}'");
                }
            }

        }

        [Fact]
        public void Contains8NamespaceOrUsings()
        {
            var usings = _results.Where(t => t.Type == TokenTypes.Name.Namespace)
                .ToArray();

            Check.That(usings).HasSize(8);
        }

        [Fact]
        public void FirstNamespaceIsDivaCore()
        {
            var nmspace = _results.First(t => t.Type == TokenTypes.Name.Namespace);

            Check.That(nmspace.Value).IsEqualTo("Diva.Core");
        }

        [Fact]
        public void NoCharactersAreLost()
        {
            var expected = SampleFile.Load("csharp-sample.txt");
            var sb = new StringBuilder(expected.Length);

            foreach (var t in _results)
                sb.Append(t.Value);

            sb.Replace("\n", "\r\n");

            Check.That(sb.ToString()).IsEqualTo(expected);
        }
    }
}