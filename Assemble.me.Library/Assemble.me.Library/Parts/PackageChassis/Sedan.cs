using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageChassis
{
    /// <summary>
    /// A class describing a chassis ot type hatchback.
    /// </summary>
    public class Sedan : Chassis
    {
        #region Constructors
        public Sedan()
        {
            this.ID = 4;
            this.Name = "Sedan Chassis";
            this.Color = ChassisColors.Grey;
            this.Price = 11000;
            this.ProductionTime = 21;
            this.NumberOfDoors = 4;
            this.NumberOfSeats = 4;
            this.Description = "A sedan is a car body configuration with four doors " +
                "provide access to a cargo area. It features a second row seating, where the interior can be flexibly" +
                "reconfigured to prioritize passenger vs. cargo volume. Sedans are a preferred choice for " +
                "family cars and executive cars.";
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
        /// Rerieves the unique identification of a hatchback chassis.
        /// </summary>
        /// <returns>The id of the hatchback chassis.</returns>
        public override int GetId()
        {
            return this.ID;
        }
        #endregion     
    }
}