using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageEngine
{
    /// <summary>
    /// This is a class describing a petrol engine.
    /// </summary>
    public class PetrolEngine : Engine
    {
        #region Constructors
        public PetrolEngine()
        {
            this.ID = 7;
            this.Name = "Petrol Engine";
            this.ProductionTime = 21;
            this.Price = 10000;
            this.Description = "A petrol engine is an internal combustion engine with spark-ignition, designed to run on " +
                "petrol (gasoline) and similar volatile fuels. In most petrol engines, the fuel and air are usually " +
                "pre-mixed before compression with the help of a carburetor or a fuel injection.";
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
