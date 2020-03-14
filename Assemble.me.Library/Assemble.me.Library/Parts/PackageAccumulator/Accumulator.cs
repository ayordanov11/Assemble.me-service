using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemble.me.Library.Parts.PackageAccumulator
{
    public class Accumulator: CarPart
    {
        #region Properties
        public AccumulatorSize Size { get; private set; }
        #endregion

        #region Constructors
        public Accumulator(AccumulatorSize size)
        {
            this.Size = size;
            this.Name = Size + " Accumulator";
            if (this.Size == AccumulatorSize.Small)
            {
                this.ProductionTime = 2;
                this.Price = 100;
                this.Description = "A small but powerful accumulator, having 35 Amper-hours and 6 Volts.";
            }
            else if (this.Size == AccumulatorSize.Average)
            {
                this.ProductionTime = 3;
                this.Price = 120;
                this.Description = "An avarage size of 65 Amper-hours, powerful accumulator having 12 Volts electricity source.";
            }
            else if (this.Size == AccumulatorSize.MegaPower)
            {
                this.ProductionTime = 4;
                this.Price = 140;
                this.Description = "A really big and powerful accumulator, having 75 Amper-hours and 24 Volts. ";
            }
        }
        #endregion

        #region Methods

        /// <inheritdoc />
        public override void Add(CarModel cm)
        {
            cm.Accumulator = this;
        }

        /// <inheritdoc />
        public override void Remove(CarModel cm)
        {
            cm.Accumulator = null;
        }
        /// <summary>
        /// Retreives the id of the accumulator depenting on its power.
        /// </summary>
        /// <returns></returns>
        public override int GetId()
        {
            if (this.Size == AccumulatorSize.Small)
                return 20;
            else if (this.Size == AccumulatorSize.Average)
                return 21;
            else if (this.Size == AccumulatorSize.MegaPower)
                return 22;
            else
                throw new InvalidOperationException("Accumulator size is inavlid");
        }
        #endregion

    }
}
