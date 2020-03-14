using Assemble.me.Library.PackageOrder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Assemble.me.Library.UnitTest
{
    [TestClass]
    public class AdministrationTest
    {
        Administration a = new Administration();
        Randomization r = new Randomization();

        /// <summary>
        /// Will fail if there are orders in the DB. Was used for initial tests.
        /// </summary>
        [TestMethod]
        public void GetOrdersEmpty_Test()
        {
           a.Orders.Add(new Order(Generator.CreateCustomer(), Generator.CreateCarModel()));           
           a.GetOrders();

           Assert.IsTrue(a.Orders.Count == 0);
        }

        [TestMethod]
        public void ProcessOrder_Test()
        {
            r.GenerateRandomOrder();
            List<Order> orders = a.GetOrders();

            orders[3].ChangeOrderPriority(OrderPriority.High);
        }

        [TestMethod]
        public void ProcessedOrders_Test()
        {
            Assert.IsTrue(a.GetProcessedOrders().Any());
        }

        [TestMethod]
        public void ProcesseAnOrders_Test()
        {
            a.Orders.Add(new Order(Generator.CreateCustomer(), Generator.CreateCarModel()));
            a.GetOrders();

            a.ProcessOrder(a.GetOrders()[0].ID);
            Assert.IsTrue(a.GetProcessedOrders().Any());
        }
    }
}
