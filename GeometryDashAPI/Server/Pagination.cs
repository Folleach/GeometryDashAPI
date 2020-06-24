using System.Text.RegularExpressions;

namespace GeometryDashAPI.Server
{
    public class Pagination
    {
        public int TotalCount { get; private set; }
        public int RangeIn { get; private set; }
        public int RangeOut { get; private set; }

        public Pagination(string hashData)
        {
            string[] data = hashData.Split(':');
            TotalCount = int.Parse(data[0]);
            RangeIn = int.Parse(data[1]);
            RangeOut = int.Parse(data[2]);
        }
    }
}
