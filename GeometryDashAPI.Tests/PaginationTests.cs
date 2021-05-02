using GeometryDashAPI.Server;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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
        [TestCase(20, 11, 20, 1, true)]
        [TestCase(20, 11, 20, 2, false)]
        [TestCase(0, 0, 0, 0, false)]
        public void Pagination_HasPage(int total, int left, int right, int page, bool has)
        {
            Assert.AreEqual(has, new Pagination(total, left, right).HasPage(page));
        }
    }
}
