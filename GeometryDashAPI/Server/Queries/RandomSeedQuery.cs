using System;
using System.Text;

namespace GeometryDashAPI.Server.Queries
{
    public class RandomSeedQuery : IQuery
    {
        private readonly int length;
        private readonly Random random = new();

        private readonly char[] alphabet = {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'H', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };
        
        public RandomSeedQuery(int length)
        {
            this.length = length;
        }
        
        public Parameters BuildQuery()
        {
            var result = new Parameters();
            BuildQuery(result);
            return result;
        }

        public void BuildQuery(Parameters parameters)
        {
            parameters.Add(new Property("rs", Generate()));
        }

        private string Generate()
        {
            var builder = new StringBuilder();
            for (var i = 0; i < length; i++)
                builder.Append(alphabet[random.Next(alphabet.Length)]);
            return builder.ToString();
        }
    }
}