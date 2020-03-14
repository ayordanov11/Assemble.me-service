using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assemble.me.Library.UnitTest
{
    // Testing the database cear functionality
    [TestClass]
    public class DatabaseOrderClearTest
    {
        [TestMethod]

        public void DatabaseUnprocessedOrdersClearTest()
        {
            Administration a = new Administration();
            a.ClearDatabaseProcessedOrders();

            Assert.AreEqual(a.GetProcessedOrders().Count, 0);
        }

        public void DatabaseProcessedOrdersClearTest()
        {
            Administration a = new Administration();
            a.ClearDatabaseProcessedOrders();

            Assert.AreEqual(a.GetOrders().Count, 0);
        }
    }
}
