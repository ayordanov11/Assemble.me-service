using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageSuspension
{
    public class Suspension : CarPart
    {
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
            throw new InvalidOperationException("Can't get the id of a unspecified suspension.");
        }
        #endregion
    }
}
