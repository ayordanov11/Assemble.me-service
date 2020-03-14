using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageSuspension
{
    /// <summary>
    /// A class describing a hydraulic suspension.
    /// </summary>
    public class HydraulicSuspension : Suspension
    {
        #region Constructors
        public HydraulicSuspension()
        {
            this.ID = 11;
            this.Name = "Hydraulic Suspension";
            this.ProductionTime = 21;
            this.Price = 1200;
            this.Description = "Hydraulic suspension is a type of motor vehicle suspension system that uses torque " +
                "multiplication in an easy way, independent of the distance between the input and output, without " +
                "the need for mechanical gears or levers.";
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
