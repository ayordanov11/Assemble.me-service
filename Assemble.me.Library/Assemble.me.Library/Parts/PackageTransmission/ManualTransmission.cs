using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageTransmission
{
    public class ManualTransmission : Transmission
    {
        #region Constructor
        public ManualTransmission()
        {
            this.ID = 18;
            this.Name = "Manual Transmission";
            this.ProductionTime = 2;
            this.Price = 200;
            this.Description = "Manual transmission is great for drivers who enjoy driving and like to switch the gears on their own.A great solution for big cars that use gears to regulate speed.";
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
