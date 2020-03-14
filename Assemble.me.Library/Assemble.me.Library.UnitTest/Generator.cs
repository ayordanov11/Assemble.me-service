using Assemble.me.Library.PackageCustomer;
using Assemble.me.Library.PackageOrder;
using Assemble.me.Library.Parts.PackageAccumulator;
using Assemble.me.Library.Parts.PackageChassis;
using Assemble.me.Library.Parts.PackageEngine;
using Assemble.me.Library.Parts.PackageExtra;
using Assemble.me.Library.Parts.PackageInterrior;
using Assemble.me.Library.Parts.PackageRims;
using Assemble.me.Library.Parts.PackageSuspension;
using Assemble.me.Library.Parts.PackageTires;
using Assemble.me.Library.Parts.PackageTransmission;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.UnitTest
{
    public class Generator
    {
        public static CarModel CreateCarModel()
        {
            CarModel m = new CarModel();
            Tires t = new Tires(Inches.Sixteen);
            m.AddPart(new Cabrio());
            m.AddPart(new AirSuspension());
            m.AddPart(new DieselEngine());
            m.AddPart(new Accumulator(AccumulatorSize.Average));
            m.AddPart(new ManualTransmission());
            m.AddPart(new LeatherInterior());
            m.AddPart(t);
            m.AddPart(new Rims(t));
            m.AddPart(new GPS());
            return m;
        }

        public static Customer CreateCustomer()
        {
            return new Customer("John", "Doe", "john@doe.lost", "123456789");
        }

        public static Order CreateOrder()
        {
            return new Order(CreateCustomer(), CreateCarModel());
        }

        public static TException AssertThrows<TException>(Action action) where TException : Exception
        {
            try
            {
                action();
            }
            catch (TException ex)
            {
                return ex;
            }
            Assert.Fail("Expected exception was not thrown");

            return null;
        }
    }
}
