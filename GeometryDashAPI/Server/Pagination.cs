using System.Text.RegularExpressions;

namespace GeometryDashAPI.Server
{
    public class Pagination
    {
        public int TotalCount { get; private set; }
        public int RangeIn { get; private set; }
        public int RangeOut { get; private set; }
        public int CountOnPage => (RangeOut - RangeIn + 1);

        public Pagination(string hashData)
        {
            string[] data = hashData.Split(':');
            TotalCount = int.Parse(data[0]);
            RangeIn = int.Parse(data[1]);
            RangeOut = int.Parse(data[2]);
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
    }
}
