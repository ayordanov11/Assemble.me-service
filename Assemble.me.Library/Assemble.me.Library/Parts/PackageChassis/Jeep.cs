using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageChassis
{
    /// <summary>
    /// A class describing a chassis of type jeep.
    /// </summary>
    public class Jeep : Chassis
    {
        #region Constructors
        public Jeep()
        {
            this.ID = 2;
            this.Name = "Jeep Chassis";
            this.Color = ChassisColors.Grey;
            this.Price = 30000;
            this.ProductionTime = 31;
            this.NumberOfDoors = 4;
            this.NumberOfSeats = 4;
            this.Description = "A jeep is an automobile body style that is suitable for offroad cars " +
                "and other big contructions. It is stable and offers great security.";
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
        /// Rerieves the unique identification of a jeep chassis.
        /// </summary>
        /// <returns>The id of the jeep chassis.</returns>
        public override int GetId()
        {
            return this.ID;
        }
        #endregion
    }
}