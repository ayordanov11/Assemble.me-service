using Assemble.me.Library.Parts.PackageTires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageRims
{
    public class Rims : CarPart
    {
        #region Properties
        public Tires Tires { get; private set; }
        #endregion

        #region Constructors
        public Rims(Tires tires)
        {
            this.Tires = tires;
            this.Name = tires.Inches + " inch Rims";
            this.ProductionTime = tires.ProductionTime;
            this.Price = tires.Price;
            this.Description = "These rims perfectly fit " + tires.Inches + " inch tires and give it a finished look.";
        }
        #endregion

        #region Methods

        /// <inheritdoc />
        public override void Add(CarModel cm)
        {
            cm.Rims = this;
        }

        /// <inheritdoc />
        public override void Remove(CarModel cm)
        {
            cm.Rims = null;
        }

        /// <summary>
        /// Retrieves the unqiue identifier of the rims, depending on
        /// the <see cref="Inches"/> size.
        /// </summary>
        /// <returns>The unique identifier of the rims.</returns>
        public override int GetId()
        {
            if (this.Tires.Inches == Inches.Fourteen)
                return 15;
            else if (this.Tires.Inches == Inches.Sixteen)
                return 16;
            else if (this.Tires.Inches == Inches.Eighteen)
                return 17;
            else
                throw new InvalidOperationException("Rims size is inavlid");
        }

        #endregion
    }
}
