using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageInterrior
{
    public class TextileInterior : Interior
    {
        #region Constructor
        public TextileInterior()
        {
            this.ID = 24;
            this.Name = "Textile Interior";
            this.ProductionTime = 7;
            this.Price = 800;
            this.Description = "A textile interior is manufactured from high quality materials " +
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
