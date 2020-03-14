using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageChassis
{
    /// <summary>
    /// A class describing a chassis of type cabrio.
    /// </summary>
    public class Cabrio : Chassis
    {
        #region Constructors
        public Cabrio()
        {
            this.ID = 1;
            this.Name = "Cabrio Chassis";
            this.Color = ChassisColors.Red;
            this.Price = 15000;
            this.ProductionTime = 14;
            this.NumberOfDoors = 2;
            this.NumberOfSeats = 4;
            this.Description = "A cabrio is an automobile body style that can convert between " +
                "an open-air mode and an enclosed one. It has a detachable hardtop, which is wind and water-proofed.";
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
        /// Rerieves the unique identification of a cabrio chassis.
        /// </summary>
        /// <returns>The id of the cabrio chassis.</returns>
        public override int GetId()
        {
            return this.ID;
        }

        #endregion
    }
}
