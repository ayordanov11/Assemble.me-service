using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assemble.me.Library;
using Assemble.me.Library.Parts.PackageChassis;
using Assemble.me.Library.Parts.PackageEngine;
using Assemble.me.Library.Parts.PackageExtra;
using Assemble.me.Library.Exceptions;

namespace Assemble.me.Library.UnitTest
{
    [TestClass]
    public class CarModelTest
    {
        /// <summary>
        /// Tests if a part is added to the model correctly.
        /// </summary>
        [TestMethod]
        public void AddPart_Test()
        {
            // Create a model and a part.
            CarModel model = new CarModel();
            Cabrio cabrio = new Cabrio();

            //Add part to model
            model.AddPart(cabrio);

            // Verify that part was added successfully.
            Assert.AreEqual(cabrio, model.Chassis);
        }

        /// <summary>
        /// Tests that an exception is correctly thrown when attempting to add a duplicate extra to a model.
        /// </summary>
        [TestMethod]
        public void AddDuplicateExtra_Test()
        {
            CarModel cm = Generator.CreateCarModel();
//            cm.AddPart(new GPS());
            Assert.IsInstanceOfType(Generator.AssertThrows<DuplicateExtraException>(() => cm.AddPart(new GPS())),
                typeof(DuplicateExtraException));

            cm.AddPart(new _4x4());
            Generator.AssertThrows<DuplicateExtraException>(() => cm.AddPart(new _4x4()));

            cm.AddPart(new AC());
            Generator.AssertThrows<DuplicateExtraException>(() => cm.AddPart(new AC()));

            cm.AddPart(new AudioSystem());
            Generator.AssertThrows<DuplicateExtraException>(() => cm.AddPart(new AudioSystem()));

            cm.AddPart(new ButtonStarter());
            Generator.AssertThrows<DuplicateExtraException>(() => cm.AddPart(new ButtonStarter()));

            cm.AddPart(new CruiseControl());
            Generator.AssertThrows<DuplicateExtraException>(() => cm.AddPart(new CruiseControl()));

            cm.AddPart(new Parktronic());
            Generator.AssertThrows<DuplicateExtraException>(() => cm.AddPart(new Parktronic()));
        }
        /// <summary>
        /// Tests if that a part of teh same type is changed correctly.
        /// </summary>
        [TestMethod]
        public void ChangePart_Test()
        {
            CarModel model = new CarModel();
            Engine petrolEngine = new PetrolEngine();
            Engine electricEngine = new ElectricEngine();

            // Add the engine to the model and verify it is correct
            model.AddPart(petrolEngine);
            Assert.AreEqual(petrolEngine, model.Engine);

            // Change the engine and verify it is correct
            model.AddPart(electricEngine);
            Assert.AreEqual(electricEngine, model.Engine);

        }

        /// <summary>
        /// Tests if a part is removed from the model correctly.
        /// </summary>
        [TestMethod]
        public void RemovePart_Test()
        {
            CarModel model = new CarModel();
            Cabrio cabrio = new Cabrio();

            // Add a chassis to the model and check if it was added correctly
            model.AddPart(cabrio);
            Assert.AreEqual(cabrio, model.Chassis);

            // Remove the chassis and check if the operation was successful.
            model.RemovePart(new Cabrio());
            Assert.IsNull(model.Chassis);
            Assert.AreEqual(0, model.GetAllParts().Count);

            // Add and remove extras
            model.AddPart(new _4x4());
            model.AddPart(new AC());
            model.RemovePart(new _4x4());
            Assert.AreEqual(1, model.Extras.Count);
            Assert.AreEqual(1, model.GetAllParts().Count);
        }
    }
}
