using GeometryDashAPI.Server;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    [TestFixture]
    public class PaginationTests
    {
        [TestCase(3, 0, 10, 0, true)]
        [TestCase(20, 11, 20, 1, true)]
        [TestCase(20, 11, 20, 2, false)]
        public void Test(int total, int rin, int rout, int page, bool has)
        {
            Assert.AreEqual(new Pagination(total, rin, rout).HasPage(page), has);
        }
    }
}
