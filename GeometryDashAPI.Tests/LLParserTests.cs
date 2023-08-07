using System;
using System.Collections.Generic;
using FluentAssertions;
using GeometryDashAPI.Serialization;
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
            var parser = new LLParserSpan(".".AsSpan(), input.AsSpan());

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
            var parser = new LLParserSpan(".".AsSpan(), input.AsSpan());

            var result = new List<string>();

            ReadOnlySpan<char> next = null;
            while ((next = parser.Next()) != null)
                result.Add(next.ToString());
            
            Assert.AreEqual(expected.Length, result.Count);
            result.ToArray().Should().Equal(expected);
        }

        [TestCase("", 0)]
        [TestCase(".", 1)]
        [TestCase("a.", 1)]
        [TestCase("a.a", 2)]
        [TestCase("..", 2)]
        [TestCase("a.a.a", 3)]
        [TestCase(".a.a", 3)]
        [TestCase("a.a.", 2)]
        [TestCase("a...", 3)]
        public void CountOfSense(string input, int expected)
        {
            var parser = new LLParserSpan(".".AsSpan(), input.AsSpan());

            var actual = parser.GetCountOfValues();
            
            Assert.AreEqual(expected, actual);
        }
    }
}