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
    public class Minivan : Chassis
    {
        #region Constructors
        public Minivan()
        {
            this.ID = 5;
            this.Name = "Minivan Chassis";
            this.Color = ChassisColors.Blue;
            this.Price = 11300;
            this.ProductionTime = 23;
            this.NumberOfDoors = 4;
            this.NumberOfSeats = 4;
            this.Description = "The minivan combines a high-roof, five-door body configuration with a mid-size platform,"+
                " engine and mechanicals. Car-like handling and fuel economy. Greater height than sedan with a design that offers"+
                " higher h-point seating, two or three rows of seating, easy passenger and cargo access with sliding wide-opening rear doors";
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