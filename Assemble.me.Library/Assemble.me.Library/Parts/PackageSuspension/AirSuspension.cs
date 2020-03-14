using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageSuspension
{
    /// <summary>
    /// A class describing an air suspension.
    /// </summary>
    public class AirSuspension : Suspension
    {
        #region Constructors
        public AirSuspension()
        {
            this.ID = 9;
            this.Name = "Air Suspension";
            this.ProductionTime = 14;
            this.Price = 1500;
            this.Description = "Air suspension is a type of vehicle suspension powered by an electric or engine-driven " + 
                "air pump or compressor. This compressor pumps the air into bellows and the air pressure inflates the "+ 
                "bellows, and raises the chassis from the axle.";
        }
        #endregion

        #region Methods

        /// <inheritdoc />
        public override void Add(CarModel cm)
        {
            cm.Suspension = this;
        }

        /// <inheritdoc />
        public override void Remove(CarModel cm)
        {
            cm.Suspension = null;
        }

        /// <inheritdoc />
        public override int GetId()
        {
            return this.ID;
        }
        #endregion
    }
}
