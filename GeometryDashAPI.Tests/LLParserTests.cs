using System;
using System.Collections.Generic;
using FluentAssertions;
using GeometryDashAPI.Parsers;
using NUnit.Framework;

namespace GeometryDashAPI.Tests
{
    [TestFixture]
    public class LLParserTests
    {
        [TestCase("", 0)]
        [TestCase("abc", 1)]
        [TestCase("1.1", 2)]
        [TestCase("1.2.3.4", 4)]
        [TestCase("1.2.3.", 3)]
        [TestCase("..", 2)]
        public void Next_CallNumbers(string input, int expectedCallNumber)
        {
            var parser = new LLParser(".", input);

            var calls = 0;
            while (parser.Next() != null)
                calls++;
            
            Assert.AreEqual(expectedCallNumber, calls);
        }

        [TestCase("")]
        [TestCase(".", "")]
        [TestCase("1", "1")]
        [TestCase("1.2", "1", "2")]
        public void Next_ValidResult(string input, params string[] expected)
        {
            var parser = new LLParser(".", input);

            var result = new List<string>();

            Span<char> next = null;
            while ((next = parser.Next()) != null)
                result.Add(next.ToString());
            
            Assert.AreEqual(expected.Length, result.Count);
            result.ToArray().Should().Equal(expected);
        }
    }
}