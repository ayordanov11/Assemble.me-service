using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageInterrior
{
    public class LeatherInterior : Interior
    {
        #region Constructor
        public LeatherInterior()
        {
            this.ID = 23;
            this.Name = "Leather Interior";
            this.ProductionTime = 10;
            this.Price = 1000;
            this.Description = "A leather interior is manufactured from high quality materials "+
                "which will be covering the seats of the vehicle, as well as parts of the dashboard and doors.";
        }
        #endregion

        #region Methods

        /// <inheritdoc />
        public override void Add(CarModel cm)
        {
            cm.Interior = this;
        }

        /// <inheritdoc />
        public override void Remove(CarModel cm)
        {
            cm.Interior = null;
        }

        /// <inheritdoc />
        public override int GetId()
        {
            return this.ID;
        }
        #endregion
    }
}
