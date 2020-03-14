using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assemble.me.Library.Parts.PackageChassis;

namespace Assemble.me.Library.UnitTest
{
    [TestClass]
    public class InventoryTest
    {
        [TestMethod]
        public void GetAvailableParts_Test()
        {
            Assert.AreEqual(Inventory.GetAvailableParts().Count, 30);
        }

        [TestMethod]
        public void Restock_Test()
        {
            Inventory.PurchaseParts(new Cabrio(), 2);
        }

        [TestMethod]
        public void VerifyQuantity_Test()
        {
            Inventory.VerifyQuantityForModel(Generator.CreateCarModel());
        }
    }
}
