using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageEngine
{
    /// <summary>
    /// This is a class describing an electric engine.
    /// </summary>
    public class ElectricEngine : Engine
    {
        #region Constructors
        public ElectricEngine()
        {
            this.ID = 8;
            this.Name = "Electric Engine";
            this.ProductionTime = 31;
            this.Price = 12000;
            this.Description = "An electric motor is an electrical machine that converts electrical energy into mechanical " +
                "energy. In normal motoring mode, most electric motors operate through the interaction between an electric motor's " +
                "magnetic field and winding currents to generate force within the motor.";
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
