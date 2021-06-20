using System.Text.RegularExpressions;
using GeometryDashAPI.Parsers;

namespace GeometryDashAPI.Server
{
    public class Pagination
    {
        public int TotalCount { get; private set; }
        public int RangeIn { get; private set; }
        public int RangeOut { get; private set; }
        public int CountOnPage => (RangeOut - RangeIn + 1);

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

        public static Pagination Parse(string raw)
        {
            var parser = new LLParser(":", raw);
            var result = new Pagination();
            result.TotalCount = int.Parse(parser.Next());
            result.RangeIn = int.Parse(parser.Next());
            result.RangeOut = int.Parse(parser.Next());
            return result;
        }

        public static string Parse(Pagination pagination)
        {
            return $"{pagination.TotalCount}:{pagination.RangeIn}:{pagination.RangeOut}";
        }
    }
}
