using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageTransmission
{
    public class AutomaticTransmission : Transmission
    {
        #region Constructor
        public AutomaticTransmission()
        {
            this.ID = 19;
            this.Name = "Automatic Transmission";
            this.ProductionTime = 3;
            this.Price = 300;
            this.Description = "Automatic transmission provides the comfort of driving in the city trafic without having to change gears.";
        }
        #endregion

        #region Methods
        /// <inheritdoc />
        public override void Add(CarModel cm)
        {
            cm.Transmission = this;
        }
        /// <inheritdoc />
        public override void Remove(CarModel cm)
        {
            cm.Transmission = null;
        }
        /// <inheritdoc />
        public override int GetId()
        {
            return this.ID;
        }
        #endregion
    }
}
