using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageTires
{
    public class Tires : CarPart
    {
        #region Properties
        public Inches Inches { get; private set; }
        #endregion

        #region Constructors
        public Tires(Inches inches)
        {
            this.Inches = inches;
            this.Name = Inches + " inch Tires";
            this.ProductionTime = 7;
            this.Price = 600;
            this.Description = "Designed for drivers who are looking for performance and style."
                + " It offers an optimal driving experience, comfort and reliability on any road, in any season.";
        }
        #endregion

        #region Methods

        /// <inheritdoc />
        public override void Add(CarModel cm)
        {
            cm.Tires = this;
        }

        /// <inheritdoc />
        public override void Remove(CarModel cm)
        {
            cm.Tires = null;
        }

        /// <inheritdoc />
        public override int GetId()
        {
            if (this.Inches == Inches.Fourteen)
                return 12;
            else if (this.Inches == Inches.Sixteen)
                return 13;
            else if (this.Inches == Inches.Eighteen)
                return 14;
            else
                throw new InvalidOperationException("Tires size is inavlid");
        }
        #endregion

    }
}
