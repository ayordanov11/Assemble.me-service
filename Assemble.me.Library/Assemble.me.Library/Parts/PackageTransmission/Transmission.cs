using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageTransmission
{
    public class Transmission : CarPart
    {
        #region Constructor
        public Transmission()
        {

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
            throw new InvalidOperationException("Can't get the id of a unspecified transmission.");
        }
        #endregion
    }
}
