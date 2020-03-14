using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageChassis
{
    /// <summary>
    /// A class describing a sports car chassis.
    /// </summary>
    public class SportCar : Chassis
    {
        #region Constructors
        public SportCar()
        {
            this.ID = 3;
            this.Name = "Sport Car Chassis";
            this.Color = ChassisColors.Blue;
            this.Price = 30000;
            this.ProductionTime = 21;
            this.NumberOfDoors = 2;
            this.NumberOfSeats = 2;
            this.Description = "A sport car chassis is an automobile body style with a low-hanging suspension. " +
                "It is aerodynamic and is the preferred choice for racing cars.";
        }
        #endregion

        #region Methods

        /// <inheritdoc />
        public override void Add(CarModel cm)
        {
            cm.Chassis = this;
        }

        /// <inheritdoc />
        public override void Remove(CarModel cm)
        {
            cm.Chassis = null;
        }

        /// <summary>
        /// Rerieves the unique identification of a sport car chassis.
        /// </summary>
        /// <returns>The id of the sport car chassis.</returns>
        public override int GetId()
        {
            return this.ID;
        }
        #endregion  

    }
}