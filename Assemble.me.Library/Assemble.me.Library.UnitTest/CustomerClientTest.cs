using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Assemble.me.Library.UnitTest
{
    [TestClass]
    public class CustomerClientTest
    {
        [TestMethod]
        public void OrderModel_Test()
        {
            CustomerClient client = new CustomerClient();
            client.OrderModel(Generator.CreateCustomer(), Generator.CreateCarModel());

            // Verify it is indeed in database - for now we do it manually
        }
        
        [TestMethod]
        public void VerifyEmail_Test()
        {
            CustomerClient client = new CustomerClient();

            Assert.AreEqual(client.CheckIfEmailIsTaken("aaa@aaa.aaa"), true);

            // Verify it is indeed in database - for now we do it manually
        }

        [TestMethod]
        public void VerifyEmail_Test_fail()
        {
            CustomerClient client = new CustomerClient();

            Assert.AreEqual(client.CheckIfEmailIsTaken("ja@yahoo.com"), false);

            // Verify it is indeed in database - for now we do it manually
        }
    }
}
