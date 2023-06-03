using System;
using System.Text.RegularExpressions;
using GeometryDashAPI.Serialization;

namespace GeometryDashAPI.Server
{
    public class Pagination
    {
        public int TotalCount { get; private set; }
        public int RangeIn { get; private set; }
        public int RangeOut { get; private set; }
        public int CountOnPage => RangeOut - RangeIn;

        public Pagination()
        {
        }

        public Pagination(int total, int rangeIn, int rangeOut)
        {
            TotalCount = total;
            RangeIn = rangeIn;
            RangeOut = rangeOut;
        }

        public bool HasPage(int page)
        {
            return page * CountOnPage < TotalCount;
        }

        public static Pagination Parse(ReadOnlySpan<char> data) => Parse(data.ToString());

        public static Pagination Parse(string raw)
        {
            var parser = new LLParserSpan(":".AsSpan(), raw.AsSpan());
            var result = new Pagination();
#if NETSTANDARD2_1
            result.TotalCount = int.Parse(parser.Next());
            result.RangeIn = int.Parse(parser.Next());
            result.RangeOut = int.Parse(parser.Next());
#else
            result.TotalCount = int.Parse(parser.Next().ToString());
            result.RangeIn = int.Parse(parser.Next().ToString());
            result.RangeOut = int.Parse(parser.Next().ToString());
#endif
            return result;
        }

        public static string Serialize(Pagination pagination)
        {
            return $"{pagination.TotalCount}:{pagination.RangeIn}:{pagination.RangeOut}";
        }
    }
}
