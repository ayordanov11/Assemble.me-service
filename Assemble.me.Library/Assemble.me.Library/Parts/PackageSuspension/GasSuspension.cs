using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageSuspension
{
    /// <summary>
    /// A class describing a gas suspension.
    /// </summary>
    public class GasSuspension : Suspension
    {
        #region Constructors
        public GasSuspension()
        {
            this.ID = 10;
            this.Name = "Gas Suspension";
            this.ProductionTime = 18;
            this.Price = 1700;
            this.Description = "Gas suspension is a type of vehicle suspension powered by gas springs that use " +
                "a compressed gas, contained within a cylinder. The springs are being variably compressed by a piston, " +
                "to exert a force.";
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
