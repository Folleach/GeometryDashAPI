using GeometryDashAPI.Server;
using NUnit.Framework;

namespace GeometryDashAPI.Tests
{
    [TestFixture]
    public class PaginationTests
    {
        [TestCase(1, 0, 10, 0, true)]
        [TestCase(1, 0, 10, 1, false)]
        
        [TestCase(10, 0, 10, 0, true)]
        [TestCase(10, 0, 10, 1, false)]
        
        [TestCase(7, 0, 5, 0, true)]
        [TestCase(7, 0, 5, 1, true)]
        [TestCase(7, 0, 5, 2, false)]
        
        [TestCase(3, 0, 10, 0, true)]
        [TestCase(20, 10, 20, 1, true)]
        [TestCase(20, 10, 20, 2, false)]
        [TestCase(0, 0, 0, 0, false)]
        public void Pagination_HasPage(int total, int rangeIn, int rangeOut, int page, bool has)
        {
            Assert.AreEqual(has, new Pagination(total, rangeIn, rangeOut).HasPage(page));
        }
    }
}
