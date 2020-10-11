using GeometryDashAPI.Parsers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestFixture]
    public class KeyValueSLLParserTest
    {
        public void Default()
        {
            var input = "a,1,b,2,z,27,k,k";
            var output = new string[,]
            {
                { "a", "1" },
                { "b", "2" },
                { "z", "27" },
                { "k", "k" }
            };

            Asserts(',', input, output);
        }

        private void Asserts(char spt, string input, string[,] expected)
        {
            var kvsll = new KeyValueSLLParser(spt, input);
            var result = new List<Tuple<string, string>>();

            while (kvsll.Next())
            {
                result.Add(Tuple.Create(kvsll.Key, kvsll.Value));
            }

            Assert.AreEqual(expected.Length, result.Count);
            for (var i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expected[i, 0], result[i].Item1);
                Assert.AreEqual(expected[i, 1], result[i].Item2);
            }
        }
    }
}
