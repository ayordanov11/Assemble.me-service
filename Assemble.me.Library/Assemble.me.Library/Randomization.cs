using Assemble.me.Library.PackageCustomer;
using Assemble.me.Library.PackageOrder;
using System;
using System.Linq;
using RandomNameGenerator;
using System.Collections.Generic;
using Assemble.me.Library.Parts.PackageSuspension;
using Assemble.me.Library.Parts.PackageAccumulator;
using Assemble.me.Library.Parts.PackageChassis;
using Assemble.me.Library.Parts.PackageEngine;
using Assemble.me.Library.Parts.PackageInterrior;
using Assemble.me.Library.Parts.PackageTires;
using Assemble.me.Library.Parts.PackageRims;
using Assemble.me.Library.Parts.PackageTransmission;
using Assemble.me.Library.Parts.PackageExtra;

namespace Assemble.me.Library
{
    /// <summary>
    /// A class providing functionality connected to randomizing orders.
    /// </summary>
    public class Randomization
    {
        #region Fields
        private CustomerClient customerClient;
        private Random rand;
        #endregion
        
        #region Constructors
        public Randomization()
        {
            customerClient = new CustomerClient();
            rand = new Random();
        }
        #endregion

        #region Methods

        /// <summary>
        /// Generates a random order.
        /// </summary>
        /// <returns>The newly generated order.</returns>
        public void GenerateRandomOrder()
        {
            // Save the order to the database with randomly generated parameters.
            this.customerClient.OrderModel(GetRandomCustomer(), GetRandomCarModel(), GetRandomPriority());
        }

        #region Helpers

        /// <summary>
        /// Generates an order priority based on a random number.
        /// </summary>
        /// <returns>The generated order priority.</returns>
        private OrderPriority GetRandomPriority()
        {
            if (rand.Next(0, 11) < 5)
                return OrderPriority.Normal;
            else
                return OrderPriority.High;
        }

        /// <summary>
        /// Generates a random Customer object.
        /// </summary>
        /// <returns>The created Customer object.</returns>
        private Customer GetRandomCustomer()
        {
            if (rand.Next(0, 2) == 0)
                return new Customer(
                    NameGenerator.GenerateFirstName(Gender.Female),
                    NameGenerator.GenerateLastName(),
                    GetRandomString(7) + "@random.org",
                    GetRandomPhone());
            else
                return new Customer(
                    NameGenerator.GenerateFirstName(Gender.Male),
                    NameGenerator.GenerateLastName(),
                    GetRandomString(7) + "@random.org",
                    GetRandomPhone());
        }

        /// <summary>
        /// Generates a random string.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rand.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Generates a random phone number with a length of 10 digits.
        /// </summary>
        /// <returns>The generated phone number.</returns>
        private string GetRandomPhone()
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[rand.Next(s.Length)]).ToArray());
        }
        
        /// <summary>
        /// Generates a random car model from a predefined list.
        /// </summary>
        /// <returns>A <see cref="CarModel"/> object.</returns>
        private CarModel GetRandomCarModel()
        {
            List<CarModel> randomModels = new List<CarModel>();

            // 1
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
            m.AddPart(new AudioSystem());
            m.AddPart(new GPS());
            randomModels.Add(m);

            // 2
            m = new CarModel();
            t = new Tires(Inches.Eighteen);
            m.AddPart(new Jeep());
            m.Chassis.SetChassisColor(ChassisColors.Red);
            m.AddPart(new HydraulicSuspension());
            m.AddPart(new DieselEngine());
            m.AddPart(new Accumulator(AccumulatorSize.MegaPower));
            m.AddPart(new ManualTransmission());
            m.AddPart(new LeatherInterior());
            m.AddPart(t);
            m.AddPart(new Rims(t));
            m.AddPart(new _4x4());
            m.AddPart(new GPS());
            randomModels.Add(m);

            // 3
            m = new CarModel();
            t = new Tires(Inches.Sixteen);
            m.AddPart(new SportCar());
            m.Chassis.SetChassisColor(ChassisColors.Blue);
            m.AddPart(new AirSuspension());
            m.AddPart(new DieselEngine());
            m.AddPart(new Accumulator(AccumulatorSize.MegaPower));
            m.AddPart(new ManualTransmission());
            m.AddPart(new LeatherInterior());
            m.AddPart(t);
            m.AddPart(new Rims(t));
            m.AddPart(new _4x4());
            m.AddPart(new AudioSystem());
            m.AddPart(new GPS());
            randomModels.Add(m);

            // 4
            m = new CarModel();
            t = new Tires(Inches.Fourteen);
            m.AddPart(new Sedan());
            m.Chassis.SetChassisColor(ChassisColors.Grey);
            m.AddPart(new HydraulicSuspension());
            m.AddPart(new PetrolEngine());
            m.AddPart(new Accumulator(AccumulatorSize.Small));
            m.AddPart(new AutomaticTransmission());
            m.AddPart(new TextileInterior());
            m.AddPart(t);
            m.AddPart(new Rims(t));
            randomModels.Add(m);

            // 5
            m = new CarModel();
            t = new Tires(Inches.Sixteen);
            m.AddPart(new Sedan());
            m.Chassis.SetChassisColor(ChassisColors.Red);
            m.AddPart(new AirSuspension());
            m.AddPart(new ElectricEngine());
            m.AddPart(new Accumulator(AccumulatorSize.Average));
            m.AddPart(new AutomaticTransmission());
            m.AddPart(new LeatherInterior());
            m.AddPart(t);
            m.AddPart(new Rims(t));
            m.AddPart(new _4x4());
            m.AddPart(new AudioSystem());
            m.AddPart(new GPS());
            m.AddPart(new Parktronic());
            m.AddPart(new AC());
            m.AddPart(new ButtonStarter());
            randomModels.Add(m);

            // 6
            m = new CarModel();
            t = new Tires(Inches.Fourteen);
            m.AddPart(new SportCar());
            m.Chassis.SetChassisColor(ChassisColors.Blue);
            m.AddPart(new HydraulicSuspension());
            m.AddPart(new DieselEngine());
            m.AddPart(new Accumulator(AccumulatorSize.MegaPower));
            m.AddPart(new ManualTransmission());
            m.AddPart(new LeatherInterior());
            m.AddPart(t);
            m.AddPart(new Rims(t));
            m.AddPart(new _4x4());
            m.AddPart(new AudioSystem());
            m.AddPart(new GPS());
            m.AddPart(new AC());
            randomModels.Add(m);

            // 7
            m = new CarModel();
            t = new Tires(Inches.Eighteen);
            m.AddPart(new Jeep());
            m.Chassis.SetChassisColor(ChassisColors.Grey);
            m.AddPart(new GasSuspension());
            m.AddPart(new PetrolEngine());
            m.AddPart(new Accumulator(AccumulatorSize.Average));
            m.AddPart(new ManualTransmission());
            m.AddPart(new TextileInterior());
            m.AddPart(t);
            m.AddPart(new Rims(t));
            m.AddPart(new _4x4());
            m.AddPart(new GPS());
            m.AddPart(new CruiseControl());
            randomModels.Add(m);

            // 8
            m = new CarModel();
            t = new Tires(Inches.Fourteen);
            m.AddPart(new Cabrio());
            m.Chassis.SetChassisColor(ChassisColors.Grey);
            m.AddPart(new HydraulicSuspension());
            m.AddPart(new DieselEngine());
            m.AddPart(new Accumulator(AccumulatorSize.Small));
            m.AddPart(new ManualTransmission());
            m.AddPart(new TextileInterior());
            m.AddPart(t);
            m.AddPart(new Rims(t));
            randomModels.Add(m);

            // 9
            m = new CarModel();
            t = new Tires(Inches.Fourteen);
            m.AddPart(new Minivan());
            m.Chassis.SetChassisColor(ChassisColors.Grey);
            m.AddPart(new HydraulicSuspension());
            m.AddPart(new PetrolEngine());
            m.AddPart(new Accumulator(AccumulatorSize.Small));
            m.AddPart(new AutomaticTransmission());
            m.AddPart(new TextileInterior());
            m.AddPart(t);
            m.AddPart(new Rims(t));
            randomModels.Add(m);

            // 9
            m = new CarModel();
            t = new Tires(Inches.Sixteen);
            m.AddPart(new Minivan());
            m.Chassis.SetChassisColor(ChassisColors.Blue);
            m.AddPart(new GasSuspension());
            m.AddPart(new DieselEngine());
            m.AddPart(new Accumulator(AccumulatorSize.Average));
            m.AddPart(new ManualTransmission());
            m.AddPart(new LeatherInterior());
            m.AddPart(t);
            m.AddPart(new Rims(t));
            randomModels.Add(m);

            // 10
            m = new CarModel();
            t = new Tires(Inches.Fourteen);
            m.AddPart(new Minivan());
            m.Chassis.SetChassisColor(ChassisColors.Blue);
            m.AddPart(new AirSuspension());
            m.AddPart(new ElectricEngine());
            m.AddPart(new Accumulator(AccumulatorSize.Average));
            m.AddPart(new AutomaticTransmission());
            m.AddPart(new LeatherInterior());
            m.AddPart(t);
            m.AddPart(new Rims(t));
            randomModels.Add(m);

            // Retrieve a model from the list based on a random integer.
            int position = rand.Next(randomModels.Count - 1);
            return randomModels.ElementAt(position);
        }
        

        #endregion

        #endregion
    }
}