using Assemble.me.Library.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageInterrior
{
    public class Interior : CarPart
    {
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
            throw new InvalidOperationException("Can't get the id of a unspecified interior.");
        }
    }
}
