using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageChassis
{
    public class Chassis : CarPart
    {
        #region Properties
        public int NumberOfDoors { get; protected set; }
        public int NumberOfSeats { get; protected set; }
        public ChassisColors Color { get; protected set; }
        #endregion

        #region Methods

        /// <summary>
        /// Chassis implements this method add because nomatter what type of chassi it will be 
        /// the this method will behave the same way
        /// </summary>
        /// <param name="cm"> The car model the part will be assigned to</param>
        public override void Add(CarModel cm)
        {
            cm.Chassis = this;
        }

        /// <summary>
        /// Chassis implements this method add because nomatter what type of chassi it will be 
        /// the this method will behave the same way
        /// </summary>
        /// <param name="cm"> The car model from which the part will be removed </param>
        public override void Remove(CarModel cm)
        {
            cm.Chassis = null;
        }

        /// <summary>
        /// This operation cannot be performed on the generic class <see cref="Chassis"/>.
        /// </summary>
        /// <exception>An <see cref="InvalidOperationException"/>.</exception>
        public override int GetId()
        {
            throw new InvalidOperationException("Can't get the id of a unspecified chassis.");
        }

        /// <summary>
        /// Method that sets the color of the chassis.
        /// </summary>
        /// <param name="c">The target color of the chassis</param>
        public void SetChassisColor(ChassisColors c)
        {
            this.Color = c;
        }
        #endregion        
    }
}
