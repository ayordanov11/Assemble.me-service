using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageEngine
{
    public class Engine : CarPart
    {
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

        /// <summary>
        /// Gets the id of the current engine.
        /// </summary>
        /// <returns>Returns the integer id of the engine</returns>
        public override int GetId()
        {
            throw new InvalidOperationException("Can't get the id of a unspecified engine.");
        }
        #endregion
    }
}
