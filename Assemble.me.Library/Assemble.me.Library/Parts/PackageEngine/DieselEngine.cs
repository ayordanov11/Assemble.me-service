using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageEngine
{
    /// <summary>
    /// A class describing a diesel engine.
    /// </summary>
    public class DieselEngine : Engine
    {
        #region Constructors
        public DieselEngine()
        {
            this.ID = 6;
            this.Name = "Diesel Engine";
            this.ProductionTime = 21;
            this.Price = 11000;
            this.Description = "The diesel engine is an internal combustion engine in which ignition of the fuel that " +
                "has been injected into the combustion chamber is caused by the high temperature which a gas achieves when " +
                "greatly compressed. Diesel engines work by compressing only the air.";
        }
        #endregion

        #region Methods
        /// <inheritdoc />
        public override void Add(CarModel cm)
        {
            cm.Engine = this;
        }
        /// <inheritdoc />
        public override void Remove(CarModel cm)
        {
            cm.Engine = null;
        }

        /// <inheritdoc />
        public override int GetId()
        {
            return this.ID;
        }
        #endregion
    }
}
